using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public class TaskFactory : ITaskFactory
    {
        public Task CreateTask(TaskTypeEnum type)
        {
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new Task();
                case TaskTypeEnum.TaskCustomer:
                    return new TaskCustomer();
                case TaskTypeEnum.TaskDirection:
                    return new TaskDirection();
                case TaskTypeEnum.TaskAppeal:
                    return new TaskAppeal();
                case TaskTypeEnum.TaskContract:
                    return new TaskContract();
                case TaskTypeEnum.TaskRevision:
                    return new TaskRevision();
                case TaskTypeEnum.TaskPlan:
                    return new TaskPlan();
            }
            return null;
        }
        public Task CreateTask(Task task)
        {
            TaskTypeEnum type = (TaskTypeEnum)task.TaskTypeID;
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new Task(task);
                case TaskTypeEnum.TaskCustomer:
                    return new TaskCustomer(task);
                case TaskTypeEnum.TaskDirection:
                    return new TaskDirection(task);
                case TaskTypeEnum.TaskAppeal:
                    return new TaskAppeal(task);
                case TaskTypeEnum.TaskContract:
                    return new TaskContract(task);
                case TaskTypeEnum.TaskRevision:
                    return new TaskRevision(task);
                case TaskTypeEnum.TaskPlan:
                    return new TaskPlan(task);
            }
            return new Task(task);
        }
    }
}
