using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTree.Model;
using System.Collections.ObjectModel;

namespace TestTree
{
    public class MainWindowViewModel
    {
        public ObservableCollection<TreeNode> Tree { get; set; }
        private void GenerateTree()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                //Генерируется список смежности. Корни в коллекции Tree.
                //Словарь используется для быстрого доступа к узлам по идентификатору задачи.
                Tree = new ObservableCollection<TreeNode>();
                Dictionary<System.Guid, TreeNode> taskNodes = new Dictionary<Guid, TreeNode>();

                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство)
                
                foreach (Model.Task task in ctx.Tasks)
                {
                    System.Guid id = (System.Guid)task.TaskID;
                    if (!taskNodes.ContainsKey(id))
                        taskNodes.Add(id, new TreeNode(task));
                    else
                        taskNodes[id].Task = task;

                    if (task.ParentTaskID == null)
                        Tree.Add(taskNodes[id]); //Это корень
                    else
                    {
                        System.Guid parentId = (System.Guid)task.ParentTaskID;
                        if (!taskNodes.ContainsKey(parentId))
                            taskNodes.Add(parentId, new TreeNode());
                        taskNodes[parentId].TreeNodes.Add(taskNodes[id]);
                        taskNodes[id].ParentNode = taskNodes[parentId];
                    }
                }
            }
        }

        public MainWindowViewModel()
        {
            GenerateTree();
        }
    }
}
