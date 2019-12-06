using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public interface ITreeNodeFactory // todo в данном случае введение интерфейса бесполезно, т.к. он не отражает полиморфности возможного поведения ещё смотреть комментарии public class TreeNodeFactory : ITreeNodeFactory
    {
        TreeNode CreateTreeNode(TaskTypeEnum type);
        TreeNode CreateTreeNode(TreeNode treeNode);
        TreeNode CreateTreeNode(Task task);
    }
}
