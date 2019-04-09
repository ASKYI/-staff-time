using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IPropertyWork
    {
        List<string> GetListOfPropValues(int propID);
        List<List> GetListIDWithTaskType(int taskTypeID);
    }
}
