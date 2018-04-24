using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffTime.Model
{
    public static partial class TaskTable
    {
        public static List<int> FaveTasks { get; }
        
        public static void Create_TaskToFave()
        {
            //TODO
        }
        public static void Read_FaveTasks(int curUserId)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                FaveTasks = (from t in ctx.UserTasks where t.UserID == curUserId select t.TaskID).ToList<int>();
            }
        }
        public static void Delete_TaskFromFave()
        {
            //TODO
        }
    }
}
