using Staff_time.Model;
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

namespace Staff_time.View.Dialog
{
    /// <summary>
    /// Interaction logic for TransferTaskView.xaml
    /// </summary>
    public partial class TransferTaskView : Window
    {

        public List<UserChecked> users { get; set; }
        public string Note { get; set; }
        int transferedTaskID;
        DateTime requestDate { get; set; }

        public TransferTaskView(int taskID, DateTime? dt = null)
        {
            if (dt == null)
                dt = DateTime.Now;
            requestDate = (DateTime)dt;
            transferedTaskID = taskID;
            var usersOrigin = Context.usersWork.Read_AllUsers();
            usersOrigin = usersOrigin.Where(u => u.LevelID >= TasksVM.DictionaryFull[taskID].Task.LevelID).ToList();
            users = new List<UserChecked>();
            foreach (var _user in usersOrigin)
            {
                var userChecked = new UserChecked(_user, false);
                users.Add(userChecked);
            }
            Topmost = true;
            InitializeComponent();
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            int transferedCnt = 0;
            foreach (var _user in users)
            {
                if (_user.IsChecked)
                {
                    TasksVM.TransferTask(GlobalInfo.CurrentUser.ID, _user.user.ID, transferedTaskID, requestDate, Note);
                    transferedCnt++;
                }
            }
            if (transferedCnt > 0)
                MessageBox.Show($"{transferedCnt} записей передано успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Не было передано ни одной записи", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
    public class UserChecked
    {
        public UserChecked(User _user, bool _isChecked) { user = _user; IsChecked = _isChecked; }
        public User user { get; set; }
        public bool IsChecked { get; set; }
    }
}
