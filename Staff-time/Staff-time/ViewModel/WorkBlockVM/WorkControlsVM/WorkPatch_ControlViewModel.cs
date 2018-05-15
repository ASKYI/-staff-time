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
    //WorkType: WorkPatch
    public class WorkPatch_ControlViewModel : WorkControlViewModelBase
    {
        public WorkPatch_ControlViewModel(Work work)
        {
            Work = (WorkPatch)work;
        }

        private WorkPatch _work;
        public new WorkPatch Work
        {
            get { return _work; }
            set
            {
                SetField(ref _work, value);
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
