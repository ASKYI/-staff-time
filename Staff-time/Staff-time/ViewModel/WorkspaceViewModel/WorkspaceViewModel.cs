using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public class WorkspaceViewModel : MainViewModel
    {
        public WorkspaceViewModel()
        {
            SelectedDate_Picker = chosenDate;
            
            MessengerInstance.Register<int>(this, SumTimeChange);
        }

        #region Selected Date TabIndex

        private DateTime _selectedDate_Picker;
        public DateTime SelectedDate_Picker
        {
            get { return _selectedDate_Picker; }
            set
            {
                SetField(ref _selectedDate_Picker, value);

                Generate_Week(SelectedDate_Picker);
            }
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

                    //update !!!
                    chosenDate = WeekTabs[SelectedTabIndex].Date;
                    WeekTabs[SelectedTabIndex].Generate_WorksForDate();
                    SumTime = WeekTabs[SelectedTabIndex].SumTime; 
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

            int dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = (dayOfWeek + 6) % 7; //!!!
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

        private int _sumTime;
        public int SumTime
        {
            get { return _sumTime; }
            set
            {
                SetField(ref _sumTime, value);

                SumHours = value / 60;
                SumMinutes = value % 60;
            }
        }

        private int _sumHours;
        public int SumHours
        {
            get { return _sumHours; }
            set
            {
                SetField(ref _sumHours, value);
            }
        }

        private int _sumMinutes;
        public int SumMinutes
        {
            get { return _sumMinutes; }
            set
            {
                SetField(ref _sumMinutes, value);
            }
        }

        public void SumTimeChange(int newSumTime) //Messenger
        {
            SumTime = newSumTime;
        }

        #endregion
    }
}
