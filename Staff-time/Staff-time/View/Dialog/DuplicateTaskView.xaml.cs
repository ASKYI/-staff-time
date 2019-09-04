using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkDialogView.xaml
    /// </summary>
    public partial class DuplicateTaskDialogView : Window, IDialogView, INotifyPropertyChanged
    {
        public DuplicateTaskDialogView(int _taskFromID)
        {
            InitializeComponent();
            TaskFromID = _taskFromID;
            _generate_Tree();
            DataContext = this;
        }

        private int TaskFromID { get; set; }
        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                _selectedTaskNode = value;
            }
        }

        private ObservableCollection<TreeNode> _treeRoots;
        public ObservableCollection<TreeNode> TreeRoots
        {
            get { return _treeRoots; }
            set
            {
                SetField(ref _treeRoots, value);
            }
        }

        private bool _isFilterEmpty;
        public bool IsFilterEmpty
        {
            get
            {
                return _isFilterEmpty;
            }
            set
            {
                _isFilterEmpty = value;
                NotifyPropertyChanged("IsFilterEmpty");
            }
        }

        private string _filterTaskText;
        public string FilterTaskText
        {
            get
            {
                return _filterTaskText;
            }
            set
            {
                _filterTaskText = value;
                if (_filterTaskText == "" || _filterTaskText == null || _filterTaskText == "Поиск...")
                    IsFilterEmpty = true;
                else
                    IsFilterEmpty = false;
                NotifyPropertyChanged("FilterTaskText");
            }
        }

        private void Filter_GotFocus(object sender, EventArgs e)
        {
            if (FilterTaskText == "Поиск...")
                FilterTaskText = "";
            else
                FilterTBox.Dispatcher.BeginInvoke(new Action(() => FilterTBox.SelectAll()));

            FilterTBox.Foreground = Brushes.Black;
        }

        private void FilterClick(object sender, EventArgs e)
        {
            if (FilterTaskText != "" && FilterTaskText != null)
                FilterTBox.Background = new SolidColorBrush(Color.FromRgb(222, 240, 243));
            else
                FilterTBox.Background = Brushes.White;
            FilterTree();
        }

        private void ClearFilterClick(object sender, EventArgs e)
        {
            FilterTBox.Text = "";
            FilterTBox.Background = Brushes.White;
            FilterTree();
        }

        private void OnKeyDownFilter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FilterTree();
                return;
            }
        }

        private void _generate_Tree()
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            int pos = 1;
            foreach (var taskNode in TasksVM.DictionaryFull)
            {
                if (taskNode.Value.ParentNode == null)
                    TreeRoots.Add(taskNode.Value);
                taskNode.Value.IndexNumber = pos;
                pos++;
            }
        }

        private void FilterTree()
        {
            var oldSelectedNode = SelectedTaskNode;
            TasksVM.FilterFullTaskText = _filterTaskText;
            _generate_Tree();

            //Восстановить развертку
            if (oldSelectedNode == null)
                return;

            int oldSelectedNodeTaskID = oldSelectedNode.Task.ID;
            var parent = oldSelectedNode.ParentNode;
            while (parent != null)
            {
                parent.IsExpanded = true;
                parent = parent.ParentNode;
            }
            var dictItem = TasksVM.DictionaryFull.FirstOrDefault(nd => nd.Key == oldSelectedNodeTaskID);
            if (dictItem.Value != null)
            {
                SelectedTaskNode = dictItem.Value;
                SelectedTaskNode.IsExpanded = true;
            }
        }


        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedTaskNode == null)
                {
                    MessageBox.Show("Необходимо выбрать новую родительскую задачу в дереве для дублирования!",
                        "Дублирование задачи", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Mouse.SetCursor(Cursors.Wait);
                Context.procedureWork.DuplicateTask(TaskFromID, SelectedTaskNode.Task.ID, GlobalInfo.CurrentUser.ID);
                FilterTaskText = "";
                FilterTree();
                Context.ReloadContext();
                Mouse.SetCursor(Cursors.Arrow);
                this.Close();
            }
            catch (Exception exp)
            {
                Mouse.SetCursor(Cursors.Arrow);
                MessageBox.Show(exp.Message, "Ошибка дублирования", MessageBoxButton.OK);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion

        #region INotifyPropertyChanged Member
        public bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
