using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using TestTree.Model;
using GalaSoft.MvvmLight;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TestTree.ViewModel 
{
    enum TaskPropDataType { ValueText, ValueInt, ValueDate, ValueTime };
    enum TaskType { Customer, Сompany, Сontract, Direction } //Заказчик Предприятие Договор Направление

    //Этот класс должен быть один. Singleton?
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов дерева.
        //Словарь для облегчения доступа
        protected Dictionary<int, TreeNode> TaskNodesDictionary { get; set; }

        private void Generate_TaskNodesDictionary()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                TaskNodesDictionary = new Dictionary<int, TreeNode>();

                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство)

                foreach (Model.Task task in ctx.Tasks)
                {
                    int id = task.ID;
                    if (!TaskNodesDictionary.ContainsKey(id))
                        TaskNodesDictionary.Add(id, new TreeNode(task));
                    else
                        TaskNodesDictionary[id].Task = task;

                    if (task.ParentTaskID != null)
                    {
                        int parentId = (int)task.ParentTaskID; //Из Nullable<int> в int, проверка на null уже была
                        if (!TaskNodesDictionary.ContainsKey(parentId))
                            TaskNodesDictionary.Add(parentId, new TreeNode());
                        TaskNodesDictionary[parentId].TreeNodes.Add(TaskNodesDictionary[id]);
                        TaskNodesDictionary[id].ParentNode = TaskNodesDictionary[parentId];
                    }
                }
            }
        }
        public MainViewModel()
        {
            Generate_TaskNodesDictionary();
        }

        protected ObservableCollection<TreeNode> ConvertTasksIntoNodes(List<int> t)
        {
            if (TaskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            foreach (var q in t)
                tasksNodes.Add(TaskNodesDictionary[q]);
            return tasksNodes;
        }
        //protected List<int> GetTasksByProp(string propName, string propValueText = null, int? propValueInt = null,
          //  DateTime? propValueDateTime = null)  
        protected List<int> GetTasksByProp(string propName, string propValueText = null, Nullable<int> propValueInt = null, 
            Nullable<DateTime> propValueDateTime = null)
          {
              using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
              {
                  //HACK: Возможно есть вариант поиска значения в соотвествие с типом проще или короче
                  Model.Property favProp = (from p in ctx.Properties where p.PropName == propName select p).FirstOrDefault();
                  if ((TaskPropDataType)favProp.DataType == TaskPropDataType.ValueText)
                    return (from p in ctx.PropValues where p.ValueText == propValueText select p.TaskID).ToList<int>();
                  if ((TaskPropDataType)favProp.DataType == TaskPropDataType.ValueInt)
                    return (from p in ctx.PropValues where p.ValueInt == propValueInt select p.TaskID).ToList<int>();
                  if ((TaskPropDataType)favProp.DataType == TaskPropDataType.ValueDate)
                    return (from p in ctx.PropValues where p.ValueDate == DbFunctions.TruncateTime(propValueDateTime) select p.TaskID).ToList<int>();
                  if ((TaskPropDataType)favProp.DataType == TaskPropDataType.ValueTime) 
                    return (from p in ctx.PropValues where Convert.ToDateTime(p.ValueTime) == propValueDateTime select p.TaskID).ToList<int>();
            }
            return null;
        }
        protected List<int> GetTasksByProp(int propID, string propValueText = null, Nullable<int> propValueInt = null,
            Nullable<DateTime> propValueDateTime = null)
          {
              using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
              {
                  Model.Property favProp = (from p in ctx.Properties where p.ID == propID select p).FirstOrDefault();
                  return GetTasksByProp(favProp.ID, propValueText, propValueInt, propValueDateTime);
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
