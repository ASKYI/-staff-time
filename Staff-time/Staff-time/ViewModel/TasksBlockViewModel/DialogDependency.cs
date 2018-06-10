using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Windows;

namespace Staff_time.ViewModel
{
    public class DialogDependency : DependencyObject
    {
        #region Dependency Properties
        public static readonly DependencyProperty DialogViewModelPropertyProperty = DependencyProperty.Register("DialogViewModel", typeof(object), typeof(DialogDependency), new UIPropertyMetadata(null));

        /// <summary>
        /// Set this to a ViewModel to display the dialog.
        /// </summary>
        public object DialogViewModel
        {
            get { return (object)GetValue(DialogViewModelPropertyProperty); }
            set { SetValue(DialogViewModelPropertyProperty, value); }
        }
        #endregion

        public DialogDependency(Model.Task task, ObservableCollection<TreeNode> roots, TaskCommandEnum command)
        {
            DialogViewModel = new TaskViewModel(task, roots, command);
        }
    }
}
