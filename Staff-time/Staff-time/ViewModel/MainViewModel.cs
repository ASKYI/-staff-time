using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using Staff_time.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private static void _initSharedStatics()
        {
            Context.Init();
            TasksVM.Init();
            WorksVM.Init();
        } 

        public MainViewModel()
        {
            _initSharedStatics();
            chosenUser = Context.GetTestUser();
        }

        #region Chosen statics (User, Date)

        protected static User chosenUser { get; set; }
        protected static DateTime chosenDate { get; set; }

        #endregion

        #region INotifyPropertyChanged Member

        public bool SetField<T>(ref T field, T value,
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
