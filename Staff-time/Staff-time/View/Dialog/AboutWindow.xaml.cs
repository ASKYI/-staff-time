using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для AddDialogWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window, IDialogView
    {
        public string AboutInfo { get; set; }
        public AboutWindow()
        {
            InitializeComponent();
            string connString = Context.GetConnString();
            AboutInfo = $@"
Учёт трудозатрат
© ООО «Химсофт» 2010-2019
www.chemsoft.ru
e-mail: git@hvd.tpu.ru

Строка подключения: {connString}";
            base.DataContext = this;
        }
    }
}
