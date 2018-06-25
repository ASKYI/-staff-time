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
            
            MessengerInstance.Register<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);
        }
        
        #region Tree

        private ObservableCollection<TreeNode> _treeRoots;
        public ObservableCollection<TreeNode> TreeRoots
        {
            get { return _treeRoots; }
            set
            { //Как бы заблокировать возможность добавления/удаления узлов
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
            if (SelectedTaskNode != null)
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

            foreach(var t in TasksVM.Dictionary)
            {
                t.Value.IsExpanded = false;
            }
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

            foreach (var t in TasksVM.Dictionary)
            {
                t.Value.IsExpanded = true;
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
