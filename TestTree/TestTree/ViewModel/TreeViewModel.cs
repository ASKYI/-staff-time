using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using TestTree.Model;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace TestTree.ViewModel
{
    public class TreeViewModel : MainViewModel
    {
        private ReadOnlyObservableCollection<TreeNode> _treeRoot;
        public ReadOnlyObservableCollection<TreeNode> TreeRoot
        {
            get { return _treeRoot; }
            set { SetField(ref _treeRoot, value); }
        }

        //Можно объединить с BaseViewModel.Generate_TaskNodesDictionary(), что может ускорить, 
        //но противоречит логике организации кода (если она у меня есть).
        private void Generate_Tree()
        {
            TreeNode root = TreeRoot[0];

            foreach (var taskNode in TaskNodesDictionary)
            {
                if (taskNode.Value.ParentNode == null)
                {
                    if (taskNode.Value.Task.ID == 1)
                        root.TreeNodeCustomers.Add((TreeNodeCustomer)taskNode.Value);
                    else
                        root.TreeNodes.Add(taskNode.Value);
                }
            }

            /* TreeRoot = new TreeNode();
             foreach (var taskNode in TaskNodesDictionary)
             {
                 if (taskNode.Value.ParentNode == null)
                 {
                     if (taskNode.Value.Task.ID == 1)
                         TreeRoot.TreeNodeCustomers.Add((TreeNodeCustomer)taskNode.Value);
                     else
                         TreeRoot.TreeNodes.Add(taskNode.Value);
                 }
             }

             \
             foreach (var taskNode in TaskNodesDictionary)
             {
                if (taskNode.Value.ParentNode == null)
                     Tree.Add(taskNode.Value); //Это корень
             }*/
        }
        private readonly ICommand _selectTaskCommand;
        public ICommand SelectTaskCommand
        {
            get
            {
                return _selectTaskCommand;
            }
        }
        public TreeViewModel() : base()
        {
            //Создание узла дерева типа нулевой задачи
            //HACK: Если можно привязать узел к TreeView, а не коллекцию узлов первого уровня
            ObservableCollection<TreeNode> root = new ObservableCollection<TreeNode>();
            root.Add(new TreeNode());
            TreeRoot = new ReadOnlyObservableCollection<TreeNode>(root);

            Generate_Tree();
            _selectTaskCommand = new RelayCommand(SelectTask, CanSelectTask);
        }

        private bool CanSelectTask(object obj)
        {
            return true;
        }
        private void SelectTask(object obj)
        {
            MessengerInstance.Register<NotificationMessage<TreeNode>>(this, (selectedTask) =>
            {
                Status += "Получен выбранный узел\n";
            });
        }
    }
}
