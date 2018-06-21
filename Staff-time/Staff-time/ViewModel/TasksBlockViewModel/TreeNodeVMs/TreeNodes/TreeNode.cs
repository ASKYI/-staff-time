using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class TreeNode : MainViewModel
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
            FullPath = treeNode.FullPath;
        }

        private Task _task;
        public Task Task {
            get { return _task; }
            set
            {
                SetField(ref _task, value);
                FullPath = TasksVM.generate_PathForTask(_task.ID);
            }
        }
        public List<string> FullPath { get; set; }

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
