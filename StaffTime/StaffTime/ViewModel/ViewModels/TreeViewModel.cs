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
    public class TreeViewModel : MainViewModel
    {
        public TreeViewModel() : base()
        {
            //Создание узла дерева типа нулевой задачи
            //HACK: Если можно привязать узел к TreeView, а не коллекцию узлов первого уровня
            ObservableCollection<TreeNode> root = new ObservableCollection<TreeNode>();
            root.Add(new TreeNode());
            root[0].Task = new Task(); root[0].Task.TaskName = "Задачи";
            TreeRoot = new ReadOnlyObservableCollection<TreeNode>(root);

            Generate_Tree();
            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
        }

        private ReadOnlyObservableCollection<TreeNode> _treeRoot;
        public ReadOnlyObservableCollection<TreeNode> TreeRoot
        {
            get { return _treeRoot; }
            set { SetField(ref _treeRoot, value); }
        }     
        private void Generate_Tree()
        {
            TreeNode root = TreeRoot[0];

            foreach (var taskNode in TaskNodesDictionary)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    root.AddChild(taskNode.Value);
                }
            }
        }

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
