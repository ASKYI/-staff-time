using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public class TasksBlockViewModel : MainViewModel
    {
        public TasksBlockViewModel()
        {
            _generate_Tree();
            
            IsEditing = false;

            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);

            IsShowed = false; //Dialog

            MessengerInstance.Register<List<int>>(this, _addRoots);
        }

        #region Selected TaskNode

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);

                if (_selectedTaskNode != null)
                {
                    EditTask = (Task)_selectedTaskNode.Task.Clone();
                }

                IsEditing = true;
                IsEnabled = false;
            }
        }

        #endregion

        #region Tree !!!

        private ObservableCollection<TreeNode> _treeRoots;
        public ObservableCollection<TreeNode> TreeRoots
        {
            get { return _treeRoots; }
            set
            {
                SetField(ref _treeRoots, value);
            }
        }

        private void _generate_Tree()
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            foreach (var taskNode in TasksVM.Dictionary)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    TreeRoots.Add(taskNode.Value);
                }
            }
        }

        private void _addRoots(List<int> ids)
        {
            foreach(int id in ids)
            {
                TreeRoots.Add(TasksVM.Dictionary[id]);
            }
        }

        private void _addNewNode(Task task)
        {
            TreeNodeFactory factory = new TreeNodeFactory();

            TreeNode node = factory.CreateTreeNode(task);
            TasksVM.Dictionary.Add(task.ID, node);
            if (task.ParentTaskID == null || task.ParentTaskID == 0)
            {
                TreeRoots.Add(node);
            }
            else
            {
                TasksVM.Dictionary[(int)task.ParentTaskID].AddChild(node);
                node.ParentNode = TasksVM.Dictionary[(int)task.ParentTaskID];
            }

            List<int> childTasks = Context.taskWork.Read_ChildTasks(task.ID);
            foreach(var t in childTasks)
            {
                TreeNode childNode = TasksVM.Dictionary[t];

                if (childNode.ParentNode != null)
                    childNode.ParentNode.TreeNodes.Remove(TasksVM.Dictionary[t]);
                else
                    TreeRoots.Remove(TasksVM.Dictionary[t]);

                node.AddChild(childNode);
                childNode.ParentNode = node;
            }
        }

        private void _deleteNode(TreeNode node, bool wirhChildren)
        {
            int parentID = 0, delTaskID = node.Task.ID;
            if (node.Task.ParentTaskID != null)
                parentID = (int)node.Task.ParentTaskID;

            //Удаляем узел, родитель его детей - родитель удаляемого узла
            if (wirhChildren)
            {
                foreach (var t in TasksVM.Dictionary[delTaskID].TreeNodes)
                {
                    if (parentID != 0)
                    {
                        t.ParentNode = TasksVM.Dictionary[parentID];
                        TasksVM.Dictionary[parentID].AddChild(t);
                    }
                    else
                    {
                        t.ParentNode = null;
                        t.Task.ParentTaskID = null;
                        TreeRoots.Add(t);
                    }
                }
            }

            if (parentID == 0)
                TreeRoots.Remove(node);
            else
                if (TasksVM.Dictionary.ContainsKey(parentID))
                TasksVM.Dictionary[parentID].TreeNodes.Remove(node);
            TasksVM.Dictionary.Remove(delTaskID);
        }

        #endregion

        #region Add Work !!!
        private readonly ICommand _addWorkCommand;
        public ICommand AddWorkCommand
        {
            get
            {
                return _addWorkCommand;
            }
        }

        private bool CanAddWork(object obj)
        {
            return SelectedTaskNode != null;
        }
        private void AddWork(object obj)
        {
            Work work = new Work();
            work.WorkName = "Новая работа";
            work.TaskID = SelectedTaskNode.Task.ID;
            work.StartDate = chosenDate.Date;
            work.EndDate = work.StartDate;

            //WorksVM.Add(work);
            //Work newWork = WorksVM.Dictionary[work.ID];

            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(
                new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Add, work));
        }
        #endregion
        #region Add Task !!!
        private readonly ICommand _addNearTaskCommand;
        public ICommand AddNearTaskCommand
        {
            get
            {
                return _addNearTaskCommand;
            }
        }

        private bool CanAddNearTask(object obj)
        {
            return true;
        }
        private void AddNearTask(object obj)
        {
            DialogTitle = "Новая задача";
            DialogDependency = new DialogDependency();
            IsShowed = true;
            
            Task newTask = new Task();
            if (SelectedTaskNode != null)
                newTask.ParentTaskID = SelectedTaskNode.Task.ParentTaskID;
            newTask.TaskName = "Новая задача";
            Context.taskWork.Create_Task(newTask);

            _addNewNode(newTask);
        }
        private readonly ICommand _addChildTaskCommand;
        public ICommand AddChildTaskCommand
        {
            get
            {
                return _addChildTaskCommand;
            }
        }

        private bool CanAddChildTask(object obj)
        {
            return SelectedTaskNode != null;
        }
        private void AddChildTask(object obj)
        {
            DialogTitle = "Новая задача";
            IsShowed = true;

            Task newTask = new Task();
            newTask.ParentTaskID = SelectedTaskNode.Task.ID;
            newTask.TaskName = "Новая задача";
            Context.taskWork.Create_Task(newTask);
            newTask.TaskName = "Новая работа";

            _addNewNode(newTask);
        }
        #endregion
        #region Delete Task !!!
        private readonly ICommand _deleteTaskCommand;
        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand;
            }
        }

        private bool CanDelteTask(object obj)
        {
            return SelectedTaskNode != null;
        }
        private void DeleteTask(object obj)
        {
            List<int> works = Context.workWork.Read_WorksForTask(SelectedTaskNode.Task.ID);
            foreach (var id in works)
            {
                Work w = WorksVM.Dictionary[id];
                MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
                    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            }

            int delTaskID = SelectedTaskNode.Task.ID;
            if (SelectedTaskNode.ParentNode == null)
            {
                foreach(var n in SelectedTaskNode.TreeNodes)
                {
                    TreeRoots.Add(n);
                }
            }
            if (TreeRoots.Contains(SelectedTaskNode))
                TreeRoots.Remove(SelectedTaskNode);

            TasksVM.DeleteAlone(delTaskID);
        }
        #endregion

        #region Dialog !!!
        //https://www.codeproject.com/Articles/35553/XAML-Dialog-Control-Enabling-MVVM-and-Dialogs-in-W

        private Boolean _isShowed;
        public Boolean IsShowed
        {
            get { return _isShowed; }
            set
            {
                SetField(ref _isShowed, value);
            }
        }

        private string _dialogTitle;
        public string DialogTitle
        {
            get { return _dialogTitle; }
            set
            {
                SetField(ref _dialogTitle, value);
            }
        }

        private DialogDependency _dialogDependency;
        public DialogDependency DialogDependency
        {
            get { return _dialogDependency; }
            set
            {
                SetField(ref _dialogDependency, value);
            }
        }
        #endregion

        #region Booleans

        private Boolean _isEdinitng;
        public Boolean IsEditing
        {
            get { return IsEditing; }
            set
            {
                SetField(ref _isEdinitng, value);

                if (_isEdinitng == false)
                {
                    EditTask = null;

                    IsEnabled = false;
                    IsReadOnly = true;
                }
                else
                {
                    EditTask = SelectedTaskNode.Task;

                    IsEnabled = true;
                    IsReadOnly = false;
                }
            }
        }

        private Boolean _isEnabled;
        public Boolean IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetField(ref _isEnabled, value);
            }
        }
        private Boolean _isReadOnly;
        public Boolean IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                SetField(ref _isReadOnly, value);
            }
        }

        #endregion

        //#region Edit Task
        private Task _editTask;
        public Task EditTask
        {
            get { return _editTask; }
            set
            {
                SetField(ref _editTask, value);
            }
        }

        //private readonly ICommand _editCommand;
        //public ICommand EditCommand
        //{
        //    get
        //    {
        //        return _editCommand;
        //    }
        //}
        //private bool CanEdit(object obj)
        //{
        //    return SelectedTaskNode != null;
        //}
        //private void Edit(object obj)
        //{
        //    if (!IsEditing)
        //    {
        //        EditTask.TaskTypeID = SelectedTaskTypeIndex;
        //        if (EditTask.ParentTaskID == 0)
        //            EditTask.ParentTaskID = null;
        //        else if (EditTask.ParentTaskID != null && !TasksVM.Dictionary.ContainsKey((int)EditTask.ParentTaskID)
        //            || SelectedTaskNode.Task.ID == EditTask.ParentTaskID)
        //            return;


        //        IsEditing = true;
        //        IsEnabled = false;

        //        Task task = (Task)EditTask.Clone();
        //        _deleteNode(SelectedTaskNode, false);
        //        _addNewNode(task);

        //        List<int> works = Context.workWork.Read_WorksForTask(task.ID);
        //        foreach (var id in works)
        //        {
        //            Work w = WorksVM.Dictionary[id];
        //            MessengerInstance.Send<KeyValuePair<int, Work>>(new KeyValuePair<int, Work>(1, w));
        //        }
        //        Context.taskWork.Update_Task(task);
        //    }
        //    else
        //    {
        //        IsEditing = false;
        //        IsEnabled = true;
        //    }
        //}
        //#endregion

        //#region Task Type
        //private int _selectedTaskTypeIndex;
        //public int SelectedTaskTypeIndex
        //{
        //    get { return _selectedTaskTypeIndex; }
        //    set
        //    {
        //        SetField<int>(ref _selectedTaskTypeIndex, value);
        //    }
        //}
        //private ObservableCollection<TaskType> _taskTypesCb;
        //public ObservableCollection<TaskType> TaskTypesCb
        //{
        //    get { return _taskTypesCb; }
        //    set
        //    {
        //        SetField<ObservableCollection<TaskType>>(ref _taskTypesCb, value);
        //    }
        //}
        //private void _generate_TaskTypesCb()
        //{
        //    TaskTypesCb = new ObservableCollection<TaskType>();
        //    List<TaskType> types = Context.typesWork.Read_TaskTypes();
        //    foreach (var t in types)
        //    {
        //        TaskTypesCb.Add(t);
        //    }
        //}
        //#endregion
    }
}
