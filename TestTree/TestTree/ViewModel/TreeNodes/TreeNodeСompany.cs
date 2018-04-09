using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.ViewModel
{
    public class TreeNodeСompany : TreeNode
    {
        public TreeNodeСompany() : base() { }
        public TreeNodeСompany(TestTree.Model.Task task) : base(task) { }
        public TreeNodeСompany(TreeNode treeNode) : base(treeNode) { }
    }
}
