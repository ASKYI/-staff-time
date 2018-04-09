using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestTree.Model;

namespace TestTree.ViewModel
{
    public class TreeNodeFactory : ITreeNodeFactory
    {
        public TreeNode CreateTreeNode(TaskTypeEnum type)
        {
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new TreeNode();
                case TaskTypeEnum.TaskCustomer:
                    return new TreeNodeCustomer();
            }
            return new TreeNode();
        }
        public TreeNode CreateTreeNode (TreeNode treeNode)
        {
            TaskTypeEnum type = (TaskTypeEnum)treeNode.Task.TaskTypeID;
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new TreeNode(treeNode);
                case TaskTypeEnum.TaskCustomer:
                    return new TreeNodeCustomer(treeNode);
            }
            return new TreeNode(treeNode);
        }

    }
}
