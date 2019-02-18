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

namespace Staff_time.View.Dialog
{
    /// <summary>
    /// Interaction logic for TaskPropView.xaml
    /// </summary>
    public partial class TaskPropView : UserControl
    {

        public TaskPropView()
        {
            InitializeComponent();
        }

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(TaskPropView), new PropertyMetadata(IdChanged));

        private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskPropView changedObject = d as TaskPropView;
            ViewModel.WorkControlViewModel context = new ViewModel.WorkControlViewModel();
            context.InitWorkControl(changedObject.ID);
            changedObject.DataContext = context;
        }
    }
}
