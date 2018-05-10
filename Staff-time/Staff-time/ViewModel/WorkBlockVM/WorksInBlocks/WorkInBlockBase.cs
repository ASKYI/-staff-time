﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    //Обертка над блоком работы для передачи контролу блока
    abstract public class WorkInBlockBase : ViewModelBase, INotifyPropertyChanged
    {
        private WorkControlViewModel _workControlDataContext;
        public WorkControlViewModel WorkControlDataContext
        {
            get { return _workControlDataContext; }
            set
            {
                SetField<WorkControlViewModel>(ref _workControlDataContext, value);
            }
        }

        #region INotifyPropertyChanged Member
        protected bool SetField<T>(ref T field, T value,
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
