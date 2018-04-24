using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffTime.ViewModel
{
    public class TreeNodeСontract : TreeNode
    {
        public TreeNodeСontract() : base() { }
        public TreeNodeСontract(StaffTime.Model.Task task) : base(task) { }
        public TreeNodeСontract(TreeNode treeNode) : base(treeNode) { }
    }
}
