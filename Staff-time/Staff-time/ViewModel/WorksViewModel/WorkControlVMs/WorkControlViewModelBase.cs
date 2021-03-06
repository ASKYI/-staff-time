﻿using System;
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

        protected Work _originWork;
        public Work OriginWork
        {
            get { return _originWork; }
            set
            {
                SetField(ref _originWork, value);
            }
        }


        private Boolean _isExpanded;
        public Boolean IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                //if (_isExpanded)
                //    CancelEditing();
                SetField(ref _isExpanded, value);
                OnPropertyChanged("IsExpanded");
            }
        }

        #region OnNotify (уведомить, кто хочет, что изменилось свойство)
        public event PropertyChangedEventHandler PropertyChangedWorkControlVMBase;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChangedWorkControlVMBase != null)
                PropertyChangedWorkControlVMBase(this, new PropertyChangedEventArgs(prop));
        }
        #endregion //OnNotify

        protected Boolean _IsEditing;
        public Boolean IsEdititig
        {
            get { return _IsEditing; }
            set
            {
                SetField(ref _IsEditing, value);

            }
        }
        

        public Boolean IsEnabled // todo модный нынче способ "expression bodied properties", выгдядит так: public Boolean IsEnabled => !_IsEditing;
        {
            get { return !_IsEditing; }
        }

        abstract public void UpdateWork();
        abstract public void DeleteWork();
        abstract public void CancelWork();
    }
}