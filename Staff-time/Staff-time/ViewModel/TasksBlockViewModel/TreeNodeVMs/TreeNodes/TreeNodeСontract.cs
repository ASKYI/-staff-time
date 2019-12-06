using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeСontract : TreeNode
    {
        public TreeNodeСontract() : base() { }
        public TreeNodeСontract(Task task) : base(task) { }
        public TreeNodeСontract(TreeNode treeNode) : base(treeNode) { }
    }
}
