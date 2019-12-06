using Staff_time.Model;
using Staff_time.Model.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        public LoginWindow(List<User> _users, int lastUserID)
        {
            users = _users;
            InitializeComponent();
            if (lastUserID > 0)
                SelectedUser = users.Find(u => u.ID == lastUserID);
            PasswordUserBox.Focus();
            DataContext = this;
        }

        public User SelectedUser { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Password = PasswordUserBox.Password;
            if (Password != null && Password.Equals(SelectedUser.Password, StringComparison.OrdinalIgnoreCase))
            {
                GlobalInfo.CurrentUser = SelectedUser;
                this.DialogResult = true;
                this.Close();
            }
            else
                MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnKeyDownPassword(object sender, KeyEventArgs e)
        {
            var Password = PasswordUserBox.Password;

            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    GlobalInfo.CurrentUser = SelectedUser;
                    this.DialogResult = true;
                    this.Close();
                    return;
                }

                OkButton.Focus();
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
        private void OnKeyDownLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
                PasswordUserBox.Focus();
        }
    }
}
