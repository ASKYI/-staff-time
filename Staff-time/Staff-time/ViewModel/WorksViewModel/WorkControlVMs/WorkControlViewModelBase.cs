using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    abstract public class WorkControlViewModelBase : MainViewModel, INotifyPropertyChanged
    {
        protected Work _work;
        public Work Work
        {
            get { return _work; }
            set
            {
                SetField(ref _work, value);
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

        private Boolean _isExpanded;
        public Boolean IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded)
                    CancelEditing();
                SetField(ref _isExpanded, value);
                OnPropertyChanged("IsExpanded");
            }
        }

        #region OnNotify (уведомить, кто хочет, что изменилось свойство)
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion //OnNotify

        protected Boolean _isEditing;
        public Boolean IsEdititig
        {
            get { return _isEditing; }
            set
            {
                SetField(ref _isEditing, value);

            }
        }
        

        public Boolean IsEnabled // todo модный нынче способ "expression bodied properties", выгдядит так: public Boolean IsEnabled => !_isEditing;
        {
            get { return !_isEditing; }
        }

        abstract public void UpdateWork();
        abstract public void DeleteWork();
        //abstract public void CancelWork();
    }
}