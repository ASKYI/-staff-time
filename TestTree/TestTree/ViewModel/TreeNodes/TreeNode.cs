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
        public TreeNode(TreeNode treeNode) : this()
        {
            Task = treeNode.Task;
            ParentNode = treeNode.ParentNode;
            TreeNodes = treeNode.TreeNodes;
            Path = treeNode.Path;
        }

        public Task Task { get; set; }
        public string Path { get; set; }

        #region Nodes
        public TreeNode ParentNode { get; set; }
        public ObservableCollection<TreeNode> TreeNodes { get; set; }
        public void AddChild(TreeNode treeNode)
        {
            TreeNodes.Add(treeNode);
        }
        #endregion

    }
}
