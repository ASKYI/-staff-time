using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class TreeNodeSpecialty : TreeNode
    {
        public TreeNodeSpecialty() : base() { }
        public TreeNodeSpecialty(Task task) : base(task) { }
        public TreeNodeSpecialty(TreeNode treeNode) : base(treeNode) { }
    }
}
