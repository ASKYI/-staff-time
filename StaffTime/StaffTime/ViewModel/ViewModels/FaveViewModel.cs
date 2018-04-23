using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

using StaffTime.Model;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace StaffTime.ViewModel 
{
    public class FaveViewModel : MainViewModel, INotifyPropertyChanged
    {
        public FaveViewModel() : base()
        {
          /*  MessengerInstance.Register<NotificationMessage<TreeNode>>(this, (selectedTask) =>
            {
                Status += "Получен выбранный узел\n";
            }); */

            SelectedTask = null;
            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);

            Generate_FavTaskNodes();
        }
        public ObservableCollection<TreeNode> FavTaskNodes { get; set; }

        #region Generate
        private void Generate_FavTaskNodes()
        {
            List<int> faveTasks = new List<int>();
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                faveTasks = (from t in ctx.UserTasks where t.UserID == CurUser.ID select t.TaskID).ToList<int>();
            }
            FavTaskNodes = ConvertTasksIntoNodes(faveTasks);
            Generate_FavTaskPaths();
        }
        private void Generate_FavTaskPaths()
        {
            foreach (var ft in FavTaskNodes)
            {
                StringBuilder stringPath = new StringBuilder();
                List<string> path = new List<string>();

                TreeNode t = ft;
                while (t.ParentNode != null)
                {
                    path.Add(t.Task.TaskName);
                    t = t.ParentNode;
                }
                path.Add(t.Task.TaskName);

                path.Reverse();
                for(int i = 0; i < path.Count; ++i)
                {
                    if (i != 0)
                        stringPath.Append("->");
                    stringPath.Append(path[i]);
                }
                ft.Path = stringPath.ToString();
            }
        }
        #endregion

        #region Select Task
        private TreeNode _selectedTask;
        public TreeNode SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                SetField(ref _selectedTask, value);
            }
        }

        private readonly ICommand _selectTaskCommand;
        public ICommand SelectTaskCommand
        {
            get
            {
                return _selectTaskCommand;
            }
        }
        private bool CanSelectTask(object obj)
        {
            return SelectedTask != null;
        }
        private void SelectTask(object obj)
        {
            MessengerInstance.Send(new NotificationMessage<TreeNode>(SelectedTask, "TaskSelection"));
        }
        #endregion
    }
}
