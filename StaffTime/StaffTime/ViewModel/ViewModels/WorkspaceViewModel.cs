using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace StaffTime.ViewModel
{
    public class WorkspaceViewModel : MainViewModel
    {
        public WorkspaceViewModel() : base() {
            WeekTabs = new ObservableCollection<TabItem>();
            Generate_Week(DateTime.Today);

            _changeDateCommand = new RelayCommand(ChangeDate, CanChangeDate);
        }

        #region Week
        public ObservableCollection<TabItem> WeekTabs { get; set; }

        private string[] DaysOfWeek = new string[6] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

        private void Generate_Week(DateTime date)
        {
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
            Generate_Week((DateTime)SelectedDate);
        }
        #endregion
    }
}
