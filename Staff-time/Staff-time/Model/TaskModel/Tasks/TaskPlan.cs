using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model
{
    public class TaskPlan : Task
    {
        public TaskPlan() : base() { }
        public TaskPlan(Task task) : base(task) { }
    }
}
