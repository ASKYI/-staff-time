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

        private ObservableCollection<AttrValue> _attrValues;
        public ObservableCollection<AttrValue> AttrValues
        {
            get { return _attrValues; }
            set
            {
                SetField<ObservableCollection<AttrValue>>(ref _attrValues, value);
            }
        }
        private void _generate_AttrValues()
        {
            List<AttrValue> valuesDB = new List<AttrValue>();
            valuesDB =    attrWork.Read_AttrValues_ForWork(Work);
            AttrValues = new ObservableCollection<AttrValue>(valuesDB);
            //AttrValues = new ObservableCollection<AttrValue>(attrWork.Read_AttrValues_ForWork(Work));
        }

        public override void DeleteWork()
        {
            workWork.Delete_Work(Work.ID);
        }
        public override void UpdateWork()
        {
            workWork.Update_Work(Work);
            attrWork.Update_AttrValues(this.AttrValues.ToList());
        }
    }
}
