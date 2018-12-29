using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace Staff_time.ViewModel
{
    public enum TaskCommandEnum { Add, Edit, None }

    public class TasksAllViewModel : MainViewModel
    {
        public TasksAllViewModel(ObservableCollection<TreeNode> faveRoots)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            TasksVM.InitFullTree();
            _generate_Full_Tree();
            Mouse.OverrideCursor = Cursors.Arrow;

            //_collapseAllCommand = new RelayCommand(CollapseAll, CanCollapseAll);
            //_saveExpandCommand = new RelayCommand(SaveCollapse, (_) => true);
            //_expandAllCommand = new RelayCommand(ExpandAll, CanExpandAll);
            //_addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);
            _editTaskCommand = new RelayCommand(EditTask, CanEditTask);
            _moveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            _moveDownCommand = new RelayCommand(MoveDown, CanMoveDown);

            FaveTreeRoots = faveRoots;

            AcceptCommand = new RelayCommand(Accept, (_) => true); // todo чем чётче мы показываем намерения, тем легче программа 
            CancelCommand = new RelayCommand(Cancel, (_) => true); // в данном случае у нас return true всегда, наглядней было бы CancelCommand = new RelayCommand(Cancel, (_) => true);

            MessengerInstance.Register<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);
        }


        #region Tree

        private TreeNode _root = new TreeNode() { Task = new Task() { TaskName = "Задачи" }, IsExpanded = true };

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                _selectedTaskNode = value;
                if (_selectedTaskNode != null)
                    FavouritingTask = _selectedTaskNode.Task;
                //SetField<TreeNode>(ref _selectedTaskNode, value); // todo вроде c# по параметру сам должен распознать тип Generic
            }
        }

        private Task _favouritingTask;
        public Task FavouritingTask
        {
            get { return _favouritingTask; }
            set
            {
                SetField(ref _favouritingTask, value);
            }
        }

        private ObservableCollection<TreeNode> _faveTreeRoots;
        public ObservableCollection<TreeNode> FaveTreeRoots
        {
            get { return _faveTreeRoots; }
            set
            { 
                SetField(ref _faveTreeRoots, value);
            }
        }


        private ObservableCollection<TreeNode> _allTreeRoots;
        public ObservableCollection<TreeNode> AllTreeRoots
        {
            get { return _allTreeRoots; }
            set
            {
                SetField(ref _allTreeRoots, value);
            }
        }

        public void AddRootNode(TreeNode node)
        {
            AllTreeRoots.Add(node);
        }
        public void DeleteRootNode(TreeNode node)
        {
            AllTreeRoots.Remove(node);
        }
        public void UpdateRootNode(int oldNodeIndex, TreeNode newNode)
        {
            AllTreeRoots[oldNodeIndex] = newNode; // todo а если oldNodeIndex элемента нет в коллекции
        }

        private void _generate_Full_Tree()
        {
            AllTreeRoots = new ObservableCollection<TreeNode>();
            foreach (var taskNode in TasksVM.DictionaryFull)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    AllTreeRoots.Add(taskNode.Value);
                }
            }
        }

        public void ChangeSelection(TreeNode value) //Нельзя в сетер - будет переполнение стека
        {
            if (_selectedTaskNode != null)
                _selectedTaskNode.IsSelected = false;

            SetField<TreeNode>(ref _selectedTaskNode, value);

            if (_selectedTaskNode != null)
                _selectedTaskNode.IsSelected = true;
        }

        #endregion

        #region Add Task
        private readonly ICommand _addNearTaskCommand;
        public ICommand AddNearTaskCommand
        {
            get
            {
                return _addNearTaskCommand;
            }
        }

        private bool CanAddNearTask(object obj)
        {
            return dialog == null;
        }
        private void AddNearTask(object obj)
        {
            base.CancelEditing();

            Task newTask = new Task();
            if (SelectedTaskNode != null && SelectedTaskNode.Task.ID != SelectedTaskNode.Task.ParentTaskID)
                newTask.ParentTaskID = SelectedTaskNode.Task.ParentTaskID;
            newTask.TaskName = "Новая задача";

            dialog = new View.AddDialogWindow(new TaskDialogViewModel(newTask, AllTreeRoots, TaskCommandEnum.Add));
            dialog.Show();
        }
        private readonly ICommand _addChildTaskCommand;
        public ICommand AddChildTaskCommand
        {
            get
            {
                return _addChildTaskCommand;
            }
        }

        private bool CanAddChildTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }
        private void AddChildTask(object obj)
        {
            base.CancelEditing();

            Task newTask = new Task();
            newTask.ParentTaskID = SelectedTaskNode.Task.ID;
            newTask.TaskName = "Новая подзадача";
            
            dialog = new View.AddDialogWindow(new TaskDialogViewModel(newTask, AllTreeRoots, TaskCommandEnum.Add));
            dialog.Show();
        }
        #endregion

        #region Edit Task

        private readonly ICommand _editTaskCommand;
        public ICommand EditTaskCommand
        {
            get
            {
                return _editTaskCommand;
            }
        }

        private bool CanEditTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }
        private void EditTask(object obj)
        {
            base.CancelEditing();

            dialog = new View.EditDialogWindow(new TaskDialogViewModel(SelectedTaskNode.Task, AllTreeRoots, TaskCommandEnum.Edit));   
            dialog.Show();
        }

        #endregion

        #region Delete Task

        private readonly ICommand _deleteTaskCommand;
        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand;
            }
        }

        private bool CanDelteTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }
        private void DeleteTask(object obj)
        {
            base.CancelEditing();

            //Roots
            int delTaskID = SelectedTaskNode.Task.ID;
            if (AllTreeRoots.Contains(SelectedTaskNode))
                AllTreeRoots.Remove(SelectedTaskNode);
            //if (TasksVM.Dictionary.ContainsKey(SelectedTaskNode.Task.ID))
            //    TasksVM.Dictionary.Remove(SelectedTaskNode.Task.ID);


            TasksVM.DeleteWithChildren(delTaskID);

            if (TasksVM.Dictionary.ContainsKey(delTaskID + 1))
                ChangeSelection(TasksVM.DictionaryFull[delTaskID + 1]);
            else
                ChangeSelection(TasksVM.DictionaryFull.FirstOrDefault().Value);
        }

        #endregion
     
        #region Navigation

        private readonly ICommand _moveUpCommand;
        public ICommand MoveUpCommand
        {
            get
            {
                return _moveUpCommand;
            }
        }

        private bool CanMoveUp(object obj)
        {
            if (SelectedTaskNode == null || dialog != null)
                return false;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                if (index - 1 >= 0)
                    return true;
            }
            else
            {
                int index = AllTreeRoots.IndexOf(SelectedTaskNode);
                if (index - 1 >= 0)
                    return true;
            }
            return false;
        }
        private void MoveUp(object obj)
        {
            base.CancelEditing();

            int curI = (int)SelectedTaskNode.Task.IndexNumber;
            TreeNode newSeleted = SelectedTaskNode;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                parentNode.TreeNodes[index].Task.IndexNumber = parentNode.TreeNodes[index - 1].Task.IndexNumber;
                parentNode.TreeNodes[index - 1].Task.IndexNumber = curI;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index - 1].Task));

                parentNode.TreeNodes.Move(index, index - 1);
                ChangeSelection(parentNode.TreeNodes[index - 1]);
            }
            else
            {
                int index = AllTreeRoots.IndexOf(SelectedTaskNode);
                AllTreeRoots[index].Task.IndexNumber = AllTreeRoots[index - 1].Task.IndexNumber;
                AllTreeRoots[index - 1].Task.IndexNumber = curI;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index - 1].Task));

                AllTreeRoots.Move(index, index - 1);
                ChangeSelection(AllTreeRoots[index - 1]);
            }
        }

        private readonly ICommand _moveDownCommand;
        public ICommand MoveDownCommand
        {
            get
            {
                return _moveDownCommand;
            }
        }

        private bool CanMoveDown(object obj)
        {
            if (SelectedTaskNode == null || dialog != null)
                return false;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                if (index + 1 < parentNode.TreeNodes.Count)
                    return true;
            }
            else
            {
                int index = AllTreeRoots.IndexOf(SelectedTaskNode);
                if (index + 1 < AllTreeRoots.Count)
                    return true;
            }
            return false;
        }
        private void MoveDown(object obj)
        {
            base.CancelEditing();
            
            int curI = (int)SelectedTaskNode.Task.IndexNumber;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                parentNode.TreeNodes[index].Task.IndexNumber = parentNode.TreeNodes[index + 1].Task.IndexNumber;
                parentNode.TreeNodes[index + 1].Task.IndexNumber = curI;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));        // todo было бы прикольно вызывать так: parentNode.TreeNodes[index].Task.EditCommand()
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index + 1].Task));

                parentNode.TreeNodes.Move(index, index + 1);
                ChangeSelection(parentNode.TreeNodes[index + 1]);
            }
            else
            {
                int index = AllTreeRoots.IndexOf(SelectedTaskNode);
                AllTreeRoots[index].Task.IndexNumber = AllTreeRoots[index + 1].Task.IndexNumber;
                AllTreeRoots[index + 1].Task.IndexNumber = curI;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index + 1].Task));

                AllTreeRoots.Move(index, index + 1);
                ChangeSelection(AllTreeRoots[index + 1]);
            }
        }

        #endregion

        #region Do Task: Add, Edit
        
        private void _doTaskCommand(KeyValuePair<TaskCommandEnum, Task> pair)
        {       
            TaskCommandEnum command = pair.Key;
            Task task = pair.Value;

            switch (command)
            {
                case TaskCommandEnum.Add:
                    TasksVM.Add(task);
                    TreeNode newNode = TasksVM.DictionaryFull[task.ID];

                    if (newNode.ParentNode == null)
                        AddRootNode(newNode);

                    ChangeSelection(newNode);
                    if (newNode.ParentNode != null)
                        newNode.ParentNode.IsExpanded = true;

                    break;
                case TaskCommandEnum.Edit:
                    TreeNode oldNode = TasksVM.DictionaryFull[task.ID];
                    int index = -1;

                    if (oldNode.ParentNode == null)
                        index = AllTreeRoots.IndexOf(oldNode);

                    TasksVM.Edit(task);
                    newNode = TasksVM.DictionaryFull[task.ID];

                    if (index != -1 && newNode.ParentNode == null)
                        UpdateRootNode(index, newNode);
                    else if (newNode.ParentNode == null)
                        AddRootNode(newNode);
                    else
                        DeleteRootNode(oldNode);

                    break;
            }
        }

        #endregion

        #region Commands       

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private bool CanAccept(object obj)
        {
            return true;
        }
        public void Accept(object obj)
        {
            if (_favouritingTask == null)
                return;
            if (TasksVM.IsFave(_favouritingTask.ID))
            {
                MessageBox.Show("Данная задача уже добавлена в избранное");
                return;
            }

            // Добавим всех родителей, если их нет в избранном
            List<TreeNode> toAddInFave = new List<TreeNode>();
            var parent = SelectedTaskNode.ParentNode;
            while (parent != null && !FaveTreeRoots.Contains(parent))
            {
                toAddInFave.Add(parent);
                parent = parent.ParentNode;
            }
            for (int i = toAddInFave.Count - 1; i >= 0; --i)
                if (!TasksVM.IsFave(toAddInFave[i].Task.ID))
                    TasksVM.AddFave(toAddInFave[i].Task);

            // Добавим себя
            TasksVM.AddFave(_favouritingTask);

            // Добавим своё поддерево
            Queue<TreeNode> nodeToFave = new Queue<TreeNode>();
            nodeToFave.Enqueue(SelectedTaskNode);
            while (nodeToFave.Count > 0)
            {
                var curNode = nodeToFave.Dequeue();
                foreach (var childNode in curNode.TreeNodes)
                {
                    TasksVM.AddFave(childNode.Task);
                    nodeToFave.Enqueue(childNode);
                }
            }
            MessageBox.Show("Задача: " + _favouritingTask.TaskName + " добавлена в избранное", "Добавление в избранное", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        public void Cancel(object obj)
        {
            if (dialog != null)
            {
                dialog.Close();
                dialog = null;
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            //ChangeSelection(null);
            //MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
            //    new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
            dialog = null;
        }

        #endregion

    }
}
