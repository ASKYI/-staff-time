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
using Microsoft.Win32;
using Staff_time.Model;
using Staff_time.ViewModel;         // todo после окончания работы над файлом желательно убрать лишние using

namespace Staff_time
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string version = "1.01";
        //MainViewModel context = new MainViewModel();
        MainViewModel context;
        RegistryKey stuffTimeSettingsKey;
        public MainWindow()
        {
            Context.Init();
            var users = Context.usersWork.Read_AllUsers();

            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey softWareKey = currentUserKey.OpenSubKey("SOFTWARE", true);
            RegistryKey stuffTimeKey = softWareKey.CreateSubKey("ChemSoftTimeManager");
            stuffTimeSettingsKey = stuffTimeKey.CreateSubKey("Settings");
            var lastUserID = stuffTimeSettingsKey.GetValue("lastUserID");
            if (lastUserID == null)
                lastUserID = 0;

            var dialog = new View.Dialog.LoginWindow(users, (int)lastUserID);
            bool? val = dialog.ShowDialog();
            if (val == false)
                Environment.Exit(0);
            else
            {
                context = new MainViewModel();
                InitializeComponent();
                Title = "Учёт трудозатрат, v" + version;
                DataContext = context;
            }
            Closing += this.Window_Closing;
        }

        private void TasksBlockView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            context.CancelEditing();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stuffTimeSettingsKey.SetValue("lastUserID", GlobalInfo.CurrentUser.ID);
        }
    }
}
