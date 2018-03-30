using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskDirection : Task
    {
        public TaskDirection() : base()
        { 
            this.TaskName += "_direction";
        }
        public TaskDirection(Task task) : base(task)
        { 
            this.TaskName += "_direction";
        }
    }
}
