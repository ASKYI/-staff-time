using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Windows;


namespace Staff_time.ViewModel
{
    public class WorkIDDependency : DependencyObject
    {
        public static readonly DependencyProperty WorkIDProperty = DependencyProperty.Register("WorkID", typeof(int), typeof(WorkIDDependency), new UIPropertyMetadata(null));

        public int WorkID
        {
            get { return (int)GetValue(WorkIDProperty); }
            set { SetValue(WorkIDProperty, value); }
        }

        public WorkIDDependency(int id)
        {
            WorkID = id;
        }
    }
}