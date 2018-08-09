using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Staff_time.ViewModel
{
    public class TreeNode : MainViewModel, INotifyPropertyChanged // todo ViewModelBase уже имеет реализованный интерфейс INotifyPropertyChanged
    {                                                             // todo для чего treeNode унаследован от MainViewModel?
        public TreeNode()
        {
            TreeNodes = new ObservableCollection<TreeNode>();

            IsExpanded = false;
        }
        public TreeNode(Task task) : this()
        {
            Task = task;
        }
        public TreeNode(TreeNode treeNode) : this()
        {
            Task = treeNode.Task;
            ParentNode = treeNode.ParentNode;
            TreeNodes = treeNode.TreeNodes;
            FullPath = treeNode.FullPath; // todo здесь будет 1 массив на двоих, если кто-то один почистит его, другой  удивится
        }

        private Task _task;
        public Task Task {
            get { return _task; }
            set
            {
                SetField(ref _task, value); // todo Должно быть ошибкой, т.к. наши подписчики подписаны на наш ivent, а не на ivent базового класса, поэтому здесь не отработает оповещение
                FullPath = TasksVM.generate_PathForTask(_task.ID);
            }
        }
        public List<string> FullPath { get; set; }

        #region Nodes

        public TreeNode ParentNode { get; set; }

        public ObservableCollection<TreeNode> TreeNodes { get; set; } // todo при замене списка не произойдёт обновление view, т.к. нотификации об этом нет

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
                //SetField(ref _isSelected, value); - Не работает
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
