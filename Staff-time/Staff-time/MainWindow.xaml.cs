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
using Staff_time.ViewModel;         // todo после окончания работы над файлом желательно убрать лишние using

namespace Staff_time
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MainViewModel context = new MainViewModel();
        MainViewModel context;
        public MainWindow()
        {
            Context.Init();
            var users = Context.usersWork.Read_AllUsers();

            var dialog = new View.Dialog.LoginWindow(users);
            bool? val = dialog.ShowDialog();
            if (val == false)
                Environment.Exit(0);
            else
            {
                context = new MainViewModel();
                InitializeComponent();
                DataContext = context;
            }
        }

        private void TasksBlockView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            context.CancelEditing();
        }
    }
}
