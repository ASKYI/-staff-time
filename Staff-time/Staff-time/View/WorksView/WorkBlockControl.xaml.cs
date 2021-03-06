﻿using System;
using System.Collections.Generic;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

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
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(workBlock_MouseDown), true);
        }

        public void Window_loaded(object sender, RoutedEventArgs e)
        {
            WorkNameCombo.Focus();
        }

        public void Description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var descTextBox = (TextBox)sender;
                descTextBox.Text += "\r\n";
            }
        }
        public void KeyDownStart(object sender, KeyEventArgs e)
        {
            var textBoxTime = (MaskedTextBox)sender;
            if (e.Key == Key.Tab)
            {
                //DateTime res;
                //if (DateTime.TryParse(textBoxTime.Text, out res))
                //    ((ViewModel.WorkBlockControlViewModel)DataContext).StartTime = res;
                //if (DateTime.TryParse(textBoxTime.Text, out res))
                //    ((ViewModel.WorkBlockControlViewModel)DataContext).EndTime = res;
                return;
            }
            if (e.Key == Key.Add)
            {
                ((ViewModel.WorkBlockControlViewModel)DataContext).AddWorkRangeCommand.Execute(null);
                return;
            }

            var text = textBoxTime.Text;
            var curInsertPos = textBoxTime.CaretIndex;
            switch (curInsertPos)
            {
                case 0:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D2) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad2))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 1:
                    string hour1 = text[0].ToString();
                    if (hour1 == "2" && (!(e.Key >= Key.D0 && e.Key <= Key.D3) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad3)))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 2:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D5) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad5))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 3:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
            }
            e.Handled = false;
        }


        private void TimeGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Dispatcher.BeginInvoke(new Action(() => text.SelectAll()));
        }

        private void WorkNameCombo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                DescTextBox.Focus();
                return;
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }
            e.Handled = true;
            //WorkNameCombo.IsDropDownOpen = true;
        }

        private void ComboBox_Loaded(object sender, EventArgs e)
        {
            var obj = (ComboBox)sender;
            if (obj != null)
            {
                var myTextBox = (TextBox)obj.Template.FindName("PART_EditableTextBox", obj);
                if (myTextBox != null)
                {
                    myTextBox.MaxLength = 50;
                }
            }
        }

        //private void TimeStartLostFocus(object sender, RoutedEventArgs e)
        //{
        //    MaskedTextBox textBox = sender as MaskedTextBox;
        //    DateTime res;
        //    if (DateTime.TryParse(textBox.Text, out res))
        //        ((ViewModel.WorkBlockControlViewModel)DataContext).WorkTimeRanges[0].StartTime = res;
        //}

        //private void TimeEndLostFocus(object sender, RoutedEventArgs e)
        //{
        //    MaskedTextBox textBox = sender as MaskedTextBox;
        //    DateTime res;
        //    if (DateTime.TryParse(textBox.Text, out res))
        //        ((ViewModel.WorkBlockControlViewModel)DataContext).WorkTimeRanges[0].EndTime = res;
        //}


        //Пробрасываение методов
        //Ищу решение лучше

        private void workBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.GetPosition(this).Y >= 30) //чтобы сворачивание задач не учитывалось
            {
                ((ViewModel.WorkBlockControlViewModel)DataContext).IsEditing = true;
                MainWindow.IsEnable = false;
            }
        }

        private void workBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            //if (((ViewModel.WorkBlockControlViewModel)DataContext).IsEditing)
            //{
            //    IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.MainWindow);
            //    if (focusedControl.GetType() == typeof(ScrollViewer))
            //    {
            //        Mouse.Capture(WorkNameTextBox);
            //    }
            //    else
            //    {
            //        Mouse.Capture(focusedControl);
            //    }
            //    return;
            //}
            ((ViewModel.WorkBlockControlViewModel)DataContext).MouseLeft = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void workBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            ((ViewModel.WorkBlockControlViewModel)DataContext).MouseLeft = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((ViewModel.WorkBlockControlViewModel)DataContext).Minutes_Changed(sender, e);
        }

        private void workBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((ViewModel.WorkBlockControlViewModel)DataContext).BlockWidth = (int)e.NewSize.Width;
        }

        private void DoubleClickTime(object sender, MouseButtonEventArgs e)
        {
            var curTime = new DateTime(1899, 12, 30, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            var tmControl = (MaskedTextBox)sender;
            tmControl.Text = curTime.ToString("HH:mm");
        }
    }
}

namespace WpfTestBench.Converters
{
    public class GridWidthConverter : MarkupExtension, IValueConverter
    {
        private static GridWidthConverter _instance;

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object borderWidth, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) - System.Convert.ToDouble(borderWidth);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new GridWidthConverter());
        }
    }
}
