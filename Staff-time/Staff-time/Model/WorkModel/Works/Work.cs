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
            this.Description = work.Description;
            this.WorkTypeID = work.WorkTypeID;

            this.Minutes = work.Minutes;
            this.StartDate = work.StartDate;
            this.UserID = work.UserID;
            //this.StartTime = work.StartTime;

            this.Task = work.Task;
            this.WorkType = work.WorkType;

            this.AttrValues = new List<AttrValue>();
            foreach (var a in work.AttrValues)
            {
                this.AttrValues.Add(a);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone(); // todo смотреть аналогично у Task
        }
    }
}