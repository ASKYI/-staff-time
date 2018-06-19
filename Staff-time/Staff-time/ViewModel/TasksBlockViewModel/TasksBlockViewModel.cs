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
    public enum TaskCommandEnum { Add, Edit, None }

    public class TasksBlockViewModel : MainViewModel
    {
        public TasksBlockViewModel()
        {
            _generate_Tree();

            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);
            _editTaskCommand = new RelayCommand(EditTask, CanEditTask);
                

            IsShowed = false; //Dialog

            MessengerInstance.Register<List<int>>(this, _addRoots);
            MessengerInstance.Register<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);
        }
        
        #region Tree

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

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);
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
      
        #endregion

        #region Add Work
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

            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(
                new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Add, work));
        }
        #endregion

        #region Add Task
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
            return IsShowed == false;
        }
        private void AddNearTask(object obj)
        {
            Task newTask = new Task();
            if (SelectedTaskNode != null)
                newTask.ParentTaskID = SelectedTaskNode.Task.ParentTaskID;
            newTask.TaskName = "Новая задача";

            DialogTitle = "Новая задача";
            DialogDependency = new DialogDependency(newTask, TreeRoots, TaskCommandEnum.Add);
            IsShowed = true;
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
            return SelectedTaskNode != null && IsShowed == false;
        }
        private void AddChildTask(object obj)
        {
            Task newTask = new Task();
            newTask.ParentTaskID = SelectedTaskNode.Task.ID;
            newTask.TaskName = "Новая подзадача";

            DialogTitle = "Новая подзадача";
            DialogDependency = new DialogDependency(newTask, TreeRoots, TaskCommandEnum.Add);
            IsShowed = true;
        }
        #endregion

        private readonly ICommand _editTaskCommand;
        public ICommand EditTaskCommand
        {
            get
            {
                return _editTaskCommand;
            }
        }

        private bool CanEditTask(object obj)
        {
            return SelectedTaskNode != null && IsShowed == false;
        }
        private void EditTask(object obj)
        {
            DialogTitle = "Редактировать задачу";
            DialogDependency = new DialogDependency(SelectedTaskNode.Task, TreeRoots, TaskCommandEnum.Edit);
            IsShowed = true;
        }

        #region Delete Task
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
            //Works
            List<int> works = Context.workWork.Read_WorksForTask(SelectedTaskNode.Task.ID);
            foreach (var id in works)
            {
                Work w = WorksVM.Dictionary[id].Work;
                MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
                    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            }

            //Roots
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

        #region Dialog
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

        #region Do Task: Add, Edit

        private void _doTaskCommand(KeyValuePair<TaskCommandEnum, Task> pair)
        {
            TaskCommandEnum command = pair.Key;
            Task task = pair.Value;

            switch(command)
            {
                case TaskCommandEnum.Add:
                    TasksVM.Add(task);
                    TreeNode newNode = TasksVM.Dictionary[task.ID];

                    if (newNode.ParentNode == null)
                        TreeRoots.Add(newNode);

                    break;
                case TaskCommandEnum.Edit:

                    if (TasksVM.Dictionary[task.ID].ParentNode == null)
                        TreeRoots.Remove(TasksVM.Dictionary[task.ID]);

                    TasksVM.Edit(task);
                    newNode = TasksVM.Dictionary[task.ID];

                    if (newNode.ParentNode == null)
                        TreeRoots.Add(newNode);

                    break;
            }

            _dialogDependency.DialogViewModel = null;
            IsShowed = false;
        }

        #endregion
        
    }
}
