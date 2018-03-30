using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskCompany : Task
    {
        public TaskCompany() :base()
        {
            this.TaskName += "_company";
        }
        public TaskCompany(Task task) : base(task)
        {
            this.TaskName += "_company";
        }
    }
}
