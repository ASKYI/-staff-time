using Staff_time.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.ViewModel.TasksBlockViewModel.TaskPropVMs
{
    class TaskPropViewModel : TaskPropViewModelBase
    {
        public TaskPropViewModel(Task task)
        {
            CurTask = task;
            //OriginWork = (Work)work.Clone();
        }

        public TaskPropViewModel() { }

        public void InitTaskControl(int taskID)
        {
            if (TasksVM.Dictionary.ContainsKey(taskID))
                CurTask = (Task)TasksVM.Dictionary[taskID].Task;
        }

        //public override void DeleteWork()
        //{
        //    MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work> // todo очень грузно
        //        (WorkCommandEnum.Delete, Work));
        //}
        //public override void UpdateWork()
        //{
        //    MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
        //        (WorkCommandEnum.Update, Work));
        //}

        //public override void CancelWork()
        //{
        //    MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
        //        (WorkCommandEnum.Update, (Work)OriginWork.Clone()));
        //}
    }
}
