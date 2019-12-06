using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public class TaskClassification : Task
    {
        public TaskClassification() : base() { }
        public TaskClassification(Task task) : base(task) { }
    }
}