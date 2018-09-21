using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class WorkspaceViewModel : MainViewModel
    {
        public WorkspaceViewModel()
        {
            SelectedDatePickerTime = chosenDate;
            
            MessengerInstance.Register<long>(this, SumTimeChange);
        }

        #region Selected Date TabIndex

        private DateTime _selectedDatePickerTime; //done: переименование
        public DateTime SelectedDatePickerTime
        {
            get { return _selectedDatePickerTime; }
            set
            {
                SetField(ref _selectedDatePickerTime, value);

                Generate_Week(_selectedDatePickerTime);
            }
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
                    WeekTabs[SelectedTabIndex].Generate_WorksForDate();
                    SumTime = WeekTabs[SelectedTabIndex].SumTime;
                    base.CancelEditing();
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
            WeekTabs = new ObservableCollection<TabItem>();

            int dayOfWeek = (int)date.DayOfWeek - 1 ; //День недели с понедельника
            if (dayOfWeek == -1)
                dayOfWeek = 6;
            DateTime startDay = date.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; ++i)
            {
                DateTime cur = startDay.AddDays(i);
                WeekTabs.Add(new TabItem(DaysOfWeek[i], cur));

                if (cur.Date == date.Date)
                {
                    SelectedTabIndex = i;
                }
            }
        }
        #endregion
        
        #region Time

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
            }
        }

        private long _sumMinutes;
        public long SumMinutes
        {
            get { return _sumMinutes; }
            set
            {
                SetField(ref _sumMinutes, value);
            }
        }

        public void SumTimeChange(long newSumTime)
        {
            SumTime = newSumTime;
        }

        #endregion
    }
}
