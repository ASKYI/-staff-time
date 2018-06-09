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
    enum WorkCommandEnum { Add, Delete, Update }
    
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

        private ObservableCollection<Work> _worksInTab;
        public ObservableCollection<Work> WorksInTab
        {
            get { return _worksInTab; }
            set
            {
                SetField(ref _worksInTab, value);
            }
        }

        public void Generate_WorksForDate()
        {
            SumTime = 0;
            WorksInTab = new ObservableCollection<Work>();

            List<int> works = Context.workWork.Read_WorksForDate(Date);
            foreach (int id in works)
            {
                Work w = WorksVM.Dictionary[id];

                WorksInTab.Add(w);
                SumTime += w.Minutes;
            }

            MessengerInstance.Send<long>(SumTime);
        }

        private void _doWorkCommand(KeyValuePair<WorkCommandEnum, Work> pair)
        {
            WorkCommandEnum command = pair.Key;
            Work work = pair.Value;

            if (pair.Key == WorkCommandEnum.Add && work.StartDate.Date == Date.Date)
            {
                WorksVM.Add(work);
                Work newWork = WorksVM.Dictionary[work.ID];

                WorksInTab.Add(newWork);

                SumTime += newWork.Minutes;
                MessengerInstance.Send<long>(SumTime);
                return;
            }

            int index = -1; //!!!
            for (int i = 0; i < WorksInTab.Count; i++)
            {
                if (WorksInTab[i].ID == work.ID)
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
                    SumTime -= WorksInTab[index].Minutes;

                    WorksVM.Delete(work.ID);
                    WorksInTab.Remove(WorksInTab[index]);

                    MessengerInstance.Send<long>(SumTime);
                    break;
                case WorkCommandEnum.Update:
                    WorksVM.Update(work);
                    Work newWork = WorksVM.Dictionary[work.ID];
          
                    Work oldWork = Context.workWork.Read_WorkByID(work.ID);
                    SumTime -= oldWork.Minutes;
                    WorksInTab.Remove(WorksInTab[index]);

                    if (work.StartDate.Date == Date.Date)
                    {
                        WorksInTab.Add(newWork);
                        SumTime += newWork.Minutes;
                    }

                    MessengerInstance.Send<long>(SumTime);
                    break;
            }
        }

        #endregion
    }
}
