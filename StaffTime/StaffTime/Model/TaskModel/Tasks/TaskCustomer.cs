using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffTime.Model
{
    public class TaskCustomer : Task
    {
        public TaskCustomer() : base() {}
        public TaskCustomer(Task task) : base(task) {}
    }
}
