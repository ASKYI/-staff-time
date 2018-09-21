using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class TreeNodeСompany : TreeNode
    {
        public TreeNodeСompany() : base() { }
        public TreeNodeСompany(Task task) : base(task) { }
        public TreeNodeСompany(TreeNode treeNode) : base(treeNode) { }
    }
}
