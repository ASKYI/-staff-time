﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Staff_time.View.Dialog;

namespace Staff_time.ViewModel
{
    public class WorkspaceViewModel : MainViewModel, INotifyPropertyChanged
    {
        public WorkspaceViewModel()
        {
            TimeSortSources = new List<string> { "../Resources/timeSort_none.ico", "../Resources/timeSort_asc.ico", "../Resources/timeSort_desc.ico" };
            NameSortSources = new List<string> { "../Resources/nameSort_none.ico", "../Resources/nameSort_asc.ico", "../Resources/nameSort_desc.ico" };
            TimeSortDirection = -1;
            NameSortDirection = -1;
            TimeByPlanDate = new Dictionary<DateTime, double>();
            SelectedDate_Picker = chosenDate;

            MessengerInstance.Register<long>(this, SumTimeChange);

            _collapseAllWorksCommand = new RelayCommand(CollapseAllWorks, (_) => true);
            _expandAllWorksCommand = new RelayCommand(ExpandAllWorks, (_) => true);
            _checkDayTimeCommand = new RelayCommand(ShowWorksRanges, (_) => true);
            //_sortWorksByStartTimeCommand = new RelayCommand(SortWorksByStartTime, (_) => true);
            _sortWorksByNameCommand = new RelayCommand(SortWorksByName, (_) => true);
            MainWindow.GlobalPropertyChanged += HandleGlobalPropertyChanged;

        }

        #region INotifyPropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }
        void HandleGlobalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
            SetTabEnable(IsMainWindowEnabled);
            if (e.PropertyName == "MainWindowClosing")
            {
                foreach (var tab in WeekTabs)
                    tab.UnregisterEvents();
                MessengerInstance.Unregister<long>(this, SumTimeChange);
            }
        }
        #endregion

        #region Selected Date TabIndex

        private void SetTabEnable(bool _isEnabled)
        {
            foreach (var tab in WeekTabs)
                tab.IsEnabled = _isEnabled;
        }

        private DateTime _selectedDate_Picker; // todo picker - это графический элемент, вьюмодель о нём не должна знать
        public DateTime SelectedDate_Picker
        {
            get { return _selectedDate_Picker; }
            set
            {
                if (_selectedDate_Picker == value)
                    return;

                //bool isTheSameWeek = false;
                //if (_selectedDate_Picker != DateTime.MinValue)
                //    isTheSameWeek = DatesAreInTheSameWeek(_selectedDate_Picker, value);
                SetField(ref _selectedDate_Picker, value);

                chosenDate = value;
                //if (!isTheSameWeek)
                Generate_Week(_selectedDate_Picker);
                //else
                //    SelectedTabIndex = (int)(_selectedDate_Picker.DayOfWeek) - 1;

                NotifyPropertyChanged("SelectedDate_Picker");
            }
        }

        //private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        //{
        //    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
        //    var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
        //    var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

        //    return d1 == d2;
        //}

        public bool IsMainWindowEnabled
        {
            get { return MainWindow.IsEnable; }
        }
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (value >= 0 && value < WeekTabs.Count)
                {
                    SetField(ref _selectedTabIndex, value);

                    chosenDate = WeekTabs[SelectedTabIndex].Date;
                    SelectedDate_Picker = chosenDate;
                    //if (!TimeByPlanDate.ContainsKey(chosenDate))
                    //    TimeByPlanDate.Add(chosenDate, Context.timeTableWork.Read_TimeByDate(chosenDate));
                    //PlanningTime = TimeByPlanDate[chosenDate];

                    //WeekTabs[SelectedTabIndex].Generate_WorksForDate();
                    SumTime = WeekTabs[SelectedTabIndex].SumTime;
                    TimeSortDirection = -1;
                    NameSortDirection = -1;
                    NotifyPropertyChanged("SelectedTabIndex");
                }
            }
        }
        public void SetDate(DateTime dt)
        {
            SelectedDate_Picker = dt;
        }

        #endregion

        #region Week
        private static readonly string[] DaysOfWeek = new string[7] { "Понедельник", "Вторник", "Среда",
            "Четверг", "Пятница", "Суббота", "Воскресенье" };

        private ObservableCollection<TabItem> _weekTabs;
        public ObservableCollection<TabItem> WeekTabs
        {
            get { return _weekTabs; }
            set
            {
                SetField(ref _weekTabs, value);
            }
        }


        public void Generate_Week(DateTime date)
        {
            bool isUpdate = false;
            if (WeekTabs == null)
                WeekTabs = new ObservableCollection<TabItem>();
            else
                isUpdate = true;

            int dayOfWeek = (int)date.DayOfWeek - 1; //День недели с понедельника
            if (dayOfWeek == -1)
                dayOfWeek = 6;
            DateTime startDay = date.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; ++i)
            {
                DateTime cur = startDay.AddDays(i);
                if (isUpdate)
                    WeekTabs[i].Update(DaysOfWeek[i], cur);
                else
                    WeekTabs.Add(new TabItem(DaysOfWeek[i], cur));

                if (cur.Date == date.Date)
                {
                    if (!TimeByPlanDate.ContainsKey(cur.Date))
                        TimeByPlanDate.Add(cur.Date, Context.timeTableWork.Read_TimeByDate(cur.Date));
                    PlanningTime = TimeByPlanDate[cur.Date];

                    if (SelectedTabIndex != i)
                        SelectedTabIndex = i;
                }
            }

            SumTime = WeekTabs[SelectedTabIndex].SumTime; // после подсчёта всей недели нужно в сумму положить текущий день
            MessengerInstance.Send<long>(SumTime);
        }
        #endregion

        #region Time

        public Dictionary<DateTime, double> TimeByPlanDate { get; set; }

        public int IsTimePlanEqual
        {
            get
            {
                if (SumTime < PlanningTime * 60)
                    return -1;
                else if (SumTime > PlanningTime * 60)
                    return 1;
                return 0;
            }
        }

        private double _planningTime;

        public double PlanningTime
        {
            get { return _planningTime; }
            set
            {
                SetField(ref _planningTime, value);
                if (TimeByPlanDate[chosenDate] != value)
                    Context.timeTableWork.Update(chosenDate, value);
                NotifyPropertyChanged("PlanningTime");
            }
        }

        private long _sumTime;
        public long SumTime
        {
            get { return _sumTime; }
            set
            {
                SetField(ref _sumTime, value);
                SumHours = value / 60;
                SumMinutes = value % 60;
            }
        }

        private long _sumHours;
        public long SumHours
        {
            get { return _sumHours; }
            set
            {
                SetField(ref _sumHours, value);
                NotifyPropertyChanged("SumHours");
            }
        }

        private long _sumMinutes;
        public long SumMinutes
        {
            get { return _sumMinutes; }
            set
            {
                SetField(ref _sumMinutes, value);
                NotifyPropertyChanged("SumMinutes");
                NotifyPropertyChanged("IsTimePlanEqual");
            }
        }

        public void SumTimeChange(long newSumTime)
        {
            SumTime = newSumTime;
        }

        private bool IsEditing()
        {
            return WorksVM.IsEditing();
        }

        #endregion

        #region Sort
        private int _timeSortDirection;
        public int TimeSortDirection
        {
            get
            {
                return _timeSortDirection;
            }
            set
            {
                _timeSortDirection = value;
                TimeSortSource = TimeSortSources[_timeSortDirection + 1];
                NameSortSource = NameSortSources[0];

            }
        }
        private int _nameSortDirection;
        public int NameSortDirection
        {
            get
            {
                return _nameSortDirection;
            }
            set
            {
                _nameSortDirection = value;
                NameSortSource = NameSortSources[_nameSortDirection + 1];
                TimeSortSource = TimeSortSources[0];
            }
        }

        private List<string> TimeSortSources { get; set; }
        private string _timeSortSource;
        public String TimeSortSource
        {
            get
            {
                return _timeSortSource;
            }
            set
            {
                _timeSortSource = value;
                NotifyPropertyChanged("TimeSortSource");
            }
        }

        private List<string> NameSortSources { get; set; }
        private string _nameSortSource;
        public String NameSortSource
        {
            get
            {
                return _nameSortSource;
            }
            set
            {
                _nameSortSource = value;
                NotifyPropertyChanged("NameSortSource");
            }
        }
        #endregion

        #region commands
        private readonly ICommand _collapseAllWorksCommand;
        public ICommand CollapseAllWorksCommand
        {
            get
            {
                return _collapseAllWorksCommand;
            }
        }

        private void CollapseAllWorks(object obj)
        {
            WorksVM.CollapseAll();
        }

        private readonly ICommand _expandAllWorksCommand;
        public ICommand ExpandAllWorksCommand
        {
            get
            {
                return _expandAllWorksCommand;
            }
        }

        private void ExpandAllWorks(object obj)
        {
            WorksVM.ExpandAll();
        }

        
        private readonly ICommand _checkDayTimeCommand;
        public ICommand CheckDayTimeCommand
        {
            get
            {
                return _checkDayTimeCommand;
            }
        }

        private void ShowWorksRanges(object obj)
        {
            //показать окно с диапазонами
            var timeRanges = new List<WorkTimeRange>();
            var paths = new Dictionary<int, string>(); 
            foreach (var workTab in WeekTabs[SelectedTabIndex].WorksInTab)
            {
                int curWorkID = workTab.WorkBlockContext.Work.ID;
                var workRanges = WorksVM.GetTimeRanges(curWorkID);
                paths.Add(workTab.WorkBlockContext.Work.ID, workTab.WorkBlockContext.FullPath);
                timeRanges.AddRange(workRanges);
            }

            var WorksRangesWnd = new WorkTimeRangesView(timeRanges, paths, PlanningTime);
            WorksRangesWnd.ShowDialog();
            if (WorksRangesWnd.WasEdited)
            {
                Mouse.SetCursor(Cursors.Wait);
                //Сдать измненения
                WorksVM.UpdateTimeRanges(timeRanges, 0);

                int newSumTime = 0;
                foreach (var workTab in WeekTabs[SelectedTabIndex].WorksInTab)
                {
                    workTab.WorkBlockContext.FillTimeRanges();
                    newSumTime += workTab.WorkBlockContext.Minutes;
                    workTab.WorkBlockContext.WorkVM.UpdateWork();
                }
                SumTime = newSumTime;
                Mouse.SetCursor(Cursors.Arrow);
            }
        }

        //private ICommand _sortWorksByStartTimeCommand;
        //public ICommand SortWorksByStartTimeCommand
        //{
        //    get
        //    {
        //        return _sortWorksByStartTimeCommand;
        //    }
        //}

        private void SortWorksByStartTime(object obj)
        {
            TimeSortDirection = (TimeSortDirection + 1) % 2;
            WeekTabs[SelectedTabIndex].SortByTime(TimeSortDirection);
        }

        private ICommand _sortWorksByNameCommand;

        public ICommand SortWorksByNameCommand
        {
            get
            {
                return _sortWorksByNameCommand;
            }
        }

        private void SortWorksByName(object obj)
        {
            NameSortDirection = (NameSortDirection + 1) % 2;
            WeekTabs[SelectedTabIndex].SortByName(NameSortDirection);
        }
        #endregion //commands
    }
}
