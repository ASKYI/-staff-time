using Microsoft.Win32;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : UserControl
    {
        public WorkspaceView()
        {
            InitializeComponent();
            DataContext = new ViewModel.WorkspaceViewModel();
            if (GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor")
                EditPlanTimeBtn.Visibility = Visibility.Visible;
            else
                EditPlanTimeBtn.Visibility = Visibility.Hidden;
        }
        //void TabChanged(object sender, SelectionChangedEventArgs args)
        //{
        //    if (!((ViewModel.WorkspaceViewModel)DataContext).IsMainWindowEnabled)
        //    {
        //        var tabs = (System.Windows.Controls.TabControl)sender;
        //        //var tabs = (System.Windows.Forms.TabControl)sender;

        //        //foreach (var tab in tabs.TabPages)
        //        //    ((System.Windows.Forms.TabPage)tabs.TabPages[tabs.SelectedIndex]).Enabled = false;

        //        //((System.Windows.Forms.TabPage)tabs.TabPages[tabs.SelectedIndex]).Enabled = true;
        //    }
        //}


        private void PrevWeek(object sender, EventArgs e)
        {
            var oldDate = ((ViewModel.WorkspaceViewModel)DataContext).SelectedDate_Picker;
            oldDate = oldDate.AddDays(-7);
            ((ViewModel.WorkspaceViewModel)DataContext).SetDate(oldDate);
        }

        private void NextWeek(object sender, EventArgs e)
        {
            var oldDate = ((ViewModel.WorkspaceViewModel)DataContext).SelectedDate_Picker;
            oldDate = oldDate.AddDays(7);
            ((ViewModel.WorkspaceViewModel)DataContext).SetDate(oldDate);
            
        }

        private void OpenChemic_Click(object sender, EventArgs e)
        {
            try
            {
                var connString = "Provider=SQLNCLI11.1;Password=1;User ID=TaskManagementChemic;Data Source=MSSQL2012-WIN12;Application Name=LISChemic;MARS Connection=True";
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey softWareKey = currentUserKey.OpenSubKey("SOFTWARE", true);
                RegistryKey stuffTimeKey = softWareKey.OpenSubKey("НИИ ВН", true);
                var chemicConnectionKey = stuffTimeKey.OpenSubKey("АРМ «Химик-аналитик»", true);
                chemicConnectionKey.SetValue("Connect", connString);

                string folderPath = @"\\13.1.77.200\Share\Programmers\_Картотека_UM\Chemic15.exe";
                System.Diagnostics.Process.Start(folderPath);
            }
            catch
            {

            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as Calendar).SelectedDate = DateTime.Now.Date;
            this.MyDatePicker.IsDropDownOpen = false;
        }

        private void EditPlanTime(object sender, RoutedEventArgs e)
        {
            PlanTimeTbox.IsEnabled ^= true;
        }
        private void PlanTimeGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Dispatcher.BeginInvoke(new Action(() => text.SelectAll()));
        }
        private void PlanTimeKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else if (e.Key == Key.Enter)
            {
                PlanTimeTbox.IsEnabled ^= true;
                EditPlanTimeBtn.Focus();
                e.Handled = false;
            }
            else if ((e.Key == Key.Decimal || e.Key == Key.OemPeriod) && text.Text.Length > 0)
                e.Handled = false;
            else
                e.Handled = true;
        }

    }

    public class MyCommands
    {
        public static RoutedCommand SelectToday = new RoutedCommand("Today", typeof(MyCommands));
    }

    public class TabControlMy : TabControl
    {

    }

    public class ScrollViewerBehavior
    {
        public static bool GetAutoScrollToTop(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToBottomProperty);
        }

        public static void SetAutoScrollToBottom(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToBottomProperty, value);
        }

        public static readonly DependencyProperty AutoScrollToBottomProperty =
            DependencyProperty.RegisterAttached("AutoScrollToBottom", typeof(bool), typeof(ScrollViewerBehavior), new PropertyMetadata(false, (o, e) =>
            {
                var scrollViewer = o as ScrollViewer;
                if (scrollViewer == null)
                {
                    return;
                }
                if ((bool)e.NewValue)
                {
                    scrollViewer.ScrollToBottom();
                    SetAutoScrollToBottom(o, false);
                }
            }));
    }
}
