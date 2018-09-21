using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Staff_time.View
{
    //Решения для возможности получать значение SelectedItem у TreeView из ViewModel
    //https://www.codeproject.com/Questions/509537/wpfplusmvvmplustreeviewplusgetplusselectedplusitem

    public partial class ExtendedTreeView : System.Windows.Controls.TreeView, INotifyPropertyChanged
    {
        public ExtendedTreeView()
            : base()
        {
            base.SelectedItemChanged += new RoutedPropertyChangedEventHandler<Object>(ExtendedTreeView_SelectedItemChanged);
        }

        public new Object SelectedItem
        {
            get { return (Object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemsProperty, value);
                NotifyPropertyChanged("SelectedItem");
            }
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItem", typeof(Object), typeof(ExtendedTreeView), new PropertyMetadata(null));

        private void ExtendedTreeView_SelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            this.SelectedItem = base.SelectedItem;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion
    }
}
