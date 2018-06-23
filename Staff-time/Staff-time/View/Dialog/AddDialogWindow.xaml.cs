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

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для AddDialogWindow.xaml
    /// </summary>
    public partial class AddDialogWindow : Window, IDialogView
    {
        public AddDialogWindow()
        {
            InitializeComponent();
        }

        public AddDialogWindow(object context)
        {
            InitializeComponent();
            base.DataContext = context;
            Closing += ((ViewModel.TaskDialogViewModel)DataContext).OnWindowClosing;
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
    }
}
