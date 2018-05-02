using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace Staff_time.Model
{
    public static partial class WorksTable
    {
        private static Dictionary<int, Work> _works;
        public static Dictionary<int, Work> Works { get { return _works; } }

        public static void Create_Work(int taskID, Work work)
        {
            //TODO
        }
        #region Read
        public static void Read_Works()
        {
            _works = new Dictionary<int, Work>();
            List<Work> worksDB = new List<Work>();
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                worksDB = ctx.Works.Include(w => w.AttrValues).Include(w => w.Task).Include(w => w.WorkType).ToList();

                WorkFactory workFactory = new WorkFactory();
                foreach (Work w in worksDB)
                {
                    //ctx.Entry(w).Reference("Task").Load(); Явная загрузка связанной работы
                    _works.Add(w.ID, workFactory.CreateWork(w));
                }
            }
        }
        public static List<Work> Read_WorksForTask(int taskID)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                return (from x in ctx.Works.Include(w => w.AttrValues).Include(w => w.Task).Include(w => w.WorkType) where x.TaskID == taskID select x).ToList();
            }
        }
        public static List<Work> Read_WorksForDate(DateTime date)
        {
            List<int> workIDs = new List<int>();
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                workIDs = (from x in ctx.Works where x.Date == date.Date select x.ID).ToList();
            }

            List<Work> worksForDate = new List<Work>();
            foreach(int i in workIDs)
            {
                worksForDate.Add(Works[i]);
            }
            return worksForDate;
        }
        #endregion
        public static void Update_Work(int id, Work work)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                var workDB = ctx.Works.Where(x => x.ID == id).FirstOrDefault();
                workDB.WorkName = work.WorkName;
                //...
                // ctx.Works(work).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            //?
            Read_Works();
        }
        public static void Delete_Work(int workId)
        {
            //TODO
        }
    }
}