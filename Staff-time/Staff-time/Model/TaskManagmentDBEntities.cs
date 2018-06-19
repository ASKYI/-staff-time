using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

using Staff_time.Model.Interfaces;

namespace Staff_time.Model
{
    public partial class TaskManagmentDBEntities : DbContext,
        ITaskWork, IWorkWork, IAttrWork, ITypesWork
    {
        #region ITaskWork

        public void Create_Task(Task task)
        {
            Tasks.Add(task);
            SaveChanges();
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

        public List<int> Read_RootTasks()
        {
            return (from t in Tasks where t.ParentTaskID == null select t.ID).ToList();
        }
     
        public List<int> Read_FaveTasks(int userID)
        {
            return (from t in UserTasks where t.UserID == userID select t.TaskID).ToList();
        }

        public List<int> Read_ChildTasks(int taskID)
        {
            List<Task> tasksDB = Tasks.Where(t => t.ParentTaskID == taskID).ToList();

            List<int> tasks = new List<int>();

            TaskFactory taskFactory = new TaskFactory();
            foreach (Task t in tasksDB)
            {
                tasks.Add(t.ID);
            }
            return tasks;
        }

        public void Update_Task(Task task)
        {
            Tasks.AddOrUpdate(task);
            SaveChanges();
        }
       
        public void Delete_Task(int taskID)
        {
            Task taskBD = Tasks.Where(t => t.ID == taskID).FirstOrDefault();

            List<Task> childTasksBD = (from t in Tasks where t.ParentTaskID == taskID select t).ToList();
            foreach (var t in childTasksBD)   
                t.ParentTaskID = taskBD.ParentTaskID;

            List<Work> worksBD = (from w in Works where w.TaskID == taskID select w).ToList();
            foreach (var w in worksBD) {
                Delete_AttrValuesFields_ForWork(w.ID);
                Works.Remove(w);
            }

            if (taskBD != null)
                Tasks.Remove(taskBD);
            SaveChanges();
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
            
            //Создание пустых полей для атрибутов в соотвествии с типом работы
            Create_AttrValuesFields_ForWork(work.ID, (WorkTypeEnum)work.WorkTypeID); 
            SaveChanges();
        }
  
        public List<Work> Read_AllWorks()
        {
            List<Work> worksDB = Works.Include(w => w.AttrValues.Select(a => a.Attribute)).ToList();

            List<Work> works = new List<Work>();
            WorkFactory taskFactory = new WorkFactory();
            foreach(Work w in worksDB)
            {
                works.Add(taskFactory.CreateWork(w));
            }
            return works;
        }

        public List<int> Read_WorksForDate(DateTime date)
        {
            List<int> l = (from w in Works where w.StartDate == date.Date select w.ID).ToList(); //!!! Date
            return l;
        }
        public List<int> Read_WorksForTask(int taskID)
        {
            return (from w in Works where w.TaskID == taskID select w.ID).ToList();
        }

        public Work Read_WorkByID(int workID)
        {
            return (from w in Works where w.ID == workID select w).FirstOrDefault();
        }

        public void Update_Work(Work work)
        {
            var workDB = Works.Where(x => x.ID == work.ID).FirstOrDefault();
            int oldTypeID = workDB.WorkTypeID, newTypeID = work.WorkTypeID;

            Works.AddOrUpdate(work);

            if (workDB.WorkTypeID != work.WorkTypeID) //При изменении типа! Удалить-перенести атрибуты типа
                Update_AttrValuesFields_ForWork(work.ID, (WorkTypeEnum)oldTypeID, (WorkTypeEnum)newTypeID);

            SaveChanges();
        }

        public void Delete_Work(int workID)
        {
            Delete_AttrValuesFields_ForWork(workID); //Удаление атрибутов работы
            var workDB = Works.Where(x => x.ID == workID).FirstOrDefault();
            if (workDB != null)
                Works.Remove(workDB);

            SaveChanges();
        }
        #endregion

        #region IAttrWork
        public void Create_AttrValuesFields_ForWork(int WorkID, WorkTypeEnum type)
        {
            int typeID = (int)type;
            List<int> attrIDs = (from a in WorkTypeAttrs where a.WorkTypeID == typeID select a.AttrID).ToList();
            foreach(var a in attrIDs)
            {
                AttrValue value = new AttrValue();
                value.WorkID = WorkID;
                value.AttrID = a;
                AttrValues.Add(value);
            }
            SaveChanges();
        }    

        public List<AttrValue> Read_AttrValues_ForWork(Work work)
        {
            return AttrValues.Include(a => a.Attribute).Where(a => a.WorkID == work.ID).ToList();
        }

        public void Update_AttrValuesFields_ForWork(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType)
        {
            Delete_AttrValuesFields_ForWork(WorkID);
            Create_AttrValuesFields_ForWork(WorkID, newType);
        }

        public void Delete_AttrValuesFields_ForWork(int WorkID)
        {
            IEnumerable<AttrValue> toDeleteDB = (from a in AttrValues where a.WorkID == WorkID select a).ToList();
            AttrValues.RemoveRange(toDeleteDB);
        }

        public void Update_AttrValues_ForWork(List<AttrValue> values)
        {
            foreach (var v in values)
            {
                AttrValues.AddOrUpdate(v);
                SaveChanges();
            }
        }
        #endregion

        #region ITypesWork
        public List<WorkType> Read_WorkTypes()
        {
            return WorkTypes.ToList();
        }

        public List<TaskType> Read_TaskTypes()
        {
            return TaskTypes.ToList();
        }
        #endregion
    }
}
