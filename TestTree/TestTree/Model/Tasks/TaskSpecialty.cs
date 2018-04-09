using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskSpecialty : Task
    {
        public TaskSpecialty() : base() {}
        public TaskSpecialty(Task task) : base(task) {}
    }
}
