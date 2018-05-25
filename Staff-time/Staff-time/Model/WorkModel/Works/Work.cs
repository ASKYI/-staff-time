using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public partial class Work
    {
        public Work(Work work)
        {
            this.AttrValues = work.AttrValues;
            this.StartDate = work.StartDate;
            this.ID = work.ID;
            this.Task = work.Task;
            this.TaskID = work.TaskID;
            this.WorkName = work.WorkName;
            this.WorkType = work.WorkType;
            this.WorkTypeID = work.WorkTypeID;
            this.Minutes = work.Minutes;
            this.StartDate = work.StartDate;
            this.EndDate = work.EndDate;
        }
    }
}