using System;
using System.Collections.Generic;

namespace Staff_time.Model.Repositories 
{
    public interface ITypesRepository // done: переименован интерфейс, функции, убраны лишние библиотеки
    {
        List<WorkType> GetWorkTypes();

        List<TaskType> GetTaskTypes();
    }
}
