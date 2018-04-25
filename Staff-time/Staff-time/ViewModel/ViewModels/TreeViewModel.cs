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
    public class TreeViewModel : MainViewModel
    {
        public TreeViewModel() : base()
        {
            _generate_Tree();

            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
        }

        #region Tree Data
        public ObservableCollection<TreeNode> TreeRoots { get; set; }
        private void _generate_Tree()
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            foreach (var taskNode in TaskNodesDictionary)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    TreeRoots.Add(taskNode.Value);
                }
            }
        }
        #endregion

        #region Select Task
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
            return true;
        }
        private void SelectTask(object obj)
        {
            MessengerInstance.Register<NotificationMessage<TreeNode>>(this, (selectedTask) =>
            {

            });
        }
        #endregion
    }
}
