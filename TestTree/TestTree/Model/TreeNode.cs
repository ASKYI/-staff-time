using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TestTree.Model
{
    public class TreeNode
    {
        public Model.Task Task {get; set;} 
        public TreeNode ParentNode { get; set; }
        public ObservableCollection<TreeNode> TreeNodes { get; set; }
        public string Path { get; set; }

        public TreeNode()
        {
            TreeNodes = new ObservableCollection<TreeNode>();
        }
        public TreeNode(Model.Task task) : this()
        {
            Task = task;
        }
        
    }
}
