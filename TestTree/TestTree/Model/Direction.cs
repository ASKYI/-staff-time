using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class Direction : Task
    {
        public Direction() : base() { }
        public Direction(Task task) : base(task) { }
    }
}
