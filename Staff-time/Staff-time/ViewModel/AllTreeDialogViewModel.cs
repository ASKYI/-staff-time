using System.Windows.Input;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;

namespace Staff_time.ViewModel
{
    //class AllTreeDialogViewModel : MainViewModel
    //{
    //    public AllTreeDialogViewModel(ObservableCollection<TreeNode> allRoots, ObservableCollection<TreeNode> faveRoots)
    //    {
    //        _generate_Tree(allRoots);
    //        AllTreeRoots = allRoots;
    //        FaveTreeRoots = faveRoots;

    //        AcceptCommand = new RelayCommand(Accept, (_) => true); // todo чем чётче мы показываем намерения, тем легче программа 
    //        CancelCommand = new RelayCommand(Cancel, (_) => true); // в данном случае у нас return true всегда, наглядней было бы CancelCommand = new RelayCommand(Cancel, (_) => true);
    //    }

    //    #region Tree

    //    private ObservableCollection<TreeNode> _allTreeRoots;
    //    public ObservableCollection<TreeNode> AllTreeRoots
    //    {
    //        get { return _allTreeRoots; }
    //        set
    //        {
    //            SetField(ref _allTreeRoots, value);
    //        }
    //    }

    //    private ObservableCollection<TreeNode> _faveTreeRoots;
    //    public ObservableCollection<TreeNode> FaveTreeRoots
    //    {
    //        get { return _faveTreeRoots; }
    //        set
    //        {
    //            SetField(ref _faveTreeRoots, value);
    //        }
    //    }

    //    private TreeNode _root = new TreeNode() { Task = new Task() { TaskName = "Задачи" }, IsExpanded = true };
    //    private void _generate_Tree(ObservableCollection<TreeNode> roots)
    //    {
    //        AllTreeRoots = new ObservableCollection<TreeNode>();
    //        AllTreeRoots.Add(_root);

    //        foreach (var r in roots)
    //            AllTreeRoots[0].AddChild(r);
    //    }

    //    private TreeNode _selectedTaskNode;
    //    public TreeNode SelectedTaskNode
    //    {
    //        get { return _selectedTaskNode; }
    //        set
    //        {
    //            _selectedTaskNode = value;
    //            FavouritingTask = _selectedTaskNode.Task;
    //            //SetField<TreeNode>(ref _selectedTaskNode, value); // todo вроде c# по параметру сам должен распознать тип Generic
    //        }
    //    }

    //    private Task _favouritingTask;
    //    public Task FavouritingTask
    //    {
    //        get { return _favouritingTask; }
    //        set
    //        {
    //            SetField(ref _favouritingTask, value);
    //        }
    //    }

    //    #endregion


    //    #region Commands       

    //    public ICommand AcceptCommand { get; set; }
    //    public ICommand CancelCommand { get; set; }

    //    private bool CanAccept(object obj)
    //    {
    //        return true;
    //    }
    //    public void Accept(object obj)
    //    {
    //        if (_favouritingTask == null)
    //            return;
    //        if (TasksVM.IsFave(_favouritingTask.ID))
    //        {
    //            MessageBox.Show("Данная задача уже добавлена в избранное");
    //            return;
    //        }

    //        // Добавим всех родителей, если их нет в избранном
    //        List<TreeNode> toAddInFave = new List<TreeNode>();
    //        var parent = SelectedTaskNode.ParentNode;
    //        while (parent != null && !FaveTreeRoots.Contains(parent))
    //        {
    //            toAddInFave.Add(parent);
    //            parent = parent.ParentNode;
    //        }
    //        for (int i = toAddInFave.Count - 1; i >= 0; --i)
    //            if (!TasksVM.IsFave(toAddInFave[i].Task.ID))
    //                TasksVM.AddFave(toAddInFave[i].Task);

    //        // Добавим себя
    //        TasksVM.AddFave(_favouritingTask);

    //        // Добавим своё поддерево
    //        Queue<TreeNode> nodeToFave = new Queue<TreeNode>();
    //        nodeToFave.Enqueue(SelectedTaskNode);
    //        while (nodeToFave.Count > 0)
    //        {
    //            var curNode = nodeToFave.Dequeue();
    //            foreach (var childNode in curNode.TreeNodes)
    //            {
    //                TasksVM.AddFave(childNode.Task);
    //                nodeToFave.Enqueue(childNode);
    //            }
    //        }
    //        MessageBox.Show("Задача: " + _favouritingTask.TaskName + " добавлена в избранное", "Добавление в избранное", MessageBoxButton.OK, MessageBoxImage.Information);
    //    }

    //    private bool CanCancel(object obj)
    //    {
    //        return true;
    //    }
    //    public void Cancel(object obj)
    //    {
    //        if (dialog != null)
    //        {
    //            dialog.Close();
    //            dialog = null;
    //        }
    //    }

    //    public void OnWindowClosing(object sender, CancelEventArgs e)
    //    {
    //        //ChangeSelection(null);
    //        //MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
    //        //    new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
    //        dialog = null;
    //    }

    //    #endregion
    //}
}
