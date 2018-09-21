using System;
using System.Collections.Generic;
using System.Linq;
using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public static class TasksVM
    {
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов
        public static Dictionary<int, TreeNode> Dictionary { get; set; }

        private static bool _init_tracker = false; //done: private
        public static void Init()
        {
            if (_init_tracker)
                return;
            _init_tracker = true;

            Dictionary = new Dictionary<int, TreeNode>();

            //В Tasks ссылка на родителя может содержать идентификатор на задачу, 
            //которая еще не встречалась в таблице при последовательном чтении.
            //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
            //В бд невозможно добавить ссылку на несуществующую задачу 

            TreeNodeFactory treeNodeFactory = new TreeNodeFactory();
            List<Task> tasksBD = Context.taskWork.GetAllTasks();
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
                    treeNode = Dictionary[id]; // todo мне кажется, или эти 2 строчки несогласованы между собой
                    treeNode = treeNodeFactory.CreateTreeNode(task); //
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
            
            Dictionary = Dictionary.OrderBy(pair => pair.Value.Task.IndexNumber).ToDictionary(pair => pair.Key, pair => pair.Value); // todo не думал что у dictionary есть порядок

            foreach (var pair in Dictionary)
            {
                pair.Value.FullPath = generate_PathForTask(pair.Key);
            }
        }

        public static void Add(Task task)
        {
            //DB
            Context.taskWork.AddTask(task);
            task.IndexNumber = task.ID;
            Context.taskWork.UpdateTask(task);

            //VM
            TreeNodeFactory factory = new TreeNodeFactory(); //done: ITreeNodeFactory удален из-за ненадобности
            TreeNode newNode = factory.CreateTreeNode(task);

            Dictionary.Add(task.ID, newNode); //done: TasksVM.Dictionary исправлено

            if (task.ParentTaskID != null)
            {
                int parentID = (int)task.ParentTaskID;
                newNode.ParentNode = Dictionary[parentID];
                Dictionary[parentID].AddChild(newNode);
            }

            newNode.FullPath = generate_PathForTask(task.ID);
        }

        //done-todo: Переписана функция
        public static void Edit(Task task) //TreeNode
        {
            //DB
            Context.taskWork.UpdateTask(task);

            //VM
            TreeNode oldNode = Dictionary[task.ID];

            TreeNodeFactory factory = new TreeNodeFactory();
            TreeNode newNode = factory.CreateTreeNode(task);

            //Parent
            if (newNode.Task.ParentTaskID != null)
            {
                newNode.ParentNode = Dictionary[(int)newNode.Task.ParentTaskID];
                if (oldNode.ParentNode != newNode.ParentNode)
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

            Dictionary[task.ID] = newNode;
            newNode.FullPath = generate_PathForTask(task.ID);
        }

        public static void DeleteWithChildren(int taskID)
        {
            //DB
            Context.taskWork.DeleteTask(taskID);

            //Works
            List<int> works = Context.workWork.GetWorksForTask(taskID);
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

            //if (parentNode != null)
              //  parentNode.TreeNodes.Remove(delNode);
            parentNode?.TreeNodes?.Remove(delNode); //done
            Dictionary.Remove(taskID);
        }

        public static void DeleteAlone(int taskID)
        {
            //DB
            Context.taskWork.DeleteTask(taskID);

            //Works
            List<int> works = Context.workWork.GetWorksForTask(taskID);
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
            int? parentID = delNode.Task.ParentTaskID; //todo

            foreach (var n in delNode.TreeNodes)
            {
                DeleteAlone(n.Task.ID);
            }

            Dictionary.Remove(taskID);
        }

        public static bool CheckWorks(int taskID)
        {
            List<int> works = Context.workWork.GetWorksForTask(taskID); //todo var
            if (works.Count > 0)
                return true;

            TreeNode node = Dictionary[taskID];

            TreeNode parentNode = node.ParentNode;
            int? parentID = node.Task.ParentTaskID; // todo

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

        public static ObservableCollection<TreeNode> Convert_TasksIntoNodes(List<int> t) //done: ObservableCollection, потому что используется во View
        {
            var tasksNodes = new ObservableCollection<TreeNode>(); //done: var
            if (t != null)
                foreach (var q in t)
                    tasksNodes.Add(Dictionary[q]);
            return tasksNodes;
        }

        public static List<string> generate_PathForTask(int taskID)
        {
            //if (!Dictionary.ContainsKey(taskID))
            //    return new List<string>();

            List<string> path = new List<string>(); //done: убран лишний возврат

            if (Dictionary.ContainsKey(taskID))
            {
                TreeNode t = Dictionary[taskID];
                while (t.ParentNode != null)
                {
                    path.Add(t.Task.TaskName);
                    t = t.ParentNode;
                }
                path.Add(t.Task.TaskName);

                path.Reverse();
            }
            return path;
        }

        public static bool CheckIsChild(int parentID, int? childID)
        {
            TreeNode parentNode = Dictionary[parentID]; // todo, а если в Dictionary нет элемента parentID
            TreeNode childNode = null; // todo преждевременно объявленная переменная
            if (childID != null)
            {
                childNode = Dictionary[(int)childID]; // todo в dictionary может не оказаться этого элемента

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
