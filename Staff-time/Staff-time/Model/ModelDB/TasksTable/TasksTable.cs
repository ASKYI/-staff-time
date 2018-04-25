using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace Staff_time.Model
{
    public static partial class TasksTable
    {
        private static List<Task> _tasks;
        public static List<Task> Tasks { get { return _tasks; } }

        public static void Create_Task(Task task)
        {
            //TODO
        }
        public static void Read_Tasks()
        {
            _tasks = new List<Task>();
            List<Task> tasksDB = new List<Task>();
            using (TaskManagmentDBEntities ctx = new TaskManagmentDBEntities())
            {
                
                tasksDB = (from x in ctx.Tasks.Include(s => s.Works).Include(s => s.TaskType).Include(s => s.PropValues).Include(s => s.UserTasks)
                          select x).ToList();
            }
            TaskFactory taskFactory = new TaskFactory();
            foreach (Task t in tasksDB)
            {
                _tasks.Add(taskFactory.CreateTask(t));
            }
        }
        public static void Update_Task(int taskId)
        {
            //TODO
            //???Как изменять? По полям вручную? Почитать.
        }
        public static void Delete_Task(int taskId)
        {
            //TODO
            //TODO: При удалении задачи, ссылки на нее удаляются.
        }
    }
}
