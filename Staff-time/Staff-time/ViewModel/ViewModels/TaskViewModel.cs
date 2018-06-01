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
    public class TaskViewModel : DependencyObject
    {
        public ICommand AcceptAddUserCommand { get; set; }
        public event EventHandler AddUserAccepted;
        public ICommand CancelAddUserCommand { get; set; }
        //public event EventHandler AddUserCanceled;

        private Task _task;

        public Task Model
        {
            get
            {
                return this._task;
            }
        }

        public string Name
        {
            get { return (string)GetValue(NamePropertyProperty); }
            set { SetValue(NamePropertyProperty, value); }
        }

        private bool CanAcceptAddUser()
        {
            return true;
        }

        private void AcceptAddUser()
        {
            //Raise an event to tell UsersViewModel.
            AddUserAccepted(this, EventArgs.Empty);
        }

        /*private bool CanCancelAddUser()
        {
            return true;
        }

        private void CancelAddUser()
        {
            AddUserCanceled(this, EventArgs.Empty);
        }*/

        public static readonly DependencyProperty NamePropertyProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(TaskViewModel));

        public TaskViewModel() : this(new Task())
        {
        }

        public TaskViewModel(Task task)
        {
            AcceptAddUserCommand = new AcceptAddUserInternalCommand(this);
            //CancelAddUserCommand = new CancelAddUserInternalCommand(this);
            this._task = task;


            this._task.TaskName = "new test";

            this.Name = this._task.TaskName;

        }

        #region Commands
        class AcceptAddUserInternalCommand : ICommand
        {
            TaskViewModel _viewModel;

            public AcceptAddUserInternalCommand(TaskViewModel viewModel)
            {
                this._viewModel = viewModel;
            }

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return this._viewModel.CanAcceptAddUser();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                this._viewModel.AcceptAddUser();
            }

            #endregion
        }

        /*class CancelAddUserInternalCommand : ICommand
        {
            UserViewModel _viewModel;

            public CancelAddUserInternalCommand(UserViewModel viewModel)
            {
                this._viewModel = viewModel;
            }

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return this._viewModel.CanCancelAddUser();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                this._viewModel.CancelAddUser();
            }

            #endregion
        }*/
        #endregion
    }
}

