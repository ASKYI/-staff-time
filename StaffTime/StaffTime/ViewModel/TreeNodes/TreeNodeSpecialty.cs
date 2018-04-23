using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffTime.ViewModel
{
    public class TreeNodeSpecialty : TreeNode
    {
        public TreeNodeSpecialty() : base() { }
        public TreeNodeSpecialty(StaffTime.Model.Task task) : base(task) { }
        public TreeNodeSpecialty(TreeNode treeNode) : base(treeNode) { }
    }
}
