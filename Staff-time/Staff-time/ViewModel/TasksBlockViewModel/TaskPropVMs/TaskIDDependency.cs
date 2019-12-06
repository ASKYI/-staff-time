using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace Staff_time.ViewModel
{
    public class TaskIDDependency : DependencyObject
    {
        public static readonly DependencyProperty TaskIDProperty = DependencyProperty.Register("TaskID", typeof(int), typeof(TaskIDDependency), new UIPropertyMetadata(null));

        public int TaskID
        {
            get { return (int)GetValue(TaskIDProperty); }
            set { SetValue(TaskIDProperty, value); }
        }

        public TaskIDDependency(int id)
        {
            TaskID = id;
        }
    }
}
