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
using Staff_time.Helpers;
using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.View;
using Staff_time.View.Dialog;
using Staff_time.ViewModel;         // todo после окончания работы над файлом желательно убрать лишние using
using Staff_time.ViewModel.LoginViewModel;


namespace Staff_time
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string version = "3.2";
        MainViewModel context;
        private static bool _isEnable;
        public static bool IsEnable
        {
            get
            {
                return _isEnable;
            }
            set
            {
                _isEnable = value;
                OnGlobalPropertyChanged("IsMainWindowEnabled");
            }
        }
        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += DumpMaker.CurrentDomain_UnhandledException;

            SplashScreen splashScreen = new SplashScreen("Resources/appImage.png");
            splashScreen.Show(true);
            if (Context.IsContextExist())
                Context.ReloadContext();
            else
                Context.Init();
            IsEnable = true;
            bool? isOK = Authorization.Login();

            if (isOK == false)
            {
                Window_Closing(null, null);
                Close();
                Environment.Exit(0);
            }
            else
            {
                try
                {
                    splashScreen.Show(true);

                    context = new MainViewModel(true);
                    InitializeComponent();
                    Title = "Учёт трудозатрат, v" + version;
                    DataContext = context;
                    this.Show();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            Closing += this.Window_Closing;
        }

        public void LogoutEvent(object sender, EventArgs e)
        {
            OnGlobalPropertyChanged("MainWindowClosing");

            if (!MainWindow.IsEnable)
            {
                var result = MessageBox.Show("Есть несохраненные изменения в работе. Сохранить?", "Сообщение", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                    context.ApplyChanges();
                else
                    if (result == MessageBoxResult.No)
                    context.CancelEditing();
                else
                    return;
            }
            var oldMainWindow = this;
            oldMainWindow.Window_Closing(null, null);
            this.Hide();
            TasksVM.Init_tracker = false;
            WorksVM.init_tracker = false;
            MainViewModel.init_tracker = false;

            new MainWindow();
            oldMainWindow.Close();
        }

        public void ShowTimeStatistics(object sender, EventArgs e)
        {
            TimeStatisticsWindow dlg = new TimeStatisticsWindow();
            dlg.ShowDialog();
        }


        public void About_Program_Click(object sender, EventArgs e)
        {
            AboutWindow dlg = new AboutWindow();
            dlg.ShowDialog();
        }

        public void ShowUserSettings(object sender, EventArgs e)
        {
            UserSettingsWindow dlg = new UserSettingsWindow();
            dlg.ShowDialog();
        }

        private System.Windows.Forms.NotifyIcon notifyIcon = null;

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GlobalInfo.UserOptions.IsCollapseTray)
            {
                notifyIcon = new System.Windows.Forms.NotifyIcon();
                notifyIcon.Click += new EventHandler(notifyIcon_Click);
                notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
                notifyIcon.Icon = Properties.Resources.appImage;

                notifyIcon.Visible = true;
                this.ShowInTaskbar = true;
            }
        }
        private void Window_State_Changed(object sender, EventArgs e)
        {
            if (GlobalInfo.UserOptions.IsCollapseTray)
            {
                var window = (MainWindow)sender;
                if (window.WindowState == WindowState.Minimized)
                    this.ShowInTaskbar = false;
                else
                    this.ShowInTaskbar = true;
            }
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
            //context.CancelEditing();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Authorization.Logout();

            if (notifyIcon == null)
                return;
            if (notifyIcon.Icon != null)
            {
                notifyIcon.Icon.Dispose();
                notifyIcon.Icon = null;
            }
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
        }

        #region INotifyPropertyChanged
        public static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };

        public static void OnGlobalPropertyChanged(string propertyName)
        {
            GlobalPropertyChanged(
                typeof(WorkspaceViewModel),
                new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
