using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using Staff_time.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

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
                    treeNodeFactory.ChangeType(treeNode, task);
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

            Init_tracker = true;
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
        public static string generate_PathForTask(TreeNode taskTreeNode)
        {
            StringBuilder stringPath = new StringBuilder();
            List<string> path = new List<string>();

            TreeNode t = taskTreeNode;
            while (t.ParentNode != null)
            {
                path.Add(t.Task.TaskName);
                t = t.ParentNode;
            }
            path.Add(t.Task.TaskName);

            path.Reverse();
            for (int i = 0; i < path.Count; ++i)
            {
                if (i != 0)
                    stringPath.Append("->");
                stringPath.Append(path[i]);
            }
            return stringPath.ToString();
        }

        #endregion
    }
}
