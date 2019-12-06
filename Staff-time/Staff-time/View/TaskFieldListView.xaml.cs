using Staff_time.Model;
using Staff_time.ViewModel;
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
    /// Interaction logic for TaskFieldListView.xaml
    /// </summary>
    public partial class TaskFieldListView : UserControl
    {
        public TaskFieldListView()
        {
            InitializeComponent();
            DataContext = this;
            //ListValues = new List<string>();
            //ListValues = Context.propertyWork.GetListOfPropValues(PropVal.PropID);
        }

        private List<string> _listValues;
        public List<string> ListValues
        {
            get
            {
                if (_listValues == null)
                    _listValues = Context.propertyWork.GetListOfPropValues(PropVal.PropID);
                return _listValues;
            }
            set
            {
                _listValues = value;
            }
        }

        public PropValue PropVal
        {
            get
            {
                return (PropValue)GetValue(PropProperty);
            }
            set { SetValue(PropProperty, value); }
        }

        public static readonly DependencyProperty PropProperty =
                DependencyProperty.Register("PropVal", typeof(PropValue), typeof(TaskFieldListView), new PropertyMetadata(default(PropValue), OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

    }
}
