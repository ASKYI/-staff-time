using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using Staff_time.Model;
using Staff_time.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public class TaskDialogViewModel : ViewModelBase
    {
        public TaskDialogViewModel(Task task, ObservableCollection<TreeNode> roots, TaskCommandEnum command)
        {
            _generate_TaskTypesCb();

            _task = task;

            if (command == TaskCommandEnum.Edit)
            {
                _generate_Tree(roots);
                Message = "Выбрать задачу-родителя";
            }
            _command = command;
            Command = (int)_command;

            EditingTask = task;
            SelectedTaskTypeIndex = task.TaskTypeID;
            
            AcceptCommand = new RelayCommand(Accept, CanAccept);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
        }

        private Task _task; //TaskNode
        private TaskCommandEnum _command;
        public int Command;

        private Task _editingTask;
        public Task EditingTask
        {
            get { return _editingTask; }
            set
            {
                SetField(ref _editingTask, value);
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetField(ref _message, value);
            }
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

        private void _generate_Tree(ObservableCollection<TreeNode> roots)
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            TreeRoots.Add(new TreeNode() { Task = new Task() { TaskName = "Задачи" } });

            foreach (var r in roots)
                TreeRoots[0].AddChild(r);
        }

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);
                if (value.Task.ID == 0)
                    _editingTask.ParentTaskID = null;
                else
                    _editingTask.ParentTaskID = value.Task.ID;
                //if (_selectedTaskNode != null)
                //{
                //    //if (TreeRoots != null && _selectedTaskNode == TreeRoots[0])
                //    //    EditingTask.ParentTaskID = null;
                //    //else
                //    //    EditingTask.ParentTaskID = _selectedTaskNode.Task.ID;
                //}
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

                EditingTask.TaskTypeID = _selectedTaskTypeIndex;
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
            List<TaskType> types = Context.typesWork.Read_TaskTypes();
            foreach (var t in types)
            {
                TaskTypesCb.Add(t);
            }
        }

        #endregion

        #region Commands

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private bool CanAccept(object obj)
        {
            return true;
        }
        public void Accept(object obj)
        {
            if (_command == TaskCommandEnum.Edit)
                if (_editingTask.ID == _editingTask.ParentTaskID || TasksVM.CheckIsChild(_editingTask.ID, _editingTask.ParentTaskID))
                {
                    MessageBox.Show("Нельзя назначить новым родителем потомка или самого себя");
                    MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
                        new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
                    return;
                }
                else
                    _editingTask.ParentTaskID = _task.ParentTaskID;

            _task = _editingTask;
            MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
                new KeyValuePair<TaskCommandEnum, Task>(_command, _task));
        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        public void Cancel(object obj)
        {
            MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
                new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
                new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
        }

        #endregion

        #region INotifyPropertyChanged Members

        public bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion

    }
}

