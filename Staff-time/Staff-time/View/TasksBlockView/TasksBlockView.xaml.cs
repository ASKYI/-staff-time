using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Dictionary<string, int> columnsSortOrder; //0 - asc, 1 - desc

        public TasksBlockView()
        {
            InitializeComponent();
            DataContext = context;
            columnsSortOrder = new Dictionary<string, int>();
            columnsSortOrder.Add("FromUser", -1);
            columnsSortOrder.Add("TaskName", -1);
            columnsSortOrder.Add("DateTransfer", 0);
            columnsSortOrder.Add("Note", -1);
            ((ViewModel.TasksFaveViewModel)DataContext).FilterTaskText = "Поиск...";//подсказка
            FilterTBox.Foreground = Brushes.Gray;
        }
        
        private void Filter_GotFocus(object sender, EventArgs e)//происходит когда элемент стает активным
        {
            if (((ViewModel.TasksFaveViewModel)DataContext).FilterTaskText == "Поиск...")
                ((ViewModel.TasksFaveViewModel)DataContext).FilterTaskText = "";
            else
                FilterTBox.Dispatcher.BeginInvoke(new Action(() => FilterTBox.SelectAll()));

            FilterTBox.Foreground = Brushes.Black;
        }
        private void FilterClick(object sender, EventArgs e)//происходит когда элемент стает активным
        {
            if (((ViewModel.TasksFaveViewModel)DataContext).FilterTaskText != "" && ((ViewModel.TasksFaveViewModel)DataContext).FilterTaskText != null)
                FilterTBox.Background = new SolidColorBrush(Color.FromRgb(222, 240, 243));
            else
                FilterTBox.Background = Brushes.White;
        }
        private void ClearFilterClick(object sender, EventArgs e)//происходит когда элемент стает активным
        {
            FilterTBox.Text = "";
            FilterTBox.Background = Brushes.White;
            context.FilterTaskCommand.Execute(sender);
        }

        private void OnKeyDownFilter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                context.FilterTaskCommand.Execute(sender);
                return;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewmodel = (ViewModel.TasksFaveViewModel)DataContext;
            viewmodel.SelectedRequests = lvRequests.SelectedItems
                .Cast<RequestItem>()
                .ToList();
        }

        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();

            int newOrder = (columnsSortOrder[sortBy] + 1) % 2;
            columnsSortOrder[sortBy] = newOrder;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvRequests.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(sortBy, (ListSortDirection)newOrder));
            view.Refresh();
        }

        private void KeyDownMainWindow(object sender, KeyEventArgs e)
        {
            //if (MainWindow.IsEnable)
                //if (e.Key == Key.W)
                //    context.AddWorkCommand.Execute(sender);
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!MainWindow.IsEnable)
                return;
            context.ChangeSelection(((TreeViewItem)sender).DataContext as ViewModel.TreeNode);

            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem();

            item = new MenuItem();
            item.Header = "Просмотр информации о задаче";
            item.Command = context.ShowTaskCommand;
            menu.Items.Add(item);

            var curNode = ((TreeViewItem)sender).DataContext as ViewModel.TreeNode;
            if (curNode != null && ((GlobalInfo.CurrentUser.ID == curNode.Task.ResponsibleID) || GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor"))
            {
                item = new MenuItem();
                item.Header = "Редактировать задачу";
                item.Command = context.EditTaskCommand;
                menu.Items.Add(item);
            }

            item = new MenuItem();
            item.Header = "Передать задачу";
            item.Command = context.TransferTaskCommand;
            menu.Items.Add(item);

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

        //private void TreeViewItem_doubleclick(object sender, RoutedEventArgs e)
        //{
        //    var curNode = ((TreeViewItem)e.Source).DataContext as ViewModel.TreeNode;
        //    if (curNode != ((ViewModel.TasksFaveViewModel)DataContext).SelectedTaskNode)
        //        return;
        //    ((ViewModel.TasksFaveViewModel)DataContext).AddWorkCommand.Execute(sender);
        //}
    }
}
