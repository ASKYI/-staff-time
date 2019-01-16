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
            TaskNameTBox.Focus();
            TaskNameTBox.SelectionStart = TaskNameTBox.Text.Length;
            base.DataContext = context;
            Closing += ((ViewModel.TaskDialogViewModel)DataContext).OnWindowClosing;  // todo косяк зависимостей, здесь это не должно быть, это может быть в вызывающей функции
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e) // todo по видимому лишние функции
        {
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
        } 
    }
}
