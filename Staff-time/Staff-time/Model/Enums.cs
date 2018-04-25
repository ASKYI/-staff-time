using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    #region Tasks
    public enum TaskPropDataType { ValueText, ValueInt, ValueDate, ValueTime };

    // Нет Заказчик Предприятие Договор Направление
    public enum TaskTypeEnum { TaskNone, TaskCustomer, TaskСompany, TaskСontract, TaskSpecialty }
    #endregion

    #region Works
    //Нет Ошибка Рефракторинг Консультация по телефону  Заплатка 
    public enum WorkTypeEnum { WorkNone, WorkError, WorkRefractoring, WorkConsultationsByPhone, WorkPatch}
    #endregion

}