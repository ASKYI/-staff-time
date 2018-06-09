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
    //WorkType: WorkError
    public class WorkError_InBlock : WorkInBlock
    {
        public WorkError_InBlock(Work work) 
        {
            WorkControlDataContext = new WorkError_ControlViewModel(work);
        }
    }
}
