using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeRevision : TreeNode
    {
        public TreeNodeRevision() : base() { }
        public TreeNodeRevision(Task task) : base(task) { }
        public TreeNodeRevision(TreeNode treeNode) : base(treeNode) { }
    }
}
