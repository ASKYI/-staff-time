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
        public static ITaskWork taskWork;
        public static IWorkWork workWork;
        public static IAttrWork attrWork;
        public static ITypesWork typesWork;

        private static void _initialize_Context()
        {
            _context = new TaskManagmentDBEntities();
            taskWork = _context;
            workWork = _context;
            attrWork = _context;
            typesWork = _context;
        }
        #endregion

        public MainViewModel()
        {
            _initialize_Context();
            _generate_TreeNodesDictionary();

            _get_TestUser();
        }

        #region Current static (User, Date)
        protected static User curUser { get; set; }
        protected static DateTime curDate { get; set; }

        //Текущий пользователь - тестовый пользователь ID == 1
        private static bool _get_TestUser_tracker = false;
        private static void _get_TestUser()
        {
            if (!_get_TestUser_tracker)
            {
                curUser = _context.Users.Where(u => u.ID == 1).FirstOrDefault();
                _get_TestUser_tracker = true;
            }
        }
        #endregion
        #region TreeNodes static
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов, узлы задач хранятся в словаре для облегчения доступа.
        public static Dictionary<int, TreeNode> TaskNodesDictionary { get; set; }

        public static bool _generateTree_tracker = false;
        public static void _generate_TreeNodesDictionary()
        {
            if (_generateTree_tracker)
                return;
            _generateTree_tracker = true;

            TaskNodesDictionary = new Dictionary<int, TreeNode>();

            //В Tasks ссылка на родителя может содержать идентификатор на задачу, 
            //которая еще не встречалась в таблице при последовательном чтении.
            //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
            //В бд невозможно добавить ссылку на несуществующую задачу 

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
                    int parentId = (int)task.ParentTaskID;                        

                    if (!TaskNodesDictionary.ContainsKey(parentId))
                        TaskNodesDictionary.Add(parentId, new TreeNode());

                    TreeNode parentTreeNode = TaskNodesDictionary[parentId];

                    parentTreeNode.AddChild(treeNode);
                    treeNode.ParentNode = parentTreeNode;

                }
            }
        }
        #endregion
        #region Functions static
        public static ObservableCollection<TreeNode> Convert_TasksIntoNodes(List<int> t)
        {
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            if (t != null)
                foreach (var q in t)
                    tasksNodes.Add(TaskNodesDictionary[q]);
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

        #region INotifyPropertyChanged Member
        public bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
