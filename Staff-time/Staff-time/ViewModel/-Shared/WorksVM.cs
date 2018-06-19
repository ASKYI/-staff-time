﻿using System;
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

namespace Staff_time.ViewModel
{
    public static class WorksVM
    {
        //Так как с работами удобнее работать как с WorkVM (дополнительные поля, доступные из WorkBlockControlVM), 
        //они хранятся в виде WorkVM
        public static Dictionary<int, WorkControlViewModelBase> Dictionary { get; set; }

        public static bool init_tracker = false;
        public static void Init()
        {
            if (init_tracker)
                return;
            init_tracker = true;

            Dictionary = new Dictionary<int, WorkControlViewModelBase>();

            List<Work> worksDB = Context.workWork.Read_AllWorks();
            foreach(Work work in worksDB)
            {
                //Если разделять представления для разных типов работ, здесь понадобится Factory
                Dictionary.Add(work.ID, new WorkControlViewModel(work));
            }
        }

        public static void Add(Work work)
        {
            //DB
            Context.workWork.Create_Work(work);

            //VM
            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work); 
            Dictionary.Add(newWork.ID, new WorkControlViewModel(newWork)); //Аналогично, другое создание при разных типах
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
    }
}
