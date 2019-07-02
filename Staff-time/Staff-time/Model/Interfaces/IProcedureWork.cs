﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IProcedureWork
    {
        void RepareUserFave(int taskID);
        void DuplicateTask(int taskFromID, int taskToID, int userID);
        void UpdateTasksIndexNumbers(int indexStart);

        void ReloadLastDay();
        void ReloadAllDays();
        void RepairInconsistances();
    }
}
