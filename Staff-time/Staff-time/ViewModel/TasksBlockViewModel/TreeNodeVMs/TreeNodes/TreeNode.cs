using System;
using System.Collections.Generic;
using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Staff_time.ViewModel
{
    public class TreeNode : INotifyPropertyChanged //done: не насленик MainViewModel
    {
        public TreeNode()
        {
            TreeNodes = new ObservableCollection<TreeNode>();

            IsExpanded = false;
        }
        public TreeNode(Task task) : this()
        {
            Task = task;
        }
        public TreeNode(TreeNode treeNode) : this() //done: исправлена глубина копирования
        {
            Task = treeNode.Task;
            ParentNode = treeNode.ParentNode;
            TreeNodes = treeNode.TreeNodes;
            FullPath = new List<string>();
            foreach (var s in treeNode.FullPath)
                FullPath.Add(s);
        }

        private Task _task;
        public Task Task {
            get { return _task; }
            set
            {
                NotifyPropertyChanged("Task"); //done: было _task, случайно
                _task = value;
                FullPath = TasksVM.generate_PathForTask(_task.ID);
            }
        }
        public List<string> FullPath { get; set; }

        #region Nodes

        public TreeNode ParentNode { get; set; }

        private ObservableCollection<TreeNode> _treeNodes;
        public ObservableCollection<TreeNode> TreeNodes //done: Нотификации
        {
            get
            {
                return _treeNodes;
            }
            set
            {
                _treeNodes = value;
            }
        }

        public void AddChild(TreeNode treeNode)
        {
            TreeNodes.Add(treeNode);
        }

        #endregion

        #region View Properties

        private Boolean _isSelected;
        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private Boolean _isExpanded;
        public Boolean IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion
    }
}
