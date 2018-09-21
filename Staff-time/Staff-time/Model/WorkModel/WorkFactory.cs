using System;
using System.Collections.Generic;

namespace Staff_time.Model
{
    public class WorkFactory : IWorkFactory
    {
        public Work CreateWork(WorkTypeEnum type)
        {
            switch (type)
            {
                case WorkTypeEnum.WorkNone:
                    return new Work();
                case WorkTypeEnum.WorkConsultationsByPhone:
                    return new WorkConsultationsByPhone();
                case WorkTypeEnum.WorkError:
                    return new WorkError();
                case WorkTypeEnum.WorkPatch:
                    return new WorkPatch();
                case WorkTypeEnum.WorkRefractoring:
                    return new WorkRefractoring();
            }
            return null;
        }
        public Work CreateWork(Work work)
        {
            WorkTypeEnum type = (WorkTypeEnum)work.WorkTypeID;
            switch (type)
            {
                case WorkTypeEnum.WorkNone:
                    return new Work(work);
                case WorkTypeEnum.WorkConsultationsByPhone:
                    return new WorkConsultationsByPhone(work);
                case WorkTypeEnum.WorkError:
                    return new WorkError(work);
                case WorkTypeEnum.WorkPatch:
                    return new WorkPatch(work);
                case WorkTypeEnum.WorkRefractoring:
                    return new WorkRefractoring(work);
            }
            return null;
        }
    }
}