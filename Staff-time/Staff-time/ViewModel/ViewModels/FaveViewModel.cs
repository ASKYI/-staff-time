using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public class FaveViewModel : MainViewModel, INotifyPropertyChanged
    {
        public FaveViewModel() : base()
        {
            //TasksTable.Read_FaveTasks(CurUser.ID);
            _generate_FavTaskNodes();

            SelectedTask = null;
            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
        }

        #region Fave Tasks
        public static ObservableCollection<TreeNode> FaveTaskNodes { get; set; }

        private static void _generate_FavTaskNodes()
        {
            FaveTaskNodes = Convert_TasksIntoNodes(taskWork.Read_FaveTasks(CurUser.ID));
            _generate_FaveTaskPaths();
        }

        private static void _generate_FaveTaskPaths()
        {
            foreach (var ft in FaveTaskNodes)
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
                for (int i = 0; i < path.Count; ++i)
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
