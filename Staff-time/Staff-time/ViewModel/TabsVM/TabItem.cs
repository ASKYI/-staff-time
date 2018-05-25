using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;

namespace Staff_time.ViewModel
{
    public class TabItem : MainViewModel
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;
            Generate_WorksForDate();

            _editWorkCommand = new RelayCommand(EditWork, CanEditWork);
        }

        #region Tab Data
        public string TabName { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Works
        private ObservableCollection<WorkInTab> _worksInTab;
        public ObservableCollection<WorkInTab> WorksInTab
        {
            get { return _worksInTab; }
            set
            {
                SetField<ObservableCollection<WorkInTab>>(ref _worksInTab, value);
            }
        }

        public void Generate_WorksForDate()
        {
            SumTime = 0;

            List<Work> works = workWork.Read_WorksForDate(Date);
            WorksInTab = new ObservableCollection<WorkInTab>();
            foreach (Work w in works)
            {
                WorksInTab.Add(new WorkInTab(w));
                if (w.Minutes != null)  
                   SumTime += (int)w.Minutes;
            }

            MessengerInstance.Send<int>(SumTime);
        }
        #endregion

        #region Edit Work
        private readonly ICommand _editWorkCommand;
        public ICommand EditWorkCommand
        {
            get
            {
                return _editWorkCommand;
            }
        }
        private bool CanEditWork(object obj)
        {
            return true;
        }
        private void EditWork(object obj)
        {
            //WorksTable.Update_Work()
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
    }
}
