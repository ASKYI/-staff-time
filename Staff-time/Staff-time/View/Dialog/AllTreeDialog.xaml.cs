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
    /// Interaction logic for AllTreeDialog.xaml
    /// </summary>
    public partial class AllTreeDialog : IDialogView
    {
        public AllTreeDialog()
        {
            InitializeComponent();
        }
        public AllTreeDialog(object context)
        {
            InitializeComponent();
            base.DataContext = context;
            Closing += ((ViewModel.AllTreeDialogViewModel)DataContext).OnWindowClosing; // todo аналогичное уже было
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
