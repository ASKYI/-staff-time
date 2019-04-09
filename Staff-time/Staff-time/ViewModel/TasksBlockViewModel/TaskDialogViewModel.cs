using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using Staff_time.Model;
using Staff_time.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel.TasksBlockViewModel.TaskPropVMs;
using GalaSoft.MvvmLight.Threading;
using System.Windows.Threading;

namespace Staff_time.ViewModel
{
    public class TaskDialogViewModel : MainViewModel
    {
        private TasksAllViewModel ALLVM { get; set; }
        public TaskDialogViewModel(TasksAllViewModel allVM, Task task, ObservableCollection<TreeNode> roots, TaskCommandEnum command, TreeNode editNodeParent = null, bool _isEnabled = true)
        {
            ALLVM = allVM;
            _generate_TaskTypesCb();

            IsEditEnabled = _isEnabled;
            _task = task;
            EditingTreeNodeParent = editNodeParent;
            EditingTask = new Task(task);

            SelectedTaskTypeIndex = task.TaskTypeID;

            levels = Context.levelWork.Read_AllLevelsLowerMe();
            SelLevel = levels[0];

            users = Context.usersWork.Read_AllUsers();
            ResponsibleUser = GlobalInfo.CurrentUser;

            if (command == TaskCommandEnum.Edit)
            {
                _generate_Tree(roots);
                Message = "Выбрать задачу-родителя";
                SelLevel = levels.FirstOrDefault(l => l.LevelID == task.LevelID);
                ResponsibleUser = users.FirstOrDefault(u => u.ID == task.ResponsibleID);
            }
            _command = command;
            Command = (int)_command;


            AcceptCommand = new RelayCommand(Accept, CanAccept); // todo чем чётче мы показываем намерения, тем легче программа 
            CancelCommand = new RelayCommand(Cancel, CanCancel); // в данном случае у нас return true всегда, наглядней было бы CancelCommand = new RelayCommand(Cancel, (_) => true);
        }

        private Task _task; //TaskNode
        private TaskCommandEnum _command;
        public int Command; // todo для чего эта переменная?

        private List<PropValueInfo> _propValuesCollection;
        public List<PropValueInfo> PropValuesCollection
        {
            get
            {
                return _propValuesCollection;
            }
            set
            {
                SetFieldTaskDialogVM<List<PropValueInfo>>(ref _propValuesCollection, value);
            }
        }

        private List<ListInfo> _taskLists;
        public List<ListInfo> TaskLists
        {
            get
            {
                return _taskLists;
            }
            set
            {
                SetFieldTaskDialogVM<List<ListInfo> >(ref _taskLists, value);
            }
        }
        

        public bool IsEditEnabled { get; set; }

        private void UpdatePropCollection()
        {
            var tmpList = new List<PropValueInfo>();
            if (EditingTask.TaskTypeID > 0)
            {
                var props = Context.taskWork.GetAllProperties(EditingTask.TaskTypeID);
                foreach (var prop in props)
                {
                    int parentListTaskID = 0;
                    List<ListsValue> listValues = new List<ListsValue>();
                    //если тип список, который тянется от родителя, нужно найти этого родителя и значения
                    if (prop.DataType == 5 && prop.ListType == true)
                    {
                        if (EditingTreeNodeParent == null)
                            continue;
                        var parentNode = EditingTreeNodeParent;
                        while (parentNode != null && parentNode.Task.TaskTypeID != prop.TaskTypeID)
                            parentNode = parentNode.ParentNode;
                        if (parentNode == null)
                        {
                            MessageBox.Show($"Ошибка! У свойства {prop.PropName} не найден родитель с типом задачи {prop.TaskTypeID} (Заказчик), из которого подгружается список. Данные свойства не будут отображены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            continue;
                        }
                        parentListTaskID = parentNode.Task.ID;
                        listValues = Context.listWork.GetListValues(parentListTaskID, (int)prop.ListID);
                    }

                    var propWithValue = EditingTask.PropValues.FirstOrDefault(pval => pval.Property == prop);
                    var propValInfo = new PropValueInfo(propWithValue, parentListTaskID, listValues);
                    if (propWithValue != null)
                        tmpList.Add(propValInfo);
                    else
                    {
                        PropValue pv = new PropValue();
                        pv.Property = prop;
                        pv.PropID = prop.ID;
                        pv.TaskID = EditingTask.ID;
                        pv.DataType = prop.DataType;
                        propValInfo = new PropValueInfo(pv, parentListTaskID, listValues);
                        tmpList.Add(propValInfo);
                    }
                }
            }
            PropValuesCollection = tmpList;
        }

        private void UpdateListsCollection()
        {
            var tmpList = new List<ListInfo>();

            var listOfTaskfList = Context.propertyWork.GetListIDWithTaskType(EditingTask.TaskTypeID);
            foreach (var lst in listOfTaskfList)
            {
                var listValues = Context.listWork.GetListValues(EditingTask.ID, lst.ID);
                tmpList.Add(new ListInfo(lst, listValues, EditingTask.ID));
            }
            TaskLists = tmpList;
        }


        //private void UpdatePropCollection()
        //{
        //    var tmpList = new List<PropValue>();
        //    if (EditingTask.TaskTypeID > 0)
        //    {
        //        var props = Context.taskWork.GetAllProperties(EditingTask.TaskTypeID);
        //        foreach (var prop in props)
        //        {
        //            var propWithValue = EditingTask.PropValues.FirstOrDefault(pval => pval.Property == prop);

        //            PropValue pv = new PropValue();
        //            pv.Property = prop;
        //            pv.PropID = prop.ID;
        //            pv.TaskID = EditingTask.ID;
        //            pv.DataType = prop.DataType;
        //            if (propWithValue != null)
        //            {
        //                pv.ValueTime = propWithValue.ValueTime;
        //                pv.ValueDate = propWithValue.ValueDate;
        //                pv.ValueText = propWithValue.ValueText;
        //                pv.ValueInt = propWithValue.ValueInt;
        //            }
        //            tmpList.Add(pv);
        //        }
        //    }
        //    PropValuesCollection = tmpList;
        //}

        private Task _editingTask;
        public Task EditingTask
        {
            get { return _editingTask; }
            set
            {
                SetFieldTaskDialogVM(ref _editingTask, value);
            }
        }
        public TreeNode EditingTreeNodeParent { get; set; }

        private List<User> _users;

        public List<User> users
        {
            get
            {
                return _users;
            }
            set
            {
                SetField(ref _users, value);
            }
        }

        private User _responsibleUser;
        public User ResponsibleUser
        {
            get
            {
                return _responsibleUser;
            }
            set
            {
                _responsibleUser = value;
                _editingTask.ResponsibleID = _responsibleUser.ID;
            }
        }

        private List<LEVEL> _levels;

        public List<LEVEL> levels
        {
            get
            {
                return _levels;
            }
            set
            {
                SetField(ref _levels, value);
            }
        }

        private LEVEL _selLevel;
        public LEVEL SelLevel
        {
            get
            {
                return _selLevel;
            }
            set
            {
                _selLevel = value;
                _editingTask.LevelID = _selLevel.LevelID;
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetFieldTaskDialogVM(ref _message, value);
            }
        }

        #region Tree

        private ObservableCollection<TreeNode> _treeRoots;
        public ObservableCollection<TreeNode> TreeRoots
        {
            get { return _treeRoots; }
            set
            {
                SetFieldTaskDialogVM(ref _treeRoots, value);
            }
        }

        private TreeNode _root = new TreeNode() { Task = new Task() { TaskName = "Задачи" }, IsExpanded = true };
        private void _generate_Tree(ObservableCollection<TreeNode> roots)
        {
            TreeRoots = new ObservableCollection<TreeNode>();
            TreeRoots.Add(_root);

            foreach (var r in roots)
                TreeRoots[0].AddChild(r);
        }

        private TreeNode _selectedTaskNode;
        public TreeNode SelectedTaskNode
        {
            get { return _selectedTaskNode; }
            set
            {
                SetFieldTaskDialogVM<TreeNode>(ref _selectedTaskNode, value); // todo вроде c# по параметру сам должен распознать тип Generic
                if (value.Task.ID == 0)
                    _editingTask.ParentTaskID = null;
                else
                    _editingTask.ParentTaskID = value.Task.ID;
            }
        }

        public void ChangeSelection(TreeNode value) //Нельзя в сетер - будет переполнение стека
        {
            //if (_selectedTaskNode != null)
            //    _selectedTaskNode.IsSelected = false;

            //SetField<TreeNode>(ref _selectedTaskNode, value);

            //if (_selectedTaskNode != null)
            //    _selectedTaskNode.IsSelected = true;
        }

        #endregion

        #region Task Type

        private int _selectedTaskTypeIndex;
        public int SelectedTaskTypeIndex
        {
            get { return _selectedTaskTypeIndex; }
            set
            {
                SetFieldTaskDialogVM<int>(ref _selectedTaskTypeIndex, value);
                EditingTask.TaskTypeID = TaskTypesCb[_selectedTaskTypeIndex].ID;
                UpdatePropCollection();
                UpdateListsCollection();
            }
        }

        private ObservableCollection<TaskType> _taskTypesCb;
        public ObservableCollection<TaskType> TaskTypesCb //todo для чего Observable ? обычный List подойдёт
        {
            get { return _taskTypesCb; }
            set
            {
                SetFieldTaskDialogVM<ObservableCollection<TaskType>>(ref _taskTypesCb, value);
            }
        }
        private void _generate_TaskTypesCb()
        {
            TaskTypesCb = new ObservableCollection<TaskType>();
            List<TaskType> types = Context.typesWork.Read_TaskTypes();
            foreach (var t in types)
            {
                TaskTypesCb.Add(t);
            }
        }

        #endregion

        #region helpers
        void FilterPropValues()
        {
            if (EditingTask.TaskTypeID != _task.TaskTypeID)
            {
                //удалить все свойства старые, которых сейчас нет
                var curPVIds = PropValuesCollection.Select(pvi => pvi.propVal.ID);
                var propValuesToDelete = EditingTask.PropValues.Where(pv => !curPVIds.Contains(pv.ID));
                Context.taskWork.DeleteProperties(propValuesToDelete.ToList());
            }
            var NullPropValues = PropValuesCollection.Where(pvi => pvi.propVal.ValueDate == null && (pvi.propVal.ValueText == null || pvi.propVal.ValueText == "") && pvi.propVal.ValueTime == null && pvi.propVal.ValueInt == null).ToList();
            foreach (var pvi in NullPropValues)
                PropValuesCollection.Remove(pvi);
            Context.taskWork.DeleteProperties(NullPropValues.Select(pvi => pvi.propVal).ToList());
            EditingTask.PropValues = (ICollection<PropValue>)(PropValuesCollection.Select(pvi => pvi.propVal).ToList());
        }

        #endregion

        #region Commands       

        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private bool CanAccept(object obj)
        {
            return true;
        }

        public void Accept(object obj)
        {
            ////Обновить в БД значения родительских списков
            //var pvLists = PropValuesCollection.Where(pvi => pvi.propVal.Property.DataType == 5 && pvi.propVal.Property.ListType == true).ToList();
            //foreach (var pvi in pvLists)
            //{
            //    var list = pvi.listsValues;
            //    var listID = pvi.propVal.Property.ListID;
            //    var parentTaskID = pvi.parentListTaskID;
            //    Context.listWork.UpdateListValues(list, parentTaskID, (int)listID);
            //}
            ////Обновить в БД значения своих списков
            //var taskLists = TaskLists.Select(tl => tl.list).ToList();
            //Context.listWork.UpdateLists(taskLists);
            
            //foreach (var listInfo in TaskLists)
            //{
            //    var list = listInfo.listsValues;
            //    var listID = listInfo.list.ID;
            //    var parentTaskID = listInfo.TaskID;
            //    Context.listWork.UpdateListValues(list, parentTaskID, (int)listID);
            //}

            ////Значения доп. полей почистить
            //FilterPropValues();

            if (_command == TaskCommandEnum.Edit)
            {
                if (_editingTask.ID == _editingTask.ParentTaskID || TasksVM.CheckIsChild(_editingTask.ID, _editingTask.ParentTaskID)) // todo по моему параметры неверно передаются в функцию CheckIsChild
                {
                    MessageBox.Show("Нельзя назначить новым родителем потомка или самого себя");
                    return;
                }
                if (_task.ResponsibleID != _editingTask.ResponsibleID)
                {
                    var dialogResult = System.Windows.MessageBox.Show("Обновить ответственного у ВСЕХ дочерних задач на текущего?", "Подтверждение",
               MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (dialogResult == MessageBoxResult.Yes)
                        TasksVM.SetResponsibleForTaskChildren(_editingTask.ID, _editingTask.ResponsibleID);
                }
            }
            else if (_command == TaskCommandEnum.Add)
            {
                //if (SelectedTaskNode != null)
                //{
                //    var parentNode = SelectedTaskNode.ParentNode;
                //    if (parentNode != null)
                //    {
                //        foreach (var currentNodeNeighbour in parentNode.TreeNodes)
                //        {
                //            if (currentNodeNeighbour.Task.TaskName.ToLower() == _editingTask.TaskName.ToLower())
                //            {
                //                MessageBox.Show("Задача с таким именем уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                //                return;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        int aa = 1;
                //    }
                //int parentTaskID = _editingTask.ParentTaskID == null ? 0 : (int)_editingTask.ParentTaskID;
                if (TasksVM.IsExist(_editingTask.TaskName, _editingTask.ParentTaskID))
                {
                    MessageBox.Show("Задача с таким именем уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Context.procedureWork.UpdateTasksIndexNumbers((int)_editingTask.IndexNumber); // обновим индексы с текущего
                //}
            }
            ALLVM.DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(_command, _editingTask));


            //Обновить в БД значения родительских списков
            var pvLists = PropValuesCollection.Where(pvi => pvi.propVal.Property.DataType == 5 && pvi.propVal.Property.ListType == true).ToList();
            foreach (var pvi in pvLists)
            {
                var list = pvi.listsValues;
                var listID = pvi.propVal.Property.ListID;
                var parentTaskID = pvi.parentListTaskID;
                Context.listWork.UpdateListValues(list, parentTaskID, (int)listID);
            }
            //Обновить в БД значения своих списков
            TaskLists.Select(tl => tl.TaskID = _editingTask.ID);
            TaskLists.ForEach(delegate (ListInfo lInfo)
            {
                lInfo.listsValues.ForEach(delegate (ListsValue listVal)
                {
                    listVal.TaskID = _editingTask.ID;
                });
            });
            var taskLists = TaskLists.Select(tl => tl.list).ToList();
            Context.listWork.UpdateLists(taskLists);

            foreach (var listInfo in TaskLists)
            {
                var list = listInfo.listsValues;
                var listID = listInfo.list.ID;
                var taskID = listInfo.TaskID > 0 ? listInfo.TaskID : _editingTask.ID;
                Context.listWork.UpdateListValues(list, taskID, (int)listID);
            }

            //Значения доп. полей почистить
            FilterPropValues();

            //         MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
            //new KeyValuePair<TaskCommandEnum, Task>(_command, _editingTask));

            if (dialog != null)
            {
                dialog.Close();
                dialog = null;
            }
        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        public void Cancel(object obj)
        {
            //ALLVM.DoTaskCommand(new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
            //ALLVM.DoTaskCommand(new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.None, _task));

            //MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
            //    new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
            //MessengerInstance.Send<KeyValuePair<FaveTaskCommandEnum, Task>>(
            //    new KeyValuePair<FaveTaskCommandEnum, Task>(FaveTaskCommandEnum.None, _task));
            if (dialog != null)
            {
                dialog.Close();
                dialog = null;
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            ChangeSelection(null);
            //MessengerInstance.Send<KeyValuePair<TaskCommandEnum, Task>>(
            //    new KeyValuePair<TaskCommandEnum, Task>(TaskCommandEnum.None, _task));
            dialog = null;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public bool SetFieldTaskDialogVM<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
    public class PropValueInfo
    {
        public PropValueInfo(PropValue pv, int _parentTaskID, List<ListsValue> _listsValues)
        {
            propVal = pv;
            parentListTaskID = _parentTaskID;
            listsValues = _listsValues;
        }
        public PropValue propVal { get; set; }
        public int parentListTaskID { get; set; }
        public List<ListsValue> listsValues { get; set; }
    }

    public class ListInfo
    {
        public ListInfo(List lst, List<ListsValue> _listsValues, int _taskID)
        {
            TaskID = _taskID;
            list = lst;
            listsValues = _listsValues;
        }
        public int TaskID { get; set; }
        public List list { get; set; }
        public List<ListsValue> listsValues { get; set; }
    }

    
}

