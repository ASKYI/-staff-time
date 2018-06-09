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
    public class TabItem : MainViewModel
    {
        public TabItem(string tabName_DayOfWeek, DateTime dateTime)
        {
            TabName = tabName_DayOfWeek;
            Date = dateTime;

            Generate_WorksForDate();

            MessengerInstance.Register< KeyValuePair<int, Work> >(this, _updateWork);
        }

        #region Tab Data
        public string TabName { get; set; }
        public DateTime Date { get; set; }
        #endregion
        #region Works
        private ObservableCollection<WorkInTab> _worksInTab;
        public ObservableCollection<WorkInTab> WorksInTab
        {
            get { return _worksInTab; }
            set
            {
                SetField<ObservableCollection<WorkInTab>>(ref _worksInTab, value);
            }
        }

        public void Generate_WorksForDate()
        {
            SumTime = 0;

            List<Work> works = workWork.Read_WorksForDate(Date);
            WorksInTab = new ObservableCollection<WorkInTab>();
            foreach (Work w in works)
            {
                WorksInTab.Add(new WorkInTab(w, false));
                if (w.Minutes != null)  
                   SumTime += (int)w.Minutes;
            }

            MessengerInstance.Send<int>(SumTime);
        }

        //А ловят-то все семь табов
        private void _updateWork(KeyValuePair<int, Work> pair)
        {
           // if (pair.Value.StartDate != Date)
             //   return;

            if (pair.Key == 2) //Добавить
            {
                WorkFactory factory = new WorkFactory();
                Work work = factory.CreateWork(pair.Value);
                if (pair.Value.Minutes != null)
                    SumTime += (int)pair.Value.Minutes;
                WorksInTab.Add(new WorkInTab(work, true));

                MessengerInstance.Send<int>(SumTime);
                return;
            }

            int index = -1;
            for (int i = 0; i < WorksInTab.Count; i++)
            {
                WorkInTab w = WorksInTab[i];
                if (w.WorkBlockControlDataContext.WorkInBlock.WorkControlDataContext.Work.ID == pair.Value.ID) //Обалдеть сколько оберток, с этим надо что-то делать
                {
                    index = i; 
                }
            }
            if (index == -1)
                return;

            switch (pair.Key)
                { 
                case 0: //Удалить
                    if (WorksInTab[index].WorkBlockControlDataContext.WorkInBlock.WorkControlDataContext.Work.Minutes != null)
                        SumTime -= (int)WorksInTab[index].WorkBlockControlDataContext.WorkInBlock.WorkControlDataContext.Work.Minutes;
                    WorksInTab.Remove(WorksInTab[index]);
                    break;
                case 1: //Редактировать (в том числе сменить тип)
                    Work oldWork = workWork.Read_WorkByID(WorksInTab[index].WorkBlockControlDataContext.WorkInBlock.WorkControlDataContext.Work.ID);
                    if (oldWork.Minutes != null)
                        SumTime -= (int)oldWork.Minutes;
                    WorksInTab.Remove(WorksInTab[index]);

                    if (pair.Value.StartDate.Date == Date.Date)
                    {
                        WorkFactory factory = new WorkFactory();
                        Work work = factory.CreateWork(pair.Value);
                        if (pair.Value.Minutes != null)
                            SumTime += (int)pair.Value.Minutes;
                        WorksInTab.Add(new WorkInTab(work, false));
                    }
                    break;
            }

            MessengerInstance.Send<int>(SumTime);
        }
        #endregion

        private int _sumTime;
        public int SumTime
        {
            get { return _sumTime; }
            set
            {
                SetField(ref _sumTime, value);
            }
        }
    }
}
