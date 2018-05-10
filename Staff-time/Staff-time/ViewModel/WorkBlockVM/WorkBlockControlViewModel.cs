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

            WorkInBlock.WorkControlDataContext.IsEdititig = true;
            _editCommand = new RelayCommand(Edit, CanEdit);
        }

        #region WorkInBlock
        private ObservableCollection<WorkInBlock> _workInThisBlock;
        public ObservableCollection<WorkInBlock> WorkInThisBlock
        {
            get { return _workInThisBlock; }
            set
            {
                SetField < ObservableCollection<WorkInBlock>>(ref _workInThisBlock, value);
            }
        }

        private WorkInBlockBase _workInBlock;
        public WorkInBlockBase WorkInBlock
        {
            get { return _workInBlock; }
            set
            {
                SetField(ref _workInBlock, value);
            }
        }

        private void _generate_WorkInBlock(Work work)
        {
            //Factory
            WorkInBlock = new WorkInBlock(work);
            WorkInThisBlock = new ObservableCollection<WorkInBlock>();
            WorkInThisBlock.Add((WorkInBlock)WorkInBlock);
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
            if (!WorkInBlock.WorkControlDataContext.IsEdititig)
            {
                workWork.Update_Work(WorkInBlock.WorkControlDataContext.Work);
                WorkInBlock.WorkControlDataContext.IsEdititig = true;
            }
            else
                WorkInBlock.WorkControlDataContext.IsEdititig = false;
        }
        #endregion
    }
}
