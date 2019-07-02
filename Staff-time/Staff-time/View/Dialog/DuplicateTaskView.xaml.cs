﻿using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkDialogView.xaml
    /// </summary>
    public partial class DuplicateTaskDialogView : Window, IDialogView
    {
        public DuplicateTaskDialogView(int _taskFromID, ObservableCollection<TreeNode> nodes)
        {
            InitializeComponent();
            TaskFromID = _taskFromID;
            TreeRoots = nodes;
            DataContext = this;
        }

        private int TaskFromID { get; set; }
        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                _selectedTaskNode = value;
            }
        }

        private ObservableCollection<TreeNode> _treeRoots;
        public ObservableCollection<TreeNode> TreeRoots
        {
            get { return _treeRoots; }
            set
            {
                _treeRoots = value;
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTaskNode == null)
            {
                if (MessageBox.Show("Необходимо выбрать узел в дереве для дублирования. Сейчас узел не выбран, дублировать в корень?",
                    "Дублирование задачи", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Context.procedureWork.DuplicateTask(TaskFromID, 0, GlobalInfo.CurrentUser.ID);
                }
            }
            else
                Context.procedureWork.DuplicateTask(TaskFromID, SelectedTaskNode.Task.ID, GlobalInfo.CurrentUser.ID);
            Context.ReloadContext();
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
