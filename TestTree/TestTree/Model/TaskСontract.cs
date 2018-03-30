using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskContract : Task
    {
        public TaskContract() :base()
        {
            this.TaskName += "_contract";
        } 
        public TaskContract(Task task) : base(task)
        {
            this.TaskName += "_contract";
        }
    }
}
