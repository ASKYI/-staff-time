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
            List<Task> tasks = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();
            foreach (Task t in Tasks)
            {
                tasks.Add(taskFactory.CreateTask(t));
            }
            return tasks;
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
        public void Create_Work(Work work)
        {
            Works.Add(work);
            SaveChanges();
        }
        public List<Work> Read_AllWorks()
        {
            List<Work> works = new List<Work>();
            WorkFactory taskFactory = new WorkFactory();
            foreach(Work w in Works)
            {
                works.Add(taskFactory.CreateWork(w));
            }
            return works;
        }
        public List<Work> Read_WorksForDate(DateTime date)
        {
            List<Work> worksDB = (from x in Works where x.StartDate == date.Date select x).ToList<Work>();
            List<Work> works = new List<Work>();
            WorkFactory taskFactory = new WorkFactory();
            foreach (Work w in worksDB)
            {
                works.Add(taskFactory.CreateWork(w));
            }
            return works;
        }
        public List<Work> Read_WorksForTask(int taskID)
        {
            throw new NotImplementedException();
        }
        public void Update_Work(Work work)
        {
            // Entry(work).State = EntityState.Modified;
            var workDB = Works.Where(x => x.ID == work.ID).FirstOrDefault();
            workDB.WorkName = work.WorkName;
            workDB.StartDate = work.StartDate;
            SaveChanges();
        }
        public void Delete_Work(int workID)
        {
            var workDB = Works.Where(x => x.ID == workID).FirstOrDefault();
            Works.Remove(workDB);
            SaveChanges();
        }
        #endregion
    }
}
