using System;
using System.Collections.Generic;

namespace Staff_time.Model
{
    public interface IWorkFactory
    {
        Work CreateWork(WorkTypeEnum type);
        Work CreateWork(Work work);
    }
}