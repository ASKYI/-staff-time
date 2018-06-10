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
    public class TaskViewModel : ViewModelBase
    {
        public TaskViewModel(Task task, ObservableCollection<TreeNode> roots, TaskCommandEnum command)
        {
            _task = task;
            TreeRoots = roots;
            _command = command;

            EditingTask = task;

            AcceptCommand = new RelayCommand(Accept, CanAccept);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
        }

        private Task _task;

        private Task _editingTask;
        public Task EditingTask
        {
            get { return _editingTask; }
            set
            {
                SetField(ref _editingTask, value);
            }
        }

        private TaskCommandEnum _command;

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

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);
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
        private void Accept(object obj)
        {
            _task = _editingTask;
            MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
                new KeyValuePair<TaskCommandEnum, Task>(_command, _task));
        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        private void Cancel(object obj)
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

