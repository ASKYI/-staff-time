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
            
            Dictionary = Dictionary.OrderBy(pair => pair.Value.Task.IndexNumber).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var pair in Dictionary)
            {
                pair.Value.FullPath = generate_PathForTask(pair.Key);
            }
        }

        public static void Add(Task task)
        {
            //DB
            Context.taskWork.Create_Task(task);
            task.IndexNumber = task.ID;
            Context.taskWork.Update_Task(task);

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

            newNode.FullPath = generate_PathForTask(task.ID);
        }

        public static void Edit(Task task) //TreeNode
        {
            //DB
            Context.taskWork.Update_Task(task);

            //VM
            TreeNode oldNode = Dictionary[task.ID];

            TreeNodeFactory factory = new TreeNodeFactory();
            TreeNode newNode = factory.CreateTreeNode(task);

            //Parent
            if (newNode.Task.ParentTaskID != null)
            {
                newNode.ParentNode = Dictionary[(int)newNode.Task.ParentTaskID];
                newNode.ParentNode.AddChild(newNode);
            }
            if (oldNode.ParentNode != null)
            {
                if (oldNode.ParentNode == newNode.ParentNode)
                    oldNode.ParentNode.TreeNodes[oldNode.ParentNode.TreeNodes.IndexOf(oldNode)] = newNode;
                else
                    oldNode.ParentNode.TreeNodes.Remove(oldNode);
            }
            
            //Children
            foreach (var n in oldNode.TreeNodes)
            {
                n.ParentNode = newNode;
                n.FullPath = generate_PathForTask(n.Task.ID);
                newNode.AddChild(n);
            }
            
            Dictionary.Remove(task.ID);
            Dictionary.Add(task.ID, newNode);
            newNode.FullPath = generate_PathForTask(task.ID);
        }

        public static void DeleteWithChildren(int taskID)
        {
            //DB
            Context.taskWork.Delete_Task(taskID);

            //Works
            List<int> works = Context.workWork.Read_WorksForTask(taskID);
            foreach (var id in works)
            {
                Work w = WorksVM.Dictionary[id].Work;
                WorksVM.Delete(w.ID);
                //MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
                //    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            }

            //VM
            TreeNode delNode = Dictionary[taskID];

            TreeNode parentNode = delNode.ParentNode;
            int? parentID = delNode.Task.ParentTaskID;

            foreach (var n in delNode.TreeNodes)
            {
                DeleteAlone(n.Task.ID);
            }

            if (parentNode != null)
                parentNode.TreeNodes.Remove(delNode);
            Dictionary.Remove(taskID);
        }

        public static void DeleteAlone(int taskID)
        {
            //DB
            Context.taskWork.Delete_Task(taskID);

            //Works
            List<int> works = Context.workWork.Read_WorksForTask(taskID);
            foreach (var id in works)
            {
                Work w = WorksVM.Dictionary[id].Work;
                WorksVM.Delete(w.ID);
                //MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
                //    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            }

            //VM
            TreeNode delNode = Dictionary[taskID];

            TreeNode parentNode = delNode.ParentNode;
            int? parentID = delNode.Task.ParentTaskID;

            foreach (var n in delNode.TreeNodes)
            {
                DeleteAlone(n.Task.ID);
            }

            Dictionary.Remove(taskID);
        }

        public static bool CheckWorks(int taskID)
        {
            List<int> works = Context.workWork.Read_WorksForTask(taskID);
            if (works.Count > 0)
                return true;

            TreeNode node = Dictionary[taskID];

            TreeNode parentNode = node.ParentNode;
            int? parentID = node.Task.ParentTaskID;

            foreach (var n in node.TreeNodes)
            {
                if (CheckWorks(n.Task.ID))
                    return true;
            }

            return false;
        }

        public static void CollapseAll()
        {
            foreach (var t in TasksVM.Dictionary)
            {
                t.Value.IsExpanded = false;
            }
        }

        public static void ExpandAll()
        {
            foreach (var t in TasksVM.Dictionary)
            {
                t.Value.IsExpanded = true;
            }
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
            if (!Dictionary.ContainsKey(taskID))
                return new List<string>();

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

        public static bool CheckIsChild(int parentID, int? childID)
        {
            TreeNode parentNode = Dictionary[parentID];
            TreeNode childNode = null;
            if (childID != null)
            {
                childNode = Dictionary[(int)childID];

                TreeNode curParent = childNode.ParentNode;
                while (curParent != null)
                {
                    if (curParent == parentNode)
                        return true;
                    curParent = curParent.ParentNode;
                }
            }
            return false;
        }

        #endregion
    }
}
