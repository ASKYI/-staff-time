using System;
using System.Collections.Generic;

namespace Staff_time.Model
{
    public partial class Task //done: ICloneable не нужен, исправлен конструктор копирования (недостаточная глубина)
    {
        public Task(Task task) 
        {
            this.ID = task.ID;
            this.TaskName = task.TaskName;
            this.TaskTypeID = task.TaskTypeID;
            this.ParentTaskID = task.ParentTaskID;
            this.Descriptions = task.Descriptions;
            this.IndexNumber = task.IndexNumber;
            this.TaskType = task.TaskType;

            this.Works = new List<Work>();
            foreach(var t in task.Works)
                this.Works.Add(t);
            this.PropValues = new List<PropValue>();
            foreach (var pv in task.PropValues)
                this.PropValues.Add(pv);
        }
    }
}
