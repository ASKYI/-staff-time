using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public static class TasksVM
    {
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов
        public static Dictionary<int, TreeNode> Dictionary { get; set; }

        public static bool Init_tracker = false;
        public static void Init()
        {
            if (Init_tracker)
                return;
            Init_tracker = true;

            Dictionary = new Dictionary<int, TreeNode>();

            //В Tasks ссылка на родителя может содержать идентификатор на задачу, 
            //которая еще не встречалась в таблице при последовательном чтении.
            //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
            //В бд невозможно добавить ссылку на несуществующую задачу 

            TreeNodeFactory treeNodeFactory = new TreeNodeFactory();
            List<Task> tasksBD = Context.taskWork.Read_AllTasks();
            foreach (Task task in tasksBD)
            {
                int id = task.ID;
                TreeNode treeNode;
                if (!Dictionary.ContainsKey(id))
                {
                    treeNode = treeNodeFactory.CreateTreeNode(task);
                    Dictionary.Add(id, treeNode);
                }
                else
                {
                    Dictionary[id].Task = task;
                    treeNode = Dictionary[id];
                    treeNode = treeNodeFactory.CreateTreeNode(task);
 //                   treeNodeFactory.ChangeType(treeNode, task);
                }

                if (task.ParentTaskID != null)
                {
                    int parentId = (int)task.ParentTaskID;

                    if (!Dictionary.ContainsKey(parentId))
                        Dictionary.Add(parentId, new TreeNode());

                    TreeNode parentTreeNode = Dictionary[parentId];

                    parentTreeNode.AddChild(treeNode);
                    treeNode.ParentNode = parentTreeNode;
                }
            }
        }

        public static void Add(Task task)
        {
            //DB
            Context.taskWork.Create_Task(task);

            //VM
            TreeNodeFactory factory = new TreeNodeFactory();
            TreeNode newNode = factory.CreateTreeNode(task);

            TasksVM.Dictionary.Add(task.ID, newNode);

            if (task.ParentTaskID != null)
            {
                int parentID = (int)task.ParentTaskID;
                newNode.ParentNode = Dictionary[parentID];
                Dictionary[parentID].AddChild(newNode);
            }

            //if (task.ParentTaskID == null || task.ParentTaskID == 0)
            //{
            //    TreeRoots.Add(node);
            //}
            //else
            //{
            //    TasksVM.Dictionary[(int)task.ParentTaskID].AddChild(node);
            //    node.ParentNode = TasksVM.Dictionary[(int)task.ParentTaskID];
            //}

            //List<int> childTasks = Context.taskWork.Read_ChildTasks(task.ID);
            //foreach (var t in childTasks)
            //{
            //    TreeNode childNode = TasksVM.Dictionary[t];

            //    if (childNode.ParentNode != null)
            //        childNode.ParentNode.TreeNodes.Remove(TasksVM.Dictionary[t]);
            //    else
            //        TreeRoots.Remove(TasksVM.Dictionary[t]);

            //    node.AddChild(childNode);
            //    childNode.ParentNode = node;
            //}
        }

        public static void Edit(Task task) //TreeNode
        {
            //DB
            Context.taskWork.Update_Task(task);

            //VM
            TreeNode node = Dictionary[task.ID];

            TreeNodeFactory factory = new TreeNodeFactory();
            TreeNode editNode = factory.CreateTreeNode(task);
            if (editNode.Task.ParentTaskID != null)
                editNode.ParentNode = Dictionary[(int)editNode.Task.ParentTaskID];

            TreeNode parentNode = editNode.ParentNode;
            int? parentID = editNode.Task.ParentTaskID;

            foreach (var n in node.TreeNodes)
            {
                n.ParentNode = editNode;
                //n.Task.ParentTaskID = editNode.Task.ID;
            }

            if (node.ParentNode != null)
            {
                int oldParentID = (int)node.ParentNode.Task.ID;
                Dictionary[oldParentID].TreeNodes.Remove(node);
            }
            if (parentNode != null)
                parentNode.TreeNodes.Add(editNode);

            Dictionary.Remove(task.ID);
            Dictionary.Add(task.ID, editNode);
        }

        public static void DeleteAlone(int taskID)
        {
            //DB
            Context.taskWork.Delete_Task(taskID);

            //VM
            TreeNode delNode = Dictionary[taskID];

            TreeNode parentNode = delNode.ParentNode;
            int? parentID = delNode.Task.ParentTaskID;

            foreach (var n in delNode.TreeNodes)
            {
                n.ParentNode = parentNode;
                n.Task.ParentTaskID = parentID;

                if (parentNode != null)
                    parentNode.TreeNodes.Add(n);
            }

            if (parentNode != null)
                parentNode.TreeNodes.Remove(delNode);
            Dictionary.Remove(taskID);
        } 

        public static void DeleteWithChildren(int taskID)
        {
            //Если понадобится
        }

        #region Other Methods

        public static ObservableCollection<TreeNode> Convert_TasksIntoNodes(List<int> t)
        {
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            if (t != null)
                foreach (var q in t)
                    tasksNodes.Add(Dictionary[q]);
            return tasksNodes;
        }

        public static List<string> generate_PathForTask(int taskID)
        {
            List<string> path = new List<string>();

            TreeNode t = Dictionary[taskID];
            while (t.ParentNode != null)
            {
                path.Add(t.Task.TaskName);
                t = t.ParentNode;
            }
            path.Add(t.Task.TaskName);

            path.Reverse();
            return path;
        }

        #endregion
    }
}
