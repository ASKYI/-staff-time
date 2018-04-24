using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffTime.ViewModel
{
    public class TreeNodeСompany : TreeNode
    {
        public TreeNodeСompany() : base() { }
        public TreeNodeСompany(StaffTime.Model.Task task) : base(task) { }
        public TreeNodeСompany(TreeNode treeNode) : base(treeNode) { }
    }
}
