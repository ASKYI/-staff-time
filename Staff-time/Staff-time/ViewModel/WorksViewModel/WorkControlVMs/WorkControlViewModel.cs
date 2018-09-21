using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.Collections.ObjectModel;

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
            var toSend = new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, Work); //done: разделено
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(toSend);
        }
        public override void UpdateWork()
        {
            var toSend = new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Update, Work);
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(toSend);
        }
    }
}
