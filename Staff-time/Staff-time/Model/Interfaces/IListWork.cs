using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IListWork
    {
        List<ListsValue> GetListValues(int taskID, int listID);
        void UpdateListValues(List<ListsValue> list, int taskID, int listID);
        void UpdateLists(List<List> lst);
    }
}
