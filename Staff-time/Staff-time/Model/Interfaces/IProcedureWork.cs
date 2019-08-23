using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IProcedureWork
    {
        void RepareUserFave(int taskID);
        void UpdateTasksIndexNumbers(int indexStart);

        void ReloadCurDay();
        void ReloadAllDays();
        void RepairInconsistances();
    }
}
