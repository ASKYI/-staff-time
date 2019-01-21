using System;
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
        }
        public void Window_loaded(object sender, RoutedEventArgs e)
        {
            WorkNameTextBox.Focus();
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
                textBoxTime.SelectedText = "    ";
                return;
            }
            var text = textBoxTime.Text;
            var curInsertPos = textBoxTime.CaretIndex;
            switch (curInsertPos)
            {
                case 0:
                    if (e.Key < Key.D0 || e.Key > Key.D2)
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 1:
                    string hour1 = text[0].ToString();
                    if (hour1 == "2" && (e.Key < Key.D0 || e.Key > Key.D3))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 3:
                    if (e.Key < Key.D0 || e.Key > Key.D5)
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 4:
                    if (e.Key < Key.D0 || e.Key > Key.D9)
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

        private void TimeStartLostFocus(object sender, RoutedEventArgs e)
        {
            MaskedTextBox textBox = sender as MaskedTextBox;
            DateTime res;
            if (DateTime.TryParse(textBox.Text, out res))
                ((ViewModel.WorkBlockControlViewModel)DataContext).StartTime = res;
        }

        private void TimeEndLostFocus(object sender, RoutedEventArgs e)
        {
            MaskedTextBox textBox = sender as MaskedTextBox;
            DateTime res;
            if (DateTime.TryParse(textBox.Text, out res))
                ((ViewModel.WorkBlockControlViewModel)DataContext).EndTime = res;
        }


        //Пробрасываение методов
        //Ищу решение лучше

        //private void EditBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if ((string)EditBtn.Content == "Редактировать")
        //        EditBtn.Content = "Применить";
        //    else
        //        EditBtn.Content = "Редактировать";
        //}

        private void workBlock_MouseLeave(object sender, MouseEventArgs e)
        {
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
            ((ViewModel.WorkBlockControlViewModel)DataContext).Block_Width = (int)e.NewSize.Width;
        }

        private void DoubleClickTime(object sender, MouseButtonEventArgs e)
        {
            var curTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            var tmControl = (MaskedTextBox)sender;
            tmControl.Text = curTime.ToString("hh:mm");
            //tmControl.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }
    }
}
