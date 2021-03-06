﻿using System;
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
using Staff_time.Model.UserModel;

namespace Staff_time.ViewModel
{
    public static class TasksVM
    {
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов
        private static Dictionary<int, TreeNode> _dictionary;

        public static Dictionary<int, TreeNode> Dictionary
        {
            get
            {
                if (FilterFaveTaskText == "" || FilterFaveTaskText == null)
                    return _dictionary;
                Mouse.SetCursor(Cursors.Wait);
                var listTreeNodeFound = _dictionary.Where(t => (t.Value.Task.TaskName.ToLower().Contains(FilterFaveTaskText.ToLower()))).Select(t => t.Value).ToList();
                Dictionary<int, TreeNode> filteredDictionary = new Dictionary<int, TreeNode>();

                Queue<TreeNode> nodeToStay = new Queue<TreeNode>();
                for (int i = 0; i < listTreeNodeFound.Count; ++i)
                {
                    var node = listTreeNodeFound[i];
                    var newNode = new TreeNode(node);
                    newNode.IsExpanded = true;
                    if (i == 0)
                        newNode.IsSelected = true;
                    nodeToStay.Enqueue(newNode);
                    filteredDictionary.Add(newNode.Task.ID, newNode);
                }

                while (nodeToStay.Count > 0)
                {
                    var curTreeNode = nodeToStay.Dequeue();

                    var parentNode = curTreeNode.ParentNode;
                    if (parentNode != null)
                    {
                        var newParentNode = new TreeNode(parentNode);
                        newParentNode.IsExpanded = true;


                        if (!filteredDictionary.ContainsKey(newParentNode.Task.ID))
                        {
                            newParentNode.TreeNodes = new ObservableCollection<TreeNode>();
                            newParentNode.TreeNodes.Add(curTreeNode);
                            filteredDictionary.Add(newParentNode.Task.ID, newParentNode);
                            nodeToStay.Enqueue(newParentNode);
                        }
                        else
                        {
                            if (filteredDictionary[newParentNode.Task.ID].TreeNodes.FirstOrDefault(n => n.Task.ID == curTreeNode.Task.ID) == null)
                                filteredDictionary[newParentNode.Task.ID].TreeNodes.Add(curTreeNode);
                        }
                    }
                }

                Mouse.SetCursor(Cursors.Arrow);
                return filteredDictionary;
            }
            set
            {
                _dictionary = value;
            }
        }

        private static Dictionary<int, TreeNode> _dictionaryFull;
        public static Dictionary<int, TreeNode> DictionaryFull
        {
            get
            {
                if (FilterFullTaskText == "" || FilterFullTaskText == null)
                    return _dictionaryFull;
                try
                {
                    Mouse.SetCursor(Cursors.Wait);
                    var listTreeNodeFound = _dictionaryFull.Where(t => (t.Value.Task.TaskName.ToLower().Contains(FilterFullTaskText.ToLower()))).Select(t => t.Value).ToList();
                    Dictionary<int, TreeNode> filteredDictionary = new Dictionary<int, TreeNode>();

                    Queue<TreeNode> nodeToStay = new Queue<TreeNode>();
                    for (int i = 0; i < listTreeNodeFound.Count; ++i)
                    {
                        var node = listTreeNodeFound[i];
                        var newNode = new TreeNode(node);
                        newNode.IsExpanded = true;
                        if (i == 0)
                            newNode.IsSelected = true;
                        nodeToStay.Enqueue(newNode);

                        filteredDictionary.Add(newNode.Task.ID, newNode);
                    }

                    while (nodeToStay.Count > 0)
                    {
                        var curTreeNode = nodeToStay.Dequeue();

                        var parentNode = curTreeNode.ParentNode;
                        if (parentNode != null)
                        {
                            var newParentNode = new TreeNode(parentNode);
                            newParentNode.IsExpanded = true;


                            if (!filteredDictionary.ContainsKey(newParentNode.Task.ID))
                            {
                                newParentNode.TreeNodes = new ObservableCollection<TreeNode>();
                                newParentNode.TreeNodes.Add(curTreeNode);
                                filteredDictionary.Add(newParentNode.Task.ID, newParentNode);
                                nodeToStay.Enqueue(newParentNode);
                            }
                            else
                            {
                                if (filteredDictionary[newParentNode.Task.ID].TreeNodes.FirstOrDefault(n => n.Task.ID == curTreeNode.Task.ID) == null)
                                    filteredDictionary[newParentNode.Task.ID].TreeNodes.Add(curTreeNode);
                            }
                        }
                    }

                    Mouse.SetCursor(Cursors.Arrow);
                    return filteredDictionary;
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    Mouse.SetCursor(Cursors.Arrow);
                    return null;
                }
            }
            set
            {
                _dictionaryFull = value;
            }
        }
        public static bool Init_Context_tracker = true;
        public static bool Init_tracker = true;
        public static bool Init_Full_tracker = false;

        public static string FilterFaveTaskText { get; set; }
        public static string FilterFullTaskText { get; set; }

        private static void FillTreeDictionaryByTasks(Dictionary<int, TreeNode> curDictionary, List<Task> tasksBD, bool isFullTree)
        {
            //tasksBD = tasksBD.OrderBy(t => t.IndexNumber).ToList();
            TreeNodeFactory treeNodeFactory = new TreeNodeFactory();
            int indexNumber = 1;
            foreach (Task taskTmp in tasksBD)
            {
                Task task;
                if (isFullTree)
                    task = taskTmp;
                else
                    task = DictionaryFull[taskTmp.ID].Task; //todo Настя попробуем хранить ссылку, чтобы избранное обновлялось сразу

                int id = task.ID;
                TreeNode treeNode;
                if (!curDictionary.ContainsKey(id))
                {
                    treeNode = treeNodeFactory.CreateTreeNode(task);
                    treeNode.IndexNumber = indexNumber;
                    curDictionary.Add(id, treeNode);
                    indexNumber++;
                }
                else
                {
                    curDictionary[id].Task = task;
                    treeNode = curDictionary[id];                          // todo мне кажется, или эти 2 строчки несогласованы между собой
                    //treeNode = treeNodeFactory.CreateTreeNode(task);    // 
                }

                if (task.ParentTaskID != null)
                {
                    int parentId = (int)task.ParentTaskID;

                    if (!curDictionary.ContainsKey(parentId))
                    {
                        var typeID = tasksBD.First(t => t.ID == parentId).TaskTypeID;
                        curDictionary.Add(parentId, treeNodeFactory.CreateTreeNode((TaskTypeEnum)typeID));
                    }

                    TreeNode parentTreeNode = curDictionary[parentId];
                    parentTreeNode.AddChild(treeNode);
                    treeNode.ParentNode = parentTreeNode;
                }
            }
        }

        private static void FillExpandedFlag(Dictionary<int, TreeNode> curDictionary)
        {
            //var userFaveTasksID = Context.taskWork.Read_FaveTasksID(GlobalInfo.CurrentUser.ID);
            curDictionary.ToList().ForEach(node => node.Value.IsExpanded = Context.taskWork.IsExpanded(node.Key, GlobalInfo.CurrentUser.ID));
            //for (int i = 0; i < curDictionary.Count; ++i)
            //{
            //    var curDictItem = curDictionary.ElementAt(i);
            //    curDictItem.Value.IsExpanded = Context.taskWork.IsExpanded(curDictItem.Key, GlobalInfo.CurrentUser.ID);
            //}
        }

        public static void InitFave()
        {
            if (Init_tracker)
                return;
            Init_tracker = true;

            //В Tasks ссылка на родителя может содержать идентификатор на задачу, 
            //которая еще не встречалась в таблице при последовательном чтении.
            //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
            //В бд невозможно добавить ссылку на несуществующую задачу 

            List<Task> faveTasksDB = Context.taskWork.Read_FaveTasks(GlobalInfo.CurrentUser.ID);
            Dictionary = new Dictionary<int, TreeNode>();
            FillTreeDictionaryByTasks(Dictionary, faveTasksDB, false);
            FillExpandedFlag(Dictionary);
            Dictionary = Dictionary.ToDictionary(pair => pair.Key, pair => pair.Value); // todo не думал что у dictionary есть порядок
            //Dictionary = Dictionary.OrderBy(pair => pair.Value.Task.IndexNumber).ToDictionary(pair => pair.Key, pair => pair.Value); // todo не думал что у dictionary есть порядок

            foreach (var pair in Dictionary)
            {
                pair.Value.FullPath = generate_PathForTask(pair.Key);
            }
        }

        public static void InitFullTree()
        {
            if (Init_Full_tracker)
                return;
            Init_Full_tracker = true;

            //В Tasks ссылка на родителя может содержать идентификатор на задачу, 
            //которая еще не встречалась в таблице при последовательном чтении.
            //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
            //В бд невозможно добавить ссылку на несуществующую задачу 

            var existTaskIDs = DictionaryFull != null ? DictionaryFull.Select(n => n.Key).ToList() : new List<int>();
            List<Task> tasksBD = Context.taskWork.Read_AllTasks(existTaskIDs);
            if (DictionaryFull == null)
                DictionaryFull = new Dictionary<int, TreeNode>();
            FillTreeDictionaryByTasks(DictionaryFull, tasksBD, true);
            DictionaryFull = DictionaryFull.OrderBy(pair => pair.Value.Task.IndexNumber).ToDictionary(pair => pair.Key, pair => pair.Value); // todo не думал что у dictionary есть порядок

            foreach (var pair in DictionaryFull)
            {
                pair.Value.FullPath = generate_PathForTask(pair.Key);
            }
            Init_tracker = false;
        }

        public static void Add(Task task)
        {
            Mouse.SetCursor(Cursors.Wait);
            //DB
            Context.taskWork.Create_Task(task);
            //Context.taskWork.Create_TaskToFave(task.ID, GlobalInfo.CurrentUser.ID);
            //task.IndexNumber = task.ID; //todo indexNumber уже должен быть заполнен
            if (task.PropValues != null && task.PropValues.Count > 0)
                Context.taskWork.Update_Task(task);

            //Добавим в избранное

            //VM
            TreeNodeFactory factory = new TreeNodeFactory(); // todo на текущий момент TreeNodeFactory имеет интерфейс ITreeNodeFactory, но при этом создаётся здесь и используется без учёта этого факта
            TreeNode newNode = factory.CreateTreeNode(task);

            TasksVM.DictionaryFull.Add(task.ID, newNode); // todo интересный момент использования классом самого себя

            if (task.ParentTaskID != null)
            {
                int parentID = (int)task.ParentTaskID;
                newNode.ParentNode = DictionaryFull[parentID];
                DictionaryFull[parentID].AddChild(newNode);
            }

            newNode.FullPath = generate_PathForTask(task.ID);
            Mouse.SetCursor(Cursors.Arrow);
        }

        public static List<string> GetAllWorksNames(int taskID)
        {
            return Context.taskWork.GetAllWorksNames(taskID);
        }

        public static void TransferTask(int fromUserID, int toUserID, int taskID, DateTime dt, string Note)
        {
            Context.requestWork.AddRequest(fromUserID, toUserID, taskID, dt, Note);
        }

        public static void AddFave(Task task)
        {
            //DB
            Context.taskWork.Create_TaskToFave(task.ID, GlobalInfo.CurrentUser.ID);
            task.IndexNumber = task.ID;

            //VM
            TreeNodeFactory factory = new TreeNodeFactory(); // todo на текущий момент TreeNodeFactory имеет интерфейс ITreeNodeFactory, но при этом создаётся здесь и используется без учёта этого факта
            TreeNode newNode = factory.CreateTreeNode(task);

            TasksVM.Dictionary.Add(task.ID, newNode); // todo интересный момент использования классом самого себя

            if (task.ParentTaskID != null)
            {
                int parentID = (int)task.ParentTaskID;
                newNode.ParentNode = Dictionary[parentID];
                Dictionary[parentID].AddChild(newNode);
            }

            newNode.FullPath = generate_PathForTask(task.ID);
        }

        public static void Edit(Task task, bool IsFullTree) //TreeNode
        {
            var curDictionary = IsFullTree ? DictionaryFull : Dictionary;
            //DB
            if (IsFullTree)
            {
                try
                {
                    Mouse.SetCursor(Cursors.Wait);
                    Context.taskWork.Update_Task(task);
                    Mouse.SetCursor(Cursors.Arrow);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    Mouse.SetCursor(Cursors.Arrow);
                }
            }
            //VM
            TreeNode oldNode = curDictionary[task.ID];

            TreeNodeFactory factory = new TreeNodeFactory();
            TreeNode newNode = factory.CreateTreeNode(task);
            newNode.IndexNumber = oldNode.IndexNumber;

            //Parent
            if (newNode.Task.ParentTaskID != null)
            {
                newNode.ParentNode = curDictionary[(int)newNode.Task.ParentTaskID];
                if (oldNode.ParentNode != newNode.ParentNode) // todo а если родитель не поменялся, значит не надо в нём регистрироваться? Хм увидел позже. Функциональность размазана, из-за этого сложно, код можно написать по-другому, см. пример
                    newNode.ParentNode.AddChild(newNode);     // условие newNode.Task.ParentTaskID != null   подразумевает, что внутри блока будет работа полностью с newNode
            }
            if (oldNode.ParentNode != null)
            {
                if (oldNode.ParentNode == newNode.ParentNode)
                    oldNode.ParentNode.TreeNodes[oldNode.ParentNode.TreeNodes.IndexOf(oldNode)] = newNode;
                else
                    oldNode.ParentNode.TreeNodes.Remove(oldNode);
            }


            //// пример реализации
            //TreeNode oldParent = null;
            //TreeNode newParent = null;

            //if (oldNode.Task.ParentTaskID.HasValue)
            //    Dictionary.TryGetValue(oldNode.Task.ParentTaskID.Value, out oldParent);

            //if (newNode.Task.ParentTaskID.HasValue)
            //    Dictionary.TryGetValue(newNode.Task.ParentTaskID.Value, out newParent);

            //if (oldParent == newParent)
            //    newParent?.UpdateChild(oldNode, newNode);   // придётся дописать
            //else
            //{
            //    oldParent?.RemoveChild(oldNode);            // и это тоже
            //    newParent?.AddChild(newNode);
            //}



            //Children
            foreach (var n in oldNode.TreeNodes)
            {
                n.ParentNode = newNode;
                n.FullPath = generate_PathForTask(n.Task.ID);
                newNode.AddChild(n);
            }

            curDictionary[task.ID] = newNode;
            //DictionaryFull.Remove(task.ID);
            //DictionaryFull.Add(task.ID, newNode);
            newNode.FullPath = generate_PathForTask(task.ID);
        }

        public static void SetResponsibleForTaskChildren(int _parentTaskID, int _responsibleID)
        {
            Mouse.SetCursor(Cursors.Wait);

            var parentNode = DictionaryFull[_parentTaskID];

            // Обновим своё поддерево
            Queue<TreeNode> nodeToFave = new Queue<TreeNode>();
            nodeToFave.Enqueue(parentNode);
            while (nodeToFave.Count > 0)
            {
                var curNode = nodeToFave.Dequeue();
                foreach (var childNode in curNode.TreeNodes)
                {
                    var task = childNode.Task;
                    task.ResponsibleID = _responsibleID;
                    Context.taskWork.Update_Task(task);

                    nodeToFave.Enqueue(childNode);
                }
            }
            Mouse.SetCursor(Cursors.Arrow);
        }

        public static void DeleteFaveWithChildren(int taskID)
        {
            if (!Dictionary.ContainsKey(taskID))
                return;
            //DB
            Context.taskWork.Delete_TaskFromFave(taskID);

            //VM
            TreeNode delNode = Dictionary[taskID];

            TreeNode parentNode = delNode.ParentNode;
            int? parentID = delNode.Task.ParentTaskID; // todo ?

            foreach (var n in delNode.TreeNodes)
            {
                DeleteFaveAlone(n.Task.ID);
            }

            if (parentNode != null)         // todo можно так parentNode?.TreeNodes?.Remove(delNode);
                parentNode.TreeNodes.Remove(delNode);
            Dictionary.Remove(taskID);
        }

        public static bool DeleteWithChildren(int taskID)
        {
            try
            {
                Mouse.SetCursor(Cursors.Wait);

                //DB
                if (Context.taskWork.Delete_Task(taskID) == false)
                    return false;

                ////Works
                //List<int> works = Context.workWork.Read_WorksForTask(taskID);
                //foreach (var id in works)
                //{
                //    Work w = WorksVM.DictionaryFull[id].Work;
                //    WorksVM.Delete(w.ID);
                //    //MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
                //    //    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
                //}

                //VM
                TreeNode delNode = DictionaryFull[taskID];

                TreeNode parentNode = delNode.ParentNode;
                int? parentID = delNode.Task.ParentTaskID; // todo ?

                foreach (var n in delNode.TreeNodes)
                {
                    DeleteAlone(n.Task.ID);
                }

                if (parentNode != null)         // todo можно так parentNode?.TreeNodes?.Remove(delNode);
                    parentNode.TreeNodes.Remove(delNode);
                DictionaryFull.Remove(taskID);
                DeleteFaveWithChildren(taskID);
                Mouse.SetCursor(Cursors.Arrow);

                return true;
            }
            catch (Exception exp)
            {
                Mouse.SetCursor(Cursors.Arrow);
                MessageBox.Show(exp.Message, "Ошибка удаления задачи");
                return false;
            }
        }

        public static void DeleteFaveAlone(int taskID)
        {
            //DB
            Context.taskWork.Delete_TaskFromFave(taskID);

            ////Works
            //List<int> works = Context.workWork.Read_WorksForTask(taskID);
            //foreach (var id in works)
            //{
            //    Work w = WorksVM.Dictionary[id].Work;
            //    WorksVM.Delete(w.ID);
            //    //MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
            //    //    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            //}

            //VM
            TreeNode delNode = Dictionary[taskID];

            TreeNode parentNode = delNode.ParentNode;
            int? parentID = delNode.Task.ParentTaskID; // todo

            foreach (var n in delNode.TreeNodes)
            {
                DeleteFaveAlone(n.Task.ID);
            }

            Dictionary.Remove(taskID);
        }

        public static void DeleteAlone(int taskID)
        {
            //DB
            if (Context.taskWork.Delete_Task(taskID) == false)
                return;

            ////Works
            //List<int> works = Context.workWork.Read_WorksForTask(taskID);
            //foreach (var id in works)
            //{
            //    Work w = WorksVM.DictionaryFull[id].Work;
            //    WorksVM.Delete(w.ID);
            //    //MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
            //    //    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            //}

            //VM
            TreeNode delNode = DictionaryFull[taskID];

            TreeNode parentNode = delNode.ParentNode;
            int? parentID = delNode.Task.ParentTaskID; // todo

            foreach (var n in delNode.TreeNodes)
            {
                DeleteAlone(n.Task.ID);
            }

            DictionaryFull.Remove(taskID);
        }

        //public static bool CheckWorks(int taskID)
        //{
        //    List<int> works = Context.workWork.Read_WorksForTask(taskID); // todo var
        //    if (works.Count > 0)            // todo какой здесь вопрос задаётся контейнеру? сколько элементов? 3 или 5 это важно?  Здесь вопрос - пустой он или нет, желательно задавать точные вопросы works.IsEmpty , плохо что такого нет у List
        //        return true;

        //    TreeNode node = Dictionary[taskID];

        //    TreeNode parentNode = node.ParentNode;
        //    int? parentID = node.Task.ParentTaskID; // todo

        //    foreach (var n in node.TreeNodes)
        //    {
        //        if (CheckWorks(n.Task.ID))
        //            return true;
        //    }

        //    return false;
        //}

        public static void CollapseAll()
        {
            foreach (var t in TasksVM.Dictionary)
            {
                t.Value.IsExpanded = false;
            }
        }

        public static void SaveCollapse(ObservableCollection<TreeNode> treeRoots)
        {
            Context.taskWork.Update_UserTaskExpended(TasksVM.Dictionary.Select(n => n.Value).ToList());

            //Queue<TreeNode> nodeToExpended = new Queue<TreeNode>();

            //foreach (var rootNode in treeRoots)
            //    nodeToExpended.Enqueue(rootNode);

            //while (nodeToExpended.Count > 0)
            //{
            //    var curNode = nodeToExpended.Dequeue();
            //    Context.taskWork.Update_UserTaskExpended(curNode.Task.ID, GlobalInfo.CurrentUser.ID, curNode.IsExpanded);
            //    foreach (var childNode in curNode.TreeNodes)
            //    {
            //        Context.taskWork.Update_UserTaskExpended(childNode.Task.ID, GlobalInfo.CurrentUser.ID, childNode.IsExpanded);
            //        nodeToExpended.Enqueue(childNode);
            //    }
            //}
        }

        public static void ExpandAll()
        {
            foreach (var t in TasksVM.Dictionary)
            {
                t.Value.IsExpanded = true;
            }
        }

        #region Other Methods

        public static ObservableCollection<TreeNode> Convert_TasksIntoNodes(List<int> t) // todo - почему возвращает ObservableCollection, а не List, который гораздо проще
        {
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>(); // todo длинное имя дважды в строке, var
            if (t != null)
                foreach (var q in t)
                    tasksNodes.Add(Dictionary[q]); // todo а что будет если в dictionary не будет числа q ? будет эксепшн
            return tasksNodes;
        }

        public static List<string> generate_PathForTask(int taskID)
        {
            Dictionary<int, TreeNode> curDictionary = new Dictionary<int, TreeNode>();
            if (DictionaryFull == null)
                curDictionary = Dictionary;
            else
                curDictionary = DictionaryFull;

            if (!curDictionary.ContainsKey(taskID))
                return new List<string>();

            List<string> path = new List<string>(); // todo дважды создаётся пустой список, достаточно одного пустого

            TreeNode t = curDictionary[taskID];
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
            if (!DictionaryFull.ContainsKey(parentID))
                throw new IndexOutOfRangeException();
            TreeNode parentNode = DictionaryFull[parentID];

            if (childID != null)
            {
                if (!DictionaryFull.ContainsKey((int)childID))
                    throw new IndexOutOfRangeException();

                TreeNode childNode = null;
                childNode = DictionaryFull[(int)childID];

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

        public static bool IsFave(int taskID)
        {
            return Context.taskWork.IsFave(taskID);
        }
        public static bool IsExist(int taskID, string taskName, int? parentTaskID)
        {
            return Context.taskWork.IsExist(taskID, taskName, parentTaskID);
        }

        public static void ReplaceUserTasks(Task task1, Task task2)
        {
            Context.taskWork.ReplaceUserTasks(task1, task2);
        }
        #endregion
    }
}
