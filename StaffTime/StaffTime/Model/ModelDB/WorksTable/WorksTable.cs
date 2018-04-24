using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace StaffTime.Model.ModelDB.WorkTable
{
    public static partial class WorksTable
    {
        private static List<Work> _works;
        public static List<Work> Works { get { return _works; } }

        public static void Create_Work(int taskID, Work work)
        {
            //TODO
        }
        #region Read
        public static void Read_Works()
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                _works = new List<Work>();

                _works /*= (from x in ctx.Tasks.Include(s => s.Works).Include(s => s.TaskType).Include(s => s.PropValues).Include(s => s.UserTasks)
                          select x).ToList()*/;
            }
        }
        public static List<int> Read_WorksForTask(int taskID)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                return (from x in ctx.Works where x.TaskID == taskID select x.ID).ToList<int>();
            }
        }
        public static List<int> Read_WorksForDate(DateTime date)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                return (from x in ctx.Works where x.Date == date.Date select x.ID).ToList();
            }
        }
        #endregion
        public static void Update_Work()
        {
            //TODO
        }
        public static void Delete_Work(int workId)
        {
            //TODO
        }
    }
}
