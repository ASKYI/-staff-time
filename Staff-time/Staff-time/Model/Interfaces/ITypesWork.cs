using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ITypesWork
    {
        List<WorkType> Read_WorkTypes();
        List<TaskType> Read_TaskTypes();
    }
}
