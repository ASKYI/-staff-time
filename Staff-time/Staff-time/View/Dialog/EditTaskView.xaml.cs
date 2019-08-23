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
            base.DataContext = context;
            Topmost = true;
            InitializeComponent();
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

        private void TreeViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        //void SelTaskTypeChanged(object sender, SelectionChangedEventArgs args)
        //{
        //    ComboBox combo = (ComboBox)sender;
        //    //if (combo.SelectedIndex != 0 &&  MessageBox.Show("При изменении типа задач данные в дополнительных полях будут удалены. Продолжить?", "Смена типа задачи",
        //    //    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //    //{
        //    var ind = ((ViewModel.TaskDialogViewModel)DataContext).SelectedTaskTypeIndex;
        //    if (ind >= 0 && combo.SelectedIndex != ind)
        //        combo.SelectedIndex = ind;
        //    //}
        //}
    }
}
