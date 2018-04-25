using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        }

        #region Tab Data
        public string TabName { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Works
        public ObservableCollection<Work> Works { get; set; }

        private void _generate_WorksForDate()
        {
            Works = new ObservableCollection<Work>(WorksTable.Read_WorksForDate(Date));
        }
        #endregion
    }
}
