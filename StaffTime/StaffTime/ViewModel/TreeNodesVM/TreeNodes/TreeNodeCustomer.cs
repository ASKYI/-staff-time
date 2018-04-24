using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StaffTime.ViewModel
{
    public class TreeNodeCustomer : TreeNode
    {
        public TreeNodeCustomer() : base() { }
        public TreeNodeCustomer(StaffTime.Model.Task task) : base(task) { }
        public TreeNodeCustomer(TreeNode treeNode) : base(treeNode) { }
    }
}