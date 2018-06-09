using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeSpecialty : TreeNode
    {
        public TreeNodeSpecialty() : base() { }
        public TreeNodeSpecialty(Task task) : base(task) { }
        public TreeNodeSpecialty(TreeNode treeNode) : base(treeNode) { }
    }
}
