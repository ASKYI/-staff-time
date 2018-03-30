using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskCustomer : Task
    {
        public TaskCustomer() : base() {
            this.TaskName += "_customer";
        }
        public TaskCustomer(Task task) : base(task) {
            this.TaskName += "_customer";
        }
    }
}
