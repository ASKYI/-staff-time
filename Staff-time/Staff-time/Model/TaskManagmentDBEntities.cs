using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Staff_time.Model.Interfaces;

namespace Staff_time.Model
{
    public partial class TaskManagmentDBEntities : DbContext,
        ITaskWork, IWorkWork
    {
        #region ITaskWork
        public void Create_Task(Task task)
        {
            throw new NotImplementedException();
        }
        public void Create_TaskToFave(int taskID, int curUserID)
        {
            throw new NotImplementedException();
        }
        public List<Task> Read_AllTasks()
        {
            return Tasks.ToList<Task>();
        }
        public List<int> Read_FaveTasks(int curUserID)
        {
            return (from t in UserTasks where t.UserID == curUserID select t.TaskID).ToList<int>();
        }
        public void Update_Task(Task task)
        {
            throw new NotImplementedException();
        }
        public void Delete_Task(int taskID)
        {
            throw new NotImplementedException();
        }
        public void Delete_TaskFromFave(int taskID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IWorkWork
        public void Create_Work(int taskID, Work work)
        {
            throw new NotImplementedException();
        }
        public List<Work> Read_AllWorks()
        {
            return Works.ToList<Work>();
        }
        public List<Work> Read_WorksForDate(DateTime date)
        {
            return (from x in Works where x.Date == date.Date select x).ToList<Work>();
        }
        public List<Work> Read_WorksForTask(int taskID)
        {
            throw new NotImplementedException();
        }
        public void Update_Work(Work work)
        {
            Entry(work).State = EntityState.Modified;
            SaveChanges();
        }
        public void Delete_Work(int workID)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
