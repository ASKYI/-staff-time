using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkControlViewModel.xaml
    /// </summary>
    public partial class WorkControl : UserControl
    {

        public WorkControl()
        {
            InitializeComponent();
        }

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(WorkControl), new PropertyMetadata(IdChanged));

        private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WorkControl changedObject = d as WorkControl;
            ViewModel.WorkControlViewModel context = new ViewModel.WorkControlViewModel();
            context.InitWorkControl(changedObject.ID);
            changedObject.DataContext = context;
        }
    }
}
