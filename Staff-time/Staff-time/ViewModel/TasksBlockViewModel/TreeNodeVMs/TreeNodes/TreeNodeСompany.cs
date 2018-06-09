using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeСompany : TreeNode
    {
        public TreeNodeСompany() : base() { }
        public TreeNodeСompany(Task task) : base(task) { }
        public TreeNodeСompany(TreeNode treeNode) : base(treeNode) { }
    }
}
