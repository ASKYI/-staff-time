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
using System.Windows;
using Staff_time.View.Dialog;
using Staff_time.Helpers;

namespace Staff_time.ViewModel
{
    public delegate void TimeHandler();

    public class WorkBlockControlViewModel : MainViewModel
    {
        public WorkBlockControlViewModel(int workID, bool IsEditingFlag = false)
        {
            WorkVM = WorksVM.Dictionary[workID];
            WorkInBlockID = new WorkIDDependency(workID);
            _generate_path();

            SelectedWorkTypeIndex = WorkVM.Work.WorkTypeID;
            _generate_TaskTypesCb();

            _applyCommand = new RelayCommand(ApplyChanges, CanEdit);
            _cancelCommand = new RelayCommand(CancelChanges, CanEdit);
            _deleteCommand = new RelayCommand(Delete, CanDelete);
            _changeTaskCommand = new RelayCommand(ChangeTask, CanChangeTask);
            _duplicateWorkCommand = new RelayCommand(DuplicateWork, (_) => true);
            _shareWorkTaskCommand = new RelayCommand(ShareWork, (_) => true);
            _addWorkRangeCommand = new RelayCommand(AddWorkTimeRange, (_) => true);
            _deleteWorkRangeCommand = new RelayCommand(param =>
            {
                if (WorkTimeRanges.Count == 1) //нельзя удалять, если диапазон один
                    return;
                WorkTimeRanges.Remove(param as TimeRange);
                UpdateWorkTime();
                RaisePropertyChanged("IsWorkTimeEnabled");
            }, (_) => true);

            WorkNames = TasksVM.GetAllWorksNames(WorkVM.Work.TaskID);

            //SaveHotCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            //DeleteHotCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.Control));

            MainWindow.GlobalPropertyChanged += HandleGlobalPropertyChanged;

            MessengerInstance.Register<string>(this, ApplyAction);
            IsEditing = IsEditingFlag;
            if (IsEditing)
            {
                IsExpanded = true;
                MainWindow.IsEnable = false;
            }
            else
                IsExpanded = false;
            MouseLeft = false;

            _hours = Minutes / 60;
            _minutesShort = Minutes % 60;

            WorkTimeRanges = new ObservableCollection<TimeRange>();

            //_endTime = Work.StartTime.AddMinutes(Work.Minutes);
            timeHandler = UpdateWorkTime;
            FillTimeRanges();
            if (WorkTimeRanges.Count == 0) //Работа новая
                AddWorkTimeRange(this);

            WorkVM.PropertyChanged += new PropertyChangedEventHandler(SetExpended);
        }

        TimeHandler timeHandler;
        public void UpdateWorkTime()
        {
            int sumRangesMinutes = 0;
            if (WorkTimeRanges.Count > 0)
            {
                foreach (var range in WorkTimeRanges)
                    sumRangesMinutes += (range.EndTime - range.StartTime).Hours * 60 + (range.EndTime - range.StartTime).Minutes;
                Work.Minutes = sumRangesMinutes;
            }

            _hours = Work.Minutes / 60;
            _minutesShort = Work.Minutes % 60;
            RaisePropertyChanged("MinutesShort");
            RaisePropertyChanged("Hours");
            RaisePropertyChanged("TimeLast");
        }

        public void FillTimeRanges()
        {
            WorkTimeRanges = new ObservableCollection<TimeRange>();
            var curWorkTimeRanges = WorksVM.GetTimeRanges(Work.ID);

            foreach (var rng in curWorkTimeRanges)
            {
                TimeRange curWorkRng = new TimeRange();
                curWorkRng.UpdateParentWorkTime = timeHandler;
                curWorkRng.StartTime = rng.StartTime;
                curWorkRng.ID = rng.ID;
                curWorkRng.EndTime = rng.EndTime;
                WorkTimeRanges.Add(curWorkRng);
            }
            UpdateWorkTime();
            var sortWorkRanges = WorkTimeRanges.OrderBy(r => r.EndTime).ToList();
            int lastRngIndex = sortWorkRanges.Count - 1;
            if (lastRngIndex >= 0)
                LastRangeTime = sortWorkRanges[lastRngIndex].EndTime;
        }

        private void UpdateWorkTimeRanges()
        {
            var DBRanges = new List<WorkTimeRange>();
            foreach (var rng in WorkTimeRanges)
            {
                WorkTimeRange workRng = new WorkTimeRange() { WorkID = WorkVM.Work.ID, StartTime = rng.StartTime, EndTime = rng.EndTime, ID = rng.ID };
                DBRanges.Add(workRng);
            }
            WorksVM.UpdateTimeRanges(DBRanges, Work.ID);
        }

        void HandleGlobalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MainWindowClosing")
                UnRegister();
        }

        public void UnRegister()
        {
            MessengerInstance.Unregister<string>(this, ApplyAction);
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
                if (IsEditing && value == false)
                    return;
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

                stringPath.Append(taskNode.FullPathAsString);

                //for (int i = taskNode.FullPath.Count - 1; i >= 0; --i) // todo есть замечательный оператор string.Join советую к нему присмотреться
                //{
                //    stringPath.Append(taskNode.FullPath[i]);
                //    if (i != 0)
                //        stringPath.Append("<-");
                //}

                return stringPath.ToString();
            }
        }

        private void _generate_path()
        {
            int maxLength = Math.Max((BlockWidth - 520) / 7 - 3, 10);
            if (!TasksVM.DictionaryFull.ContainsKey(Work.TaskID))
            {
                PathTruncated = Work.WorkName;
                return;
            }
            TreeNode taskNode = TasksVM.DictionaryFull[Work.TaskID];

            StringBuilder stringPath = new StringBuilder();
            stringPath.Append(Work.WorkName + "<-");

            PathFirstLevel = taskNode.FullPath != null ? taskNode.FullPath[0] : "";
            PathSecondLevel = taskNode.FullPath.Count > 1 ? taskNode.FullPath[1] : "";

            for (int i = taskNode.FullPath.Count - 1; i >= 2; --i)
            {
                stringPath.Append(taskNode.FullPath[i]);
                if (stringPath.Length > maxLength)
                {
                    int extra = stringPath.Length - maxLength;
                    stringPath.Remove(maxLength, extra);
                    stringPath.Append("...");
                    break;
                }
                if (i != 0)
                    stringPath.Append("<-");
            }
            PathTruncated = stringPath.ToString();
        }

        private string _pathTruncated;
        public string PathTruncated
        {
            get
            {
                //_generate_path();
                return _pathTruncated;
            }
            set
            {
                _pathTruncated = value;
                RaisePropertyChanged("PathTruncated");
            }
        }
       
        public string PathFirstLevel { get; set; }

        public string PathSecondLevel { get; set; }

        public string TimeRange
        {
            get
            {
                if (WorkTimeRanges == null || WorkTimeRanges.Count == 0)
                    return "";
                var startTimeString = WorkTimeRanges[0].StartTime.ToString("HH:mm");
                var endTimeString = WorkTimeRanges[0].EndTime.ToString("HH:mm");
                return startTimeString + " - " + endTimeString;
            }
        }

        public string TimeLast
        {
            get
            {
                return (Minutes / 60).ToString() + " ч. " + (Minutes % 60).ToString() + " мин.";
            }
        }
        #endregion

        #region Time

        private DateTime _lastRangeTime;
        public DateTime LastRangeTime
        {
            get
            {
                return _lastRangeTime;
            }
            set
            {
                _lastRangeTime = value;
                RaisePropertyChanged("LastRangeTime");
            }
        }

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
        private ObservableCollection<TimeRange> _workTimeRanges;
        public ObservableCollection<TimeRange> WorkTimeRanges
        {
            get
            {
                return _workTimeRanges;
            }
            set
            {
                _workTimeRanges = value;
                RaisePropertyChanged("WorkTimeRanges");
            }
        }

        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                Minutes = _hours * 60 + MinutesShort;
            }
        }

        private int _minutesShort;

        public int MinutesShort
        {
            get { return _minutesShort; }
            set
            {
                _minutesShort = value;
                Minutes = Hours * 60 + _minutesShort;
            }
        }

        public int Minutes
        {
            get { return Work.Minutes; }
            set
            {
                Work.Minutes = value;
                if (WorkTimeRanges.Count == 1)
                    WorkTimeRanges[0].UpdateEndTimeSilent(Work.Minutes);

                RaisePropertyChanged("MinutesShort");
                RaisePropertyChanged("Hours");
            }
        }

        private readonly ICommand _addWorkRangeCommand;
        public ICommand AddWorkRangeCommand
        {
            get
            {
                return _addWorkRangeCommand;
            }
        }

        private void AddWorkTimeRange(object obj)
        {
            TimeRange rng = new TimeRange();
            rng.UpdateParentWorkTime = timeHandler;

            DateTime newDt = DateTime.Now;
            var oldDt = WorkVM.Work.StartDate;
            if (oldDt.Hour != 0 || oldDt.Minute != 0)
            {
                newDt = (DateTime)WorkVM.Work.StartDate;
                WorkVM.Work.StartDate = new DateTime(oldDt.Year, oldDt.Month, oldDt.Day, 0, 0, 0);
            }
            rng.StartTime = new DateTime(1899, 12, 30, newDt.Hour, newDt.Minute, 0);
            rng.EndTime = new DateTime(1899, 12, 30, newDt.Hour, newDt.Minute, 0);
            rng.IsFocused = true;
            WorkTimeRanges.Add(rng);
            RaisePropertyChanged("IsWorkTimeEnabled");
        }

        private readonly ICommand _deleteWorkRangeCommand;
        public ICommand DeleteWorkRangeCommand
        {
            get
            {
                return _deleteWorkRangeCommand;
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
            //if (IsEditing)
            //{
            ApplyChanges();
            var workDuplicate = (Work)_workVM.Work.Clone();
            workDuplicate.ID = 0;
            workDuplicate.AttrValues.Clear();
            MessengerInstance.Send<MessageWorkObject>(new MessageWorkObject
               (WorkCommandEnum.Add, workDuplicate, workDuplicate.StartDate));
            // }
        }
        private readonly ICommand _shareWorkTaskCommand;
        public ICommand ShareWorkTaskCommand
        {
            get
            {
                return _shareWorkTaskCommand;
            }
        }

        private void ShareWork(object obj)
        {
            //Показываем диалог с пользователями
            if (Work.TaskID <= 0)
            {
                MessageBox.Show("У работы отсутствует родительская задача! Данная работа не может быть передана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dt = new DateTime(chosenDate.Year, chosenDate.Month, chosenDate.Day, WorkTimeRanges[0].StartTime.Hour,
                WorkTimeRanges[0].StartTime.Minute, WorkTimeRanges[0].StartTime.Second);
            TransferTaskView dlg = new TransferTaskView(Work.TaskID, dt);
            dlg.Show();
        }

        public bool IsWorkTimeEnabled
        {
            get
            {
                return WorkTimeRanges.Count <= 1;
            }
        }

        #endregion //time

        #region Edit
        private List<string> _workNames;
        public List<string> WorkNames
        {
            get
            {
                return _workNames;
            }
            set
            {
                _workNames = value;
                RaisePropertyChanged("WorkNames");
            }
        }

        public Boolean IsEditing
        {
            get { return _workVM.IsEdititig; }
            set
            {
                if (!MainWindow.IsEnable)
                    return;
                _workVM.IsEdititig = value; //Потому что нельзя передать свойство по ссылке в SetField
                RaisePropertyChanged("IsEditing");
                //RaisePropertyChanged("IsRed");
                RaisePropertyChanged("IsReadOnly");
                RaisePropertyChanged("IsEnabled");

                RaisePropertyChanged("FullPath");
                RaisePropertyChanged("Path");
            }
        }
        //public Boolean IsRed
        //{
        //    get
        //    {
        //        return _workVM.IsEdititig;
        //    }
        //}
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

        private bool CanEdit(object obj)
        {
            return dialog == null;
        }

        private void ApplyChanges(object obj)
        {
            if (IsEditing)
            {
                Minutes = Hours * 60 + MinutesShort;
                _hours = Minutes / 60;
                _minutesShort = Minutes % 60;
                Work.WorkTypeID = SelectedWorkTypeIndex;
                UpdateWorkTimeRanges();
                WorkVM.UpdateWork();
                IsEditing = false;
                MainWindow.IsEnable = true;

                var sortWorkRanges = WorkTimeRanges.OrderBy(r => r.EndTime).ToList();
                int lastRngIndex = sortWorkRanges.Count - 1;
                if (lastRngIndex >= 0)
                    LastRangeTime = sortWorkRanges[lastRngIndex].EndTime;

                _generate_path();
                RaisePropertyChanged("MinutesShort");
                RaisePropertyChanged("Hours");
                RaisePropertyChanged("TimeLast");
                RaisePropertyChanged("LastRangeTime");
            }
        }

        private void CancelChanges(object obj)
        {
            if (WorkVM.IsEdititig)
            {
                WorkVM.CancelWork();
                IsEditing = false;
                MainWindow.IsEnable = true;
            }
        }

        private readonly ICommand _applyCommand;
        public ICommand ApplyCommand
        {
            get
            {
                return _applyCommand;
            }
        }
        private readonly ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand;
            }
        }

        public void ApplyAction(string message)
        {
            if (message.ToLower() == "cancel")
                CancelChanges(null);
            else if (message.ToLower() == "apply")
                ApplyChanges(null);
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
            return dialog == null;
        }
        private void Delete(object obj)
        {
            var dialogResult = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить работу?", "Подтверждение",
               MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.No)
                return;
            WorkVM.DeleteWork();
            UnRegister();
            IsEditing = false;
            CancelChanges(obj);
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

        private int _blockWidth;
        public int BlockWidth
        {
            get { return _blockWidth; }
            set
            {
                _blockWidth = value;
                _generate_path();
                RaisePropertyChanged("Path");
            }
        }

        #region HotKeys Commands
        public ICommand SaveHotCommand;
        public ICommand DeleteHotCommand;
        #endregion // HotKeys Commands
    }


    public class TimeRange : INotifyPropertyChanged
    {
        public void UpdateEndTimeSilent(int minutes)
        {
            _endTime = StartTime.AddMinutes(minutes);
            NotifyPropertyChanged("EndTime");
        }

        public TimeHandler UpdateParentWorkTime;

        public int ID { get; set; }
        private DateTime _startTime;
        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                DateTime DTvalue = new DateTime(1899, 12, 30, value.Hour, value.Minute, 0);
                _startTime = DTvalue;
                UpdateParentWorkTime();
                NotifyPropertyChanged("StartTime");
            }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                DateTime DTvalue = new DateTime(1899, 12, 30, value.Hour, value.Minute, 0);
                _endTime = DTvalue;
                UpdateParentWorkTime();
                NotifyPropertyChanged("EndTime");
            }
        }

        public bool IsFocused { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion
    }
}
