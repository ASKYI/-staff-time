using System;
using Staff_time.Model;
using System.Collections.ObjectModel;

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

        public Boolean IsEnabled => !_isEditing;

        abstract public void UpdateWork();
        abstract public void DeleteWork();
    }
}