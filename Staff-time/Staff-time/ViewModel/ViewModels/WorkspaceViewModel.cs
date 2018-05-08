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
            SelectedDate = DateTime.Today.AddDays(-7);

            _changeDateCommand = new RelayCommand(ChangeDate, CanChangeDate);
        }

        #region Week
        private static readonly string[] DaysOfWeek = new string[7] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

        public static ObservableCollection<TabItem> WeekTabs { get; set; }
        public static void Generate_Week(DateTime date)
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

        #region Selected Data Picker Date
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetField<DateTime>(ref _selectedDate, value);
                Generate_Week(SelectedDate);
            }
        }
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

        #region Selected Tab
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex;}
            set
            {
                SetField<int>(ref _selectedIndex, value);

                CurDate = WeekTabs[_selectedIndex].Date;
                selectedIndexStatic = value;
            }
        }
        public static int selectedIndexStatic;
        #endregion
    }
}
