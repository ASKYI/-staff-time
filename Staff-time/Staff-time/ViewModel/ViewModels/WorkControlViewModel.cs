using System;
using System.Collections.Generic;
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
    public class WorkControlViewModel : MainViewModel
    {
        public WorkControlViewModel(Work work)
        {
            Work = work;

            IsEdititig = true;
            _editCommand = new RelayCommand(Edit, CanEdit);
        }

        private Work _work;
        public Work Work
        {
            get { return _work; }
            set
            {
                SetField(ref _work, value);
            }
        }

        #region Edit Command
        private Boolean _isEditing; //false = edit
        public Boolean IsEdititig
        {
            get { return _isEditing; }
            set
            {
                SetField(ref _isEditing, value);
            }
        }

        private readonly ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand;
            }
        }
        private bool CanEdit(object obj)
        {
            return true;
        }
        private void Edit(object obj)
        {
            if (!IsEdititig)
            {
                //WorksTable.Update_Work(Work.ID, Work);
                workWork.Update_Work(Work);
                IsEdititig = true;
            }
            else
                IsEdititig = false;
        }
        #endregion
    }
}
