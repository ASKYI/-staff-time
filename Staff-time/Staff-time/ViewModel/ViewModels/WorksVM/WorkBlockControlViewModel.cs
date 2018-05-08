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
    public class WorkBlockControlViewModel : MainViewModel
    {
        public WorkBlockControlViewModel(Work work)
        {
            WorkDataContext = new WorkControlViewModel(work);

            WorkDataContext.IsEdititig = true;
            _editCommand = new RelayCommand(Edit, CanEdit);
        }

        private WorkControlViewModel _workDataContext;
        public WorkControlViewModel WorkDataContext
        {
            get { return _workDataContext; }
            set
            {
                SetField(ref _workDataContext, value);
            }
        }

        #region Edit Command

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
            if (!WorkDataContext.IsEdititig)
            {
                //WorksTable.Update_Work(Work.ID, Work);
                workWork.Update_Work(WorkDataContext.Work);
                WorkDataContext.IsEdititig = true;
            }
            else
                WorkDataContext.IsEdititig = false;
        }
        #endregion
    }
}
