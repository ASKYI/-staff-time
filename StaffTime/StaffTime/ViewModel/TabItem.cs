using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Runtime.CompilerServices;

namespace StaffTime.ViewModel
{
    public class TabItem : ViewModelBase, INotifyPropertyChanged
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;
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

        public ObservableCollection<StaffTime.Model.Work> Works;

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
