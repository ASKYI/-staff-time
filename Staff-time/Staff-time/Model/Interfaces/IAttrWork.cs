using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IAttrWork
    {
        void Create_AttrValuesFields_ForWorkType(int WorkID, WorkTypeEnum type);
        List<AttrValue> Read_AttrValues_ForWork(Work work);
        void Update_AttrValuesFields_ForWorkType(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType);
        void Update_AttrValues(List<AttrValue> values);
        void Delete_AttrValuesFields_ForWork(int WorkID);
    }
}
