using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Staff_time.ViewModel
{
    abstract public class WorkControlViewModelBase : MainViewModel
    {
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