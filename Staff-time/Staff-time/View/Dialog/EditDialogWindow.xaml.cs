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
    /// Логика взаимодействия для EditDialogWindow.xaml
    /// </summary>
    public partial class EditDialogWindow : Window
    {
        public EditDialogWindow()
        {
            InitializeComponent();
        }

        public EditDialogWindow(object context)
        {
            InitializeComponent();
            base.DataContext = context;
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
