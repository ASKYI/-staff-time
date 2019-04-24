using System;
using System.Collections.Generic;

using Staff_time.Model;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Runtime.CompilerServices;
using Staff_time.Model.UserModel;

namespace Staff_time.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private static void _initSharedStatics()
        {
            Context.Init();
            TasksVM.InitFullTree();
            TasksVM.InitFave();
            WorksVM.Init();
        }

        public MainViewModel(bool IsLoad)
        {
            _initSharedStatics();
            _initChosen();
        }
        public MainViewModel()
        {
        }

        #region Chosen statics (User, Date)

        public static User chosenUser { get; set; }
        protected static DateTime chosenDate { get; set; }

        public static bool init_tracker = false;
        private static void _initChosen()
        {
            if (init_tracker)
                return;
            init_tracker = true;

            chosenUser = GlobalInfo.CurrentUser;
            chosenDate = DateTime.Now.Date;
        }

        #endregion

        public void CancelEditing()
        {
            MessengerInstance.Send<string>("Cancel");
        }

        public void ApplyChanges()
        {
            MessengerInstance.Send<string>("Apply");
        }

        protected static View.IDialogView dialog; //Ага, ViewModel видит View...

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
