using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Windows;

namespace Staff_time.ViewModel
{
    public class WorkControlViewModel : DependencyObject, INotifyPropertyChanged
    {
        private string _a = "OMG";
        public string a
        {
            get { return _a; }
            set
            {
                _a = value;
                RaisePropertyChanged("a");
            }
        }
        private Work _work;
        public Work Work
        {
            get { return _work; }
            set
            {
                //SetField(ref _work, value);
            }
        }
        public WorkControlViewModel(Work work)
        {
            Work = work;
            a = "OMG WORKS!!!!"; 
        }

        public WorkControlViewModel() { }

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

        #endregion
    }
}
