﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskDirection : Task
    {
        public TaskDirection() : base() {}
        public TaskDirection(Task task) : base(task) {}
    }
}
