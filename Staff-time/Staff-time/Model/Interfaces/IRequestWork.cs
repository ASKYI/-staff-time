using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IRequestWork
    {
        List<Request> Read_AllRequests();
        //void RefreshRequests();

        void DeleteRequests(List<int> requestsIds);
        void AddRequest(int fromUserID, int toUserID, int taskID, DateTime dt, string Note);
    }
}
