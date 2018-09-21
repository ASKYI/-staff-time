using System;
using System.Collections.Generic;

namespace Staff_time.Model
{
    public partial class Work //done: ICloneable не нужен
    {
        public Work(Work work)
        { 
            this.ID = work.ID;
            this.TaskID = work.TaskID;
            this.WorkName = work.WorkName;
            this.WorkTypeID = work.WorkTypeID;

            this.Minutes = work.Minutes;
            this.StartDate = work.StartDate;
            this.StartTime = work.StartTime;

            this.Task = work.Task;
            this.WorkType = work.WorkType;

            this.AttrValues = new List<AttrValue>();
            foreach (var a in work.AttrValues)
            {
                this.AttrValues.Add(a);
            }
        }
    }
}