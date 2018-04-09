using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using TestTree.Model;

namespace TestTree.ViewModel
{
    public class TreeNode
    {
        public TreeNode()
        {
            TreeNodes = new ObservableCollection<TreeNode>();
        }
        public TreeNode(Task task) : this()
        {
            Task = task;
        }
        public TreeNode(TreeNode node) : this()
        {
            Task = node.Task;
            ParentNode = node.ParentNode;
            TreeNodes = node.TreeNodes;
            TreeNodeCustomers = node.TreeNodeCustomers;
            Path = node.Path;
        }

        public Task Task { get; set; }
        public string Path { get; set; }

        #region Nodes
        public TreeNode ParentNode { get; set; }
        public ObservableCollection<TreeNode> TreeNodes { get; set; }
        public ObservableCollection<TreeNodeCustomer> TreeNodeCustomers { get; set; }
        #endregion

    }
}
