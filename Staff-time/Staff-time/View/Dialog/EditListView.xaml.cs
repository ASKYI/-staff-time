using Staff_time.Model;
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
    public partial class EditListView : Window
    {
        public EditListView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            EditingProperty.PropertiesLists.Clear();
            EditingProperty.PropertiesLists = NameList;
        }

        public EditListView(Property prop)
        {
            InitializeComponent();
            DataContext = this;
            EditingProperty = prop;
            NameList = prop.PropertiesLists.ToList();
            Closing += OnWindowClosing;
        }

        private Property EditingProperty;

        public void AddName(object sender, RoutedEventArgs e)
        {
            try
            {
                PropertiesList newListItem = new PropertiesList();
                newListItem.PropID = EditingProperty.ID;
                newListItem.Value = "";
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

        //private void ListSelection_Changed(object sender, SelectionChangedEventArgs e)
        //{
        //    ListBoxItem selItem = ((sender as ListBox).SelectedItem as ListBoxItem);
        //    if (selItem == null)
        //        return;

        //}

        private void NameGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Dispatcher.BeginInvoke(new Action(() => text.SelectAll()));
        }

        private List<PropertiesList> _nameList;
        public List<PropertiesList> NameList
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
