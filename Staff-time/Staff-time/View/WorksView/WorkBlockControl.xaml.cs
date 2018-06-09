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
        public WorkBlockControl()
        {
            InitializeComponent();
            DataContext = new ViewModel.WorkBlockControlViewModel(WorkID);
        }

        public int WorkID { get; set; } //!!!

        public int WorkIDProperty
        {
            get { return (int)GetValue(WorkIDPropertyProperty); }
            set { SetValue(WorkIDPropertyProperty, value); }
        }
        
        public static readonly DependencyProperty WorkIDPropertyProperty =
            DependencyProperty.Register("WorkID", typeof(int), typeof(WorkBlockControl), new PropertyMetadata(0));
    }
}
