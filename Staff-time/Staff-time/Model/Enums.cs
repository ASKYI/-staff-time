using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    #region Tasks
    // Нет Заказчик Предприятие Договор Направление
    public enum TaskTypeEnum { TaskClassification, TaskCustomer, TaskDirection, TaskAppeal, TaskContract, TaskRevision, TaskPlan, TaskIntegration, TaskTask }
    #endregion

    #region Works
    //Нет Ошибка Рефракторинг Консультация по телефону  Заплатка 
    public enum WorkTypeEnum { WorkNone, WorkError, WorkRefractoring, WorkConsultationsByPhone, WorkPatch}
    #endregion

    #region Data Types
    //Текст Целое число Дата время 
    public enum DataTypeEnum { TypeText, TypeInt, TypeDate, TypeTime }
    #endregion
}