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
    /// Логика взаимодействия для WorkBlockControl.xaml
    /// </summary>
    public partial class WorkBlockControl : UserControl
    {
        public ViewModel.WorkBlockControlViewModel context = new ViewModel.WorkBlockControlViewModel();

        public WorkBlockControl()
        {
            InitializeComponent();
            DataContext = context;
        }

        public int WorkID
        {
            get { return (int)GetValue(WorkIDProperty); }
            set { SetValue(WorkIDProperty, value); }
        }
        
        public static readonly DependencyProperty WorkIDProperty =
            DependencyProperty.Register("WorkID", typeof(int), typeof(WorkBlockControl), new PropertyMetadata(IdChanged));

        private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WorkBlockControl changedObject = d as WorkBlockControl;
            changedObject.context.InitWork(changedObject.WorkID);
        }
    }
}
