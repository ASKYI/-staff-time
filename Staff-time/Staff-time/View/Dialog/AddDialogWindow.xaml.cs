using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для AddDialogWindow.xaml
    /// </summary>
    public partial class AddDialogWindow : Window, IDialogView //done: убраны лишнии функции, оставили Closing += (+ метод при закрытии)
    {
        public AddDialogWindow()
        {
            InitializeComponent();
        }

        public AddDialogWindow(object context)
        {
            InitializeComponent();
            base.DataContext = context;
            Closing += ((ViewModel.TaskDialogViewModel)DataContext).OnWindowClosing;
        }
    }
}
