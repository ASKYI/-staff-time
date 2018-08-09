using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    //Обертка над контестом WorkBlock для передачи через шаблон
    public class WorkInTab : ViewModelBase, INotifyPropertyChanged // todo довольно странный класс, не вижу в нём смысла
    {
        public WorkInTab(int workID)
        {
            WorkBlockContext = new WorkBlockControlViewModel(workID);
        }

        private WorkBlockControlViewModel _workBlockContext;
        public WorkBlockControlViewModel WorkBlockContext
        {
            get { return _workBlockContext; }
            set
            {
                SetField(ref _workBlockContext, value);
            }
        }

        #region INotifyPropertyChanged Member

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
