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
        public ObservableCollection<TreeNode> Tree { get; set; }

        //Можно объединить с BaseViewModel.Generate_TaskNodesDictionary(), что может ускорить, 
        //но противоречит логике организации кода (если она у меня есть).
        private void Generate_Tree()
        {
            //Генерируется список смежности. Корни в коллекции Tree.
            Tree = new ObservableCollection<TreeNode>();

            foreach (var taskNode in TaskNodesDictionary)
            {
                if (taskNode.Value.ParentNode == null)
                    Tree.Add(taskNode.Value); //Это корень
            }
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

            });
        }
    }
}
