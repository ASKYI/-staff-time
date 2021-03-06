﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

using Staff_time.Model.Interfaces;
using System.Windows;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using static Staff_time.View.TimeStatisticsWindow;
using System.Globalization;

namespace Staff_time.Model
{
    public partial class TaskManagmentDBEntities : DbContext,
        ITaskWork, IWorkWork, IAttrWork, ITypesWork, IUserWork, ILevelWork, ITimeTableWork, IProcedureWork, IRequestWork, IPropertyWork, IListWork, ILogWork
    {
        #region IUserWork
        public List<User> Read_AllUsers()
        {
            return Users.Where(u => u.IsActive == true).OrderBy(u => u.UserName).ToList();
        }

        public string GetUserNameByID(int _userID)
        {
            var user = Users.FirstOrDefault(u => u.ID == _userID);
            return user == null ? "" : user.UserName;
        }

        public void SaveCurrentUser()
        {
            ChangeTracker.DetectChanges();
            SaveChanges();
        }


        #endregion

        #region ILevelWork
        public Dictionary<string, int> Read_AllLevels()
        {
            return LEVELS.Select(t => new { t.LevelName, t.LevelID })
                   .ToDictionary(t => t.LevelName, t => t.LevelID);
        }
        public List<LEVEL> Read_AllLevelsLowerMe()
        {
            return LEVELS.Where(t => t.LevelID <= GlobalInfo.CurrentUser.LevelID).ToList();
        }

        #endregion //ILevelWork

        #region IPropertyWork
        public List<string> GetListOfPropValues(int propID)
        {
            return PropertiesLists.Where(pl => pl.PropID == propID).Select(pl => pl.Value).ToList();
        }

        public List<List> GetListIDWithTaskType(int taskTypeID)
        {
            //выгружаем все списки для типа задачи и типа свойства 5 (списки), где ListType = true (список от родителя)
            return Properties.Where(p => p.DataType == 5 && p.ListType == true && p.TaskTypeID == taskTypeID).Select(p => p.List).Distinct().ToList();
        }


        #endregion //IPropertyWork

        #region ITimeTableWork
        public double Read_TimeByDate(DateTime dt)
        {
            var planTime = TimeTables.Where(t => t.Date == dt).Select(t => t.PlanningTime).FirstOrDefault();
            return planTime;
        }

        public List<TimeTable> GetTimeForAMonth(int year, int month)
        {
            return TimeTables.Where(t => t.Date.Year == year && t.Date.Month == month).ToList();
        }

        public void Update(DateTime dt, double tm)
        {
            var timeTable = TimeTables.Where(t => t.Date == dt).FirstOrDefault();
            if (timeTable == null)
            {
                timeTable = new TimeTable();
                timeTable.Date = dt;
                //throw new ArgumentNullException("timeTable");
            }
            timeTable.PlanningTime = tm;
            ChangeTracker.DetectChanges();
            TimeTables.AddOrUpdate(timeTable);
            SaveChanges();
        }

        #endregion //ITimeTableWork

        #region ITaskWork

        public void Create_Task(Task task) // todo здесь надо подобрать более точное название, Register или Add, по сути ведь Task уже создан, так что точно не Create
        {
            ChangeTracker.DetectChanges();
            Tasks.Add(task);
            SaveChanges();
            WriteLog(task.ID, task.TaskName, DateTime.Now, "INSERT");
            ChangeTracker.DetectChanges();
            SaveChanges();
        }
        public void Create_TaskToFave(int taskID, int curUserID)
        {
            var maxIndex = UserTasks.Select(ut => ut.IndexNumber).Max();
            if (maxIndex == null)
                maxIndex = 0;
            maxIndex++;
            ChangeTracker.DetectChanges();
            UserTasks.Add(new UserTask() { TaskID = taskID, UserID = curUserID, IndexNumber = maxIndex });
            var taskName = Tasks.Where(t => t.ID == taskID).Select(t => t.TaskName).FirstOrDefault();
            WriteLog(taskID, taskName, DateTime.Now, "INSERT_FAVE");
            SaveChanges();
        }

        public void Update_UserTaskExpended(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                int taskID = node.Task.ID;
                var userTask = UserTasks.Where(ut => ut.TaskID == taskID && ut.UserID == GlobalInfo.CurrentUser.ID).FirstOrDefault();
                if (userTask != null)
                {
                    if (userTask.IsExpanded != node.IsExpanded)
                    {
                        userTask.IsExpanded = node.IsExpanded;
                        UserTasks.AddOrUpdate();
                    }
                }
            }
            ChangeTracker.DetectChanges();
            SaveChanges();
        }

        public List<Task> Read_AllTasks(List<int> existTaskIDs)
        {
            TaskFactory taskFactory = new TaskFactory();

            var allPossibleTasksToFave = Tasks.Where(t => (!existTaskIDs.Contains(t.ID) && t.LevelID <= GlobalInfo.CurrentUser.LevelID));
            List<Task> tasksDB = new List<Task>(allPossibleTasksToFave.OrderBy(t => t.IndexNumber));
            List<Task> tasks = new List<Task>();

            foreach (Task t in tasksDB)
                tasks.Add(taskFactory.CreateTask(t));

            return tasks;
        }

        public List<int> Read_RootTasks() // todo в имени не отражено, что считывются идетификаторы
        {
            return (from t in Tasks where t.ParentTaskID == null select t.ID).ToList();
        }

        public void ReplaceUserTasks(Task task1, Task task2)
        {
            var curUser = GlobalInfo.CurrentUser.ID;
            var userTask1 = UserTasks.Where(t => t.UserID == curUser && t.TaskID == task1.ID).FirstOrDefault();
            var userTask2 = UserTasks.Where(t => t.UserID == curUser && t.TaskID == task2.ID).FirstOrDefault();
            if (userTask1 == null || userTask2 == null)
            {
                MessageBox.Show("Ошибка обмена местами задач в избранном, одна из задач отсутствует!");
                return;
            }
            var index = userTask1.IndexNumber;
            userTask1.IndexNumber = userTask2.IndexNumber;
            userTask2.IndexNumber = index;
            ChangeTracker.DetectChanges();
            UserTasks.AddOrUpdate();

            SaveChanges();
            //var task1_number = (from t in UserTasks where t.UserID == curUser && t.TaskID == task1.ID select t.IndexNumber).ToList();
            //var task2_number = (from t in UserTasks where t.UserID == curUser && t.TaskID == task2.ID select t.IndexNumber).ToList();
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
        public List<Property> GetAllProperties(int tasktTypeID)
        {
            return TaskTypeProps.Where(tp => tp.TaskTypeID == tasktTypeID).Select(tp => tp.Property).OrderBy(tp => tp.ID).ToList();
        }

        public void DeleteProperties(List<PropValue> deleteList)
        {
            foreach (var pv in deleteList)
            {
                var delPV = PropValues.FirstOrDefault(p => p.PropID == pv.PropID && p.TaskID == pv.TaskID);
                if (delPV != null)
                    PropValues.Remove(delPV);
            }
        }

        public void AddRequest(int fromUserID, int toUserID, int taskID, DateTime dt, string Note)
        {
            Request _request = new Request();
            _request.FromUserID = fromUserID;
            _request.ToUserID = toUserID;
            _request.TaskID = taskID;
            _request.TransferDateTime = dt;
            _request.Note = Note;

            ChangeTracker.DetectChanges();
            Requests.AddOrUpdate(_request);
            SaveChanges();
        }

        public bool IsExist(int taskID, string taskName, int? parentTaskID)
        {
            List<Task> tasks = new List<Task>();
            if (parentTaskID == null || (int)parentTaskID > 0)
                tasks = Tasks.Where(t => t.ID != taskID && t.TaskName.ToLower() == taskName.ToLower() && t.ParentTaskID == parentTaskID).ToList();
            else
                tasks = Tasks.Where(t => t.ID != taskID && t.TaskName.ToLower() == taskName.ToLower() && (t.ParentTaskID == null || t.ParentTaskID <= 0)).ToList();
            return tasks.Count != 0;
        }

        public List<int> Read_FaveTasksID(int curUser)
        {
            return (from t in UserTasks where t.UserID == curUser orderby t.IndexNumber select t.TaskID).ToList();
        }

        public List<Task> Read_FaveTasks(int userID)
        {
            TaskFactory taskFactory = new TaskFactory();

            var userFaveTasksID = Read_FaveTasksID(userID);
            var faveTasks = Tasks.Where(task => userFaveTasksID.Contains(task.ID) && task.LevelID <= GlobalInfo.CurrentUser.LevelID).ToList();
            faveTasks = faveTasks.OrderBy(t => userFaveTasksID.IndexOf(t.ID)).ToList();

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
            WriteLog(task.ID, task.TaskName, DateTime.Now, "UPDATE");
            ChangeTracker.DetectChanges();
            foreach (var pv in task.PropValues)
            {
                var delPV = PropValues.FirstOrDefault(p => p.PropID == pv.PropID && p.TaskID == pv.TaskID);
                delPV = delPV == null ? pv : delPV;
                if (delPV.Property != null)
                {
                    var curIDs = delPV.Property.PropertiesLists.Select(p => p.ID).ToList();
                    PropertiesLists.RemoveRange(PropertiesLists.Where(pl => pl.PropID == delPV.PropID && !curIDs.Contains(pl.ID))); // удалить те, которых сейчас нет
                }
                PropValues.AddOrUpdate(delPV);
            }

            Tasks.AddOrUpdate(task);
            SaveChanges();
        }

        public bool Delete_Task(int taskID)
        {
            ChangeTracker.DetectChanges();

            Task taskBD = Tasks.Where(t => t.ID == taskID).FirstOrDefault();
            if (taskBD == null)
                return true;

            //Проверим, есть ли задача в избранном у пользователей
            var userDeleteTask = UserTasks.Where(ut => ut.TaskID == taskBD.ID).ToList();
            if (userDeleteTask.Count > 0)
            {
                var users = userDeleteTask.Select(u => u.User.UserName);
                var usersNames = String.Join("\n", users);
                var dialogResult = System.Windows.MessageBox.Show($"Данная задача содержится в избранном у следующих пользователей:\n{usersNames}", "Предупреждение",
            MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Проверим, есть ли у пользователей работы по задаче
            var worksDeleteTask = Works.Where(w => w.TaskID == taskBD.ID).ToList();
            if (worksDeleteTask.Count > 0)
            {
                var users = worksDeleteTask.Select(w => w.User.UserName);
                var usersNames = String.Join("\n", users);
                if (System.Windows.MessageBox.Show($"По задаче '{taskBD.TaskName}' есть работы у следующих пользователей:\n{usersNames}. Продолжить удаление?", "Предупреждение",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return false;
            }

            List<Task> childTasksBD = (from t in Tasks where t.ParentTaskID == taskID select t).ToList();
            foreach (var t in childTasksBD)
                t.ParentTaskID = taskBD.ParentTaskID;

            List<Work> worksBD = (from w in Works where w.TaskID == taskID select w).ToList();
            foreach (var w in worksBD)
            {
                Delete_AttrValuesFields_ForWork(w.ID);
                Works.Remove(w);
            }

            if (taskBD != null)
                Tasks.Remove(taskBD);

            WriteLog(taskID, taskBD.TaskName, DateTime.Now, "DELETE");
            SaveChanges();
            return true;
        }
        public void Delete_TaskFromFave(int taskID)
        {
            var taskName = Tasks.Where(t => t.ID == taskID).Select(t => t.TaskName).FirstOrDefault();

            WriteLog(taskID, taskName, DateTime.Now, "DELETE_FAVE");

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
            ChangeTracker.DetectChanges();

            if (userTaskDB != null)
                UserTasks.Remove(userTaskDB);
            SaveChanges();
        }

        public List<string> GetAllWorksNames(int taskID)
        {
            return Works.Where(w => w.TaskID == taskID).Select(w => w.WorkName).Distinct().ToList();
        }
        public void UpdateUserTaskIndexNumber(int taskID, int pos)
        {
            ChangeTracker.DetectChanges();
            var usertask = UserTasks.FirstOrDefault(ut => ut.TaskID == taskID && ut.UserID == GlobalInfo.CurrentUser.ID);
            if (usertask != null)
            {
                usertask.IndexNumber = pos;
                UserTasks.AddOrUpdate(usertask);
                SaveChanges();
            }
        }


        #endregion


        #region IWorkWork

        public void Create_Work(Work work)
        {
            try
            {
                ChangeTracker.DetectChanges();

                Work w = new Work(work);
                Works.Add(w);

                //Создание пустых полей для атрибутов в соотвествии с типом работы
                SaveChanges(); //Чтобы был актуальный ID у работы
                Create_AttrValuesFields_ForWork(w, (WorkTypeEnum)work.WorkTypeID);
                work.ID = w.ID;
                work.Task = w.Task;
                work.AttrValues = w.AttrValues;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Work> Read_AllWorks(int curUser)
        {
            List<Work> worksDB = Works.Include(w => w.AttrValues.Select(a => a.Attribute)).ToList();
            //var userFaveTasksID = Read_FaveTasksID(curUser);
            worksDB = worksDB.Where(w => (w.UserID == curUser /*&& userFaveTasksID.Contains(w.TaskID)*/)).ToList();
            List<Work> works = new List<Work>();
            WorkFactory taskFactory = new WorkFactory();
            foreach (Work w in worksDB)
            {
                 works.Add(taskFactory.CreateWork(w));
            }
            return works;
        }

        public List<int> Read_WorksForDate(DateTime date)
        {
            //var userTasks = UserTasks.Where(ut => ut.UserID == GlobalInfo.CurrentUser.ID);
            //var userTasksID = userTasks.Select(t => t.TaskID).ToList();
            return (from w in Works where (w.StartDate == date.Date && w.UserID == GlobalInfo.CurrentUser.ID) select w.ID).ToList();
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
            int oldTypeID = 0, newTypeID = 0;
            var workDB = Works.FirstOrDefault(x => x.ID == work.ID);
            if (workDB != null)
            {
                oldTypeID = workDB.WorkTypeID;
                newTypeID = work.WorkTypeID;
            }
            ChangeTracker.DetectChanges();
            Works.AddOrUpdate(work);

            // todo была ошибка, проверить в истории
            if (oldTypeID != newTypeID) //При изменении типа! Удалить-перенести атрибуты типа
                Update_AttrValuesFields_ForWork(work, (WorkTypeEnum)oldTypeID, (WorkTypeEnum)newTypeID);

            SaveChanges();
        }

        public void Delete_Work(int workID)
        {
            ChangeTracker.DetectChanges();

            Delete_AttrValuesFields_ForWork(workID); //Удаление атрибутов работы
            var workDB = Works.Where(x => x.ID == workID).FirstOrDefault();
            if (workDB != null)
                Works.Remove(workDB);

            SaveChanges();
        }

        public List<WorkTimeRange> GetTimeRanges(int workID)
        {
            return WorkTimeRanges.Where(wt => wt.WorkID == workID).ToList();
        }
        public void UpdateTimeRanges(List<WorkTimeRange> list, int workID)
        {
            //Обновим диапазоны
            if (workID == 0) //обновить все, их просто редактировали за день
                WorkTimeRanges.AddOrUpdate(list.ToArray());
            else
            {
                var oldWorkRanges = WorkTimeRanges.Where(wr => wr.WorkID == workID).ToList();
                var extraWorkRng = oldWorkRanges.Where(old => list.FirstOrDefault(l => l.ID == old.ID) == null).ToList();
                if (extraWorkRng.Count > 0)
                    WorkTimeRanges.RemoveRange(extraWorkRng);
                WorkTimeRanges.AddOrUpdate(list.ToArray());
            }
            ChangeTracker.DetectChanges();
            SaveChanges();
        }

        public List<Reason> GetReasonAbsence()
        {
            return Reasons.OrderBy(r => r.ID).ToList();
        }
        public List<WorkAbsence> GetUserReasonAbsence()
        {
            return WorkAbsences.Where(wa => wa.UserID == GlobalInfo.CurrentUser.ID).ToList();
        }

        public void UpdateReasonAbsence(List<DateTime> dates, int? reasonID)
        {
            ChangeTracker.DetectChanges();
            if (reasonID == null)
            {
                var userResonsToDelete = WorkAbsences.Where(wa => dates.Contains(wa.AbsenceDate) && wa.UserID == GlobalInfo.CurrentUser.ID);
                WorkAbsences.RemoveRange(userResonsToDelete);
                SaveChanges();
                return;
            }

            foreach (var dt in dates)
            {
                var old = WorkAbsences.Where(wa => wa.AbsenceDate == dt && wa.UserID == GlobalInfo.CurrentUser.ID).ToList();
                if (old.Count > 1)
                {
                    MessageBox.Show("Не может быть больше одного дня с причиной", "Ошибка");
                    return;
                }

                if (old.Count == 1)
                {
                    if (old[0].ReasonID != reasonID)
                    {
                        old[0].ReasonID = (int)reasonID;
                        WorkAbsences.AddOrUpdate(old[0]);
                    }
                }
                else
                {
                    WorkAbsence newWorkAbsence = new WorkAbsence();
                    newWorkAbsence.ReasonID = (int)reasonID;
                    newWorkAbsence.UserID = GlobalInfo.CurrentUser.ID;
                    newWorkAbsence.AbsenceDate = dt;
                    WorkAbsences.AddOrUpdate(newWorkAbsence);
                }
            }
            SaveChanges();
        }


        //public int GetTimeRangeID(DateTime startTime, DateTime endTime)
        //{
        //    var rng = WorkTimeRanges.FirstOrDefault(r => r.StartTime == startTime && r.EndTime == endTime);
        //    if (rng == null)
        //    {
        //        rng = new WorkTimeRange();
        //        rng.StartTime = startTime;
        //        rng.EndTime = endTime;
        //        WorkTimeRanges.Add(rng);
        //        SaveChanges();
        //    }
        //    return rng.ID;
        //}

        #endregion


        #region IListWork
        public List<ListsValue> GetListValues(int taskID, int listID)
        {
            return ListsValues.Where(lv => lv.TaskID == taskID && lv.ListID == listID).ToList();
        }

        public void UpdateListValues(List<ListsValue> list, int taskID, int listID)
        {
            var oldVal = ListsValues.Where(lv => lv.ListID == listID && lv.TaskID == taskID).ToList();
            ChangeTracker.DetectChanges();
            oldVal = oldVal.Except(list).ToList();
            if (oldVal.Count > 0)
                ListsValues.RemoveRange(oldVal);
            ListsValues.AddOrUpdate(list.ToArray());
            SaveChanges();
        }

        public void UpdateLists(List<List> lst)
        {
            ChangeTracker.DetectChanges();
            Lists.AddOrUpdate(lst.ToArray());
            SaveChanges();
        }


        #endregion IListWork


        #region IAttrWork
        public void Create_AttrValuesFields_ForWork(Work _work, WorkTypeEnum type)
        {
            ChangeTracker.DetectChanges();
            int typeID = (int)type;
            var oldAttrIds = _work.AttrValues.Select(a => a.AttrID).ToList();
            List<int> attrIDs = (from a in WorkTypeAttrs where a.WorkTypeID == typeID select a.AttrID).ToList();
            attrIDs.RemoveAll(a => oldAttrIds.Contains(a)); //Удаляем те атрибуты, что была, добавлять будем только новые
            List<AttrValue> vals = new List<AttrValue>();
            foreach (var a in attrIDs)
            {
                AttrValue value = new AttrValue();
                value.WorkID = _work.ID;
                value.AttrID = a;           // todo почему здесь не заполняется поле DataType ? 
                vals.Add(value);
            }
            AttrValues.AddRange(vals);
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttrValue> Read_AttrValues_ForWork(Work work)
        {
            return AttrValues.Include(a => a.Attribute).Where(a => a.WorkID == work.ID).ToList(); // todo, не нашёл где используется этот метод
        }

        public void Update_AttrValuesFields_ForWork(Work _work, WorkTypeEnum oldType, WorkTypeEnum newType)
        {
            //Delete_AttrValuesFields_ForWork(WorkID);  // todo, надо дописать только недостающие атрибуты, т.к. в старых может содержаться важная информация
            Create_AttrValuesFields_ForWork(_work, newType);
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
                ChangeTracker.DetectChanges();
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

        #region IProcedureWork
        public void RepareUserFave(int taskID)
        {
            ChangeTracker.DetectChanges();
            RepareUserTree(taskID);
            SaveChanges();
        }

        public void DuplicateTask(int copyTaskID, int taskToID, int userID)
        {
            Task copyTask = Tasks.Where(t => t.ID == copyTaskID).FirstOrDefault();
            if (copyTask == null)
                return;

            Task taskTo = Tasks.Where(t => t.ID == taskToID).FirstOrDefault();
            if (taskTo != null)
            {
                WriteLog(copyTaskID, copyTask.TaskName, DateTime.Now, "DUPLICATE_THIS");
                WriteLog(taskToID, taskTo.TaskName, DateTime.Now, "DUPLICATE_TO_HERE");
            }
            else
                WriteLog(copyTaskID, copyTask.TaskName, DateTime.Now, "DUPLICATE_TO_ROOT");

            SaveChanges();

            ChangeTracker.DetectChanges();
            TaskDuplicate(copyTaskID, taskToID, userID);
        }

        public void UpdateTasksIndexNumbers(int indexStart)
        {
            ChangeTracker.DetectChanges();
            UpdateTaskIndexNumbersAfterAppend(indexStart);
            SaveChanges();
        }

        public void ReloadCurDay()
        {
            ChangeTracker.DetectChanges();
            var curDate = DateTime.Now;
            GenerateTaskResults2(curDate);
            SaveChanges();
        }
        public void ReloadAllDays()
        {
            GenerateTaskResults2All();
        }
        public void RepairInconsistances()
        {
            GenerateTaskResults2Changed();
        }

        public int GetLastAppealNumber(int taskID)
        {
            var numbr =  Database.SqlQuery<int?>("select dbo.GetMaxAppealNumber(@t_ID) t_ID", new System.Data.SqlClient.SqlParameter("@t_ID", taskID)).Single();
            return numbr != null ? (int)numbr : 0;
        }

        #endregion //IProcedureWork


        #region IRequestWork
        public List<Request> Read_AllRequests()
        {
            return Requests.Where(r => r.ToUserID == GlobalInfo.CurrentUser.ID).OrderBy(r => r.TransferDateTime).ToList();
        }

        public void DeleteRequests(List<int> requestsIds)
        {
            ChangeTracker.DetectChanges();
            var requestsForDelete = Requests.Where(r => requestsIds.Contains(r.ID));
            Requests.RemoveRange(requestsForDelete);
            SaveChanges();
        }

        #endregion //IRequestWork

        #region ILogWork
        public void WriteLogWithSave(int taskID, string taskName, DateTime dt, string operType)
        {
            WriteLog(taskID, taskName, dt, operType);
            SaveChanges();
        }
        public void WriteLog(int taskID, string taskName, DateTime dt, string operType)
        {
            LogTable newRow = new LogTable();
            newRow.TaskID = taskID;
            newRow.TaskName = taskName;
            newRow.UserID = GlobalInfo.CurrentUser.ID;
            newRow.OperationType = operType;
            newRow.ChangeDate = dt;
            LogTables.Add(newRow);
        }
        #endregion //ILogWork
    }
}
