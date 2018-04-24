using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StaffTime.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using System.Runtime.CompilerServices;

namespace StaffTime.ViewModel
{
    public class TabItem : ViewModelBase
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;
        }

        #region Tab Data
        public string TabName { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Works
        public ObservableCollection<Work> Works { get; set; }

        private void _generate_WorksForDate()
        {
            
        }
        #endregion
    }
}
