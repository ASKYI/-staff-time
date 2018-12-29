﻿using System;
using System.Collections.Generic;
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

        //Пробрасываение методов
        //Ищу решение лучше

        private void workBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            ((ViewModel.WorkBlockControlViewModel)DataContext).MouseLeft = true;
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
            var tmControl = (DateTimeUpDown)sender;
            tmControl.Value = curTime;
        }
    }
}
