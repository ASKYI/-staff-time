using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestTree.ViewModel
{
    public class TreeNodeCustomer : TreeNode
    {
        public TreeNodeCustomer() : base() { }
        public TreeNodeCustomer(TestTree.Model.Task task) : base(task) { }
        public TreeNodeCustomer(TreeNode node) : base(node) { }
    }
}