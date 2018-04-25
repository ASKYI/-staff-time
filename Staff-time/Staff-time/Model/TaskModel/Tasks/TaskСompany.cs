using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public class TaskCompany : Task
    {
        public TaskCompany() : base() { }
        public TaskCompany(Task task) : base(task) { }
    }
}