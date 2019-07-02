using Staff_time.Model;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Staff_time.View.Dialog
{
    /// <summary>
    /// Interaction logic for EditListView.xaml
    /// </summary>
    public partial class EditParentListView : Window
    {
        public EditParentListView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private int TaskID { get; set; }
        private int ListID { get; set; }
        //public void OnWindowClosing(object sender, CancelEventArgs e)
        //{
        //    EditingPropValueInfo.listsValues.Clear();
        //    EditingPropValueInfo.listsValues = NameList;
        //}

        public EditParentListView(List<ListsValue> lst, int taskID, int listID)
        {
            Topmost = true;
            InitializeComponent();
            DataContext = this;
            TaskID = taskID;
            ListID = listID;
            //EditingPropValueInfo = pvi;
            NameList = lst;
            //Closing += OnWindowClosing;
        }

        //private PropValueInfo EditingPropValueInfo;

        public void AddName(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsValue newListItem = new ListsValue();
                newListItem.ListID = ListID;
                newListItem.TaskID = TaskID;
                newListItem.Val = "";
                NameList.Add(newListItem);
                ListBoxNames.Items.Refresh();
                int ind = ListBoxNames.Items.Count - 1;
                ListBoxNames.SelectedIndex = ind;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public void DeleteName(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelIndex < 0)
                {
                    MessageBox.Show("Сначала выберите запись для удаления!", "Ошибка");
                    return;
                }
                NameList.RemoveAt(SelIndex);
                ListBoxNames.SelectedIndex = Math.Min(ListBoxNames.Items.Count - 1, SelIndex);
                ListBoxNames.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private int _selIndex;
        public int SelIndex
        {
            get
            {
                return _selIndex;
            }
            set
            {
                if (value >= 0)
                    _selIndex = value;
            }
        }

        private void NameGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Dispatcher.BeginInvoke(new Action(() => text.SelectAll()));
        }

        private List<ListsValue> _nameList;
        public List<ListsValue> NameList
        {
            get
            {
                return _nameList;
            }
            set
            {
                _nameList = value;
            }
        }
    }
}
