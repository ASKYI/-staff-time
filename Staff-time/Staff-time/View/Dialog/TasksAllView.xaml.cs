using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        extern private static int SetWindowLong(IntPtr hwnd, int index, int value);

        public AllTreeDialog(object _context)
        {
            InitializeComponent();
            base.DataContext = _context;
            context = (TasksAllViewModel)_context;
            ((ViewModel.TasksAllViewModel)DataContext).FilterTaskText = "Поиск...";//подсказка
            FilterTBox.Foreground = Brushes.Gray;

            Closing += ((ViewModel.TasksAllViewModel)DataContext).OnWindowClosing;

            this.SourceInitialized += (x, y) =>
            {
                this.HideMinimizeAndMaximizeButtons();
            };
        }

        private void Filter_GotFocus(object sender, EventArgs e)
        {
            if (((ViewModel.TasksAllViewModel)DataContext).FilterTaskText == "Поиск...")
                ((ViewModel.TasksAllViewModel)DataContext).FilterTaskText = "";
            else
                FilterTBox.Dispatcher.BeginInvoke(new Action(() => FilterTBox.SelectAll()));

            FilterTBox.Foreground = Brushes.Black;
        }
        private void FilterClick(object sender, EventArgs e)
        {
            if (((ViewModel.TasksAllViewModel)DataContext).FilterTaskText != "" && ((ViewModel.TasksAllViewModel)DataContext).FilterTaskText != null)
                FilterTBox.Background = new SolidColorBrush(Color.FromRgb(222, 240, 243));
            else
                FilterTBox.Background = Brushes.White;
        }
        private void ClearFilterClick(object sender, EventArgs e)
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

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllTreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var curNode = ((TreeViewItem)sender).DataContext as ViewModel.TreeNode;
            context.ChangeSelection(curNode);

            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem();

            item.Header = "Добавить задачу рядом";
            item.Command = context.AddNearTaskCommand;
            menu.Items.Add(item);

            item = new MenuItem();
            item.Header = "Добавить подзадачу";
            item.Command = context.AddChildTaskCommand;
            menu.Items.Add(item);

            if ((GlobalInfo.CurrentUser.ID == curNode.Task.ResponsibleID) || GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor")
            {
                item = new MenuItem();
                item.Header = "Редактировать задачу";
                item.Command = context.EditTaskCommand;
                menu.Items.Add(item);
            }

            if (GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor")
            {
                item = new MenuItem();
                item.Header = "Удалить задачу";
                item.Command = context.DeleteTaskCommand;
                menu.Items.Add(item);
            }

            (sender as TreeViewItem).ContextMenu = menu;
        }
        private void TreeViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }

    internal static class WindowExtensions
    {
        // from winuser.h
        private const int GWL_STYLE = -16,
                          WS_MAXIMIZEBOX = 0x10000,
                          WS_MINIMIZEBOX = 0x20000;

        [DllImport("user32.dll")]
        extern private static int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        extern private static int SetWindowLong(IntPtr hwnd, int index, int value);

        internal static void HideMinimizeAndMaximizeButtons(this Window window)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
            var currentStyle = GetWindowLong(hwnd, GWL_STYLE);

            SetWindowLong(hwnd, GWL_STYLE, (currentStyle & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX));
        }
    }
}
