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
    public class TreeViewModel : MainViewModel
    {
        public TreeViewModel() : base()
        {
            _generate_Tree();
            _generate_TaskTypesCb();
            SelectedTaskNode = null;
            IsEnabled = false; IsEditing = true; IsChangingType = true;

            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);
            _editCommand = new RelayCommand(Edit, CanEdit);
            _changeTypeCommand = new RelayCommand(ChangeType, CanChangeType);
        }

        #region Tree Data
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
        #endregion
        #region Select Task
        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);
            }
        }

        private readonly ICommand _selectTaskCommand;
        public ICommand SelectTaskCommand
        {
            get
            {
                return _selectTaskCommand;
            }
        }

        private bool CanSelectTask(object obj)
        {
            return true;
        }
        private void SelectTask(object obj)
        {
            MessengerInstance.Register<NotificationMessage<TreeNode>>(this, (selectedTask) =>
            {

            });
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
            newWork.StartDate = CurDate.Date;
            workWork.Create_Work(newWork);

            MessengerInstance.Send<NotificationMessage>(new NotificationMessage("Update!"));
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

            _generateTree_tracker = false;
            _generate_TreeNodesDictionary();
            _generate_Tree();
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

            _generateTree_tracker = false;
            _generate_TreeNodesDictionary();
            _generate_Tree();
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
            taskWork.Delete_Task(SelectedTaskNode.Task.ID);

            _generateTree_tracker = false;
            _generate_TreeNodesDictionary();
            _generate_Tree();
            MessengerInstance.Send<NotificationMessage>(new NotificationMessage("Update!"));
        }
        #endregion
        #region Edit Task
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
                if (SelectedTaskNode.Task.ParentTaskID == 0)
                    SelectedTaskNode.Task.ParentTaskID = null;
                if (SelectedTaskNode.Task.ParentTaskID != null)
                {
                    if (TaskNodesDictionary.ContainsKey((int)SelectedTaskNode.Task.ParentTaskID))
                        taskWork.Update_Task(SelectedTaskNode.Task);
                }
                else
                    taskWork.Update_Task(SelectedTaskNode.Task);
                IsEditing = true;
                IsEnabled = false;

                _generateTree_tracker = false;
                _generate_TreeNodesDictionary();
                _generate_Tree();
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
                ChangeWorkType();
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
            foreach (var t in TaskTypes)
            {
                TaskTypesCb.Add(t);
            }
        }

        private Boolean _isChangingType;
        public Boolean IsChangingType
        {
            get { return _isChangingType; }
            set
            {
                SetField(ref _isChangingType, value);
            }
        }
        private Boolean _isEnabledCbx;
        public Boolean IsEnabledCbx
        {
            get { return _isEnabledCbx; }
            set
            {
                SetField<Boolean>(ref _isEnabledCbx, value);
            }
        }
        private readonly ICommand _changeTypeCommand;
        public ICommand ChangeTypeCommand
        {
            get
            {
                return _changeTypeCommand;
            }
        }
        private bool CanChangeType(object obj)
        {
            return SelectedTaskNode != null;
        }
        private void ChangeType(object obj)
        {
            if (!IsChangingType)
            {
                IsEnabledCbx = false;
                IsChangingType = true;
            }
            else
            {
                IsEnabledCbx = true;
                IsChangingType = false;
            }
        }
        void ChangeWorkType()
        {
            if (!IsChangingType)
            {
                SelectedTaskNode.Task.TaskTypeID = SelectedTaskTypeIndex;
                taskWork.Update_Task(SelectedTaskNode.Task);

                _generateTree_tracker = false;
                _generate_TreeNodesDictionary();
                _generate_Tree();
            }
        }
        #endregion
    }
}
