using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class WorkInBlockFactory : IWorkInBlockFactory
    {
        public WorkInBlock CreateWorkInBlock(Work work)
        {
            WorkTypeEnum type = (WorkTypeEnum)work.WorkTypeID;
            switch (type)
            {
                case WorkTypeEnum.WorkNone:
                    return new WorkInBlock(work);
                case WorkTypeEnum.WorkConsultationsByPhone:
                    return new WorkConsultationsByPhone_InBlock(work);
                case WorkTypeEnum.WorkError:
                    return new WorkError_InBlock(work);
                case WorkTypeEnum.WorkPatch:
                    return new WorkPatch_InBlock(work);
                case WorkTypeEnum.WorkRefractoring:
                    return new WorkRefractoring_InBlock(work);
            }
            return new WorkInBlock(work);
        }
    }
}
