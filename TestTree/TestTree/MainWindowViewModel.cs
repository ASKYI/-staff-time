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
        private Dictionary<System.Guid, TreeNode> _taskNodesDictionary;
        public ObservableCollection<TreeNode> Tree { get; set; }
        public ObservableCollection<TreeNode> FavTasks { get; set; }
        private void GenerateTree()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                //Генерируется список смежности. Корни в коллекции Tree.
                //Словарь используется для быстрого доступа к узлам по идентификатору задачи.
                Tree = new ObservableCollection<TreeNode>();
                _taskNodesDictionary  = new Dictionary<Guid, TreeNode>();

                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство)
                
                foreach (Model.Task task in ctx.Tasks)
                {
                    System.Guid id = (System.Guid)task.TaskID;
                    if (!_taskNodesDictionary.ContainsKey(id))
                        _taskNodesDictionary.Add(id, new TreeNode(task));
                    else
                        _taskNodesDictionary[id].Task = task;

                    if (task.ParentTaskID == null)
                        Tree.Add(_taskNodesDictionary[id]); //Это корень
                    else
                    {
                        System.Guid parentId = (System.Guid)task.ParentTaskID;
                        if (!_taskNodesDictionary.ContainsKey(parentId))
                            _taskNodesDictionary.Add(parentId, new TreeNode());
                        _taskNodesDictionary[parentId].TreeNodes.Add(_taskNodesDictionary[id]);
                        _taskNodesDictionary[id].ParentNode = _taskNodesDictionary[parentId];
                    }
                }
            }
        }
        private void GenerateFavTask()
        {
            if (_taskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            FavTasks = ConvertTasksIntoNodes(GetTasksByProp("Favorite", "True"));
        }
       
        private ObservableCollection<TreeNode> ConvertTasksIntoNodes(List<System.Guid> t)
        {
            if (_taskNodesDictionary == null)
                throw new Exception();
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            foreach(var q in t)
                tasksNodes.Add(_taskNodesDictionary[q]);
            return tasksNodes;
        }

        private List<System.Guid> GetTasksByProp(string propName, string propValue)
        { 
            if (_taskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                Model.Property favProp = (from p in ctx.Properties where p.PropName == propName select p).FirstOrDefault();
                return (from p in ctx.PropValues where p.Value == propValue select p.TaskID).ToList<System.Guid>();
            }
        }

        private List<System.Guid> GetTasksByProp(System.Guid propID, string propValue)
        {
            if (_taskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                Model.Property favProp = (from p in ctx.Properties where p.PropID == propID select p).FirstOrDefault();
                return (from p in ctx.PropValues where p.Value == propValue select p.TaskID).ToList<System.Guid>();
            }
        } 

        public MainWindowViewModel()
        {
            GenerateTree();
            GenerateFavTask();
        }
    }
}
