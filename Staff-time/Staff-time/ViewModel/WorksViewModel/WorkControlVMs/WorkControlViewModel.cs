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
using Staff_time.Helpers;

namespace Staff_time.ViewModel
{ 
    public class WorkControlViewModel : WorkControlViewModelBase
    {
        public WorkControlViewModel(Work work)
        {
            Work = work;
            OriginWork = (Work)work.Clone();
            IsEdititig = false;
        }

        public WorkControlViewModel() {}

        public void InitWorkControl(int workID)
        {
            if (WorksVM.Dictionary.ContainsKey(workID))
            {
                Work = (Work)WorksVM.Dictionary[workID].Work;
            }
        }

        public override void DeleteWork()
        {
            MessengerInstance.Send<MessageWorkObject>(new MessageWorkObject // todo очень грузно
                (WorkCommandEnum.Delete, Work, Work.StartDate));
        }
        public override void UpdateWork()
        {
            MessengerInstance.Send<MessageWorkObject>(new MessageWorkObject
                (WorkCommandEnum.Update, Work, Work.StartDate));
        }

        public override void CancelWork()
        {
            MessengerInstance.Send<MessageWorkObject>(new MessageWorkObject
                (WorkCommandEnum.Update, (Work)OriginWork.Clone(), Work.StartDate));
        }
    }
}
