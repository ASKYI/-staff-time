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
        ITaskWork, IWorkWork, IAttrWork
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
            //Создать пустые поля для атрибутов в соотвествии с типом
            Create_AttrValuesFields_ForWorkType(work.ID, (WorkTypeEnum)work.WorkTypeID); 
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
            WorkFactory workFactory = new WorkFactory();
            foreach (Work w in worksDB)
            {
                works.Add(workFactory.CreateWork(w));
            }
            return works;
        }
        public List<Work> Read_WorksForTask(int taskID)
        {
            throw new NotImplementedException();
        }
        public void Update_Work(Work work)
        {
            var workDB = Works.Where(x => x.ID == work.ID).FirstOrDefault();
            if (workDB.WorkTypeID != work.WorkTypeID) //При изменении типа! Удалить-перенести атрибуты типа
                Update_AttrValuesFields_ForWorkType(work.ID, (WorkTypeEnum)workDB.WorkTypeID, (WorkTypeEnum)work.WorkTypeID);
            Works.AddOrUpdate(work);
            SaveChanges();
        }
        public void Delete_Work(int workID)
        {
            Delete_AttrValuesFields_ForWork(workID); //Удаление атрибутов работы
            var workDB = Works.Where(x => x.ID == workID).FirstOrDefault();
            Works.Remove(workDB);
            SaveChanges();
        }
        #endregion

        #region IAttrWork
        public void Create_AttrValuesFields_ForWorkType(int WorkID, WorkTypeEnum type)
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
            //Загрузка знаений атрибутов и связанных с ними атрибутов
            List<AttrValue> l = AttrValues.Include(a => a.Attribute).Where(a => a.WorkID == work.ID).ToList();
            return l;          
            //return (from a in AttrValues where a.WorkID == work.ID select a).ToList<AttrValue>();
        }
        public void Update_AttrValuesFields_ForWorkType(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType)
        {
            //TODO
            throw new NotImplementedException();
        }
        public void Update_AttrValues(List<AttrValue> values)
        {            
            foreach(var v in values)
                AttrValues.AddOrUpdate(v);
            SaveChanges();
        }
        public void Delete_AttrValuesFields_ForWork(int WorkID)
        {
            IEnumerable<AttrValue> toDeleteDB = (from a in AttrValues where a.WorkID == WorkID select a).ToList();
            AttrValues.RemoveRange(toDeleteDB);
        }
        #endregion
    }
}
