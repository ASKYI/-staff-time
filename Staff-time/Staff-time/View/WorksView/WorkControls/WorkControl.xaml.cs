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

//TimePicker
//https://social.msdn.microsoft.com/Forums/vstudio/en-US/359125db-b4e9-4e52-82ea-6bc8cd736f30/wpf-timepicker-control?forum=wpf
//https://marlongrech.wordpress.com/about/
using AC.AvalonControlsLibrary.Controls;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkControlViewModel.xaml
    /// </summary>
    public partial class WorkControl : UserControl
    {

        public WorkControl()
        {
            InitializeComponent();
        }

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(WorkControl), new PropertyMetadata(IdChanged));

        private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WorkControl changedObject = d as WorkControl;
            ViewModel.WorkControlViewModel context = new ViewModel.WorkControlViewModel();
            context.InitWorkControl(changedObject.ID);
            changedObject.DataContext = context;
        }
    }
}
