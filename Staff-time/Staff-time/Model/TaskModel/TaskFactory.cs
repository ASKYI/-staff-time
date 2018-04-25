using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public enum TaskPropDataType { ValueText, ValueInt, ValueDate, ValueTime };

    // Нет Заказчик Предприятие Договор Направление
    public enum TaskTypeEnum { TaskNone, TaskCustomer, TaskСompany, TaskСontract, TaskSpecialty }

    public class TaskFactory : ITaskFactory
    {
        public Task CreateTask(TaskTypeEnum type) //Factory Pattern
        {
            switch (type)
            {
                case TaskTypeEnum.TaskNone:
                    return new Task();
                case TaskTypeEnum.TaskCustomer:
                    return new TaskCustomer();
                case TaskTypeEnum.TaskSpecialty:
                    return new TaskSpecialty();
                case TaskTypeEnum.TaskСompany:
                    return new TaskCompany();
                case TaskTypeEnum.TaskСontract:
                    return new TaskContract();
            }
            return new Task();
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
                case TaskTypeEnum.TaskSpecialty:
                    return new TaskSpecialty(task);
                case TaskTypeEnum.TaskСompany:
                    return new TaskCompany(task);
                case TaskTypeEnum.TaskСontract:
                    return new TaskContract(task);
            }
            return new Task(task);
        }
    }
}
