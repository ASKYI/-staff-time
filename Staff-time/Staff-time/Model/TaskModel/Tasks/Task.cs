using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public partial class Task : ICloneable
    {
        //Есть ли более умный способ написания конструктора копирования?
        public Task(Task task) 
        {
            this.ID = task.ID;
            this.TaskName = task.TaskName;
            this.TaskTypeID = task.TaskTypeID;
            this.ParentTaskID = task.ParentTaskID;
            this.Descriptions = task.Descriptions;

            this.TaskType = task.TaskType;
            this.Works = task.Works;
            this.PropValues = task.PropValues;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
