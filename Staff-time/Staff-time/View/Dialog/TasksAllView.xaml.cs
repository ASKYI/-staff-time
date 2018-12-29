using Staff_time.ViewModel;
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
using System.Windows.Shapes;


namespace Staff_time.View
{
    /// <summary>
    /// Interaction logic for AllTreeDialog.xaml
    /// </summary>
    public partial class AllTreeDialog : IDialogView
    {
        ViewModel.TasksAllViewModel context;
        public AllTreeDialog()
        {
            InitializeComponent();
        }
        public AllTreeDialog(object _context)
        {
            InitializeComponent();
            base.DataContext = _context;
            context = (TasksAllViewModel)_context;
            Closing += ((ViewModel.TasksAllViewModel)DataContext).OnWindowClosing; // todo аналогичное уже было
        }
        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            //     if (((ViewModel.TaskDialogViewModel)DataContext).ToClose)
            //      this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllTreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
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

            (sender as TreeViewItem).ContextMenu = menu;
        }
    }
}
