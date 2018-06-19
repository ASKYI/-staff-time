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
            IsEdititig = false;
        }

        public WorkControlViewModel() {}

        public void InitWorkControl(int workID)
        {
            if (WorksVM.Dictionary.ContainsKey(workID))
            {
                Work = WorksVM.Dictionary[workID].Work;
            }
        }

        public override void DeleteWork()
        {
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
                (WorkCommandEnum.Delete, Work));
        }
        public override void UpdateWork()
        {
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
                (WorkCommandEnum.Update, Work));
        }
    }
}
