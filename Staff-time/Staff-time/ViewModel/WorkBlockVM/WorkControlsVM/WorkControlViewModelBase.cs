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
        private Work _work;
        public Work Work
        {
            get { return _work; }
            set
            {
                SetField(ref _work, value);
            }
        }

        abstract public void DeleteWork();
        abstract public void UpdateWork();

        private Boolean _isEditing; //false = edit
        public Boolean IsEdititig
        {
            get { return _isEditing; }
            set
            {
                SetField(ref _isEditing, value);
            }
        }
    }
}