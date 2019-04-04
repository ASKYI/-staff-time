using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeAppeal : TreeNode
    {
        public TreeNodeAppeal() : base() { }
        public TreeNodeAppeal(Task task) : base(task) { }
        public TreeNodeAppeal(TreeNode treeNode) : base(treeNode) { }
    }
}
