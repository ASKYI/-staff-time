using Staff_time.Model.UserModel;
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
            var levels = Context.levelWork.Read_AllLevels();
            if (GlobalInfo.CurrentUser.LevelID == levels["Editor"])
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

        private void SortTime_Click(object sender, RoutedEventArgs e)
        {
            SortNameButton.Background = Brushes.White;
            if (SortTimeButton.Content == FindResource("TimeDesc"))
            {
                SortTimeButton.Content = FindResource("TimeNone");
                SortTimeButton.Background = Brushes.White;
            }
            else
            {
                if (SortTimeButton.Content == FindResource("TimeNone"))
                    SortTimeButton.Content = FindResource("TimeAsc");
                else
                    SortTimeButton.Content = FindResource("TimeDesc");
                SortTimeButton.Background = new SolidColorBrush(Color.FromRgb(222, 240, 243));

            }
        }

        private void SortName_Click(object sender, RoutedEventArgs e)
        {
            SortTimeButton.Background = Brushes.White;

            if (SortNameButton.Content == FindResource("NameDesc"))
            {
                SortNameButton.Content = FindResource("NameNone");
                SortNameButton.Background = Brushes.White;
            }
            else
            {
                if (SortNameButton.Content == FindResource("NameNone"))
                    SortNameButton.Content = FindResource("NameAsc");
                else
                    SortNameButton.Content = FindResource("NameDesc");
                SortNameButton.Background = new SolidColorBrush(Color.FromRgb(222, 240, 243));
            }
        }

        private void PrevWeek(object sender, EventArgs e)
        {
            var oldDate = ((ViewModel.WorkspaceViewModel)DataContext).SelectedDate_Picker;
            oldDate = oldDate.AddDays(-7);
            ((ViewModel.WorkspaceViewModel)DataContext).SelectedDate_Picker = oldDate;
        }

        private void NextWeek(object sender, EventArgs e)
        {
            var oldDate = ((ViewModel.WorkspaceViewModel)DataContext).SelectedDate_Picker;
            oldDate = oldDate.AddDays(7);
            ((ViewModel.WorkspaceViewModel)DataContext).SelectedDate_Picker = oldDate;
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

    public class TabControlMy: TabControl
    {
        
    }
}
