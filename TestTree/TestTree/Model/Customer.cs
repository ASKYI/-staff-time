﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class Customer : Task
    {
        public Customer() : base() { }
        public Customer(Task task) : base(task) { }
    }
}
