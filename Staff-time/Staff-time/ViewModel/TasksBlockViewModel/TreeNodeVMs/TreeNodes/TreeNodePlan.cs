using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodePlan : TreeNode
    {
        public TreeNodePlan() : base() { }
        public TreeNodePlan(Task task) : base(task) { }
        public TreeNodePlan(TreeNode treeNode) : base(treeNode) { }
    }
}
