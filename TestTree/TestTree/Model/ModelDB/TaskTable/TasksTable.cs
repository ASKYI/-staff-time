using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public static partial class TasksTable
    {
        //Генерация TreeNode dictionary, возвращает словарь узлов?
        public static Dictionary<int, TestTree.ViewModel.TreeNode> Generate_TaskNodesDictionary()
        {
            Dictionary<int, TestTree.ViewModel.TreeNode> TaskNodesDictionary = new Dictionary<int, TestTree.ViewModel.TreeNode>();
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство)

                TaskFactory taskFactory = new TaskFactory();
                TestTree.ViewModel.TreeNodeFactory treeNodeFactory = new TestTree.ViewModel.TreeNodeFactory();
                foreach (Task taskDB in ctx.Tasks)
                {
                    Task task = taskFactory.CreateTask(taskDB);

                    int id = task.ID;
                    TestTree.ViewModel.TreeNode treeNode;
                    if (!TaskNodesDictionary.ContainsKey(id))
                    {
                        treeNode = treeNodeFactory.CreateTreeNode(task);
                        TaskNodesDictionary.Add(id, treeNode);
                    }
                    else
                    {
                        TaskNodesDictionary[id].Task = task;
                        treeNode = TaskNodesDictionary[id];
                        treeNodeFactory.ChangeType(ref treeNode, task);
                        //treeNode.Task = task;
                    }

                    if (task.ParentTaskID != null)
                    {
                        int parentId = (int)task.ParentTaskID; //Из Nullable<int> в int, проверка на null уже была                        

                        if (!TaskNodesDictionary.ContainsKey(parentId))
                            TaskNodesDictionary.Add(parentId, new TestTree.ViewModel.TreeNode());

                        TestTree.ViewModel.TreeNode parentTreeNode = TaskNodesDictionary[parentId];

                        parentTreeNode.AddChild(treeNode);
                        treeNode.ParentNode = parentTreeNode;
                    }
                }
            }
            return TaskNodesDictionary;
        }

        //Доставать задачи по свойствам
    }
}
