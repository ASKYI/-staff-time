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
            WorksTable.Read_Works();
            _generate_Week(DateTime.Today);

            SelectedDate = null;
            _changeDateCommand = new RelayCommand(ChangeDate, CanChangeDate);
        }

        #region Week
        private static readonly string[] DaysOfWeek = new string[7] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

        public static ObservableCollection<TabItem> WeekTabs { get; set; }
        private static void _generate_Week(DateTime date)
        {
            WeekTabs = new ObservableCollection<TabItem>();

            int dayOfWeek = (int)date.DayOfWeek;
            dayOfWeek = (dayOfWeek + 6) % 7;
            DateTime startDay = date.AddDays(-dayOfWeek);
            for (int i = 0; i < 7; ++i)
            {
                WeekTabs.Add(new TabItem(DaysOfWeek[i], startDay.AddDays(i)));
            }
        }
        #endregion

        #region Select Date
        public Nullable<DateTime> SelectedDate;
        private readonly ICommand _changeDateCommand;
        public ICommand ChangeDateCommand
        {
            get
            {
                return _changeDateCommand;
            }
        }
        private bool CanChangeDate(object obj)
        {
            return SelectedDate != null;
        }
        private void ChangeDate(object obj)
        {
            _generate_Week((DateTime)SelectedDate);
        }
        #endregion
    }
}
