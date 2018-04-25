using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeCustomer : TreeNode
    {
        public TreeNodeCustomer() : base() { }
        public TreeNodeCustomer(Task task) : base(task) { }
        public TreeNodeCustomer(TreeNode treeNode) : base(treeNode) { }
    }
}
