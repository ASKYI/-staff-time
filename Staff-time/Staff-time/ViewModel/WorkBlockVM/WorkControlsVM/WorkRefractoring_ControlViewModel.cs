using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public class WorkRefractoring_ControlViewModel : WorkControlViewModelBase
    {
        public WorkRefractoring_ControlViewModel(Work work)
        {
            Work = (WorkRefractoring)work;
        }
        private new WorkRefractoring _work;
        public new WorkRefractoring Work
        {
            get { return _work; }
            set
            {
                SetField<WorkRefractoring>(ref _work, value);
            }
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
