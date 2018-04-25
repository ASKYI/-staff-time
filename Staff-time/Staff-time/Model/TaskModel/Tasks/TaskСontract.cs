using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public class TaskContract : Task
    {
        public TaskContract() : base() { }
        public TaskContract(Task task) : base(task) { }
    }
}
