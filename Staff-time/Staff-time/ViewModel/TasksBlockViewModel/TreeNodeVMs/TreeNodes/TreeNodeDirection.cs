using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeDirection : TreeNode
    {
        public TreeNodeDirection() : base() { }
        public TreeNodeDirection(Task task) : base(task) { }
        public TreeNodeDirection(TreeNode treeNode) : base(treeNode) { }
    }
}
