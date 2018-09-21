using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using Staff_time.Model.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public static class WorksVM
    {
        //Так как с работами удобнее работать как с WorkVM (дополнительные поля, доступные из WorkBlockControlVM), 
        //они хранятся в виде WorkVM
        public static SortedDictionary<int, WorkControlViewModelBase> Dictionary { get; set; }

        public static bool init_tracker = false;
        public static void Init()
        {
            if (init_tracker)
                return;
            init_tracker = true;

            Dictionary = new SortedDictionary<int, WorkControlViewModelBase>();

            List<Work> worksDB = Context.workWork.GetAllWorks();
            foreach(Work work in worksDB)
            {
                //Если разделять представления для разных типов работ, здесь понадобится Factory
                Dictionary.Add(work.ID, new WorkControlViewModel(work));
            }
        }

        public static void Add(Work work)
        {
            //DB
            Context.workWork.AddWork(work);

            //VM
            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work); 
            Dictionary.Add(newWork.ID, new WorkControlViewModel(newWork) { IsEdititig = true }); //Аналогично, другое создание при разных типах
        }
        public static void Delete (int workID)
        {
            //DB
            Context.workWork.DeleteWork(workID);

            //Vm
            Dictionary.Remove(workID);
        }
        public static void Update(Work work)
        {
            //DB
            Context.workWork.UpdateWork(work);

            //VM
            Dictionary.Remove(work.ID);

            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work);
            Dictionary.Add(newWork.ID, new WorkControlViewModel(newWork)); //Аналогично, другое создание при разных типах
        }
    }
}
