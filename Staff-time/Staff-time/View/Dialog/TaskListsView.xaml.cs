using Microsoft.Win32;
using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
using System.Windows.Threading;

namespace Staff_time.View.Dialog
{
    /// <summary>
    /// Interaction logic for TaskPropView.xaml
    /// </summary>
    public partial class TaskListsView : UserControl
    {
        public TaskListsView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public List<ListInfo> Lists
        {
            get
            {
                return (List<ListInfo>)GetValue(PropProperty);
            }
            set { SetValue(PropProperty, value);}
        }

        public static readonly DependencyProperty PropProperty =
                DependencyProperty.Register("Lists", typeof(List<ListInfo>), typeof(TaskListsView), new PropertyMetadata(default(List<ListInfo>), OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #region commnads
        public void ShowParentListValues(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = (Button)sender;
                var listInfo = ((ListInfo)obj.DataContext);

                EditParentListView listEditDlg = new EditParentListView(listInfo.listsValues.ToList(), listInfo.TaskID, listInfo.list.ID);
                listEditDlg.ShowDialog();
                listInfo.listsValues = listEditDlg.NameList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
        #endregion
    }
}
