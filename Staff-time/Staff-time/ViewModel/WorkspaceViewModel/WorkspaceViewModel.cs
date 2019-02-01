using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Staff_time.ViewModel
{
    public class WorkspaceViewModel : MainViewModel, INotifyPropertyChanged
    {
        public WorkspaceViewModel()
        {
            SelectedDate_Picker = chosenDate;

            MessengerInstance.Register<long>(this, SumTimeChange);

            _collapseAllWorksCommand = new RelayCommand(CollapseAllWorks, (_) => true);
            _expandAllWorksCommand = new RelayCommand(ExpandAllWorks, (_) => true);
            MainWindow.GlobalPropertyChanged += HandleGlobalPropertyChanged;
        }

        #region Selected Date TabIndex
        void HandleGlobalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
            SetTabEnable(IsMainWindowEnabled);
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }
        #endregion

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
                SetField(ref _selectedDate_Picker, value);

                Generate_Week(_selectedDate_Picker);
                NotifyPropertyChanged("SelectedDate_Picker");
            }
        }


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
                if (value >= 0 && value < WeekTabs.Count) //Иногда он сюда попадает
                {
                    SetField(ref _selectedTabIndex, value);

                    chosenDate = WeekTabs[SelectedTabIndex].Date;
                    SelectedDate_Picker = chosenDate;
                    PlanningTime = Context.timeTableWork.Read_TimeByDate(chosenDate);
                    WeekTabs[SelectedTabIndex].Generate_WorksForDate();
                    SumTime = WeekTabs[SelectedTabIndex].SumTime;
                    NotifyPropertyChanged("SelectedTabIndex");
                }
            }
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
                    SelectedTabIndex = i; 
            }

            SumTime = WeekTabs[SelectedTabIndex].SumTime; // после подсчёта всей недели нужно в сумму положить текущий день
            MessengerInstance.Send<long>(SumTime);
        }
        #endregion

        #region Time

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
        #endregion //commands
    }
}
