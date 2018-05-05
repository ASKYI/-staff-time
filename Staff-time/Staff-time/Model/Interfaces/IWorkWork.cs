using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IWorkWork
    {
        void Create_Work(int taskID, Work work);
        void Read_AllWorks();
        void Read_WorksForDate(DateTime date));
        void Read_WorksForTask(int taskID);
        void Update_Work(Work work);
        void Delete_Work(int workID);
    }
}
