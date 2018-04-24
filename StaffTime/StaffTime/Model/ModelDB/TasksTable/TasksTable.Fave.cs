using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace StaffTime.Model
{
    public static partial class TasksTable
    {
        private static List<int> _faveTasks;
        public static List<int> FaveTasks { get { return _faveTasks; } }
        
        public static void Create_TaskToFave()
        {
            //TODO
        }
        public static void Read_FaveTasks(int curUserId)
        {
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                _faveTasks = new List<int>();
                _faveTasks = (from t in ctx.UserTasks where t.UserID == curUserId select t.TaskID).ToList<int>();
            }
        }
        public static void Delete_TaskFromFave()
        {
            //TODO
        }
    }
}
