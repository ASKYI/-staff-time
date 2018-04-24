using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace StaffTime.Model.ModelDB.WorkTable
{
    public static partial class WorksTable
    {
        public static void Create_Work(int taskID, Work work)
        {
            //TODO
        }
        public static List<int> Read_WorksForTask(int taskID)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                return (from x in ctx.Works where x.TaskID == taskID select x.ID).ToList<int>();
            }
        }
        public static List<Work> Read_WorksForDate(DateTime date)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                return (from x in ctx.Works select x).ToList();
            }
        }
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
