using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
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
                case TaskTypeEnum.TaskSpecialty:
                    return new TreeNodeSpecialty();
                case TaskTypeEnum.TaskСompany:
                    return new TreeNodeСompany();
                case TaskTypeEnum.TaskСontract:
                    return new TreeNodeСontract();
            }
            return new TreeNode();
        }
        public TreeNode CreateTreeNode(TreeNode treeNode)
        {
            TaskTypeEnum type = (TaskTypeEnum)treeNode.Task.TaskTypeID;
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new TreeNode(treeNode);
                case TaskTypeEnum.TaskCustomer:
                    return new TreeNodeCustomer(treeNode);
                case TaskTypeEnum.TaskSpecialty:
                    return new TreeNodeSpecialty(treeNode);
                case TaskTypeEnum.TaskСompany:
                    return new TreeNodeСompany(treeNode);
                case TaskTypeEnum.TaskСontract:
                    return new TreeNodeСontract(treeNode);
            }
            return new TreeNode(treeNode);
        }

        public TreeNode CreateTreeNode(Task task)
        {
            TaskTypeEnum type = (TaskTypeEnum)task.TaskTypeID;
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new TreeNode(task);
                case TaskTypeEnum.TaskCustomer:
                    return new TreeNodeCustomer(task);
                case TaskTypeEnum.TaskSpecialty:
                    return new TreeNodeSpecialty(task);
                case TaskTypeEnum.TaskСompany:
                    return new TreeNodeСompany(task);
                case TaskTypeEnum.TaskСontract:
                    return new TreeNodeСontract(task);
            }
            return new TreeNode(task);
        }
    }
}
