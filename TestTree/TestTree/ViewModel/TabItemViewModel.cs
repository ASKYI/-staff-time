using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Runtime.CompilerServices;

namespace TestTree.ViewModel
{
    public class TabItemViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public TabItemViewModel(string tabName_DayOfWeek)
        {
            TabName = tabName_DayOfWeek;
        }

        private string _tabName;
        public string TabName
        {
            get { return _tabName; }
            set
            {
                SetField(ref _tabName, value);
            }
        }
        private DateTime _date; 
        public DateTime Date
        {
            get { return _date; }
            set
            {
                SetField(ref _date, value);
            }
        }

        public ObservableCollection<TestTree.Model.Work> Works;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
