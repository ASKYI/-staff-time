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

namespace Staff_time.ViewModel
{
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

            _endTime = Work.StartTime.AddMinutes(Work.Minutes);
            WorkVM.PropertyChanged += new PropertyChangedEventHandler(SetExpended);
        }

        void HandleGlobalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MainWindowClosing")
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

        private string _path;
        private void _generate_path()
        {
            //int max_k = Block_Width / 200;
            int maxLength = Block_Width / 8 - 25;

            if (!TasksVM.DictionaryFull.ContainsKey(Work.TaskID))
            {
                _path = Work.WorkName;
                return;
            }
            TreeNode taskNode = TasksVM.DictionaryFull[Work.TaskID];

            StringBuilder stringPath = new StringBuilder();
            stringPath.Append(Work.WorkName + "<-");

            for (int i = taskNode.FullPath.Count - 1; i >= 0; --i)
            {
                if (stringPath.Length + taskNode.FullPath[i].Length > maxLength)
                {
                    stringPath.Append("...");
                    break;
                }
                stringPath.Append(taskNode.FullPath[i]);
                stringPath.Append("<-");
            }

            //if (stringPath.Length > maxLength)
            //    stringPath.Append("...");

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
                DateTime DTvalue = new DateTime(1899, 12, 30, value.Hour, value.Minute, 0);
                Work.StartTime = DTvalue;
                Work.Minutes = (EndTime - StartTime).Hours * 60 + (EndTime - StartTime).Minutes;
                _hours = Work.Minutes / 60;
                _minutesShort = Work.Minutes % 60;

                RaisePropertyChanged("StartTime"); // todo посмотреть функцию nameof
                RaisePropertyChanged("MinutesShort");
                RaisePropertyChanged("Hours");
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
                _hours = Work.Minutes / 60;
                _minutesShort = Work.Minutes % 60;

                RaisePropertyChanged("EndTime");
                RaisePropertyChanged("MinutesShort");
                RaisePropertyChanged("Hours");
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
                _endTime = Work.StartTime.AddMinutes(Work.Minutes);

                RaisePropertyChanged("StartTime");
                RaisePropertyChanged("EndTime");

                RaisePropertyChanged("MinutesShort");
                RaisePropertyChanged("Hours");
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
            ApplyChanges();
            var workDuplicate = (Work)_workVM.Work.Clone();
            workDuplicate.ID = 0;
            workDuplicate.AttrValues.Clear();
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
               (WorkCommandEnum.Add, workDuplicate));
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
            TransferTaskView dlg = new TransferTaskView(Work.TaskID);
            dlg.Show();
        }
        

        #endregion //time

        #region Edit

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
            if (WorkVM.IsEdititig)
            {
                Minutes = Hours * 60 + MinutesShort;
                _hours = Minutes / 60;
                _minutesShort = Minutes % 60;
                Work.WorkTypeID = SelectedWorkTypeIndex;
                WorkVM.UpdateWork();
                IsEditing = false;
                MainWindow.IsEnable = true;
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
            ApplyChanges();
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

        #region HotKeys Commands
        public ICommand SaveHotCommand;
        public ICommand DeleteHotCommand;
        #endregion // HotKeys Commands
    }
}
