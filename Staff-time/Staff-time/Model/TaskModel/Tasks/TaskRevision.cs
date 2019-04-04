using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model
{
    public class TaskRevision : Task
    {
        public TaskRevision() : base() { }
        public TaskRevision(Task task) : base(task) { }
    }
}
