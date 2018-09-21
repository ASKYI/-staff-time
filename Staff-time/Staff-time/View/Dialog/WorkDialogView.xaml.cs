using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkDialogView.xaml
    /// </summary>
    public partial class WorkDialogView : Window, IDialogView //done: убраны лишнии функции, оставили Closing += (+ метод при закрытии)
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
    }
}
