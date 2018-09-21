using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class TreeNodeСontract : TreeNode
    {
        public TreeNodeСontract() : base() { }
        public TreeNodeСontract(Task task) : base(task) { }
        public TreeNodeСontract(TreeNode treeNode) : base(treeNode) { }
    }
}
