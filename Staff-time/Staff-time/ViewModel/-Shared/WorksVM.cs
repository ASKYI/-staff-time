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
    }
}
