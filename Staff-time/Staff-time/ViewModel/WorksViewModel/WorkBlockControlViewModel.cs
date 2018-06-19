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
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public class WorkBlockControlViewModel : MainViewModel
    {
        public WorkBlockControlViewModel(int workID)
        {
            WorkVM = WorksVM.Dictionary[workID];
            WorkInBlockID = new WorkIDDependency(workID);

            _fullPath = TasksVM.generate_PathForTask(Work.TaskID);

            SelectedWorkTypeIndex = WorkVM.Work.WorkTypeID;
            _generate_TaskTypesCb();
            
            _editCommand = new RelayCommand(Edit, CanEdit);
            _deleteCommand = new RelayCommand(Delete, CanDelete);
            MessengerInstance.Register<string>(this, _cancelEditing);

            if (IsEdititig)
                IsExpanded = true;
            else
                IsExpanded = false;
            MouseLeft = false;
        }

        private WorkControlViewModelBase _workVM;
        public WorkControlViewModelBase WorkVM
        {
            get { return _workVM; }
            set
            {
                SetField(ref _workVM, value);
            }
        }

        public Work Work
        {
            get { return _workVM.Work; }
        }
        public WorkIDDependency WorkInBlockID;

        private Boolean _isExpanded;
        public Boolean IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded)
                    CancelEditing();
                SetField(ref _isExpanded, value);
            }
        }

        public bool MouseLeft;

        #region Path

        private List<string> _fullPath;

        public string FullPath
        {
            get
            {
                StringBuilder stringPath = new StringBuilder();
                for (int i = 0; i < _fullPath.Count; ++i)
                {
                    if (i != 0)
                        stringPath.Append("->");
                    stringPath.Append(_fullPath[i]);
                }
                stringPath.Append(">>" + Work.WorkName);
                return stringPath.ToString();
            }
        }       
        public string Path
        {
            get
            {
                StringBuilder stringPath = new StringBuilder();
                for (int i = 0; i < Math.Min(3, _fullPath.Count); ++i)
                {
                    if (i != 0)
                        stringPath.Append("->");
                    stringPath.Append(_fullPath[i]);
                }

                if (_fullPath.Count > 3)
                    stringPath.Append("...");

                stringPath.Append(">>" + Work.WorkName);
                return stringPath.ToString();
            }
        }

        #endregion

        #region Time

        public void StartTime_Changed(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            DateTime old = Work.StartDate;
            Work.StartDate = new DateTime(old.Year, old.Month, old.Day, //Я не придумала ничего лучше
                e.NewTime.Hours, e.NewTime.Minutes, e.NewTime.Seconds);
            Work.EndDate = Work.StartDate.AddMinutes(Work.Minutes);
        }

        public void EndTime_Changed(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            DateTime old = Work.EndDate;
            Work.EndDate = new DateTime(old.Year, old.Month, old.Day,
                e.NewTime.Hours, e.NewTime.Minutes, e.NewTime.Seconds);
            Work.StartDate = Work.EndDate.AddMinutes(-Work.Minutes);
        }

        public int StartHours
        {
            get { return Work.StartDate.Hour; }
            set
            {
                DateTime old = Work.StartDate;
                Work.StartDate = new DateTime(old.Year, old.Month, old.Day,
                    value, old.Minute, old.Second);
                RaisePropertyChanged("StartHours");
            }
        }
        public int StartMinutes
        {
            get { return Work.StartDate.Minute; }
            set
            {
                DateTime old = Work.StartDate;
                Work.StartDate = new DateTime(old.Year, old.Month, old.Day,
                    old.Hour, value, old.Second);
                RaisePropertyChanged("StartMinutes");
            }
        }

        public int EndHours
        {
            get { return Work.StartDate.Hour; }
            set
            {
                DateTime old = Work.EndDate;
                Work.EndDate = new DateTime(old.Year, old.Month, old.Day,
                    value, old.Minute, old.Second);
                RaisePropertyChanged("EndHours");
            }
        }
        public int EndMinutes
        {
            get { return Work.StartDate.Minute; }
            set
            {
                DateTime old = Work.EndDate;
                Work.EndDate = new DateTime(old.Year, old.Month, old.Day,
                    old.Hour, value, old.Second);
                RaisePropertyChanged("EndMinutes");
            }
        }

        #endregion

        #region Edit

        public Boolean IsEdititig
        {
            get { return _workVM.IsEdititig; }
            set
            {
                _workVM.IsEdititig = value; //Потому что нельзя передать свойство по ссылке в SetField
                RaisePropertyChanged("IsEdititig");
                RaisePropertyChanged("IsRed");
                RaisePropertyChanged("IsReadOnly");
                RaisePropertyChanged("IsEnabled");

                RaisePropertyChanged("FullPath");
                RaisePropertyChanged("Path");
            }
        }
        public Boolean IsRed
        {
            get
            {
                return _workVM.IsEdititig;
            }
        }
        public Boolean IsReadOnly
        {
            get
            {
                return !_workVM.IsEdititig;
            }
        }
        public Boolean IsEnabled
        {
            get
            {
                return _workVM.IsEdititig;
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
            if (IsEdititig)
            {
                Work.WorkTypeID = SelectedWorkTypeIndex;
                WorkVM.UpdateWork();
            
                IsEdititig = false;
            }
            else
            {
                IsEdititig = true;
            }          
        }

        public void _cancelEditing(string message)
        {
            if (IsEdititig && MouseLeft)
                Edit(this);
        }

        #endregion

        #region Delete

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
            WorkVM.DeleteWork();
        }

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

    }
}
