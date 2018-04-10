using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.ViewModel
{
    public class TreeNodeSpecialty : TreeNode
    {
        public TreeNodeSpecialty() : base() { }
        public TreeNodeSpecialty(TestTree.Model.Task task) : base(task) { }
        public TreeNodeSpecialty(TreeNode treeNode) : base(treeNode) { }
    }
}
