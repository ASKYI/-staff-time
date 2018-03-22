using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public class TaskFactory : ITaskFactory
    {
        public Task CreateTask(TestTree.ViewModel.TaskType type) //Factory Pattern
        {
            switch (type)
            {
                case ViewModel.TaskType.None:
                    return new Task();
                case ViewModel.TaskType.Customer:
                    return new Customer();
                case ViewModel.TaskType.Direction:
                    return new Direction();
                case ViewModel.TaskType.Сompany:
                    return new Company();
                case ViewModel.TaskType.Сontract:
                    return new Contract();
            }
            return new Task();
        }
        public Task CreateTask(Task task)
        {
            TestTree.ViewModel.TaskType type = (TestTree.ViewModel.TaskType)task.TaskTypeID;
            switch (type)
            {
                case ViewModel.TaskType.None:
                    return new Task(task);
                case ViewModel.TaskType.Customer:
                    return new Customer(task);
                case ViewModel.TaskType.Direction:
                    return new Direction(task);
                case ViewModel.TaskType.Сompany:
                    return new Company(task);
              //  case ViewModel.TaskType.Сontract: TOFIX
                    //return new Contract(task);
            }
            return new Task(task);
        }
    }
}
