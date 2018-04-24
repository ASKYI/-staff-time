using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaffTime.Model
{
    public static partial class TasksTable
    {
        private static List<Task> _taskDictionary;
        public static List<Task> TaskDictionary { get { return _taskDictionary; } }
        
        public static void Create_Task(Task task)
        {
            //TODO
        }
        public static void Read_TaskNodesDictionary()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                _taskDictionary = (from x in ctx.Tasks select x).ToList<Task>();
            }
            /*
            Dictionary<int, StaffTime.ViewModel.TreeNode> TaskNodesDictionary = new Dictionary<int, StaffTime.ViewModel.TreeNode>();
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство),
                
                //TODO: При удалении задачи, ссылки на нее удаляются.

                TaskFactory taskFactory = new TaskFactory();
                StaffTime.ViewModel.TreeNodeFactory treeNodeFactory = new StaffTime.ViewModel.TreeNodeFactory();
                foreach (Task taskDB in ctx.Tasks)
                {
                    Task task = taskFactory.CreateTask(taskDB);

                    int id = task.ID;
                    StaffTime.ViewModel.TreeNode treeNode;
                    if (!TaskNodesDictionary.ContainsKey(id))
                    {
                        treeNode = treeNodeFactory.CreateTreeNode(task);
                        TaskNodesDictionary.Add(id, treeNode);
                    }
                    else
                    {
                        TaskNodesDictionary[id].Task = task;
                        treeNode = TaskNodesDictionary[id];
                        treeNodeFactory.ChangeType(treeNode, task);
                    }

                    if (task.ParentTaskID != null)
                    {
                        int parentId = (int)task.ParentTaskID; //Из Nullable<int> в int, проверка на null уже была                        

                        if (!TaskNodesDictionary.ContainsKey(parentId))
                            TaskNodesDictionary.Add(parentId, new StaffTime.ViewModel.TreeNode());

                        StaffTime.ViewModel.TreeNode parentTreeNode = TaskNodesDictionary[parentId];

                        parentTreeNode.AddChild(treeNode);
                        treeNode.ParentNode = parentTreeNode;
                    }
                }*/
            }
            return TaskNodesDictionary;
        }
        public static void Update_Task(int taskId)
        {
            //TODO
            //???Как изменять? По полям вручную?
        }
        public static void Delete_Task(int taskId)
        {
            //TODO
        }
    }
}
