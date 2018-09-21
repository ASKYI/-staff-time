using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        //Решение лучше не было найдено

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
            ((ViewModel.WorkBlockControlViewModel)DataContext).Block_Width = (int) e.NewSize.Width;
        }
    }
}
