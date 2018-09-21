using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkDialogView.xaml
    /// </summary>
    public partial class WorkDialogView : Window, IDialogView
    {
        public WorkDialogView()
        {
            InitializeComponent();
        }

        public WorkDialogView(object context)
        {
            InitializeComponent();
            DataContext = context;
            Closing += ((ViewModel.WorkDialogViewModel)DataContext).OnWindowClosing;
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
