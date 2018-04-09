using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public enum TaskPropDataType { ValueText, ValueInt, ValueDate, ValueTime };
    
    //Заказчик Предприятие Договор Направление
    public enum TaskTypeEnum { TaskNone, TaskCustomer, TaskСompany, TaskСontract, TaskDirection } 

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
                case TaskTypeEnum.TaskDirection:
                    return new TaskDirection();
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
                case TaskTypeEnum.TaskDirection:
                    return new TaskDirection(task);
                case TaskTypeEnum.TaskСompany:
                    return new TaskCompany(task);
                case TaskTypeEnum.TaskСontract:
                    return new TaskContract(task);
            }
            return new Task(task);
        }
    }
}
