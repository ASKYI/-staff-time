using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Staff_time.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{ 
    public class WorkControlViewModel : WorkControlViewModelBase
    {
        public WorkControlViewModel(Work work)
        {
            Work = work;
        }

        public override void DeleteWork()
        {
            workWork.Delete_Work(Work.ID);
        }
        public override void UpdateWork()
        {
            workWork.Update_Work(Work);
        }
    }
}
