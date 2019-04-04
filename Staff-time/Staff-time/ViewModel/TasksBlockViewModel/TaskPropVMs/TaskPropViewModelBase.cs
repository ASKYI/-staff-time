using Staff_time.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Staff_time.ViewModel.TasksBlockViewModel.TaskPropVMs
{
    abstract public class TaskPropViewModelBase : MainViewModel, INotifyPropertyChanged
    {
        protected Task _task;
        public Task CurTask
        {
            get { return _task; }
            set
            {
                SetField(ref _task, value);
            }
        }

        //protected Work _originWork;
        //public Work OriginWork
        //{
        //    get { return _originWork; }
        //    set
        //    {
        //        SetField(ref _originWork, value);
        //    }
        //}


        #region OnNotify (уведомить, кто хочет, что изменилось свойство)
        public event PropertyChangedEventHandler PropertyChangedWorkControlVMBase;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChangedWorkControlVMBase != null)
                PropertyChangedWorkControlVMBase(this, new PropertyChangedEventArgs(prop));
        }
        #endregion //OnNotify

        //abstract public void UpdateWork();
        //abstract public void DeleteWork();
        //abstract public void CancelWork();
    }
}
