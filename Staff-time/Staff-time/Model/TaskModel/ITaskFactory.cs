using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public interface ITaskFactory
    {
        Task CreateTask(TaskTypeEnum type);
        Task CreateTask(Task task);
    }
}
