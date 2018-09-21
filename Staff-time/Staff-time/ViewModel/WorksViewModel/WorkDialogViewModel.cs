using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using Staff_time.Model.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using System.Windows.Input;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public class WorkDialogViewModel : MainViewModel
    {
        public WorkDialogViewModel(WorkControlViewModelBase work)
        {
            WorkVM = work;
            _generate_Tree();
            Message = "Выбор задачи";
          //  ChangeSelection(TasksVM.Dictionary[work.Work.TaskID]);

            AcceptCommand = new RelayCommand(Accept, CanAccept);
            CancelCommand = new RelayCommand(Cancel, CanCancel);

            WorkVM.IsEdititig = false;
        }

        private WorkControlViewModelBase _workVM;
        public WorkControlViewModelBase WorkVM
        {
            get { return _workVM; }
            set
            {
                SetField(ref _workVM, value);
            }
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
            //if (_selectedTaskNode != null)
            //    _selectedTaskNode.IsSelected = false;

            //SetField<TreeNode>(ref _selectedTaskNode, value);

            //if (_selectedTaskNode != null)
            //    _selectedTaskNode.IsSelected = true;
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

        #endregion

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetField(ref _message, value);
            }
        }

        #region Commands

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private bool CanAccept(object obj)
        {
            return true;
        }
        public void Accept(object obj)
        {
            WorkVM.Work.TaskID = SelectedTaskNode.Task.ID;
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
                (WorkCommandEnum.Update, WorkVM.Work));
        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        public void Cancel(object obj)
        {
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
               (WorkCommandEnum.None, WorkVM.Work));
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            MessengerInstance.Send<KeyValuePair<WorkCommandEnum, Work>>(new KeyValuePair<WorkCommandEnum, Work>
               (WorkCommandEnum.None, WorkVM.Work));
            dialog = null;
        }

        #endregion

    }
}
