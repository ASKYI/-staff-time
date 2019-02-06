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

namespace Staff_time.ViewModel
{
    public enum FaveTaskCommandEnum { Add, Edit, Delete, None }


    public class TasksFaveViewModel : MainViewModel, INotifyPropertyChanged
    {
        //test
        public TasksFaveViewModel()
        {
            _generate_Tree();
            _showFullTreeCommand = new RelayCommand(ShowTree, CanShowTree);

            _collapseAllCommand = new RelayCommand(CollapseAll, CanCollapseAll);
            _saveExpandCommand = new RelayCommand(SaveCollapse, (_) => true);
            _expandAllCommand = new RelayCommand(ExpandAll, CanExpandAll);
            _addWorkCommand = new RelayCommand(AddWork, CanAddWork);
            _applyRequestCommand = new RelayCommand(ApplyRequest, CanApplyRequest);
            _deleteRequestCommand = new RelayCommand(DeleteRequest, CanDeleteRequest);
            _refreshRequestCommand = new RelayCommand(RefreshRequest, (_) => true);
            _deleteTaskCommand = new RelayCommand(DeleteFaveTask, CanDeleteTask);
            _transferTaskCommand = new RelayCommand(TransferTask, CanTransferTask);
            _moveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            _moveDownCommand = new RelayCommand(MoveDown, CanMoveDown);

            FillRequests();
            MessengerInstance.Register<KeyValuePair<FaveTaskCommandEnum, Task>>(this, _doTaskCommand);
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

        private bool CanShowTree(object obj)
        {
            return MainWindow.IsEnable;
        }

        private bool CanAddWork(object obj)
        {
            return SelectedTaskNode != null && dialog == null && MainWindow.IsEnable;
        }

        private void ShowTree(object obj)
        {
            var dialog = new View.AllTreeDialog(new ViewModel.TasksAllViewModel(TreeRoots, SelectedTaskNode));
            dialog.ShowDialog();
            _generate_Tree();
        }

        private void AddWork(object obj)
        {
            Work work = new Work();
            work.WorkName = "";
            work.TaskID = SelectedTaskNode.Task.ID;
            work.StartDate = chosenDate.Date;
            work.StartTime = new DateTime(1899, 12, 30, DateTime.Now.Hour, DateTime.Now.Minute, 0);
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
            if (SelectedTaskNode == null || dialog != null || !MainWindow.IsEnable)
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


                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));
                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, parentNode.TreeNodes[index - 1].Task));

                parentNode.TreeNodes.Move(index, index - 1);
                ChangeSelection(parentNode.TreeNodes[index - 1]);
            }
            else
            {
                int index = TreeRoots.IndexOf(SelectedTaskNode);

                TreeRoots[index].IndexNumber = TreeRoots[index - 1].IndexNumber;
                TreeRoots[index - 1].IndexNumber = curI;
                task2 = TreeRoots[index - 1].Task;

                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, TreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, TreeRoots[index - 1].Task));

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
            if (SelectedTaskNode == null || dialog != null || !MainWindow.IsEnable)
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

                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, parentNode.TreeNodes[index].Task));        // todo было бы прикольно вызывать так: parentNode.TreeNodes[index].Task.EditCommand()
                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, parentNode.TreeNodes[index + 1].Task));

                parentNode.TreeNodes.Move(index, index + 1);
                ChangeSelection(parentNode.TreeNodes[index + 1]);
            }
            else
            {
                int index = TreeRoots.IndexOf(SelectedTaskNode);
                TreeRoots[index].IndexNumber = TreeRoots[index + 1].IndexNumber;
                TreeRoots[index + 1].IndexNumber = curI;
                task2 = TreeRoots[index + 1].Task;


                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, TreeRoots[index].Task));
                _doTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.Edit, TreeRoots[index + 1].Task));

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
            return SelectedTaskNode != null && dialog == null && MainWindow.IsEnable;
        }
        private void DeleteFaveTask(object obj)
        {
            var dialogResult = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить задачу из избранного?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.No)
                return;

            //Mouse.OverrideCursor = Cursors.Wait;
            Mouse.SetCursor(Cursors.Wait);

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
            //Mouse.OverrideCursor = Cursors.Arrow;
            Mouse.SetCursor(Cursors.Arrow);
        }


        private void _doTaskCommand(KeyValuePair<FaveTaskCommandEnum, Task> pair)
        {
            FaveTaskCommandEnum command = pair.Key;
            Task task = pair.Value;

            if (!TasksVM.Dictionary.ContainsKey(task.ID))
                return;

            switch (command)
            {
                case FaveTaskCommandEnum.Edit:
                    TreeNode oldNode = TasksVM.Dictionary[task.ID];
                    int index = -1;

                    if (oldNode.ParentNode == null)
                        index = TreeRoots.IndexOf(oldNode);

                    //if (oldNode.ParentNode != TasksVM.DictionaryFull[task.ID].ParentNode)
                    //{
                    //    Context.procedureWork.RepareUserFave(task.ID);
                    //    TasksVM.Init_tracker = false;
                    //    TasksVM.InitFave();
                    //}

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

        #region RequestsRegion
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
            //Показываем диалог с пользователями
            TransferTaskView dlg = new TransferTaskView(SelectedTaskNode.Task.ID);
            dlg.Show();
            //TasksVM.TransferTask(taskID, myuserID, touserID);
            //Mouse.SetCursor(Cursors.Wait);

            ////Roots
            //int delTaskID = SelectedTaskNode.Task.ID;
            //if (TreeRoots.Contains(SelectedTaskNode))
            //    TreeRoots.Remove(SelectedTaskNode);
            ////if (TasksVM.Dictionary.ContainsKey(SelectedTaskNode.Task.ID))
            ////    TasksVM.Dictionary.Remove(SelectedTaskNode.Task.ID);


            //TasksVM.DeleteFaveWithChildren(delTaskID);

            //if (TasksVM.Dictionary.ContainsKey(delTaskID + 1))
            //    ChangeSelection(TasksVM.Dictionary[delTaskID + 1]);
            //else
            //    ChangeSelection(TasksVM.Dictionary.FirstOrDefault().Value);
            ////Mouse.OverrideCursor = Cursors.Arrow;
            //Mouse.SetCursor(Cursors.Arrow);
        }

        private readonly ICommand _refreshRequestCommand;
        public ICommand RefreshRequestCommand
        {
            get
            {
                return _refreshRequestCommand;
            }
        }
        
        void RefreshRequest(object obj)
        {
            //Context.requestWork.RefreshRequests();
            FillRequests();
        }

        private readonly ICommand _deleteRequestCommand;
        public ICommand DeleteRequestCommand
        {
            get
            {
                return _deleteRequestCommand;
            }
        }
       
       

        bool CanDeleteRequest(object obj)
        {
            return SelectedRequests.Count > 0;
        }

        void DeleteRequest(object obj)
        {
            List<int> requestsIds = new List<int>();
            foreach (var selItem in SelectedRequests)
            {
                requestsIds.Add(selItem.ID);
                RequestsList.Remove(selItem);
            }
            if (requestsIds.Count > 0)
                Context.requestWork.DeleteRequests(requestsIds);
        }

        private readonly ICommand _applyRequestCommand;
        public ICommand ApplyRequestCommand
        {
            get
            {
                return _applyRequestCommand;
            }
        }

        bool CanApplyRequest(object obj)
        {
            return SelectedRequests.Count > 0;
        }

        void ApplyRequest(object obj)
        {
            Mouse.SetCursor(Cursors.Wait);

            List<int> requestsIds = new List<int>();

            foreach (var selItem in SelectedRequests)
            {
                requestsIds.Add(selItem.ID);
                RequestsList.Remove(selItem);
                AddToFave(selItem.TaskID);
                SelectedTaskNode = TasksVM.Dictionary[selItem.TaskID];
            }
            SelectedTaskNode.IsExpanded = true;
            AddWork(null);

            if (requestsIds.Count > 0)
                Context.requestWork.DeleteRequests(requestsIds);
            Mouse.SetCursor(Cursors.Arrow);
        }

        public void AddToFave(int taskID)
        {
            if (TasksVM.IsFave(taskID))
                return;
            var taskNode = TasksVM.DictionaryFull[taskID];
            // Добавим всех родителей, если их нет в избранном
            List<TreeNode> toAddInFave = new List<TreeNode>();
            var parent = taskNode.ParentNode;
            while (parent != null && !TreeRoots.Contains(parent))
            {
                toAddInFave.Add(parent);
                parent = parent.ParentNode;
            }
            for (int i = toAddInFave.Count - 1; i >= 0; --i)
                if (!TasksVM.IsFave(toAddInFave[i].Task.ID))
                    TasksVM.AddFave(toAddInFave[i].Task);

            // Добавим себя
            TasksVM.AddFave(taskNode.Task);
        }

        private void FillRequests()
        {
            _requestsList = new ObservableCollection<RequestItem>();
            var requests = Context.requestWork.Read_AllRequests();
            foreach (var req in requests)
                _requestsList.Add(new RequestItem(req.ID, req.FromUserID, req.TaskID, req.TransferDateTime));
            SelectedRequests = new List<RequestItem>();
        }

        private ObservableCollection<RequestItem> _requestsList;
        public ObservableCollection<RequestItem> RequestsList
        {
            get { return _requestsList; }
            set
            {
                //_requestsList = value;
                SetField(ref _requestsList, value);
            }
        }

        private List<RequestItem> _selectedRequests;
        public List<RequestItem> SelectedRequests {
            get
            {
                return _selectedRequests;
            }
            set
            {
                SetField(ref _selectedRequests, value);
            }
        }
        #endregion //RequestsRegion
    }
    public class RequestItem
    {
        public int ID { get; set; }
        public string FromUser { get; set; }
        public string TaskName { get; set; }
        public string FullPathTask { get; set; }
        public int TaskID { get; set; }
        public DateTime DateTransfer { get; set; }

        public RequestItem(int _id, int _fromUserID, int _taskID, DateTime dt)
        {
            ID = _id;
            TaskID = _taskID;
            FullPathTask = TasksVM.DictionaryFull[_taskID].FullPathAsString;
            FromUser = Context.usersWork.GetUserNameByID(_fromUserID);
            TaskName = TasksVM.DictionaryFull[_taskID].Task.TaskName;
            DateTransfer = dt;
        }
    }
}
