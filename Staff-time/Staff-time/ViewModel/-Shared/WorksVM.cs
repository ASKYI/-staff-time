using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using Staff_time.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using Staff_time.Model.UserModel;

namespace Staff_time.ViewModel
{
    public static class WorksVM
    {
        //Так как с работами удобнее работать как с WorkVM (дополнительные поля, доступные из WorkBlockControlVM), 
        //они хранятся в виде WorkVM
        public static SortedDictionary<int, WorkControlViewModelBase> Dictionary { get; set; } // todo зачем здесь sortedDictionary ? его доп. функциональность не используется

        public static bool init_tracker = false; // todo для чего поле public?
        public static void Init()
        {
            if (init_tracker)
                return;
            init_tracker = true;

            Dictionary = new SortedDictionary<int, WorkControlViewModelBase>();

            List<Work> worksDB = Context.workWork.Read_AllWorks(GlobalInfo.CurrentUser.ID); // todo нам же все работы не нужны! с течением времени будет запускаться всё дольше + нам не нужны будут работы других пользователей
            foreach(Work work in worksDB)
            {
                //Если разделять представления для разных типов работ, здесь понадобится Factory
                Dictionary.Add(work.ID, new WorkControlViewModel(work));
            }
        }

        public static int Add(Work work)
        {
            //DB
            Context.workWork.Create_Work(work);

            //VM
            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work); 
            Dictionary.Add(newWork.ID, new WorkControlViewModel(newWork) { IsEdititig = true }); //Аналогично, другое создание при разных типах
            return newWork.ID;
        }
        public static void Delete (int workID)
        {
            //DB
            Context.workWork.Delete_Work(workID);

            //Vm
            Dictionary.Remove(workID);
        }
        public static void Update(Work work)
        {
            //DB
            Context.workWork.Update_Work(work);

            //VM
            Dictionary.Remove(work.ID);

            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work);
            Dictionary.Add(newWork.ID, new WorkControlViewModel(newWork)); //Аналогично, другое создание при разных типах
        }

        public static bool IsEditing()
        {
            var editWorks = Dictionary.Select(d => d.Value).Where(w => w.IsEdititig == true).ToList();
            return editWorks.Count > 0;
        }

        #region other methods

        public static void CollapseAll()
        {
            foreach (var t in WorksVM.Dictionary)
            {
                t.Value.IsExpanded = false;
            }
        }

        public static void ExpandAll()
        {
            foreach (var t in WorksVM.Dictionary)
            {
                t.Value.IsExpanded = true;
            }
        }
        #endregion //other methods
    }
}
