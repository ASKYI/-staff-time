using System;
using System.Collections.Generic;
using System.Linq;
using Staff_time.Model;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public static class WorksVM
    {
        //Так как с работами удобнее работать как с WorkVM (дополнительные поля, доступные из WorkBlockControlVM), 
        //они хранятся в виде WorkVM
        public static SortedDictionary<int, WorkControlViewModelBase> Dictionary { get; set; }

        private static bool _init_tracker = false; //done было public
        public static void Init()
        {
            if (_init_tracker)
                return;
            _init_tracker = true;

            Dictionary = new SortedDictionary<int, WorkControlViewModelBase>();

            List<Work> worksDB = Context.workWork.GetAllWorks(); // todo нам же все работы не нужны! с течением времени будет запускаться всё дольше + нам не нужны будут работы других пользователей
            foreach (Work work in worksDB)
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
