using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using StaffTime.Model;
using GalaSoft.MvvmLight;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace StaffTime.ViewModel
{
    public class BaseViewModel : ViewModelBase, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

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
