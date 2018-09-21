using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для TasksBlockView.xaml
    /// </summary>
    public partial class TasksBlockView : UserControl
    {
        ViewModel.TasksBlockViewModel context = 
            new ViewModel.TasksBlockViewModel();
        public TasksBlockView()
        {
            InitializeComponent();
            DataContext = context;
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            context.ChangeSelection(((TreeViewItem)sender).DataContext as ViewModel.TreeNode);

            ContextMenu menu = new ContextMenu();

            MenuItem item = new MenuItem();
            item.Header = "Добавить задачу рядом";
            item.Command = context.AddNearTaskCommand;
            menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Добавить подзадачу";
            item.Command = context.AddChildTaskCommand;
            menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Редактировать задачу";
            item.Command = context.EditTaskCommand;
            menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Удалить задачу";
            item.Command = context.DeleteTaskCommand;
            menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Добавить работу";
            item.Command = context.AddWorkCommand;
            menu.Items.Add(item);

            (sender as TreeViewItem).ContextMenu = menu;
        }
    }
}
