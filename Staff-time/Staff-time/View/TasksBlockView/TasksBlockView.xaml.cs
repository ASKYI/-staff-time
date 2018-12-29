using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для TasksBlockView.xaml
    /// </summary>
    public partial class TasksBlockView : UserControl
    {
        ViewModel.TasksFaveViewModel context = 
            new ViewModel.TasksFaveViewModel();
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

            //item.Header = "Добавить задачу рядом";
            //item.Command = context.AddNearTaskCommand;
            //menu.Items.Add(item);

            //item = new MenuItem();
            //item.Header = "Добавить подзадачу";
            //item.Command = context.AddChildTaskCommand;
            //menu.Items.Add(item);

            //item = new MenuItem();
            //item.Header = "Редактировать задачу";
            //item.Command = context.EditTaskCommand;
            //menu.Items.Add(item);

            //item = new MenuItem();
            //item.Header = "Удалить задачу";
            //item.Command = context.DeleteTaskCommand;
            //menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Добавить работу";
            item.Command = context.AddWorkCommand;
            menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Удалить из избранного";
            item.Command = context.DeleteTaskCommand;
            menu.Items.Add(item);

            (sender as TreeViewItem).ContextMenu = menu;
        }
    }
}
