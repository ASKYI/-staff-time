﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Collections.ObjectModel;
using TestTree.Model;
using GalaSoft.MvvmLight;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace TestTree.ViewModel 
{
    //Этот класс должен быть один. Singleton?
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Status += "Запуск/n";
            Generate_TaskNodesDictionary();

            //Временно
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                CurUser = (from u in ctx.Users where u.ID == 1 select u).FirstOrDefault();
            }
        }

        protected User CurUser { get; set; }
        //Так как с задачами удобнее работать как с узлами дерева (имея доступ ко всем наследникам и предку), 
        //они хранятся в виде узлов дерева.
        //Словарь для облегчения доступа
        protected Dictionary<int, TreeNode> TaskNodesDictionary { get; set; }
        private void Generate_TaskNodesDictionary()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                TaskNodesDictionary = new Dictionary<int, TreeNode>();

                //В таблице ссылка на родителя может содержать идентификатор на задачу, 
                //которая еще не встречалась в таблице при последовательном чтении.
                //В таком случае создается узел с пустым значением задачи, которая заполнится, когда задача встретится.
                //В бд невозможно добавить ссылку на несуществующую задачу (стоит свойство)

                TaskFactory taskFactory = new TaskFactory();
                TreeNodeFactory treeNodeFactory = new TreeNodeFactory();
                foreach (Task taskDB in ctx.Tasks)
                {
                    Task task = taskFactory.CreateTask(taskDB);

                    int id = task.ID;
                    TreeNode treeNode;
                    if (!TaskNodesDictionary.ContainsKey(id)) {
                        treeNode = treeNodeFactory.CreateTreeNode(task);
                        TaskNodesDictionary.Add(id, treeNode);
                    }
                    else {
                        TaskNodesDictionary[id].Task = task;
                        treeNode = TaskNodesDictionary[id];
                        treeNodeFactory.ChangeType(ref treeNode, task);
                        //treeNode.Task = task;
                    }

                    if (task.ParentTaskID != null)
                    {
                        int parentId = (int)task.ParentTaskID; //Из Nullable<int> в int, проверка на null уже была                        

                        if (!TaskNodesDictionary.ContainsKey(parentId))
                            TaskNodesDictionary.Add(parentId, new TreeNode());

                        TreeNode parentTreeNode = TaskNodesDictionary[parentId];


                        parentTreeNode.AddChild(treeNode);
                        treeNode.ParentNode = parentTreeNode;
                    }
                }
            }
        }

        #region Functions for Tasks Lists
        protected ObservableCollection<TreeNode> ConvertTasksIntoNodes(List<int> t)
        {
            ObservableCollection<TreeNode> tasksNodes = new ObservableCollection<TreeNode>();
            if (t != null)            
                foreach (var q in t)
                    tasksNodes.Add(TaskNodesDictionary[q]);
            return tasksNodes;
        }
        protected List<int> GetTasksByProp(string propName, string propValueText = null, Nullable<int> propValueInt = null, 
            Nullable<DateTime> propValueDateTime = null)
          {
              using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
              {
                  Model.Property favProp = (from p in ctx.Properties where p.PropName == propName select p).FirstOrDefault();
                if (favProp != null)
                {
                    switch((TaskPropDataType)favProp.DataType)
                    {
                        case TaskPropDataType.ValueText:
                            return (from p in ctx.PropValues where p.ValueText == propValueText select p.TaskID).ToList<int>();
                        case TaskPropDataType.ValueInt:
                            return (from p in ctx.PropValues where p.ValueInt == propValueInt select p.TaskID).ToList<int>();
                        case TaskPropDataType.ValueDate:
                            return (from p in ctx.PropValues where p.ValueDate == DbFunctions.TruncateTime(propValueDateTime) select p.TaskID).ToList<int>();
                        case TaskPropDataType.ValueTime:
                            return (from p in ctx.PropValues where Convert.ToDateTime(p.ValueTime) == propValueDateTime select p.TaskID).ToList<int>();
                    }
                }
            }
            return null;
        }
        protected List<int> GetTasksByProp(int propID, string propValueText = null, Nullable<int> propValueInt = null,
            Nullable<DateTime> propValueDateTime = null)
          {
              using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
              {
                  Model.Property favProp = (from p in ctx.Properties where p.ID == propID select p).FirstOrDefault();
                  return GetTasksByProp(favProp.ID, propValueText, propValueInt, propValueDateTime);
            }
          }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion

        //Временно
        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                SetField(ref _status, value);
            }
        }
    }
}