using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using TestTree.Model;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace TestTree.ViewModel 
{
    public class FaveViewModel : MainViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<TreeNode> FavTasks { get; set; }
        private TreeNode _selectedTask;
        public TreeNode SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = value;
                RaisePropertyChanged("SelectedTask");
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
        private void Generate_FavTaskPaths()
        {
            if (TaskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            FavTasks = ConvertTasksIntoNodes(GetTasksByProp("Favorite", "True"));

            foreach (var ft in FavTasks)
            {
                string stringPath = "";
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
                        stringPath += "->"; //HACK: Может долго работать, исправить
                    stringPath += path[i];
                }
                ft.Path = stringPath;
            }
        }

        public FaveViewModel() : base()
        {
            SelectedTask = null;
            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
            try
            {
                Generate_FavTaskPaths();
            }
            catch
            {

            }

        }

        private bool CanSelectTask(object obj)
        {
            if (SelectedTask == null)
                return false;
            return true;
        }
        private void SelectTask(object obj)
        {
            MessengerInstance.Send(new NotificationMessage<TreeNode>(SelectedTask, "TaskSelection"));
        }
    }
}
