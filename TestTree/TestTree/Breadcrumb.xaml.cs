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

namespace TestTree
{
    /// <summary>
    /// Логика взаимодействия для Breadcrumb.xaml
    /// </summary>
    public partial class Breadcrumb : UserControl
    {
        //Just a TextBlock
        //TODO: Make it an actual BreadCrumb
        public Breadcrumb()
        {
            InitializeComponent();
        }

        public string BreadcrumbTextBoxToolTip
        {
            get { return this.TextBlockToolTip.Text; }
            set { TextBlockToolTip.Text = value; }
        }
    }
}
