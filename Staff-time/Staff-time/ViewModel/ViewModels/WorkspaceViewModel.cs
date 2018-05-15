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
            Generate_Week(MainViewModel.CurDate);
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
                    CurDate = WeekTabs[value].Date;
                }
            }
        }
        #endregion
    }
}
