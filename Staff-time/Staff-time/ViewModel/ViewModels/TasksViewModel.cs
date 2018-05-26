using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public class TasksViewModel : MainViewModel
    {
        public TasksViewModel()
        {
            _generate_Tree();

            _generate_TaskTypesCb();

            SelectedTaskNode = null;
            EditTask = null;
            IsEnabled = false; IsEditing = true;
            
            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);
            _editCommand = new RelayCommand(Edit, CanEdit);
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
            foreach (var taskNode in TaskNodesDictionary)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    TreeRoots.Add(taskNode.Value);
                }
            }
        }

        private void _addNewNode(Task task)
        {
            TreeNodeFactory factory = new TreeNodeFactory();

            TreeNode node = factory.CreateTreeNode(task);
            TaskNodesDictionary.Add(task.ID, node);
            if (task.ParentTaskID == null || task.ParentTaskID == 0)
            {
                TreeRoots.Add(node);
            }
            else
                TaskNodesDictionary[(int)task.ParentTaskID].AddChild(node);

            List<int> childTasks = taskWork.Read_ChildTasks(task.ID);
            foreach(var t in childTasks)
            {
                node.AddChild(TaskNodesDictionary[t]);

                if (TaskNodesDictionary[t].ParentNode != null)
                    TaskNodesDictionary[t].ParentNode.TreeNodes.Remove(TaskNodesDictionary[t]);
                else
                    TreeRoots.Remove(TaskNodesDictionary[t]);
            }
        }
        private void _deleteNode(TreeNode node)
        {
            int parentID = 0, delTaskID = node.Task.ID;
            if (node.Task.ParentTaskID != null)
                parentID = (int)node.Task.ParentTaskID;

            //Удаляем узел, родитель его детей - родитель удаляемого узла
            foreach (var t in TaskNodesDictionary[delTaskID].TreeNodes)
            {
                if (parentID != 0)
                {
                    t.ParentNode = TaskNodesDictionary[parentID];
                    TaskNodesDictionary[parentID].AddChild(t);
                }
                else
                {
                    t.ParentNode = null;
                    t.Task.ParentTaskID = null;
                    TreeRoots.Add(t);
                }
            }
            if (parentID == 0)
                TreeRoots.Remove(node);
            else
                if (TaskNodesDictionary.ContainsKey(parentID))
                    TaskNodesDictionary[parentID].TreeNodes.Remove(node);
            TaskNodesDictionary.Remove(delTaskID);
        }
        #endregion
        #region Selected Task
        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);

                if (_selectedTaskNode != null)
                {
                    SelectedTaskTypeIndex = _selectedTaskNode.Task.TaskTypeID;
                    EditTask = (Task)_selectedTaskNode.Task.Clone();
                }

                IsEditing = true;
                IsEnabled = false;
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
            Work newWork = new Work();

            newWork.WorkName = "Новая работа";
            newWork.TaskID = SelectedTaskNode.Task.ID;
            newWork.StartDate = curDate.Date;
            workWork.Create_Work(newWork);

            MessengerInstance.Send<KeyValuePair<int, Work>>(new KeyValuePair<int, Work>(2, newWork));
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
            return true;
        }
        private void AddNearTask(object obj)
        {
            Task newTask = new Task();
            if (SelectedTaskNode != null)
                newTask.ParentTaskID = SelectedTaskNode.Task.ParentTaskID;
            newTask.TaskName = "Новая задача";
            taskWork.Create_Task(newTask);

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
            Task newTask = new Task();
            newTask.ParentTaskID = SelectedTaskNode.Task.ID;
            newTask.TaskName = "Новая задача";
            taskWork.Create_Task(newTask);
            newTask.TaskName = "Новая работа";

            _addNewNode(newTask);
        }
        #endregion
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
            List<Work> works = workWork.Read_WorksForTask(SelectedTaskNode.Task.ID);
            foreach(var w in works)
                MessengerInstance.Send<KeyValuePair<int, Work>>(new KeyValuePair<int, Work>(0, w));

            int delID = SelectedTaskNode.Task.ID;
            _deleteNode(SelectedTaskNode);
            taskWork.Delete_Task(delID);
        }
        #endregion
        #region Edit Task
        private Task _editTask;
        public Task EditTask
        {
            get { return _editTask; }
            set
            {
                SetField(ref _editTask, value);
            }
        }

        private Boolean _isEnabled;
        public Boolean IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetField<Boolean>(ref _isEnabled, value);
            }
        }
        private Boolean _isEditing;
        public Boolean IsEditing
        {
            get { return _isEditing; }
            set
            {
                SetField<Boolean>(ref _isEditing, value);
            }
        }

        private readonly ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand;
            }
        }
        private bool CanEdit(object obj)
        {
            return SelectedTaskNode != null;
        }
        private void Edit(object obj)
        {
            if (!IsEditing)
            {
                EditTask.TaskTypeID = SelectedTaskTypeIndex;
                if (EditTask.ParentTaskID == 0)
                    EditTask.ParentTaskID = null;
                else if (EditTask.ParentTaskID != null && !TaskNodesDictionary.ContainsKey((int)EditTask.ParentTaskID)
                    || SelectedTaskNode.Task.ID == EditTask.ParentTaskID)
                    return;


                IsEditing = true;
                IsEnabled = false;

                Task task = (Task)EditTask.Clone();
                _deleteNode(SelectedTaskNode);
                _addNewNode(task);
            }
            else
            {
                IsEditing = false;
                IsEnabled = true;
            }
        }
        #endregion
        #region Task Type
        private int _selectedTaskTypeIndex;
        public int SelectedTaskTypeIndex
        {
            get { return _selectedTaskTypeIndex; }
            set
            {
                SetField<int>(ref _selectedTaskTypeIndex, value);
            }
        }
        private ObservableCollection<TaskType> _taskTypesCb;
        public ObservableCollection<TaskType> TaskTypesCb
        {
            get { return _taskTypesCb; }
            set
            {
                SetField<ObservableCollection<TaskType>>(ref _taskTypesCb, value);
            }
        }
        private void _generate_TaskTypesCb()
        {
            TaskTypesCb = new ObservableCollection<TaskType>();
            List<TaskType> types = typesWork.Read_TaskTypes();
            foreach (var t in types)
            {
                TaskTypesCb.Add(t);
            }
        }
        #endregion
    }
}
