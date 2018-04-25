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
    //Нет Консультация по телефону Ошибка Заплатка Рефракторинг
    public enum WorkTypeEnum { WorkNone, WorkConsultationsByPhone, WorkError, WorkPatch, WorkRefractoring }
    #endregion

}