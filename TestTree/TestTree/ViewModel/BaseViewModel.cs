using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using TestTree.Model;

namespace TestTree.ViewModel
{
    //Этот класс должен быть один. Singleton?
    public class BaseViewModel : INotifyPropertyChanged
    {
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов дерева.
        //Словарь для облегчения доступа
        protected Dictionary<System.Guid, TreeNode> TaskNodesDictionary { get; set; }

        private void Generate_TaskNodesDictionary()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                TaskNodesDictionary = new Dictionary<Guid, TreeNode>();

                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство)

                foreach (Model.Task task in ctx.Tasks)
                {
                    System.Guid id = (System.Guid)task.TaskID;
                    if (!TaskNodesDictionary.ContainsKey(id))
                        TaskNodesDictionary.Add(id, new TreeNode(task));
                    else
                        TaskNodesDictionary[id].Task = task;

                    if (task.ParentTaskID != null)
                    {
                        System.Guid parentId = (System.Guid)task.ParentTaskID;
                        if (!TaskNodesDictionary.ContainsKey(parentId))
                            TaskNodesDictionary.Add(parentId, new TreeNode());
                        TaskNodesDictionary[parentId].TreeNodes.Add(TaskNodesDictionary[id]);
                        TaskNodesDictionary[id].ParentNode = TaskNodesDictionary[parentId];
                    }
                }
            }
        }

        public BaseViewModel()
        {
            Generate_TaskNodesDictionary();
        }

        protected ObservableCollection<TreeNode> ConvertTasksIntoNodes(List<System.Guid> t)
        {
            if (TaskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            foreach (var q in t)
                tasksNodes.Add(TaskNodesDictionary[q]);
            return tasksNodes;
        }
        protected List<System.Guid> GetTasksByProp(string propName, string propValue)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                Model.Property favProp = (from p in ctx.Properties where p.PropName == propName select p).FirstOrDefault();
                return (from p in ctx.PropValues where p.Value == propValue select p.TaskID).ToList<System.Guid>();
            }
        }
        protected List<System.Guid> GetTasksByProp(System.Guid propID, string propValue)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                Model.Property favProp = (from p in ctx.Properties where p.PropID == propID select p).FirstOrDefault();
                return (from p in ctx.PropValues where p.Value == propValue select p.TaskID).ToList<System.Guid>();
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
