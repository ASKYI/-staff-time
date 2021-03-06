﻿using Microsoft.Win32;
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
using System.Text.RegularExpressions;
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
    public partial class TaskPropView : UserControl
    {
        public TaskPropView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public bool CanEdit
        {
            get
            {
                return (bool)GetValue(EditableProperty);
            }
            set { SetValue(EditableProperty, value); }
        }

        public List<PropValueInfo> PropValues
        {
            get
            {
                return (List<PropValueInfo>)GetValue(PropProperty);
            }
            set { SetValue(PropProperty, value); }
        }

        public static readonly DependencyProperty PropProperty =
                DependencyProperty.Register("PropValues", typeof(List<PropValueInfo>), typeof(TaskPropView), new PropertyMetadata(default(List<PropValueInfo>), OnItemsPropertyChanged));

        public static readonly DependencyProperty EditableProperty =
              DependencyProperty.Register("CanEdit", typeof(bool), typeof(TaskPropView), new PropertyMetadata(default(bool), OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void DateBox_DoubleClick(object sender, EventArgs e)
        {
            var obj = (Xceed.Wpf.Toolkit.MaskedTextBox)sender;
            if (obj.ToolTip != null && obj.ToolTip.ToString() != "")
                obj.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }
        private void TimeBox_DoubleClick(object sender, EventArgs e)
        {
            var obj = (Xceed.Wpf.Toolkit.MaskedTextBox)sender;
            if (obj.ToolTip != null && obj.ToolTip.ToString() != "")
                obj.Text = DateTime.Now.ToString("HH:mm");
        }


        #region commnads

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public void OpenDoc(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                ((PropValueInfo)obj.DataContext).propVal.ValueText = openFileDialog.FileName;
                var docTextBox = FindVisualChild<TextBox>(obj.Parent); //ищем TextBox с выводом пути к файлу
                BindingExpression bindingExpr = BindingOperations.GetBindingExpression(docTextBox, TextBox.TextProperty);
                bindingExpr.UpdateTarget();
            }
        }

        public void ShowDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = (Button)sender;
                var path = ((PropValueInfo)obj.DataContext).propVal.ValueText;
                if (path == null || path.Length == 0)
                    return;
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public void OpenFolder(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            System.Windows.Forms.FolderBrowserDialog openfolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            openfolderDialog.SelectedPath = "Z:\\Договора_Города";
            if (openfolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ((PropValueInfo)obj.DataContext).propVal.ValueText = openfolderDialog.SelectedPath;
                var folderTextBox = FindVisualChild<TextBox>(obj.Parent); //ищем TextBox с выводом пути к файлу
                BindingExpression bindingExpr = BindingOperations.GetBindingExpression(folderTextBox, TextBox.TextProperty);
                bindingExpr.UpdateTarget();
            }
        }

        public void ShowFolder(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = (Button)sender;
                var path = ((PropValueInfo)obj.DataContext).propVal.ValueText;
                if (path == null || path.Length == 0)
                    return;
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public void ShowListValues(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = (Button)sender;
                var prop = ((PropValueInfo)obj.DataContext).propVal.Property;
                EditListView listEditDlg = new EditListView(prop);
                listEditDlg.ShowDialog();

                var listComboBox = FindVisualChild<ComboBox>(obj.Parent); //ищем ComboBox со списков в текущей панельке
                BindingExpression bindingExpr = BindingOperations.GetBindingExpression(listComboBox, ComboBox.ItemsSourceProperty);
                bindingExpr.UpdateTarget();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public void ShowParentListValues(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = (Button)sender;
                var pvi = (PropValueInfo)obj.DataContext;
                EditParentListView listEditDlg = new EditParentListView(pvi.listsValues.ToList(), pvi.parentListTaskID, (int)pvi.propVal.Property.ListID);
                listEditDlg.ShowDialog();

                pvi.listsValues = listEditDlg.NameList;
                var listComboBox = FindVisualChild<ComboBox>(obj.Parent); //ищем ComboBox со списков в текущей панельке
                BindingExpression bindingExpr = BindingOperations.GetBindingExpression(listComboBox, ComboBox.ItemsSourceProperty);
                bindingExpr.UpdateTarget();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public void PreviewNumberInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        public void ShowConstListValues(object sender, RoutedEventArgs e)
        {
            if (GlobalInfo.CurrentUser.LEVEL.LevelName.ToLower() == "editor")
                ShowListValues(sender, e);
            else
                MessageBox.Show("Только редактор может редактировать статические списки, в которых значения постоянны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        //public int ID
        //{
        //    get { return (int)GetValue(IDProperty); }
        //    set { SetValue(IDProperty, value); }
        //}

        //public static readonly DependencyProperty IDProperty =
        //    DependencyProperty.Register("ID", typeof(int), typeof(TaskPropView), new PropertyMetadata(IdChanged));
        //private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    TaskPropView changedObject = d as TaskPropView;
        //    ViewModel.TreeNode context = new ViewModel.TreeNode();
        //    context.InitTaskControl(changedObject.ID, -1);
        //    changedObject.DataContext = context;
        //}

        //public void Refresh(DependencyObject d, int newTypeID)
        //{
        //    TaskPropView changedObject = d as TaskPropView;
        //    ViewModel.TreeNode context = new ViewModel.TreeNode();
        //    context.InitTaskControl(changedObject.ID, newTypeID);
        //    changedObject.DataContext = context;
        //}
    }

    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (DateTime)value == DateTime.MinValue)
                return null;
            return ((DateTime)value).ToString("dd.MM.yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt;
            DateTime.TryParse((string)value, out dt);
            return dt;
        }
    }
}
