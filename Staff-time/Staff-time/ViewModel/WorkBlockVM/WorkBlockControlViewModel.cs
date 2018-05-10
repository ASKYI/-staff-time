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
            _generate_WorkInBlock(work);

            WorkInThisBlock.WorkControlDataContext.IsEdititig = true;
            _editCommand = new RelayCommand(Edit, CanEdit);
        }

        #region WorkInBlock
        private WorkInBlockBase _workInThisBlock;
        public WorkInBlockBase WorkInThisBlock
        {
            get { return _workInThisBlock; }
            set
            {
                SetField(ref _workInThisBlock, value);
            }
        }

        private void _generate_WorkInBlock(Work work)
        {
            //Factory
            WorkInThisBlock = new WorkInBlock(work);
        }
        #endregion

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
            if (!WorkInThisBlock.WorkControlDataContext.IsEdititig)
            {
                workWork.Update_Work(WorkInThisBlock.WorkControlDataContext.Work);
                WorkInThisBlock.WorkControlDataContext.IsEdititig = true;
            }
            else
                WorkInThisBlock.WorkControlDataContext.IsEdititig = false;
        }
        #endregion
    }
}
