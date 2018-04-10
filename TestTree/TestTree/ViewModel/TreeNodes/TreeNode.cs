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
            TreeNodeCustomers = new ObservableCollection<TreeNodeCustomer>();
            TreeNodeCompanies = new ObservableCollection<TreeNodeСompany>();
            TreeNodeContracts = new ObservableCollection<TreeNodeСontract>();
            TreeNodeSpecialties = new ObservableCollection<TreeNodeSpecialty>();
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
            TreeNodeCustomers = treeNode.TreeNodeCustomers;
            Path = treeNode.Path;
        }

        public Task Task { get; set; }
        public string Path { get; set; }

        #region Nodes
        public TreeNode ParentNode { get; set; }
        public ObservableCollection<TreeNode> TreeNodes { get; set; }
        public ObservableCollection<TreeNodeCustomer> TreeNodeCustomers { get; set; }
        public ObservableCollection<TreeNodeSpecialty> TreeNodeSpecialties { get; set; }
        public ObservableCollection<TreeNodeСompany> TreeNodeCompanies { get; set; }
        public ObservableCollection<TreeNodeСontract> TreeNodeContracts { get; set; }
        public void AddChild(TreeNode treeNode)
        {
            if (treeNode is TreeNodeCustomer)
            {
                TreeNodeCustomers.Add((TreeNodeCustomer)treeNode);
            }
            else if (treeNode is TreeNodeSpecialty)
            {
                TreeNodeSpecialties.Add((TreeNodeSpecialty)treeNode);
            }
            else if (treeNode is TreeNodeСompany)
            {
                TreeNodeCompanies.Add((TreeNodeСompany)treeNode);
            }
            else if (treeNode is TreeNodeСontract)
            {
                TreeNodeContracts.Add((TreeNodeСontract)treeNode);
            }
            else
            {
                TreeNodes.Add(treeNode);
            }
        }
        #endregion

    }
}
