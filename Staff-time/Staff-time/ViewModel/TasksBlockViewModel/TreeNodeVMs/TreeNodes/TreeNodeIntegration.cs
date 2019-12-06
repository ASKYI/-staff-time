using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeIntegration : TreeNode
    {
        public TreeNodeIntegration() : base() { }
        public TreeNodeIntegration(Task task) : base(task) { }
        public TreeNodeIntegration(TreeNode treeNode) : base(treeNode) { }
    }
}
