﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using TestTree.Model;
using System.Windows.Input;
using System.ComponentModel;

namespace TestTree.ViewModel 
{
    public class FaveViewModel : BaseViewModel, INotifyPropertyChanged
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
        private void Generate_FavTask()
        {
            if (TaskNodesDictionary == null)
                throw new Exception("Dictionary has not been generated");
            FavTasks = ConvertTasksIntoNodes(GetTasksByProp("Favorite", "True"));
        }

        public FaveViewModel() : base()
        {
            SelectedTask = null;
            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
            try
            {
                Generate_FavTask();
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
