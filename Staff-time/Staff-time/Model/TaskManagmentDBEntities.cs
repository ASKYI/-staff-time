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
        //Создание задачи
        public void Create_Task(Task task)
        {
            Tasks.Add(task);
            SaveChanges();
        }
        public void Create_TaskToFave(int taskID, int curUserID)
        {
            throw new NotImplementedException();
        }

        //Возвращает список правильно созданных (верный тип) задач 
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

        //Возвращает список ID избранных задач пользователя
        public List<int> Read_FaveTasks(int userID)
        {
            return (from t in UserTasks where t.UserID == userID select t.TaskID).ToList();
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

        //Создание работы
        public void Create_Work(Work work)
        {
            Works.Add(work);
            //Создание пустых полей для атрибутов в соотвествии с типом работы
            Create_AttrValuesFields_ForWork(work.ID, (WorkTypeEnum)work.WorkTypeID); 
            SaveChanges();
        }

        //Возвращает список правильно созданных (верный тип) работ (с загрузкой задач)
        public List<Work> Read_AllWorks()
        {
            List<Work> worksDB = Works.Include(w => w.Task).ToList();
            List<Work> works = new List<Work>();
            WorkFactory taskFactory = new WorkFactory();
            foreach(Work w in worksDB)
            {
                works.Add(taskFactory.CreateWork(w));
            }
            return works;
        }

        //Возвращает список правильно созданных (верный тип) работ за определенную дату - дата начала (с загрузкой задач)
        public List<Work> Read_WorksForDate(DateTime date)
        {
            List<Work> worksDB = Works.Include(w => w.Task).Where(w => w.StartDate == date).ToList();
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

        //Изменение работы
        public void Update_Work(Work work)
        {
            var workDB = Works.Where(x => x.ID == work.ID).FirstOrDefault();
            if (workDB.WorkTypeID != work.WorkTypeID) //При изменении типа! Удалить-перенести атрибуты типа
                Update_AttrValuesFields_ForWork(work.ID, (WorkTypeEnum)workDB.WorkTypeID, (WorkTypeEnum)work.WorkTypeID);
            Works.AddOrUpdate(work);
            SaveChanges();
        }
        //Удаление работы
        public void Delete_Work(int workID)
        {
            Delete_AttrValuesFields_ForWork(workID); //Удаление атрибутов работы
            var workDB = Works.Where(x => x.ID == workID).FirstOrDefault();
            Works.Remove(workDB);
            SaveChanges();
        }
        #endregion

        #region IAttrWork
        //При создании новой работы, создание пустых записей атрибутов для типа работы 
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

        //Возвращает значение всех атрибутов для задач (с загрузкой атрибутов)
        public List<AttrValue> Read_AttrValues_ForWork(Work work)
        {
            return AttrValues.Include(a => a.Attribute).Where(a => a.WorkID == work.ID).ToList();
        }

        //При изменении типа работы, изменение записей атрибутов (не сохраняет сопадающие!) 
        public void Update_AttrValuesFields_ForWork(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType)
        {
            //TODO: сохранение значений совпадающих атрибутов
            Delete_AttrValuesFields_ForWork(WorkID);
            Create_AttrValuesFields_ForWork(WorkID, newType);
        }

        //Изменение значений атрибутов
        public void Update_AttrValues(List<AttrValue> values)
        {            
            foreach(var v in values)
                AttrValues.AddOrUpdate(v);
            SaveChanges();
        }

        //Удаление записей значений атрибутов для работы
        public void Delete_AttrValuesFields_ForWork(int WorkID)
        {
            IEnumerable<AttrValue> toDeleteDB = (from a in AttrValues where a.WorkID == WorkID select a).ToList();
            AttrValues.RemoveRange(toDeleteDB);
        }
        #endregion
    }
}
