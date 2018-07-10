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

    public class TasksBlockViewModel : MainViewModel
    {
        public TasksBlockViewModel()
        {
            _generate_Tree();

            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDelteTask);
            _editTaskCommand = new RelayCommand(EditTask, CanEditTask);
            _collapseAllCommand = new RelayCommand(CollapseAll, CanCollapseAll);
            _expandAllCommand = new RelayCommand(ExpandAll, CanExpandAll);
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
            TreeRoots[oldNodeIndex] = newNode;
        }

        private void _generate_Tree()
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            foreach (var taskNode in TasksVM.Dictionary)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    TreeRoots.Add(taskNode.Value);
                }
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
      
        #endregion

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
        private void AddWork(object obj)
        {
            base.CancelEditing();

            Work work = new Work();
            work.WorkName = "Новая работа";
            work.TaskID = SelectedTaskNode.Task.ID;
            work.StartDate = chosenDate.Date;
            work.StartTime = DateTime.Now;

            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(
                new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Add, work));
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

            dialog = new View.AddDialogWindow(new TaskDialogViewModel(newTask, TreeRoots, TaskCommandEnum.Add));
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
            
            dialog = new View.AddDialogWindow(new TaskDialogViewModel(newTask, TreeRoots, TaskCommandEnum.Add));
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

            dialog = new View.EditDialogWindow(new TaskDialogViewModel(SelectedTaskNode.Task, TreeRoots, TaskCommandEnum.Edit));
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

            //Works
            List<int> works = Context.workWork.Read_WorksForTask(SelectedTaskNode.Task.ID);
            foreach (var id in works)
            {
                Work w = WorksVM.Dictionary[id].Work;
                MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>
                    (new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, w));
            }

            //Roots
            int delTaskID = SelectedTaskNode.Task.ID;
            if (SelectedTaskNode.ParentNode == null)
            {
                foreach(var n in SelectedTaskNode.TreeNodes)
                {
                    TreeRoots.Add(n);
                }
            }
            if (TreeRoots.Contains(SelectedTaskNode))
                TreeRoots.Remove(SelectedTaskNode);

            TasksVM.DeleteAlone(delTaskID);

            if (TasksVM.Dictionary.ContainsKey(delTaskID + 1))
                ChangeSelection(TasksVM.Dictionary[delTaskID + 1]);
            else
                ChangeSelection(TasksVM.Dictionary.FirstOrDefault().Value);
        }

        #endregion

        #region Expand Collapse

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
                int index = TreeRoots.IndexOf(SelectedTaskNode);
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
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                TreeRoots[index].Task.IndexNumber = TreeRoots[index - 1].Task.IndexNumber;
                TreeRoots[index - 1].Task.IndexNumber = curI;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index - 1].Task));

                TreeRoots.Move(index, index - 1);
                ChangeSelection(TreeRoots[index - 1]);
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
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                if (index + 1 < TreeRoots.Count)
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

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index + 1].Task));

                parentNode.TreeNodes.Move(index, index + 1);
                ChangeSelection(parentNode.TreeNodes[index + 1]);
            }
            else
            {
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                TreeRoots[index].Task.IndexNumber = TreeRoots[index + 1].Task.IndexNumber;
                TreeRoots[index + 1].Task.IndexNumber = curI;

                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, TreeRoots[index + 1].Task));

                TreeRoots.Move(index, index + 1);
                ChangeSelection(TreeRoots[index + 1]);
            }
        }

        #endregion

        #region Do Task: Add, Edit

        private void _doTaskCommand(KeyValuePair<TaskCommandEnum, Task> pair)
        {
            dialog = null;

            TaskCommandEnum command = pair.Key;
            Task task = pair.Value;

            switch(command)
            {
                case TaskCommandEnum.Add:
                    TasksVM.Add(task);
                    TreeNode newNode = TasksVM.Dictionary[task.ID];

                    if (newNode.ParentNode == null)
                        AddRootNode(newNode);

                    ChangeSelection(newNode);
                    if (newNode.ParentNode != null)
                        newNode.ParentNode.IsExpanded = true;

                    break;
                case TaskCommandEnum.Edit:
                    TreeNode oldNode = TasksVM.Dictionary[task.ID];
                    int index = -1;

                    if (oldNode.ParentNode == null)
                        index = TreeRoots.IndexOf(oldNode);

                    TasksVM.Edit(task);
                    newNode = TasksVM.Dictionary[task.ID];

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
