using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model
{
    public class TaskAppeal : Task
    {
        public TaskAppeal() : base() { }
        public TaskAppeal(Task task) : base(task) { }
    }
}
