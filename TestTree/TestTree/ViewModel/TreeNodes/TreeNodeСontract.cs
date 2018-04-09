using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.ViewModel
{
    public class TreeNodeСontract : TreeNode
    {
        public TreeNodeСontract() : base() { }
        public TreeNodeСontract(TestTree.Model.Task task) : base(task) { }
        public TreeNodeСontract(TreeNode treeNode) : base(treeNode) { }
    }
}
