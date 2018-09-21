﻿using System;
using System.Collections.Generic;
using System.Linq;
using Staff_time.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;

namespace Staff_time.ViewModel
{
    enum WorkCommandEnum { Add, Delete, Update, None }  
    // todo, чтобы воспользоваться этим enum, надо вписать using Staff_time.ViewModel, здесь же можно сделать расширение для Work, чтобы легче было вызывать
    //но ведь он используется только в ViewModel

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
                MessengerInstance.Send<long>(SumTime); //done
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

            List<int> works = Context.workWork.GetWorksForDate(Date);
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
                WorksVM.Add(work);
                Work newWork = WorksVM.Dictionary[work.ID].Work;
                
                AddWork(new WorkInTab(newWork.ID));

                SumTime += newWork.Minutes;
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
                    
                    break;
                case WorkCommandEnum.Update:
                    int oldWorkMinutes = Context.workWork.GetWorkByID(work.ID).Minutes;

                    WorksVM.Update(work);
                    Work newWork = WorksVM.Dictionary[work.ID].Work;
                    
                    SumTime -= oldWorkMinutes;
                    WorksInTab.Remove(WorksInTab[index]);

                    if (work.StartDate.Date == Date.Date)
                    {
                        AddWork(new WorkInTab(newWork.ID));
                        SumTime += newWork.Minutes;
                    }
                    
                    break;
            }
        }

        #endregion
    }
}
