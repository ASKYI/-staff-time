using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ILogWork
    {
        void WriteLog(int taskID, string taskName, DateTime dt, string operType);
        void WriteLogWithSave(int taskID, string taskName, DateTime dt, string operType);
    }
}
