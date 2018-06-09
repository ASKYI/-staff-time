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

namespace Staff_time.ViewModel
{
    public static class WorksVM
    {
        public static Dictionary<int, Work> Dictionary { get; set; }

        public static bool init_tracker = false;
        public static void Init()
        {
            if (init_tracker)
                return;

            Dictionary = new Dictionary<int, Work>();

            List<Work> worksDB = Context.workWork.Read_AllWorks();
            foreach(Work work in worksDB)
            {
                Dictionary.Add(work.ID, work);
            }

            init_tracker = true;
        }

        public static void Add(Work work)
        {
            //DB
            Context.workWork.Create_Work(work);

            //VM
            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work); 
            Dictionary.Add(newWork.ID, newWork);
        }
        public static void Delete (int workID)
        {
            //DB

            //Vm
            Dictionary.Remove(workID);
        }
        public static void Update(Work work)
        {
            //DB

            //VM
            Dictionary.Remove(work.ID);

            WorkFactory factory = new WorkFactory();
            Work newWork = factory.CreateWork(work);
            Dictionary.Add(newWork.ID, newWork);
        }
    }
}
