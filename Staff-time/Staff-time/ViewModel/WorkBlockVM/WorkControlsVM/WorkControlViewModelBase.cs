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
        protected new Work _work;
        public Work Work
        {
            get { return _work; }
            set
            {
                SetField<Work>(ref _work, value);
            }
        }
        protected ObservableCollection<AttrValueExtended> _attrValues;
        public ObservableCollection<AttrValueExtended> AttrValues
        {
            get { return _attrValues; }
            set
            {
                SetField<ObservableCollection<AttrValueExtended>>(ref _attrValues, value);
            }
        }

        abstract public void DeleteWork();
        abstract public void UpdateWork();

        protected Boolean _isEditing; //false = edit
        public Boolean IsEdititig
        {
            get { return _isEditing; }
            set
            {
                SetField(ref _isEditing, value);

                foreach (var v in AttrValues)
                    v.IsEditing = value;
            }
        }
    }
}