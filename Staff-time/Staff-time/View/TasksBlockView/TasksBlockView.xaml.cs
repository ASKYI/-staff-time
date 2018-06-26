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
    /// Логика взаимодействия для TasksBlockView.xaml
    /// </summary>
    public partial class TasksBlockView : UserControl
    {
        ViewModel.TasksBlockViewModel context = 
            new ViewModel.TasksBlockViewModel();
        public TasksBlockView()
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
