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
            _generate_AttrValues();
        }

        private void _generate_AttrValues()
        {
            AttrValues = new ObservableCollection<AttrValueExtended>();
            List<AttrValue> values = attrWork.Read_AttrValues_ForWork(Work);
            foreach(var v in values)
            {
                AttrValues.Add(new AttrValueExtended(IsEdititig, v));
            }
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

            attrWork.Update_AttrValues(values);
        }
    }
}
