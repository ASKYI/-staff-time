﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class TreeNodeFactory : ITreeNodeFactory // todo в данном случае гораздо точнее будет решение со статическим классом, интерфейс здесь вводит в заблуждение (см. комментарии к public static void Add(Task task))
    {
        public TreeNode CreateTreeNode(TaskTypeEnum type)
        {
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new TreeNode();
                case TaskTypeEnum.TaskCustomer:
                    return new TreeNodeCustomer();
                case TaskTypeEnum.TaskDirection:
                    return new TreeNodeDirection();
                case TaskTypeEnum.TaskAppeal:
                    return new TreeNodeAppeal();
                case TaskTypeEnum.TaskContract:
                    return new TreeNodeСontract();
                case TaskTypeEnum.TaskRevision:
                    return new TreeNodeRevision();
                case TaskTypeEnum.TaskPlan:
                    return new TreeNodePlan();
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
                case TaskTypeEnum.TaskDirection:
                    return new TreeNodeDirection(treeNode);
                case TaskTypeEnum.TaskAppeal:
                    return new TreeNodeAppeal(treeNode);
                case TaskTypeEnum.TaskContract:
                    return new TreeNodeСontract(treeNode);
                case TaskTypeEnum.TaskRevision:
                    return new TreeNodeRevision(treeNode);
                case TaskTypeEnum.TaskPlan:
                    return new TreeNodePlan(treeNode);
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
                case TaskTypeEnum.TaskDirection:
                    return new TreeNodeDirection(task);
                case TaskTypeEnum.TaskAppeal:
                    return new TreeNodeAppeal(task);
                case TaskTypeEnum.TaskContract:
                    return new TreeNodeСontract(task);
                case TaskTypeEnum.TaskRevision:
                    return new TreeNodeRevision(task);
                case TaskTypeEnum.TaskPlan:
                    return new TreeNodePlan(task);
            }
            return new TreeNode(task);
        }
    }
}
