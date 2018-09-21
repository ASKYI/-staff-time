using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class TreeNodeCustomer : TreeNode
    {
        public TreeNodeCustomer() : base() { }
        public TreeNodeCustomer(Task task) : base(task) { }
        public TreeNodeCustomer(TreeNode treeNode) : base(treeNode) { }
    }
}
