using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public interface IWorkFactory
    {
        Work CreateWork(WorkTypeEnum type);
        Work CreateWork(Work work);
    }
}