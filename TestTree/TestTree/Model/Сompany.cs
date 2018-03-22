using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class Company : Task
    {
        public Company() :base() { }
        public Company(Task task) : base(task) { }
    }
}
