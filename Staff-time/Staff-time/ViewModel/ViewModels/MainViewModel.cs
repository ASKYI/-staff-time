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

    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Context
        private static TaskManagmentDBEntities _context;
        protected static ITaskWork taskWork;
        protected static IWorkWork workWork;

        private static void _initialize_Context()
        {
            _context = new TaskManagmentDBEntities();
            taskWork = _context;
            workWork = _context;
        }

        #endregion

        public MainViewModel()
        {
            _get_TestUser();
            _initialize_Context();
            
            _generate_TreeNodesDictionary();
            _generate_WorkTypes();
        }

        #region Current Users, Date
        protected static User CurUser { get; set; }
        private static void _get_TestUser()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                CurUser = (from u in ctx.Users where u.ID == 1 select u).FirstOrDefault();
            }
        }
        protected static DateTime CurDate { get; set; }
        #endregion

        #region TreeNodes
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов, узлы задач хранятся в словаре для облегчения доступа.
        protected static Dictionary<int, TreeNode> TaskNodesDictionary { get; set; }

        private static void _generate_TreeNodesDictionary()
        {
            TaskNodesDictionary = new Dictionary<int, TreeNode>();
            //В Tasks ссылка на родителя может содержать идентификатор на задачу, 
            //которая еще не встречалась в таблице при последовательном чтении.
            //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
            //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство), 
            //TODO: При удалении задачи, ссылки на нее удаляются.

            TreeNodeFactory treeNodeFactory = new TreeNodeFactory();
            List<Task> tasks = taskWork.Read_AllTasks();
            foreach (Task task in tasks) 
            {
                int id = task.ID;
                TreeNode treeNode;
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
                        TaskNodesDictionary.Add(parentId, new TreeNode());

                    TreeNode parentTreeNode = TaskNodesDictionary[parentId];

                    parentTreeNode.AddChild(treeNode);
                    treeNode.ParentNode = parentTreeNode;

                }
            }
        }

        protected static ObservableCollection<TreeNode> Convert_TasksIntoNodes(List<int> t)
        {
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            if (t != null)
                foreach (var q in t)
                    tasksNodes.Add(TaskNodesDictionary[q]);
            return tasksNodes;
        }
        #endregion

        #region WorkTypes
        public static ObservableCollection<WorkType> WorkTypes;
        private static void _generate_WorkTypes()
        {
            WorkTypes = new ObservableCollection<WorkType>();
            foreach(var t in _context.WorkTypes)
            {
                WorkTypes.Add(t);
            } 
        }
        #endregion
        /*     #region TODO: Move to model

             protected static List<int> GetTasksByProp(string propName, string propValueText = null, Nullable<int> propValueInt = null,
                 Nullable<DateTime> propValueDateTime = null)
             {
                 using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
                 {
                     Model.Property favProp = (from p in ctx.Properties where p.PropName == propName select p).FirstOrDefault();
                     if (favProp != null)
                     {
                         switch ((TaskPropDataType)favProp.DataType)
                         {
                             case TaskPropDataType.ValueText:
                                 return (from p in ctx.PropValues where p.ValueText == propValueText select p.TaskID).ToList<int>();
                             case TaskPropDataType.ValueInt:
                                 return (from p in ctx.PropValues where p.ValueInt == propValueInt select p.TaskID).ToList<int>();
                             case TaskPropDataType.ValueDate:
                                 return (from p in ctx.PropValues where p.ValueDate == DbFunctions.TruncateTime(propValueDateTime) select p.TaskID).ToList<int>();
                             case TaskPropDataType.ValueTime:
                                 return (from p in ctx.PropValues where Convert.ToDateTime(p.ValueTime) == propValueDateTime select p.TaskID).ToList<int>();
                         }
                     }
                 }
                 return null;
             }
             protected static List<int> GetTasksByProp(int propID, string propValueText = null, Nullable<int> propValueInt = null,
                 Nullable<DateTime> propValueDateTime = null)
             {
                 using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
                 {
                     Model.Property favProp = (from p in ctx.Properties where p.ID == propID select p).FirstOrDefault();
                     return GetTasksByProp(favProp.ID, propValueText, propValueInt, propValueDateTime);
                 }
             }

             #endregion*/

        #region INotifyPropertyChanged Member
        protected bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public static event PropertyChangedEventHandler PropertyChanged_static;

        public static void OnPropertyChanged_static(string name)
        {
            if (PropertyChanged_static != null)
            {
                PropertyChanged_static(null, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
