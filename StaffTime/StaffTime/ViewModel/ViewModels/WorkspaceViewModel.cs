using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StaffTime.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace StaffTime.ViewModel
{
    public class WorkspaceViewModel : MainViewModel
    {
        public WorkspaceViewModel() : base() {
            _generate_Week(DateTime.Today);

            SelectedDate = null;
            _changeDateCommand = new RelayCommand(ChangeDate, CanChangeDate);
        }

        #region Week
        private static readonly string[] DaysOfWeek = new string[6] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

        public static ObservableCollection<TabItem> WeekTabs { get; set; }
        private static void _generate_Week(DateTime date)
        {
            WeekTabs = new ObservableCollection<TabItem>();

            int dayOfWeek = (int)date.DayOfWeek;
            DateTime startDay = date.AddDays(-dayOfWeek + 1);
            for (int i = 0; i < 6; ++i)
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
