using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public partial class Task
    {
        public Task(Task task) //IClonable? 
        {
            this.TaskName = task.TaskName;
            this.TaskTypeID = task.TaskTypeID;
            this.ID = task.ID;
            this.PropValues = task.PropValues;
            this.TaskType = task.TaskType;
            this.Works = task.Works;
        }
    }
}
