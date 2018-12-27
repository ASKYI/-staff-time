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

namespace Staff_time.ViewModel
{
    enum WorkCommandEnum { Add, Delete, Update, None } // todo, чтобы воспользоваться этим enum, надо вписать using Staff_time.ViewModel, здесь же можно сделать расширение для Work, чтобы легче было вызывать

    //static class WorkExtension
    //{
    //    public static void Add(this Work work, IMessenger messenger)  // теперь в любом месте можно легко вызывать
    //    {
    //        messenger.Send(new KeyValuePair<WorkCommandEnum, Work>(WorkCommandEnum.Delete, work));
    //    }
    //}

    public class TabItem : MainViewModel
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;

            Generate_WorksForDate();

            MessengerInstance.Register< KeyValuePair<WorkCommandEnum, Work> >(this, _doWorkCommand);
        }

        public string TabName { get; set; }

        public DateTime Date { get; set; }

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

        private ObservableCollection<WorkInTab> _worksInTab;
        public ObservableCollection<WorkInTab> WorksInTab
        {
            get { return _worksInTab; }
            set
            {
                SetField(ref _worksInTab, value);
            }
        }

        public void AddWork(WorkInTab work)
        {
            WorksInTab.Add(work);
            WorksInTab = new ObservableCollection<WorkInTab>(WorksInTab.OrderBy(w => w.WorkBlockContext.Work.ID));
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

            MessengerInstance.Send<long>(SumTime);
        }

        private void _doWorkCommand(KeyValuePair<WorkCommandEnum, Work> pair)
        {
            dialog = null;

            WorkCommandEnum command = pair.Key;
            Work work = pair.Value;

            if (command == WorkCommandEnum.None)
                return;
            if (command == WorkCommandEnum.Add 
                && work.StartDate.Date == Date.Date)
            {
                var newWorkID = WorksVM.Add(work);
                Work newWork = WorksVM.Dictionary[newWorkID].Work;
                
                AddWork(new WorkInTab(newWork.ID));

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
                    WorksInTab.Remove(WorksInTab[index]);

                    MessengerInstance.Send<long>(SumTime); // todo а это нельзя сделать в сэттере свойства SumTime ?
                    break;
                case WorkCommandEnum.Update:

                    int oldWorkMinutes = Context.workWork.Read_WorkByID(work.ID).Minutes;

                    WorksVM.Update(work);
                    Work newWork = WorksVM.Dictionary[work.ID].Work;
                    
                    SumTime -= oldWorkMinutes;
                    WorksInTab.Remove(WorksInTab[index]);

                    if (work.StartDate.Date == Date.Date)
                    {
                        AddWork(new WorkInTab(newWork.ID));
                        SumTime += newWork.Minutes;
                    }

                    MessengerInstance.Send<long>(SumTime);
                    break;
            }
        }

        #endregion
    }
}
