using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

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
            FullPath = treeNode.FullPath.ToList();
            Task = treeNode.Task;
            if (treeNode.ParentNode != null)
                ParentNode = (TreeNode)treeNode.ParentNode.MemberwiseClone();

            TreeNodes = new ObservableCollection<TreeNode>();
            foreach (var node in treeNode.TreeNodes)
                TreeNodes.Add((TreeNode)node.MemberwiseClone());
            IndexNumber = IndexNumber;
            IsExpanded = IsExpanded;
        }

        //public void InitTaskControl(int taskID, int taskTypeID)
        //{
        //    if (TasksVM.DictionaryFull.ContainsKey(taskID))
        //    {
        //        Task = (Task)TasksVM.DictionaryFull[taskID].Task;

        //        if (taskTypeID > 0)
        //        {
        //            if (Task.PropValues != null)
        //            {
        //                Context.taskWork.DeleteProperties(Task.PropValues.ToList());
        //                Task.PropValues.Where(t => Task.PropValues.Remove(t));
        //                Task.PropValues = null;
        //            }
        //        }
        //        var taskType = taskTypeID >= 0 ? taskTypeID : Task.TaskTypeID;
        //        var props = Context.taskWork.GetAllProperties(taskType);
        //        foreach (var prop in props)
        //        {
        //            if (Task.PropValues == null)
        //                Task.PropValues = new HashSet<PropValue>();
        //            var propWithValue = Task.PropValues.FirstOrDefault(pv => pv.Property == prop);
        //            if (propWithValue == null)
        //            {
        //                PropValue pv = new PropValue();
        //                pv.Property = prop;
        //                pv.PropID = prop.ID;
        //                pv.TaskID = taskID;
        //                pv.DataType = prop.DataType;
        //                Task.PropValues.Add(pv);
        //            }
        //        }
        //    }
        //}


        private Task _task;
        public Task Task {
            get { return _task; }
            set
            {
                SetField(ref _task, value); // todo Должно быть ошибкой, т.к. наши подписчики подписаны на наш ivent, а не на ivent базового класса, поэтому здесь не отработает оповещение
                if (FullPath == null || FullPath.Count == 0)
                    FullPath = TasksVM.generate_PathForTask(_task.ID); //todo Настя
            }
        }

        //private Task _taskForPropValues;
        //public Task TaskForPropValues
        //{
        //    get { return _taskForPropValues; }
        //    set
        //    {
        //        SetField(ref _taskForPropValues, value); // todo Должно быть ошибкой, т.к. наши подписчики подписаны на наш ivent, а не на ivent базового класса, поэтому здесь не отработает оповещение
        //        FullPath = TasksVM.generate_PathForTask(_taskForPropValues.ID); //todo Настя
        //        RaisePropertyChanged("TaskForPropValues");
        //    }
        //}
        

        private Nullable<int> _indexNumber;
        public Nullable<int> IndexNumber
        {
            get { return _indexNumber; }
            set
            {
                _indexNumber = value;
            }
        }
        
        public List<string> FullPath { get; set; }
        public string FullPathAsString
        {
            get
            {
                StringBuilder stringPath = new StringBuilder();

                for (int i = FullPath.Count - 1; i >= 0; --i) // todo есть замечательный оператор string.Join советую к нему присмотреться
                {
                    stringPath.Append(FullPath[i]);
                    if (i != 0)
                        stringPath.Append("<-");
                }
                return stringPath.ToString();
            }
        }
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
                    if (_isExpanded == true)
                        IsSelected = true;
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
