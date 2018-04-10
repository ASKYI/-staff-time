using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TestTree.ViewModel
{
    public class WorkspaceViewModel : MainViewModel
    {
        public WorkspaceViewModel() : base() {
            WeekTabs = new ObservableCollection<TabItem>();
            Generate_Week(DateTime.Today);
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
    }
}
