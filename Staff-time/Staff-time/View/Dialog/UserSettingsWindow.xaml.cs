using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
using System.Xml.Serialization;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для AddDialogWindow.xaml
    /// </summary>
    public partial class UserSettingsWindow : Window, IDialogView, INotifyPropertyChanged
    {
        public UserSettingsWindow()
        {
            InitializeComponent();

            UserOptions = new UserSettings(GlobalInfo.UserOptions);
            user = GlobalInfo.CurrentUser;
            WorkModes = new List<string>() { "полный день", "1/2 ставки", "студент", "гибкий график" };

            this.DataContext = this;
        }

        public List<string> WorkModes { get; set; }
        public User user { get; set; }
        public UserSettings UserOptions { get; set; }
        public bool IsEditor
        {
            get
            {
                return GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor";
            }
        }

        #region Click events
        public void ApplyClick(object sender, EventArgs e)
        {
            var curPassWord = CurPasswordBox.Password;
            var newPassWord = NewPasswordBox.Password;
            var confurmPassWord = ConfurmPasswordBox.Password;
            if (curPassWord != "" || newPassWord != "" || confurmPassWord != "")
            {
                if (curPassWord.ToLower() != GlobalInfo.CurrentUser.Password.ToLower())
                {
                    MessageBox.Show("Указан неверный текущий пароль. Повторите попытку", "Неверный пароль", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (newPassWord.ToLower() != confurmPassWord.ToLower())
                {
                    MessageBox.Show("Пароли не совпадают. Повторите попытку", "Пароли не совпадают", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                GlobalInfo.CurrentUser.Password = newPassWord;
            }

            //Сохранение настроек
            GlobalInfo.UserOptions = UserOptions;
            Context.usersWork.SaveCurrentUser();
            Close();
        }
        public void RepairClick(object sender, EventArgs e)
        {
            Mouse.SetCursor(Cursors.Wait);
            Context.procedureWork.RepairInconsistances();
            Mouse.SetCursor(Cursors.Arrow);
        }

        public void ReloadLastDayClick(object sender, EventArgs e)
        {
            Mouse.SetCursor(Cursors.Wait);
            Context.procedureWork.ReloadCurDay();
            Mouse.SetCursor(Cursors.Arrow);
        }

        public void ReloadAllDaysClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Процесс может занимать длительное время, продолжить?", "Вопрос на продолжение операции", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            Mouse.SetCursor(Cursors.Wait);
            Context.procedureWork.ReloadAllDays();
            Mouse.SetCursor(Cursors.Arrow);
        }


        #endregion //clickEvents

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion
    }
}
