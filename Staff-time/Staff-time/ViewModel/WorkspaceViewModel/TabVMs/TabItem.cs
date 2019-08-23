using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Staff_time.Helpers;

namespace Staff_time.ViewModel
{
    public enum WorkCommandEnum { Add, Delete, Update, None } // todo, чтобы воспользоваться этим enum, надо вписать using Staff_time.ViewModel, здесь же можно сделать расширение для Work, чтобы легче было вызывать
    enum SortType: int {ASC = 0, DESC}
    //static class WorkExtension
    //{
    //    public static void Add(this Work work, IMessenger messenger)  // теперь в любом месте можно легко вызывать
    //    {
    //        messenger.Send(new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, work));
    //    }
    //}

    public class TabItem : MainViewModel, INotifyPropertyChanged
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;
            IsEnabled = true;
            Generate_WorksForDate();
            MessengerInstance.Register<MessageWorkObject>(this, _doWorkCommand);
        }

        public void UnregisterEvents()
        {
            MessengerInstance.Unregister<MessageWorkObject>(this, _doWorkCommand);
        }

        public void Update(string newTabName, DateTime dateTime)
        {
            TabName = newTabName;
            Date = dateTime;

            Generate_WorksForDate();
        }

        private string _tabName;
        public string TabName
        {
            get
            {
                return _tabName;
            }
            set
            {
                _tabName = value;
                NotifyPropertyChanged("TabName");
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged("IsEnabled");
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }

        private long _sumTime;
        public long SumTime
        {
            get { return _sumTime; }
            set
            {
                SetField(ref _sumTime, value);
            }
        }

        #region Works
        public void SortByTime(int direction)
        {
            if (direction == (int)SortType.ASC)
                WorksInTab = new ObservableCollection<WorkInTab>(WorksInTab.OrderBy(w => w.WorkBlockContext.Work.StartTime));
            else
                WorksInTab = new ObservableCollection<WorkInTab>(WorksInTab.OrderByDescending(w => w.WorkBlockContext.Work.StartTime));
        }

        public void SortByName(int direction)
        {
            if (direction == (int)SortType.ASC)
                WorksInTab = new ObservableCollection<WorkInTab>(WorksInTab.OrderBy(w => w.WorkBlockContext.Work.Task.TaskName));
            else
                WorksInTab = new ObservableCollection<WorkInTab>(WorksInTab.OrderByDescending(w => w.WorkBlockContext.Work.Task.TaskName));
        }

        private ObservableCollection<WorkInTab> _worksInTab;
        public ObservableCollection<WorkInTab> WorksInTab
        {
            get { return _worksInTab; }
            set
            {
                SetField(ref _worksInTab, value);
                NotifyPropertyChanged("WorksInTab");
            }
        }

        private bool _resetWorksView;
        public bool ResetWorksView
        {
            get { return _resetWorksView; }
            set
            {
                _resetWorksView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ResetWorksView"));
            }
        }
        public void AddWork(WorkInTab work)
        {
            WorksInTab.Add(work);
            WorksInTab = new ObservableCollection<WorkInTab>(WorksInTab.OrderBy(w => w.WorkBlockContext.Work.ID));
            ResetWorksView = true;
        }

        public void Generate_WorksForDate()
        {
            SumTime = 0;
            WorksInTab = new ObservableCollection<WorkInTab>();
            List<int> works = Context.workWork.Read_WorksForDate(Date);
            foreach (int id in works)
            {
                Work w = WorksVM.Dictionary[id].Work;

                WorksInTab.Add(new WorkInTab(w.ID));
                SumTime += w.Minutes;
            }


            //List<int> works = Context.workWork.Read_WorksForDate(Date);
            //if (WorksInTab == null)
            //    WorksInTab = new ObservableCollection<WorkInTab>();

            //var curWorks = WorksInTab.Select(wt => wt.WorkBlockContext.Work.ID);
            //var workdToAdd = works.Where(w => !curWorks.Contains(w));
            //foreach (int id in workdToAdd)
            //{
            //    Work w = WorksVM.Dictionary[id].Work;

            //    WorksInTab.Add(new WorkInTab(w.ID));

            //}
            //foreach (var wt in WorksInTab)
            //    SumTime += wt.WorkBlockContext.Work.Minutes;

            MessengerInstance.Send<long>(SumTime);
        }

        private void _doWorkCommand(MessageWorkObject obj)
        {
            if ((obj.dt.Year != Date.Year || obj.dt.Month != Date.Month || obj.dt.Day != Date.Day) && obj._commandType != WorkCommandEnum.Update)
                return;
            dialog = null;

            WorkCommandEnum command = obj._commandType;
            Work work = obj._work;

            if (command == WorkCommandEnum.None)
                return;
            if (command == WorkCommandEnum.Add
                && (work.StartDate.Date.Year == Date.Date.Year && work.StartDate.Date.Month == Date.Date.Month && work.StartDate.Date.Day == Date.Date.Day))
            {
                var newWorkID = WorksVM.Add(work);
                Work newWork = WorksVM.Dictionary[newWorkID].Work;

                AddWork(new WorkInTab(newWorkID, true));

                SumTime += newWork.Minutes;
                MessengerInstance.Send<long>(SumTime);

                return;
            }

            int index = -1;
            for (int i = 0; i < WorksInTab.Count; i++)
            {
                if (WorksInTab[i].WorkBlockContext.Work.ID == work.ID)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
                return;

            switch (command)
            {
                case WorkCommandEnum.Delete:
                    SumTime -= WorksInTab[index].WorkBlockContext.Work.Minutes;

                    WorksVM.Delete(work.ID);
                    WorksInTab[index].WorkBlockContext.UnRegister();
                    WorksInTab.Remove(WorksInTab[index]);

                    MessengerInstance.Send<long>(SumTime); // todo а это нельзя сделать в сэттере свойства SumTime ?
                    break;
                case WorkCommandEnum.Update:

                    int oldWorkMinutes = Context.workWork.Read_WorkByID(work.ID).Minutes;

                    Work newWork = WorksVM.Update(work);
                    //Work newWork = WorksVM.Dictionary[work.ID].Work;

                    SumTime -= oldWorkMinutes;

                    if (newWork.StartDate.Date == Date.Date)
                    {
                        //WorksInTab[index].WorkBlockContext = new WorkBlockControlViewModel(newWork.ID, false);
                        MainWindow.IsEnable = true;
                        WorksInTab[index].WorkBlockContext.IsEditing = false;

                        SumTime += newWork.Minutes;
                    }
                    else
                    {
                        WorksInTab[index].WorkBlockContext.UnRegister();
                        WorksInTab.Remove(WorksInTab[index]);
                    }


                    //if (work.StartDate.Date == Date.Date)
                    //{
                    //    AddWork(new WorkInTab(newWork.ID));
                    //    SumTime += newWork.Minutes;
                    //}

                    MessengerInstance.Send<long>(SumTime);
                    break;
            }
        }

        #endregion

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
