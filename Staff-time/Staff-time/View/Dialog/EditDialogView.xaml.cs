using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для EditDialogWindow.xaml
    /// </summary>
    public partial class EditDialogWindow : IDialogView //done: убраны лишнии функции, оставили Closing += (+ метод при закрытии)
    {
        public EditDialogWindow()
        {
            InitializeComponent();
        }

        public EditDialogWindow(object context)
        {
            InitializeComponent();
            base.DataContext = context;
            Closing += ((ViewModel.TaskDialogViewModel)DataContext).OnWindowClosing;
        }
    }
}
