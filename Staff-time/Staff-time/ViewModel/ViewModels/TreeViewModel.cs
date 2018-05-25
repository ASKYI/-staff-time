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
            SelectedTask = null;

            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);
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
        private TreeNode _selectedTask;
        public TreeNode SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                SetField<TreeNode>(ref _selectedTask, value);
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
            return SelectedTask != null;
        }
        private void AddWork(object obj)
        {
            Work newWork = new Work();
            newWork.WorkName = "Новая работа";
            newWork.TaskID = SelectedTask.Task.ID;
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
            if (SelectedTask != null)
                newTask.ParentTaskID = SelectedTask.Task.ParentTaskID;
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
            return SelectedTask != null;
        }
        private void AddChildTask(object obj)
        {
            Task newTask = new Task();
            newTask.ParentTaskID = SelectedTask.Task.ID;
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
            return SelectedTask != null;
        }
        private void DeleteTask(object obj)
        {
            taskWork.Delete_Task(SelectedTask.Task.ID);

            _generateTree_tracker = false;
            _generate_TreeNodesDictionary();
            _generate_Tree();
            MessengerInstance.Send<NotificationMessage>(new NotificationMessage("Update!"));
        }
        #endregion
    }
}
