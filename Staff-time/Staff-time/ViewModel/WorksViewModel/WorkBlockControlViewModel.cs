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
            _generate_path();

            SelectedWorkTypeIndex = WorkVM.Work.WorkTypeID;
            _generate_TaskTypesCb();
            
            _editCommand = new RelayCommand(Edit, CanEdit);
            _deleteCommand = new RelayCommand(Delete, CanDelete);
            _changeTaskCommand = new RelayCommand(ChangeTask, CanChangeTask);
            _duplicateWorkCommand = new RelayCommand(DuplicateWork, (_) => true);
            MessengerInstance.Register<string>(this, _cancelEditing);
            //MessengerInstance.Register<string>(this, ApplyAction); //todo сделать так 

            if (IsEdititig)
                IsExpanded = true;
            else
                IsExpanded = false;
            MouseLeft = false;

            _endHours = Work.StartTime.Hour + (Work.StartTime.Minute + Work.Minutes) / 60;
            _endMinutes = (Work.StartTime.Minute + Work.Minutes) % 60;

            _endTime = Work.StartTime.AddMinutes(Work.Minutes);
            WorkVM.PropertyChanged += new PropertyChangedEventHandler(SetExpended);
        }

        private void SetExpended(object sender, PropertyChangedEventArgs e)
        {
            var baseModel = (WorkControlViewModelBase)sender;
            IsExpanded = baseModel.IsExpanded;
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

        public string FullPath
        {
            get
            {
                if (!TasksVM.Dictionary.ContainsKey(Work.TaskID))
                {
                    return Work.WorkName;
                }
                TreeNode taskNode = TasksVM.Dictionary[Work.TaskID];

                
                StringBuilder stringPath = new StringBuilder();
                stringPath.Append(Work.WorkName + "<-");
                
                for (int i = taskNode.FullPath.Count - 1; i >= 0; --i) // todo есть замечательный оператор string.Join советую к нему присмотреться
                {
                    stringPath.Append(taskNode.FullPath[i]);
                    if (i != 0)
                        stringPath.Append("<-");
                }
                
                return stringPath.ToString();
            }
        }

        private string _path;
        private void _generate_path()
        {
            int max_k = Block_Width / 200;

            if (!TasksVM.Dictionary.ContainsKey(Work.TaskID))
            {
                _path = Work.WorkName;
                return;
            }
            TreeNode taskNode = TasksVM.Dictionary[Work.TaskID];

            StringBuilder stringPath = new StringBuilder();
            stringPath.Append(Work.WorkName + "<-");

            int n_i = Math.Max(0, taskNode.FullPath.Count - max_k);
            for (int i = taskNode.FullPath.Count - 1; i >= n_i; --i)
            {
                stringPath.Append(taskNode.FullPath[i]);
                if (i != n_i)
                    stringPath.Append("<-");
            }

            if (taskNode.FullPath.Count > max_k)
                stringPath.Append("...");

            _path = stringPath.ToString();
        }

        public string Path
        {
            get
            {
                _generate_path();
                return _path;
            }
        }

        public string TimeRange
        {
            get
            {
                var startTimeString = StartTime.ToString("HH:mm");
                var endTimeString = EndTime.ToString("HH:mm");
                return " (" + startTimeString + " - " + endTimeString + ") - " + (Minutes / 60).ToString() + " часов " + (Minutes % 60).ToString() + " минут";
                //return " (" + StartHours + ":" + StartMinutes + " - " + EndHours + ":" + EndMinutes + ") - " + (Minutes / 60).ToString() + " часов " + (Minutes % 60).ToString() + " минут";
            }
        }
        #endregion

        #region Time

        public void Minutes_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //Оно не всегда хорошо работает
            
            /*int minutes = 0;
            try
            {
                minutes = Convert.ToInt32(((System.Windows.Controls.TextBox)sender).Text);
            }
            catch
            {
                minutes = 0;
            }

            int hours = (minutes / 60) % 24; //Нельзя больше дня
            minutes %= 60;
            EndMinutes = StartMinutes + minutes;
            EndHours = StartHours + hours;

            Minutes = hours * 60 + minutes;

            RaisePropertyChanged("StartHours");
            RaisePropertyChanged("StartMinutes");
            RaisePropertyChanged("EndHours");
            RaisePropertyChanged("EndMinutes");*/
        }

        public DateTime StartTime
        {
            get { return Work.StartTime; }
            set
            {
                DateTime DTvalue = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
                Work.StartTime = DTvalue;
                Work.Minutes = (EndTime - StartTime).Hours * 60 + (EndTime - StartTime).Minutes;

                StartMinutes = StartTime.Minute;
                StartHours = StartTime.Hour;

                RaisePropertyChanged("StartTime"); // todo посмотреть функцию nameof
                RaisePropertyChanged("Minutes");
            }
        }
        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                DateTime DTvalue = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
                _endTime = DTvalue;
                Work.Minutes = (EndTime - StartTime).Hours * 60 + (EndTime - StartTime).Minutes;


                EndMinutes = EndTime.Minute;
                EndHours = EndTime.Hour;


                RaisePropertyChanged("EndTime");
                RaisePropertyChanged("Minutes");
            }
        }

        public int StartHours
        {
            get { return Work.StartTime.Hour; }
            set
            {
                DateTime old = Work.StartTime;
                Work.Minutes -= (value - old.Hour) * 60;
                RaisePropertyChanged("Minutes");

                Work.StartTime = new DateTime(old.Year, old.Month, old.Day,
                    value, old.Minute, 0);

                RaisePropertyChanged("StartHours");
                //RaisePropertyChanged("StartMinutes");
                //RaisePropertyChanged("EndHours");
                //RaisePropertyChanged("EndMinutes");
            }
        }
        public int StartMinutes
        {
            get { return Work.StartTime.Minute; }
            set
            {
                DateTime old = Work.StartTime;
                Work.Minutes += old.Minute - value;
                RaisePropertyChanged("Minutes");

                Work.StartTime = new DateTime(old.Year, old.Month, old.Day,
                    old.Hour, value, 0);

                //RaisePropertyChanged("StartHours");
                RaisePropertyChanged("StartMinutes");
                //RaisePropertyChanged("EndHours");
                //RaisePropertyChanged("EndMinutes");
            }
        }

        private int _endHours;
        public int EndHours
        {
            get { return _endHours; }
            set
            {
                _endHours = value;
                Work.Minutes = (value * 60 + EndMinutes) - (StartHours * 60 + StartMinutes);

                RaisePropertyChanged("Minutes");
                //RaisePropertyChanged("StartHours");
                //RaisePropertyChanged("StartMinutes");
                RaisePropertyChanged("EndHours");
                //RaisePropertyChanged("EndMinutes");
            }
        }

        private int _endMinutes;
        public int EndMinutes
        {
            get { return _endMinutes; }
            set
            {
                _endMinutes = value;
                Work.Minutes = (EndHours * 60 + value) - (StartHours * 60 + StartMinutes);

                RaisePropertyChanged("Minutes");
                //RaisePropertyChanged("StartHours");
                //RaisePropertyChanged("StartMinutes");
                //RaisePropertyChanged("EndHours");
                RaisePropertyChanged("EndMinutes");
            }
        }

        public int Minutes
        {
            get { return Work.Minutes; }
            set
            {
               Work.Minutes = value;


                _endMinutes = Work.StartTime.Minute + Work.Minutes % 60;
                _endHours = Work.StartTime.Hour + Work.Minutes / 60 + _endMinutes / 60;
                _endMinutes = _endMinutes % 60;

                EndTime = Work.StartTime.AddMinutes(Work.Minutes);

                RaisePropertyChanged("Minutes");
                RaisePropertyChanged("StartTime");
                RaisePropertyChanged("EndTime");

                RaisePropertyChanged("StartHours");
                RaisePropertyChanged("StartMinutes");
                RaisePropertyChanged("EndHours");
                RaisePropertyChanged("EndMinutes");
            }
        }

        private readonly ICommand _duplicateWorkCommand;
        public ICommand DuplicateWorkCommand
        {
            get
            {
                return _duplicateWorkCommand;
            }
        }

        private void DuplicateWork(object obj)
        {
            //CancelEditing(); //todo разобраться
            Edit(obj);
            var workDuplicate = (Work)_workVM.Work.Clone();

            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
               (WorkCommandEnum.Add, workDuplicate));
            Edit(obj); //не было раньше
        }

        #endregion //time

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
            return dialog == null;
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
                base.CancelEditing();
                IsEdititig = true;
            }          
        }

        public void _cancelEditing(string message)
        {
            if (IsEdititig && MouseLeft)
                Edit(this);
        }

        //public void ApplyAction(string message)
        //{
        //    if (message.ToLower() == "cancel")
        //    {
        //        if (IsEdititig && MouseLeft)
        //            Edit(this);
        //        //WorkVM.CancelWork();
        //        IsEdititig = false;
        //    }
        //    else if (message.ToLower() == "apply")
        //        ApplyChanges(message);
        //}

        //public void ApplyChanges(string message)
        //{
        //    if (IsEdititig)
        //    {
        //        Work.WorkTypeID = SelectedWorkTypeIndex;
        //        WorkVM.UpdateWork();
        //        IsEdititig = false;
        //    }
        //}

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
            return dialog == null;
        }
        private void Delete(object obj)
        {
            Edit(obj);
            WorkVM.DeleteWork();
        }

        #endregion

        #region Change Task

        private readonly ICommand _changeTaskCommand;
        public ICommand ChangeTaskCommand
        {
            get
            {
                return _changeTaskCommand;
            }
        }
        private bool CanChangeTask(object obj)
        {
            return dialog == null;
        }
        private void ChangeTask(object obj)
        {
            base.CancelEditing();

            dialog = new View.WorkDialogView(new WorkDialogViewModel(_workVM));
            dialog.Show();
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

        private int _width = 500;
        public int Block_Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged("Path");
            }
        }
    }
}
