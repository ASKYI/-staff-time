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
using Staff_time.Model.UserModel;

namespace Staff_time.ViewModel
{
    //public enum TaskCommandEnum { Add, Edit, None }

    public class TasksFaveViewModel : MainViewModel
    {
        //test
        public TasksFaveViewModel()
        {
            _generate_Tree();
            _showFullTreeCommand = new RelayCommand(ShowTree, (_) => true);
            
            _collapseAllCommand = new RelayCommand(CollapseAll, CanCollapseAll);
            _saveExpandCommand = new RelayCommand(SaveCollapse, (_) => true);
            _expandAllCommand = new RelayCommand(ExpandAll, CanExpandAll);
            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            //_addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            //_addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteFaveTask, CanDeleteTask);
            //_editTaskCommand = new RelayCommand(EditTask, CanEditTask);
            _moveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            _moveDownCommand = new RelayCommand(MoveDown, CanMoveDown);

            MessengerInstance.Register<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);
        }
        
        #region Tree
        private ObservableCollection<TreeNode> _treeRoots;
        public ObservableCollection<TreeNode> TreeRoots
        {
            get { return _treeRoots; }
            set
            { 
                SetField(ref _treeRoots, value);
            }
        }

        public void AddRootNode(TreeNode node)
        {
            TreeRoots.Add(node);
        }
        public void DeleteRootNode(TreeNode node)
        {
            TreeRoots.Remove(node);
        }
        public void UpdateRootNode(int oldNodeIndex, TreeNode newNode)
        {
            TreeRoots[oldNodeIndex] = newNode; // todo а если oldNodeIndex элемента нет в коллекции
        }

        private void _generate_Tree()
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            int pos = 1;
            foreach (var taskNode in TasksVM.Dictionary)
            {
                if (taskNode.Value.ParentNode == null)
                    TreeRoots.Add(taskNode.Value);
                taskNode.Value.IndexNumber = pos;
                pos++;
            }
        }

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetField<TreeNode>(ref _selectedTaskNode, value);
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
        #endregion //Tree

        #region show full Tree
        private readonly ICommand _showFullTreeCommand;
        public ICommand ShowFullTreeCommand
        {
            get
            {
                return _showFullTreeCommand;
            }
        }
        #endregion //show full Tree

        #region Add Work
        private readonly ICommand _addWorkCommand;
        public ICommand AddWorkCommand
        {
            get
            {
                return _addWorkCommand;
            }
        }

        private bool CanAddWork(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }

        private void ShowTree(object obj)
        {
            var dialog = new View.AllTreeDialog(new ViewModel.TasksAllViewModel(TreeRoots, SelectedTaskNode));
            dialog.ShowDialog();
            _generate_Tree();
        }

        private void AddWork(object obj)
        {
            base.CancelEditing();

            Work work = new Work();
            work.WorkName = "";
            work.TaskID = SelectedTaskNode.Task.ID;
            work.StartDate = chosenDate.Date;
            work.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            work.UserID = GlobalInfo.CurrentUser.ID;

            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(
                new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Add, work));
        }
        #endregion //Add Work

        #region Expand Collapse

        private readonly ICommand _saveExpandCommand;
        public ICommand SaveExpandCommand
        {
            get
            {
                return _saveExpandCommand;
            }
        }
        private void SaveCollapse(object obj)
        {
            TasksVM.SaveCollapse(TreeRoots);
            MessageBox.Show("Развертка сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private readonly ICommand _collapseAllCommand;
        public ICommand CollapseAllCommand
        {
            get
            {
                return _collapseAllCommand;
            }
        }
        private bool CanCollapseAll(object obj)
        {
            return dialog == null;
        }
        private void CollapseAll(object obj)
        {
            base.CancelEditing();

            TasksVM.CollapseAll();
        }


        private readonly ICommand _expandAllCommand;
        public ICommand ExpandAllCommand
        {
            get
            {
                return _expandAllCommand;
            }
        }
        private bool CanExpandAll(object obj)
        {
            return dialog == null;
        }
        private void ExpandAll(object obj)
        {
            base.CancelEditing();

            TasksVM.ExpandAll();
        }

        #endregion //Expand Collapse

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
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                if (index - 1 >= 0)
                    return true;
            }
            return false;
        }
        private void MoveUp(object obj)
        {
            base.CancelEditing();

            int curI = (int)SelectedTaskNode.IndexNumber;
            var task1 = SelectedTaskNode.Task;
            Task task2 = null;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                parentNode.TreeNodes[index].IndexNumber = parentNode.TreeNodes[index - 1].IndexNumber;
                parentNode.TreeNodes[index - 1].IndexNumber = curI;
                task2 = parentNode.TreeNodes[index - 1].Task;


                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index - 1].Task));

                parentNode.TreeNodes.Move(index, index - 1);
                ChangeSelection(parentNode.TreeNodes[index - 1]);
            }
            else
            {
                int index = TreeRoots.IndexOf(SelectedTaskNode);

                TreeRoots[index].IndexNumber = TreeRoots[index - 1].IndexNumber;
                TreeRoots[index - 1].IndexNumber = curI;
                task2 = TreeRoots[index - 1].Task;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index - 1].Task));

                TreeRoots.Move(index, index - 1);
                ChangeSelection(TreeRoots[index - 1]);
            }
            TasksVM.ReplaceUserTasks(task1, task2);
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
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                if (index + 1 < TreeRoots.Count)
                    return true;
            }
            return false;
        }
        private void MoveDown(object obj)
        {
            base.CancelEditing();

            int curI = (int)SelectedTaskNode.IndexNumber;
            var task1 = SelectedTaskNode.Task;
            Task task2 = null;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                parentNode.TreeNodes[index].IndexNumber = parentNode.TreeNodes[index + 1].IndexNumber;
                parentNode.TreeNodes[index + 1].IndexNumber = curI;
                task2 = parentNode.TreeNodes[index + 1].Task;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));        // todo было бы прикольно вызывать так: parentNode.TreeNodes[index].Task.EditCommand()
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index + 1].Task));

                parentNode.TreeNodes.Move(index, index + 1);
                ChangeSelection(parentNode.TreeNodes[index + 1]);
            }
            else
            {
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                TreeRoots[index].IndexNumber = TreeRoots[index + 1].IndexNumber;
                TreeRoots[index + 1].IndexNumber = curI;
                task2 = TreeRoots[index + 1].Task;


                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index + 1].Task));

                TreeRoots.Move(index, index + 1);
                ChangeSelection(TreeRoots[index + 1]);
            }
            TasksVM.ReplaceUserTasks(task1, task2);
        }

        #endregion

        #region Do Task: Edit

        private readonly ICommand _deleteTaskCommand;
        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand;
            }
        }

        private bool CanDeleteTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }
        private void DeleteFaveTask(object obj)
        {
            var dialogResult = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить задачу из избранного?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.No)
                return;

            base.CancelEditing();
            Mouse.OverrideCursor = Cursors.Wait;
            //Roots
            int delTaskID = SelectedTaskNode.Task.ID;
            if (TreeRoots.Contains(SelectedTaskNode))
                TreeRoots.Remove(SelectedTaskNode);
            //if (TasksVM.Dictionary.ContainsKey(SelectedTaskNode.Task.ID))
            //    TasksVM.Dictionary.Remove(SelectedTaskNode.Task.ID);


            TasksVM.DeleteFaveWithChildren(delTaskID);

            if (TasksVM.Dictionary.ContainsKey(delTaskID + 1))
                ChangeSelection(TasksVM.Dictionary[delTaskID + 1]);
            else
                ChangeSelection(TasksVM.Dictionary.FirstOrDefault().Value);
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void _doTaskCommand(KeyValuePair<TaskCommandEnum, Task> pair)
        {
            TaskCommandEnum command = pair.Key;
            Task task = pair.Value;

            if (!TasksVM.Dictionary.ContainsKey(task.ID))
                return;

            switch (command)
            {
                case TaskCommandEnum.Edit:
                    TreeNode oldNode = TasksVM.Dictionary[task.ID];
                    int index = -1;

                    if (oldNode.ParentNode == null)
                        index = TreeRoots.IndexOf(oldNode);

                    TasksVM.Edit(task, false);
                    TreeNode newNode = TasksVM.Dictionary[task.ID];

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

    }
}
