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

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkControlViewModel.xaml
    /// </summary>
    public partial class WorkControl : UserControl
    {
        public ViewModel.WorkControlViewModel context = new ViewModel.WorkControlViewModel();

        public WorkControl()
        {
            InitializeComponent();
            DataContext = context;
        }

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("WorkID", typeof(int), typeof(WorkControl), new PropertyMetadata(IdChanged));

        private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WorkControl changedObject = d as WorkControl;
            changedObject.context.InitWorkControl(changedObject.ID);
        }
    }
}
