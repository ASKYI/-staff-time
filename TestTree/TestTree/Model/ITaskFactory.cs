using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree.Model
{
    public interface ITaskFactory
    {
        Task CreateTask(TaskTypeEnum type);
        Task CreateTask(Task task);
    }
}
