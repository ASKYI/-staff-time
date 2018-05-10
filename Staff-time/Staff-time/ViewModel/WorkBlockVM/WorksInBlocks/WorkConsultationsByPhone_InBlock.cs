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
    public class WorkConsultationsByPhone_InBlock : WorkInBlock
    {
       public WorkConsultationsByPhone_InBlock(Model.Work work)
       {
            WorkControlDataContext = new WorkConsultationsByPhone_ControlViewModel(work);
       }
    }
}
