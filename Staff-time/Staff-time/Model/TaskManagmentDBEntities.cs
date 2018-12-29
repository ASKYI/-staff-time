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
        ITaskWork, IWorkWork, IAttrWork, ITypesWork, IUserWork
    {
        #region IUserWork
        public List<User> Read_AllUsers()
        {
            return Users.OrderBy(u => u.UserName).ToList();
        }
        #endregion

        #region ITaskWork

        public void Create_Task(Task task) // todo здесь надо подобрать более точное название, Register или Add, по сути ведь Task уже создан, так что точно не Create
        {
            Tasks.Add(task);
            SaveChanges();
        }
        public void Create_TaskToFave(int taskID, int curUserID)
        {
            UserTasks.Add(new UserTask() { TaskID = taskID, UserID = curUserID });
            SaveChanges();
        }

        public void Update_UserTaskExpended(int taskID, int curUserID, bool isExpanded)
        {
            var userTask = UserTasks.Where(ut => ut.TaskID == taskID && ut.UserID == curUserID).FirstOrDefault();
            if (userTask != null)
            {
                userTask.IsExpanded = isExpanded;
                UserTasks.AddOrUpdate();

                SaveChanges();
            }
        }

        public List<Task> Read_AllTasks() 
        {
            TaskFactory taskFactory = new TaskFactory();
            
            // как альтернатива
            //return Tasks.OrderBy(t => t.IndexNumber).Select(task => taskFactory.CreateTask(task)).ToList();

            List<Task> tasks = new List<Task>();
            var allPossibleTasksToFave = Tasks.Where(t => (t.LevelID <= GlobalInfo.CurrentUser.LevelID));
            List<Task> tasksDB = new List<Task>(allPossibleTasksToFave.OrderBy(t => t.IndexNumber));

            foreach (Task t in tasksDB)
                tasks.Add(taskFactory.CreateTask(t));
            return tasks;
        }

        public List<int> Read_RootTasks() // todo в имени не отражено, что считывются идетификаторы
        {
            return (from t in Tasks where t.ParentTaskID == null select t.ID).ToList();
        }

        public bool IsExpanded(int taskID, int userID)
        {
            var userFave = UserTasks.Where(ut => ut.TaskID == taskID && ut.UserID == userID).ToList();
            if (userFave.Count == 0)
                return false;
            bool? isExpanded = userFave[0].IsExpanded;
            if (isExpanded == null)
                return false;
            return (bool)isExpanded;
        }

        public bool IsFave(int taskID)
        {
            var userFave = UserTasks.Where(ut => ut.TaskID == taskID && ut.UserID == GlobalInfo.CurrentUser.ID).ToList();
            return userFave.Count != 0;
        }

        public List<int> Read_FaveTasksID(int curUser)
        {
            return (from t in UserTasks where t.UserID == curUser select t.TaskID).ToList();
        }

        public List<Task> Read_FaveTasks(int userID)
        {
            TaskFactory taskFactory = new TaskFactory();

            var userFaveTasksID = Read_FaveTasksID(userID);

            var faveTasks = Tasks.OrderBy(t => t.IndexNumber).Where(task => userFaveTasksID.Contains(task.ID)).ToList();
            return faveTasks.Select(task => taskFactory.CreateTask(task)).ToList();
        }

        public List<int> Read_ChildTasks(int taskID)
        {
            // todo как альтернатива
            //return Tasks.Where(t => t.ParentTaskID == taskID).Select(task => task.ID).ToList();

            List<Task> tasksDB = Tasks.Where(t => t.ParentTaskID == taskID).ToList();

            List<int> tasks = new List<int>();

            TaskFactory taskFactory = new TaskFactory(); // todo зачем эта штука?
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
            UserTask userTaskDB = UserTasks.Where(t => t.TaskID == taskID && t.UserID == GlobalInfo.CurrentUser.ID).FirstOrDefault();

            //List<Task> childTasksBD = (from t in Tasks where t.ParentTaskID == taskID select t).ToList();
            //foreach (var t in childTasksBD)
            //    t.ParentTaskID = taskBD.ParentTaskID; //зачем-то переподцеплять детей к родителю. Удаляем, так удаляем и поддерево!

            //List<Work> worksBD = (from w in Works where w.TaskID == taskID select w).ToList();
            //foreach (var w in worksBD)
            //{
            //    Delete_AttrValuesFields_ForWork(w.ID);
            //    Works.Remove(w);
            //}

            if (userTaskDB != null)
                UserTasks.Remove(userTaskDB);
            SaveChanges();
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
  
        public List<Work> Read_AllWorks(int curUser)
        {
            List<Work> worksDB = Works.Include(w => w.AttrValues.Select(a => a.Attribute)).ToList();
            var userFaveTasksID = Read_FaveTasksID(curUser);
            worksDB = worksDB.Where(w => (w.UserID == curUser && userFaveTasksID.Contains(w.TaskID))).ToList();
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
            var userTasks = UserTasks.Where(ut => ut.UserID == GlobalInfo.CurrentUser.ID);
            var userTasksID = userTasks.Select(t => t.TaskID).ToList();
            return (from w in Works where (w.StartDate == date.Date && w.UserID == GlobalInfo.CurrentUser.ID && userTasksID.Contains(w.TaskID)) select w.ID).ToList();
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

            // todo была ошибка, проверить в истории
            if (oldTypeID != newTypeID) //При изменении типа! Удалить-перенести атрибуты типа
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
                value.AttrID = a;           // todo почему здесь не заполняется поле DataType ? 
                AttrValues.Add(value);
            }
            SaveChanges();
        }    

        public List<AttrValue> Read_AttrValues_ForWork(Work work)
        {
            return AttrValues.Include(a => a.Attribute).Where(a => a.WorkID == work.ID).ToList(); // todo, не нашёл где используется этот метод
        }

        public void Update_AttrValuesFields_ForWork(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType)
        {
            //Delete_AttrValuesFields_ForWork(WorkID);  // todo, надо дописать только недостающие атрибуты, т.к. в старых может содержаться важная информация
            Create_AttrValuesFields_ForWork(WorkID, newType);
        }

        public void Delete_AttrValuesFields_ForWork(int WorkID)
        {
            // todo довольно странный способ удаления объектов, но в EF реально грустно
            // здесь есть некоторый ускоритель, в некоторых случаях он реально может спасти https://habr.com/post/203214/

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
