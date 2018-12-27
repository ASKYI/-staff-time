using Staff_time.Model;
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

namespace Staff_time.View.Dialog
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public List<User> users { get; set; }
        public LoginWindow(List<User> _users)
        {
            users = _users;
            InitializeComponent();
            DataContext = this;
        }

        public User SelectedUser { get; set; }
        public string Password { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password != null && Password.Equals(SelectedUser.Password, StringComparison.OrdinalIgnoreCase))
            {
                GlobalInfo.CurrentUser = SelectedUser;
                this.DialogResult = true;
                this.Close();
            }
            else
                MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
