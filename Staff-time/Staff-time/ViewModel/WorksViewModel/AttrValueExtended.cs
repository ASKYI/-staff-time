using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public class AttrValueExtended : ViewModelBase, INotifyPropertyChanged
    {
        public AttrValueExtended (Boolean isEditing, AttrValue value)
        {
            IsEditing = isEditing;
            AttrValue = value;
        }

        private Boolean _isEditing;
        public Boolean IsEditing
        {
            get { return _isEditing; }
            set
            {
                SetField(ref _isEditing, value, "IsEditing"); //done: забыла IsEditing
            }
        }

        private AttrValue _attrValue;
        public AttrValue AttrValue
        {
            get { return _attrValue; }
            set
            {

                SetField(ref _attrValue, value, "AttrValue");
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