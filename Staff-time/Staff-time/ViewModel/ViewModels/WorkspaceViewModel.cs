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
        public WorkspaceViewModel() : base()
        {
            SelectedDatePicker = DateTime.Today.AddDays(0);

            MessengerInstance.Register<NotificationMessage>(this, Notify);
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
        public void Notify(NotificationMessage notificationMessage)
        {
            WeekTabs[SelectedIndex].Generate_WorksForDate();
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
                    SumTime = WeekTabs[value].SumTime;
                }
            }
        }
        #endregion
        #region Update
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

        private int _sumTime;
        public int SumTime
        {
            get { return _sumTime; }
            set
            {
                SetField(ref _sumTime, value);
            }
        }
        public void SumTimeChange(int newSumTime)
        {
            SumTime = newSumTime;
        }
    }
}
