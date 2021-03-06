﻿using System;
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
using Staff_time.View.Dialog;
using Staff_time.Helpers;

namespace Staff_time.ViewModel
{
    public enum TaskCommandEnum { Add, Edit, None }

    public class TasksAllViewModel : MainViewModel
    {
        private TasksFaveViewModel TaskFaveVM { get; set; }
        public TasksAllViewModel(TasksFaveViewModel TFVM, ObservableCollection<TreeNode> faveRoots, TreeNode selectedTaskNode)
        {
            TaskFaveVM = TFVM;
            Mouse.SetCursor(Cursors.Wait);
            //TasksVM.InitFullTree();
            _generate_Full_Tree(selectedTaskNode);
            Mouse.SetCursor(Cursors.Arrow);


            _addNearTaskCommand = new RelayCommand(AddNearTask, CanAddNearTask);
            _addChildTaskCommand = new RelayCommand(AddChildTask, CanAddChildTask);
            _duplicateTaskCommand = new RelayCommand(DuplicateTask, CanDuplicateTask);
            _deleteTaskCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            _editTaskCommand = new RelayCommand(EditTask, CanEditTask);
            _transferTaskCommand = new RelayCommand(TransferTask, CanTransferTask);
            _showTaskCommand = new RelayCommand(ShowTask, CanShowTask);
            _moveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            _moveDownCommand = new RelayCommand(MoveDown, CanMoveDown);
            _filterTaskCommand = new RelayCommand(FilterTree, (_) => true);

            //Права на удаление и редактирование
            var levels = Context.levelWork.Read_AllLevels();

            HaveRight = false;

            FaveTreeRoots = faveRoots;

            AcceptCommand = new RelayCommand(Accept, (_) => true); // todo чем чётче мы показываем намерения, тем легче программа 
            CancelCommand = new RelayCommand(Cancel, (_) => true); // в данном случае у нас return true всегда, наглядней было бы CancelCommand = new RelayCommand(Cancel, (_) => true);

            //MessengerInstance.Register<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);
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
                {
                    FavouritingTask = _selectedTaskNode.Task;
                    HaveRight = (GlobalInfo.CurrentUser.ID == SelectedTaskNode.Task.ResponsibleID) || (GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor");
                    //SetField<TreeNode>(ref _selectedTaskNode, value); // todo вроде c# по параметру сам должен распознать тип Generic
                }
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

        public void _generate_Full_Tree(TreeNode selectedTaskNode)
        {
            AllTreeRoots = new ObservableCollection<TreeNode>();
            foreach (var taskNode in TasksVM.DictionaryFull)
            {
                if (taskNode.Value.ParentNode == null)
                    AllTreeRoots.Add(taskNode.Value);
                if (selectedTaskNode != null && TreeHelper.IsEqualTreeNodes(selectedTaskNode, taskNode.Value))
                {
                    var curNode = taskNode.Value;
                    curNode.IsExpanded = true;
                    curNode.IsSelected = true;
                    while (curNode.ParentNode != null)
                    {
                        curNode = curNode.ParentNode;
                        curNode.IsExpanded = true;
                    }
                }
            }

           //fillInd();

        }
        //int ind = 1;

        //private void fillInd()
        //{
        //    for (int j = 0; j < AllTreeRoots.Count; ++j)
        //    {
        //        var node = AllTreeRoots[j];
        //        FillChildTree(node);
        //        ind++;
        //    }
        //}
        //private void FillChildTree(TreeNode node)
        //{
        //    node.Task.IndexNumber = ind;
        //    Context.taskWork.Update_Task(node.Task);

        //    for (int i = 0; i < node.TreeNodes.Count; ++i)
        //    {
        //        ind++;
        //        var nd = node.TreeNodes[i];
        //        FillChildTree(nd);
        //    }
        //}


        public void ChangeSelection(TreeNode value) //Нельзя в сетер - будет переполнение стека
        {
            if (_selectedTaskNode != null)
                _selectedTaskNode.IsSelected = false;

            SetField<TreeNode>(ref _selectedTaskNode, value);

            if (_selectedTaskNode != null)
                _selectedTaskNode.IsSelected = true;
        }

        private bool _isFilterEmpty;
        public bool IsFilterEmpty
        {
            get
            {
                return _isFilterEmpty;
            }
            set
            {
                _isFilterEmpty = value;
                RaisePropertyChanged("IsFilterEmpty");
            }
        }

        private string _filterTaskText;
        public string FilterTaskText
        {
            get
            {
                return _filterTaskText;
            }
            set
            {
                _filterTaskText = value;
                if (_filterTaskText == "" || _filterTaskText == null || _filterTaskText == "Поиск...")
                    IsFilterEmpty = true;
                else
                    IsFilterEmpty = false;
                RaisePropertyChanged("FilterTaskText");
            }
        }

        private readonly ICommand _filterTaskCommand;
        public ICommand FilterTaskCommand
        {
            get
            {
                return _filterTaskCommand;
            }
        }

        private void FilterTree(object obj)
        {
            var oldSelectedNode = SelectedTaskNode;
            TasksVM.FilterFullTaskText = _filterTaskText;
            _generate_Full_Tree(SelectedTaskNode);

            //Восстановить развертку
            if (oldSelectedNode == null)
                return;

            int oldSelectedNodeTaskID = oldSelectedNode.Task.ID;
            var parent = oldSelectedNode.ParentNode;
            while (parent != null)
            {
                parent.IsExpanded = true;
                parent = parent.ParentNode;
            }
            var dictItem = TasksVM.DictionaryFull.FirstOrDefault(nd => nd.Key == oldSelectedNodeTaskID);
            if (dictItem.Value != null)
            {
                SelectedTaskNode = dictItem.Value;
                SelectedTaskNode.IsExpanded = true;
            }
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
            FilterTaskText = "";
            FilterTree(obj);
            Task newTask = new Task();
            if (SelectedTaskNode != null && SelectedTaskNode.Task.ID != SelectedTaskNode.Task.ParentTaskID)
                newTask.ParentTaskID = SelectedTaskNode.Task.ParentTaskID;
            newTask.TaskName = "Новая задача";
            newTask.CreateDate = DateTime.Now;

            int newIndexNumber = TasksVM.DictionaryFull.Max(t => (int)t.Value.Task.IndexNumber) + 1;
            if (newTask.ParentTaskID != null)
            {
                var parentNode = TasksVM.DictionaryFull.FirstOrDefault(n => n.Value.Task.ID == newTask.ParentTaskID).Value;
                if (parentNode != null)
                    newIndexNumber = parentNode.TreeNodes.Max(n => (int)n.Task.IndexNumber) + 1;
            }
            newTask.IndexNumber = newIndexNumber;

            dialog = new View.AddDialogWindow(new TaskDialogViewModel(this, newTask, TaskCommandEnum.Add, SelectedTaskNode.ParentNode));
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
            FilterTaskText = "";
            FilterTree(obj);
            Task newTask = new Task();
            newTask.ParentTaskID = SelectedTaskNode.Task.ID;
            newTask.TaskName = "Новая подзадача";
            newTask.CreateDate = DateTime.Now;

            int newIndexNumber = TasksVM.DictionaryFull.Max(t => (int)t.Value.Task.IndexNumber) + 1;
            if (SelectedTaskNode.TreeNodes.Count > 0)
                newIndexNumber = SelectedTaskNode.TreeNodes.Max(n => (int)n.Task.IndexNumber) + 1;
            else
                newIndexNumber = (int)SelectedTaskNode.Task.IndexNumber + 1;
            newTask.IndexNumber = newIndexNumber;

            dialog = new View.AddDialogWindow(new TaskDialogViewModel(this, newTask, TaskCommandEnum.Add, SelectedTaskNode));
            dialog.Show();
        }

        private readonly ICommand _duplicateTaskCommand;
        public ICommand DuplicateTaskCommand
        {
            get
            {
                return _duplicateTaskCommand;
            }
        }
        private bool CanDuplicateTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }
        private void DuplicateTask(object obj)
        {
            FilterTaskText = "";
            FilterTree(obj);

            var dlg = new View.DuplicateTaskDialogView(SelectedTaskNode.Task.ID);
            dlg.ShowDialog();
        }
        #endregion

        #region Show Task

        private readonly ICommand _showTaskCommand;
        public ICommand ShowTaskCommand
        {
            get
            {
                return _showTaskCommand;
            }
        }

        private bool CanShowTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null;
        }
        private void ShowTask(object obj)
        {
            FilterTaskText = "";
            FilterTree(obj);
            var isEnabled = false;
            dialog = new View.EditDialogWindow(new TaskDialogViewModel(this, SelectedTaskNode.Task, TaskCommandEnum.Edit, SelectedTaskNode, isEnabled));
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
            FilterTaskText = "";
            FilterTree(obj);
            dialog = new View.EditDialogWindow(new TaskDialogViewModel(this, SelectedTaskNode.Task, TaskCommandEnum.Edit, SelectedTaskNode));
            dialog.Show();
        }

        #endregion

        #region Delete Task

        private bool _haveRight;
        public bool HaveRight
        {
            get
            {
                return _haveRight;
            }
            set
            {
                SetField(ref _haveRight, value);
            }
        }

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
        private void DeleteTask(object obj)
        {
            FilterTaskText = "";
            FilterTree(obj);
            //Roots
            int delTaskID = SelectedTaskNode.Task.ID;
            var curNodeToDelete = SelectedTaskNode;
            if (TasksVM.DeleteWithChildren(delTaskID) == false) //дети узла дерева удалятся внутри
                return;

            if (AllTreeRoots.Contains(curNodeToDelete))
                AllTreeRoots.Remove(curNodeToDelete);

            //if (TasksVM.Dictionary.ContainsKey(SelectedTaskNode.Task.ID))
            //    TasksVM.Dictionary.Remove(SelectedTaskNode.Task.ID);


            if (TasksVM.DictionaryFull.ContainsKey(delTaskID + 1))
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
            int curI = (int)SelectedTaskNode.Task.IndexNumber;
            TreeNode newSeleted = SelectedTaskNode;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                parentNode.TreeNodes[index].Task.IndexNumber = parentNode.TreeNodes[index - 1].Task.IndexNumber;
                parentNode.TreeNodes[index - 1].Task.IndexNumber = curI;

                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));
                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index - 1].Task));

                parentNode.TreeNodes.Move(index, index - 1);
                ChangeSelection(parentNode.TreeNodes[index - 1]);
            }
            else
            {
                int index = AllTreeRoots.IndexOf(SelectedTaskNode);
                AllTreeRoots[index].Task.IndexNumber = AllTreeRoots[index - 1].Task.IndexNumber;
                AllTreeRoots[index - 1].Task.IndexNumber = curI;

                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index].Task));
                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index - 1].Task));

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
            int curI = (int)SelectedTaskNode.Task.IndexNumber;

            if (SelectedTaskNode.ParentNode != null)
            {
                TreeNode parentNode = SelectedTaskNode.ParentNode;
                int index = parentNode.TreeNodes.IndexOf(SelectedTaskNode);
                parentNode.TreeNodes[index].Task.IndexNumber = parentNode.TreeNodes[index + 1].Task.IndexNumber;
                parentNode.TreeNodes[index + 1].Task.IndexNumber = curI;

                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));        // todo было бы прикольно вызывать так: parentNode.TreeNodes[index].Task.EditCommand()
                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, parentNode.TreeNodes[index + 1].Task));

                parentNode.TreeNodes.Move(index, index + 1);
                ChangeSelection(parentNode.TreeNodes[index + 1]);
            }
            else
            {
                int index = AllTreeRoots.IndexOf(SelectedTaskNode);
                AllTreeRoots[index].Task.IndexNumber = AllTreeRoots[index + 1].Task.IndexNumber;
                AllTreeRoots[index + 1].Task.IndexNumber = curI;

                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index].Task));
                DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.Edit, AllTreeRoots[index + 1].Task));

                AllTreeRoots.Move(index, index + 1);
                ChangeSelection(AllTreeRoots[index + 1]);
            }
        }

        #endregion

        #region Do Task: Add, Edit

        //private void _doTaskCommand(KeyValuePair<TaskCommandEnum, Task> pair)
        public void DoTaskCommand(KeyValuePair<TaskCommandEnum, Task> pair)
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

                    TasksVM.Edit(task, true);
                    newNode = TasksVM.DictionaryFull[task.ID];

                    if (index != -1 && newNode.ParentNode == null)
                        UpdateRootNode(index, newNode);
                    else if (newNode.ParentNode == null)
                        AddRootNode(newNode);
                    else
                        DeleteRootNode(oldNode);

                    if (oldNode.ParentNode != newNode.ParentNode)
                    {
                        Context.procedureWork.RepareUserFave(task.ID);
                        TasksVM.Init_tracker = false;
                        TasksVM.InitFave();
                    }
                    TaskFaveVM.DoTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, task));
                //    MessengerInstance.Send<KeyValuePair<FaveTaskCommandEnum, Task>>(
                //new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, task)); //todo Настя сделать ссылки на task из общего словаря, чтобы не пришлось пробрасывать изменения в избранное
                    break;
            }
        }

        #endregion

        #region Commands       

        private readonly ICommand _transferTaskCommand;
        public ICommand TransferTaskCommand
        {
            get
            {
                return _transferTaskCommand;
            }
        }
        private bool CanTransferTask(object obj)
        {
            return SelectedTaskNode != null && dialog == null && MainWindow.IsEnable;
        }

        private void TransferTask(object obj)
        {
            FilterTaskText = "";
            FilterTree(obj);
            TransferTaskView dlg = new TransferTaskView(SelectedTaskNode.Task.ID);
            dlg.Show();
        }

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
            //Mouse.OverrideCursor = Cursors.Wait;
            Mouse.SetCursor(Cursors.Wait);


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
            //Mouse.OverrideCursor = Cursors.Arrow;
            Mouse.SetCursor(Cursors.Arrow);
            MessageBox.Show("Задача: " + _favouritingTask.TaskName + " добавлена в избранное", "Добавление в избранное", MessageBoxButton.OK, MessageBoxImage.Information);
            //MessengerInstance.Unregister<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);

        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        public void Cancel(object obj)
        {
            //MessengerInstance.Unregister<KeyValuePair<TaskCommandEnum, Task>>(this, _doTaskCommand);

            if (dialog != null)
            {
                dialog.Close();
                dialog = null;
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            FilterTaskText = "";
            FilterTree(sender);
            //ChangeSelection(null);
            //MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
            //    new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
            dialog = null;
        }

        #endregion

    }
}
