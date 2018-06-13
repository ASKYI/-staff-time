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

        public WorkControlViewModel()
        {
            //_generate_AttrValues();
        }

        public void InitWorkControl(int workID)
        {
            if (WorksVM.Dictionary.ContainsKey(workID))
            {
                Work = WorksVM.Dictionary[workID].Work;
            }
        }

        public override void DeleteWork()
        {
            //Context.workWork.Delete_Work(Work.ID);
        }
        public override void UpdateWork()
        {
            //Context.workWork.Update_Work(Work);

            //List<AttrValue> values = new List<AttrValue>();
            //foreach (var v in AttrValues)
            //    values.Add(v.AttrValue);

            //Context.attrWork.Update_AttrValues_ForWork(values);
        }
    }
}
