using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public partial class Work : ICloneable
    {
        //Есть ли более умный способ написания конструктора копирования?
        public Work(Work work)
        { 
            this.ID = work.ID;
            this.TaskID = work.TaskID;
            this.WorkName = work.WorkName;
            this.WorkTypeID = work.WorkTypeID;

            this.Minutes = work.Minutes;
            this.StartDate = work.StartDate;
            this.EndDate = work.EndDate;

            this.Task = work.Task;
            this.WorkType = work.WorkType;
            this.AttrValues = work.AttrValues;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}