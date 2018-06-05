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
            _generate_AttrValues();
        }

        public override void DeleteWork()
        {
            workWork.Delete_Work(Work.ID);
        }
        public override void UpdateWork()
        {
            workWork.Update_Work(Work);

            List<AttrValue> values = new List<AttrValue>();
            foreach (var v in AttrValues)
                values.Add(v.AttrValue);

            attrWork.Update_AttrValues_ForWork(values);
        }
    }
}
