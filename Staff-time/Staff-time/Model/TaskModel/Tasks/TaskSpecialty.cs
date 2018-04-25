using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public class TaskSpecialty : Task
    {
        public TaskSpecialty() : base() { }
        public TaskSpecialty(Task task) : base(task) { }
    }
}