using System;
using System.Collections.Generic;

namespace Staff_time.Model
{
    public interface ITaskFactory
    {
        Task CreateTask(TaskTypeEnum type);
        Task CreateTask(Task task);
    }
}
