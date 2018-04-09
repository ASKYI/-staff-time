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
            Generate_Week();
        }

        private ObservableCollection<TabItemViewModel> _weekTabs;
        public ObservableCollection<TabItemViewModel> WeekTabs
        {
            get { return _weekTabs; }
            set
            {
                SetField(ref _weekTabs, value);
            }
        }
        private void Generate_Week()
        {
            WeekTabs = new ObservableCollection<TabItemViewModel>();
            WeekTabs.Add(new TabItemViewModel("Понедельник"));
            WeekTabs.Add(new TabItemViewModel("Вторник"));
            WeekTabs.Add(new TabItemViewModel("Среда"));
            WeekTabs.Add(new TabItemViewModel("Четверг"));
            WeekTabs.Add(new TabItemViewModel("Пятница"));
            WeekTabs.Add(new TabItemViewModel("Суббота"));
        }
    }
}
