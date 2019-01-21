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
    public class WorkspaceViewModel : MainViewModel
    {
        public WorkspaceViewModel()
        {
            SelectedDate_Picker = chosenDate;

            MessengerInstance.Register<long>(this, SumTimeChange);

            _collapseAllWorksCommand = new RelayCommand(CollapseAllWorks, (_) => true);
            _expandAllWorksCommand = new RelayCommand(ExpandAllWorks, (_) => true);
        }

        #region Selected Date TabIndex

        private DateTime _selectedDate_Picker; // todo picker - это графический элемент, вьюмодель о нём не должна знать
        public DateTime SelectedDate_Picker
        {
            get { return _selectedDate_Picker; }
            set
            {
                //if (_selectedDate_Picker == value)
                //    return;
                SetField(ref _selectedDate_Picker, value);

                Generate_Week(_selectedDate_Picker);
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

                    chosenDate = WeekTabs[SelectedTabIndex].Date;
                    WeekTabs[SelectedTabIndex].Generate_WorksForDate();
                    SumTime = WeekTabs[SelectedTabIndex].SumTime;

                    //if (IsEditing())
                    //{
                    //    var dialogResult = System.Windows.MessageBox.Show("На текущий день есть несохраненные изменения в работе. Сохранить?", 
                    //        "Подтверждение",MessageBoxButton.YesNo, MessageBoxImage.Question);

                    // if (dialogResult == MessageBoxResult.No)
                    base.CancelEditing();
                    //else
                    //    base.ApplyChanges();

                    //}

                    //base.CancelEditing(); //поменяла на принятие изменения
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

            int dayOfWeek = (int)date.DayOfWeek - 1; //День недели с понедельника
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

            SumTime = WeekTabs[SelectedTabIndex].SumTime; // после подсчёта всей недели нужно в сумму положить текущий день
            MessengerInstance.Send<long>(SumTime);
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
            base.CancelEditing();

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
            base.CancelEditing();

            WorksVM.ExpandAll();
        }
        #endregion //commands
    }
}
