using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeClassification : TreeNode
    {
        public TreeNodeClassification() : base() { }
        public TreeNodeClassification(Task task) : base(task) { }
        public TreeNodeClassification(TreeNode treeNode) : base(treeNode) { }
    }
}
