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
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public class WorkBlockControlViewModel : MainViewModel
    {
        public WorkBlockControlViewModel()
        {
            _generate_TaskTypesCb();
        }

        public void InitWork(int workID)
        {
            _generate_TaskTypesCb();

            if (WorksVM.Dictionary.ContainsKey(workID))
            {
                Work = WorksVM.Dictionary[workID].Work;
                WorkInBlockID = new WorkIDDependency(workID);
                SelectedWorkTypeIndex = Work.WorkTypeID;
            }
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
        public WorkIDDependency WorkInBlockID;

        #region Edit

        #endregion

        #region Work Types

        private int _selectedWorkTypeIndex;
        public int SelectedWorkTypeIndex
        {
            get { return _selectedWorkTypeIndex; }
            set
            {
                SetField<int>(ref _selectedWorkTypeIndex, value);

                Work.WorkTypeID = _selectedWorkTypeIndex;
            }
        }

        private ObservableCollection<WorkType> _workTypesCb;
        public ObservableCollection<WorkType> WorkTypesCb
        {
            get { return _workTypesCb; }
            set
            {
                SetField(ref _workTypesCb, value);
            }
        }
        private void _generate_TaskTypesCb()
        {
            WorkTypesCb = new ObservableCollection<WorkType>();
            List<WorkType> types = Context.typesWork.Read_WorkTypes();
            foreach (var t in types)
            {
                WorkTypesCb.Add(t);
            }
        }

        #endregion
        //_generate_WorkInBlock(work);
        //_generate_WorkTypesCb();
        //SelectedWorkTypeIndex = work.WorkTypeID;

        //if (isEditting == true)
        //{
        //    IsChangingType = false; IsEnabledCbx = true;
        //    WorkInBlock.WorkControlDataContext.IsEdititig = false;
        //    IsExpanded = true;
        //}
        //else
        //{
        //    IsChangingType = true; IsEnabledCbx = false;
        //    WorkInBlock.WorkControlDataContext.IsEdititig = true;
        //    IsExpanded = false;
        //}

        //_changeTypeCommand = new RelayCommand(ChangeType, CanChangeType);
        //_editCommand = new RelayCommand(Edit, CanEdit);
        //_deleteCommand = new RelayCommand(Delete, CanDelete);
    }

    //#region WorkInBlock
    //private ObservableCollection<WorkInBlock> _workInThisBlock;
    //public ObservableCollection<WorkInBlock> WorkInThisBlock
    //{
    //    get { return _workInThisBlock; }
    //    set
    //    {
    //        SetField < ObservableCollection<WorkInBlock>>(ref _workInThisBlock, value);
    //    }
    //}

    //private WorkInBlock _workInBlock;
    //public WorkInBlock WorkInBlock
    //{
    //    get { return _workInBlock; }
    //    set
    //    {
    //        SetField(ref _workInBlock, value);
    //    }
    //}

    //private void _generate_WorkInBlock(Work work)
    //{
    //    WorkInBlockFactory workInBlockFactory = new WorkInBlockFactory();
    //    WorkInBlock = workInBlockFactory.CreateWorkInBlock(work);
    //    WorkInThisBlock = new ObservableCollection<WorkInBlock>();
    //    WorkInThisBlock.Add((WorkInBlock)WorkInBlock);

    //    int taskId = WorkInBlock.WorkControlDataContext.Work.TaskID;
    //    if (TasksVM.Dictionary.ContainsKey(taskId))
    //        Path = TasksVM.generate_PathForTask(TasksVM.Dictionary[WorkInBlock.WorkControlDataContext.Work.TaskID]) + "-->" + WorkInBlock.WorkControlDataContext.Work.WorkName;
    //    else
    //        Path = "Ошибка пути";
    //}

    //private string _path;
    //public string Path
    //{
    //    get { return _path; }
    //    set
    //    {
    //        SetField(ref _path, value);
    //    }
    //}
    //#endregion

    //private int _selectedWorkTypeIndex;
    //public int SelectedWorkTypeIndex
    //{
    //    get { return _selectedWorkTypeIndex; }
    //    set
    //    {
    //        SetField<int>(ref _selectedWorkTypeIndex, value);
    //        ChangeWorkType();
    //    }
    //}
    //private ObservableCollection<WorkType> _workTypesCb;
    //public ObservableCollection<WorkType> WorkTypesCb
    //{
    //    get { return _workTypesCb; }
    //    set
    //    {
    //        SetField<ObservableCollection<WorkType>>(ref _workTypesCb, value);
    //    }
    //}
    //private void _generate_WorkTypesCb()
    //{
    //    WorkTypesCb = new ObservableCollection<WorkType>();
    //    List<WorkType> types = Context.typesWork.Read_WorkTypes();
    //    foreach (var t in types)
    //    {
    //        WorkTypesCb.Add(t);
    //    }
    //}

    //#endregion
    //private Boolean _isChangingType;
    //public Boolean IsChangingType
    //{
    //    get { return _isChangingType; }
    //    set
    //    {
    //        SetField(ref _isChangingType, value);
    //    }
    //}
    //private Boolean _isEnabledCbx;
    //public Boolean IsEnabledCbx
    //{
    //    get { return _isEnabledCbx; }
    //    set
    //    {
    //        SetField<Boolean>(ref _isEnabledCbx, value);
    //    }
    //}
    //private readonly ICommand _changeTypeCommand;
    //public ICommand ChangeTypeCommand
    //{
    //    get
    //    {
    //        return _changeTypeCommand;
    //    }
    //}
    //private bool CanChangeType(object obj)
    //{
    //    return true;
    //}
    //private void ChangeType(object obj)
    //{
    //    if (!IsChangingType)
    //    {
    //        IsEnabledCbx = false;
    //        IsChangingType = true;
    //    }
    //    else
    //    {
    //        IsEnabledCbx =  true;
    //        IsChangingType = false;
    //    }
    //}
    //void ChangeWorkType()
    //{
    //    if (!IsChangingType)
    //    {
    //        WorkInBlock.WorkControlDataContext.Work.WorkTypeID = SelectedWorkTypeIndex;

    //        WorkInBlock.WorkControlDataContext.UpdateWork();

    //        MessengerInstance.Send<NotificationMessage>(new NotificationMessage("Update!"));
    //    }
    //}
    //#endregion
    //#region Delete Command
    //private readonly ICommand _deleteCommand;
    //public ICommand DeleteCommand
    //{
    //    get
    //    {
    //        return _deleteCommand;
    //    }
    //}
    //private bool CanDelete(object obj)
    //{
    //    return true;
    //}
    //private void Delete(object obj)
    //{
    //    WorkInBlock.WorkControlDataContext.DeleteWork();
    //    MessengerInstance.Send<KeyValuePair<int, Work>>(new KeyValuePair<int, Work>(0,
    //        WorkInBlock.WorkControlDataContext.Work));
    //}
    //#endregion
    //#region Edit Command
    //private Boolean _isEnabled;
    //public Boolean IsEnabled
    //{
    //    get { return _isEnabled; }
    //    set
    //    {
    //        SetField<Boolean>(ref _isEnabled, value);
    //    }
    //}
    //private readonly ICommand _editCommand;
    //public ICommand EditCommand
    //{
    //    get
    //    {
    //        return _editCommand;
    //    }
    //}
    //private bool CanEdit(object obj)
    //{
    //    return true;
    //}
    //private void Edit(object obj)
    //{
    //    if (!WorkInBlock.WorkControlDataContext.IsEdititig)
    //    {
    //        WorkInBlock.WorkControlDataContext.Work.WorkTypeID = SelectedWorkTypeIndex;

    //        MessengerInstance.Send<KeyValuePair<int, Work>>(new KeyValuePair<int, Work>(1,
    //            WorkInBlock.WorkControlDataContext.Work));

    //        WorkInBlock.WorkControlDataContext.UpdateWork();

    //        WorkInBlock.WorkControlDataContext.IsEdititig = true;
    //        IsEnabled = false;
    //    }
    //    else
    //    {
    //        WorkInBlock.WorkControlDataContext.IsEdititig = false;
    //        IsEnabled = true;
    //    }
    //}
    //#endregion

    //private Boolean _isExpanded;
    //public Boolean IsExpanded
    //{
    //    get { return _isExpanded; }
    //    set
    //    {
    //        SetField(ref _isExpanded, value);
    //    }
    //}

}