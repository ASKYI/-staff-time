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
    abstract public class WorkControlViewModelBase : MainViewModel
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

        protected Boolean _isEditing;
        public Boolean IsEdititig
        {
            get { return _isEditing; }
            set
            {
                SetField(ref _isEditing, value);

            }
        }

        public Boolean IsEnabled
        {
            get { return !_isEditing; }
        }

        abstract public void UpdateWork();
        abstract public void DeleteWork();
    }
}