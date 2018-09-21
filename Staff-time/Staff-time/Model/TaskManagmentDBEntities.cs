using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using Staff_time.Model.Interfaces;

//done: Переименовано
//Не везде можно лямбды, ведь я возвращаю уже задачи верного типа

namespace Staff_time.Model
{
    public partial class TaskManagmentDBEntities : DbContext, 
        ITaskRepository, IWorkRepository, IAttributeRepository, ITypesRepository
    {

        #region ITaskRepository

        public void AddTask(Task task)
        {
            Tasks.Add(task);
            SaveChanges();
        }
        public void AddTaskToFave(int taskID, int curUserID)
        {
            throw new NotImplementedException();
        }

        public List<Task> GetAllTasks() 
        {
            List<Task> tasks = new List<Task>();
            List<Task> tasksDB = new List<Task>(Tasks.OrderBy(t => t.IndexNumber));

            TaskFactory taskFactory = new TaskFactory();
            foreach (Task t in tasksDB)
            {
                tasks.Add(taskFactory.CreateTask(t));
            }
            return tasks;
            //return Tasks.ToList(); - без верных типов
        }

        public List<int> GetRootTasks()
        {
            return (from t in Tasks where t.ParentTaskID == null select t.ID).ToList();
        }
     
        public List<int> GetFaveTasks(int userID)
        {
            return (from t in UserTasks where t.UserID == userID select t.TaskID).ToList();
        }

        public List<int> GetChildTasks(int taskID)
        {
            List<Task> tasksDB = Tasks.Where(t => t.ParentTaskID == taskID).ToList();

            List<int> tasks = new List<int>();
            foreach (Task t in tasksDB)
            {
                tasks.Add(t.ID);
            }
            return tasks;
            //return Tasks.Where(t => t.ParentTaskID == taskID).Select(task => task.ID).ToList(); - без верных типов
        }

        public void UpdateTask(Task task)
        {
            Tasks.AddOrUpdate(task);
            SaveChanges();
        }
       
        public void DeleteTask(int taskID)
        {
            Task taskBD = Tasks.Where(t => t.ID == taskID).FirstOrDefault();

            List<Task> childTasksBD = (from t in Tasks where t.ParentTaskID == taskID select t).ToList();
            foreach (var t in childTasksBD)   
                t.ParentTaskID = taskBD.ParentTaskID;

            List<Work> worksBD = (from w in Works where w.TaskID == taskID select w).ToList();
            foreach (var w in worksBD) {
                DeleteAttributValuesFieldsForWork(w.ID);
                Works.Remove(w);
            }

            if (taskBD != null)
                Tasks.Remove(taskBD);
            SaveChanges();
        }
        public void DeleteTaskFromFave(int taskID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWorkRepository

        public void AddWork(Work work)
        {
            Works.Add(work);
            
            //Создание пустых полей для атрибутов в соотвествии с типом работы
            AddAtributeValuesFields(work.ID, (WorkTypeEnum)work.WorkTypeID); 
            SaveChanges();
        }
  
        public List<Work> GetAllWorks()
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

        public List<int> GetWorksForDate(DateTime date)
        {
            return (from w in Works where w.StartDate == date.Date select w.ID).ToList();
        }
        public List<int> GetWorksForTask(int taskID)
        {
            return (from w in Works where w.TaskID == taskID select w.ID).ToList();
        }

        public Work GetWorkByID(int workID)
        {
            return (from w in Works where w.ID == workID select w).FirstOrDefault();
        }

        public void UpdateWork(Work work)
        {
            var workDB = Works.Where(x => x.ID == work.ID).FirstOrDefault();
            int oldTypeID = workDB.WorkTypeID, newTypeID = work.WorkTypeID;

            Works.AddOrUpdate(work);

            // done: была ошибка со сравнением одного и того же объекта
            if (oldTypeID != newTypeID) //При изменении типа! Изменить атрибуты типа
                UpdateAttributeValuesFieldsForWork(work.ID, (WorkTypeEnum)oldTypeID, (WorkTypeEnum)newTypeID);

            SaveChanges();
        }

        public void DeleteWork(int workID)
        {
            DeleteAttributValuesFieldsForWork(workID); //Удаление атрибутов работы
            var workDB = Works.Where(x => x.ID == workID).FirstOrDefault();
            if (workDB != null)
                Works.Remove(workDB);

            SaveChanges();
        }

        #endregion

        #region IAttributeRepository

        public void AddAtributeValuesFields(int WorkID, WorkTypeEnum type)
        {
            int typeID = (int)type;
            List<int> attrIDs = (from a in WorkTypeAttrs where a.WorkTypeID == typeID select a.AttrID).ToList();
            foreach(var a in attrIDs)
            {
                AttrValue value = new AttrValue();
                value.WorkID = WorkID;
                value.AttrID = a;
                //value.DataType = ; //done-todo: заполнить тип откуда?
                AttrValues.Add(value);
            }
            SaveChanges();
        }    

        public List<AttrValue> GetAttributeValuesForWork(Work work) //done: Пусть будет запасной метод, как и для избранных задач
        {
            return AttrValues.Include(a => a.Attribute).Where(a => a.WorkID == work.ID).ToList();
        }

        public void UpdateAttributeValuesFieldsForWork(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType)
        {
            DeleteAttributValuesFieldsForWork(WorkID);
            AddAtributeValuesFields(WorkID, newType);
        }

        public void DeleteAttributValuesFieldsForWork(int WorkID)
        {
            // todo: довольно странный способ удаления объектов, но в EF реально грустно
            // здесь есть некоторый ускоритель, в некоторых случаях он реально может спасти https://habr.com/post/203214/

            IEnumerable<AttrValue> toDeleteDB = (from a in AttrValues where a.WorkID == WorkID select a).ToList();
            AttrValues.RemoveRange(toDeleteDB);
        }

        public void UpdateAttributeValuesForWork(List<AttrValue> values)
        {
            foreach (var v in values)
            {
                AttrValues.AddOrUpdate(v);
                SaveChanges();
            }
        }

        #endregion

        #region ITypesRepository

        public List<WorkType> GetWorkTypes()
        {
            return WorkTypes.ToList();
        }

        public List<TaskType> GetTaskTypes()
        {
            return TaskTypes.ToList();
        }

        #endregion

    }
}
