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
    /// Логика взаимодействия для EditDialogWindow.xaml
    /// </summary>
    public partial class EditDialogWindow : IDialogView
    {
        public EditDialogWindow()
        {
            InitializeComponent();
        }

        public EditDialogWindow(object context)
        {
            InitializeComponent();
            base.DataContext = context;
            Closing += ((ViewModel.TaskDialogViewModel)DataContext).OnWindowClosing; // todo аналогичное уже было
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
       //     if (((ViewModel.TaskDialogViewModel)DataContext).ToClose)
          //      this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
