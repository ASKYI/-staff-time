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

namespace TestTree.View
{
    /// <summary>
    /// Логика взаимодействия для Fave.xaml
    /// </summary>
    public partial class FaveView : UserControl
    {
        TestTree.ViewModel.FaveViewModel faveVM = new TestTree.ViewModel.FaveViewModel();
        public FaveView()
        {
            InitializeComponent();
            DataContext = faveVM;
        }
    }
}
