using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StaffTime.Model;

namespace StaffTime.ViewModel
{
    public interface ITreeNodeFactory
    {
        TreeNode CreateTreeNode(TaskTypeEnum type);
        TreeNode CreateTreeNode(TreeNode treeNode);

        TreeNode CreateTreeNode(Task task);
    }
}
