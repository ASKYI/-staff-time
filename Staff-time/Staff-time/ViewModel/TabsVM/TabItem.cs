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
    public class TabItem : ViewModelBase
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;
            _generate_WorksForDate();

            _editWorkCommand = new RelayCommand(EditWork, CanEditWork);
        }

        #region Tab Data
        public string TabName { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Works
        public ObservableCollection<WorkInTab> WorksInTab { get; set; }

        private void _generate_WorksForDate()
        {
            List<Work> works = WorksTable.Read_WorksForDate(Date);
            WorksInTab = new ObservableCollection<WorkInTab>();
            foreach (Work w in works)
            {
                WorksInTab.Add(new WorkInTab(w));
            } 
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
    }
}
