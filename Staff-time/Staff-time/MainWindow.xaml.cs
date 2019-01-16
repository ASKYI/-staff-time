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
using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;         // todo после окончания работы над файлом желательно убрать лишние using
using Staff_time.ViewModel.LoginViewModel;


namespace Staff_time
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string version = "1.2";
        MainViewModel context;
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        public MainWindow()
        {
            Context.Init();
          
            //RegistryKey currentUserKey = Registry.CurrentUser;
            //RegistryKey softWareKey = currentUserKey.OpenSubKey("SOFTWARE", true);
            //RegistryKey stuffTimeKey = softWareKey.CreateSubKey("ChemSoftTimeManager");
            //stuffTimeSettingsKey = stuffTimeKey.CreateSubKey("Settings");
            //var lastUserID = stuffTimeSettingsKey.GetValue("lastUserID");
            //if (lastUserID == null)
            //    lastUserID = 0;

            //var dialog = new View.Dialog.LoginWindow(users, (int)lastUserID);
            bool? isOK = Authorization.Login();

            if (isOK == false)
                Environment.Exit(0);
            else
            {
                context = new MainViewModel();
                InitializeComponent();
                Title = "Учёт трудозатрат, v" + version;
                DataContext = context;
                this.Show();
            }
            Closing += this.Window_Closing;
        }

        public void LogoutEvent(object sender, EventArgs e)
        {
            var oldMainWindow = this;
            this.Hide();
            TasksVM.Init_tracker = false;
            WorksVM.init_tracker = false;
            MainViewModel.init_tracker = false;

            new MainWindow();
            oldMainWindow.Close();
            //this.Hide();
            //Authorization.Login();
        }


        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Click += new EventHandler(notifyIcon_Click);
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            notifyIcon.Icon = Properties.Resources.appImage;

            notifyIcon.Visible = true;
            this.ShowInTaskbar = true;
        }
        private void Window_State_Changed(object sender, EventArgs e)
        {
            var window = (MainWindow)sender;
            if (window.WindowState == WindowState.Minimized)
                this.ShowInTaskbar = false;
            else
                this.ShowInTaskbar = true;
        }

        private void moveSplitter(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }
        private void leaveSplitter(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Maximized;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Maximized;
        }

        private void TasksBlockView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            context.CancelEditing();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (notifyIcon.Icon != null)
            {
                notifyIcon.Icon.Dispose();
                notifyIcon.Icon = null;
            }
            if (notifyIcon != null)
                notifyIcon.Dispose();
            Authorization.Logout();
        }
    }
}
