using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ITaskWork
    {
        void Create_Task(Task task);
        void Create_TaskToFave(int taskID, int curUserID);
        void Read_AllTasks();
        void Read_FaveTasks(int curUser);
        void Update_Task(Task task);
        void Delete_Task(int taskID);
        void Delete_TaskFromFave(int taskID);
    }
}
