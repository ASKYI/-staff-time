﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Data.Entity;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public class AttrValueExtended : ViewModelBase, INotifyPropertyChanged
    {
        public AttrValueExtended (Boolean _IsEditing, AttrValue value)
        {
            IsEditing = _IsEditing;
             AttrValue = value;
        }

        private Boolean _IsEditing;
        public Boolean IsEditing
        {
            get { return _IsEditing; }
            set
            {
                SetField(ref _IsEditing, value); // todo у какого объекта вызывается Notify?
            }
        }

        private AttrValue _attrValue;
        public AttrValue AttrValue
        {
            get { return _attrValue; }
            set
            {

                SetField(ref _attrValue, value);
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