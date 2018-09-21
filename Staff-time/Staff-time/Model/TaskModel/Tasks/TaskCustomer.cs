using System;
using System.Collections.Generic;

namespace Staff_time.Model
{
    public class TaskCustomer : Task
    {
        public TaskCustomer() : base() { }
        public TaskCustomer(Task task) : base(task) { }
    }
}