﻿using System;
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
            _deleteCommand = new RelayCommand(Delete, CanDelete);
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

        private WorkInBlock _workInBlock;
        public WorkInBlock WorkInBlock
        {
            get { return _workInBlock; }
            set
            {
                SetField(ref _workInBlock, value);
            }
        }

        private void _generate_WorkInBlock(Work work)
        {
            WorkInBlockFactory workInBlockFactory = new WorkInBlockFactory();
            WorkInBlock = workInBlockFactory.CreateWorkInBlock(work);
            WorkInThisBlock = new ObservableCollection<WorkInBlock>();
            WorkInThisBlock.Add((WorkInBlock)WorkInBlock);
        }
        #endregion

        #region Delete Command
        private readonly ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
            }
        }
        private bool CanDelete(object obj)
        {
            return true;
        }
        private void Delete(object obj)
        {
            WorkInBlock.WorkControlDataContext.DeleteWork();
            WorkspaceViewModel.Generate_Week(CurDate);
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
                WorkInBlock.WorkControlDataContext.UpdateWork();
                WorkInBlock.WorkControlDataContext.IsEdititig = true;
            }
            else
                WorkInBlock.WorkControlDataContext.IsEdititig = false;
        }
        #endregion
    }
}
