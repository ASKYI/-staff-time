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
            SelectedDatePicker = DateTime.Today.AddDays(0);
            
            MessengerInstance.Register<int>(this, SumTimeChange);

            _updateCommand = new RelayCommand(Update, CanUpdate);
        }

        #region Week
        private static readonly string[] DaysOfWeek = new string[7] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

        private ObservableCollection<TabItem> _weekTabs;
        public ObservableCollection<TabItem> WeekTabs
        {
            get { return _weekTabs; }
            set
            {
                SetField<ObservableCollection<TabItem>>(ref _weekTabs, value);
            }
        }

        public void Generate_Week(DateTime date)
        {
            WeekTabs = new ObservableCollection<TabItem>();

            int dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = (dayOfWeek + 6) % 7;
            DateTime startDay = date.AddDays(-dayOfWeek);

            for (int i = 0; i < 7; ++i)
            {
                WeekTabs.Add(new TabItem(DaysOfWeek[i], startDay.AddDays(i)));
                if (startDay.AddDays(i).Date == date.Date)
                {
                    SelectedIndex = i;
                }
            }
        }
        #endregion
        #region Selected
        private DateTime _selectedDatePicker;
        public DateTime SelectedDatePicker
        {
            get { return _selectedDatePicker; }
            set
            {
                SetField<DateTime>(ref _selectedDatePicker, value);
                Generate_Week(SelectedDatePicker);
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value >= 0 && value < WeekTabs.Count) //Иногда он сюда попадает
                {
                    SetField<int>(ref _selectedIndex, value);
                    curDate = WeekTabs[value].Date;
                    WeekTabs[value].Generate_WorksForDate();
                    SumTime = WeekTabs[value].SumTime;
                }
            }
        }
        #endregion
        #region Update All
        private readonly RelayCommand _updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return _updateCommand; }
        }
        private bool CanUpdate(object obj)
        {
            return true;
        }
        private void Update(object obj)
        {
            Generate_Week(DateTime.Now.Date);
            SelectedIndex = 0;
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
        public void SumTimeChange(int newSumTime)
        {
            SumTime = newSumTime;
        }
        #endregion
    }
}
