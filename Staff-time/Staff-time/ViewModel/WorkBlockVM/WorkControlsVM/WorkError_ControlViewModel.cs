﻿using System;
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
    //WorkType: WorkError
    public class WorkError_ControlViewModel : WorkControlViewModelBase
    {
        public WorkError_ControlViewModel(Work work)
        {
            Work = (WorkError)work;
        }

        private new WorkError _work;
        public new WorkError Work
        {
            get { return _work; }
            set
            {
                SetField<WorkError>(ref _work, value);
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
