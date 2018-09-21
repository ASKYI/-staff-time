using System;
using System.Collections.Generic;

namespace Staff_time.Model.Interfaces
{
    public interface IAttributeRepository // done: переименован интерфейс, функции, убраны лишние библиотеки
    {
        //При создании новой работы, создание пустых записей атрибутов для типа работы 
        void AddAtributeValuesFields(int WorkID, WorkTypeEnum type);

        //Возвращает значение всех атрибутов для задач (с загрузкой атрибутов)
        List<AttrValue> GetAttributeValuesForWork(Work work);

        //При изменении типа работы, изменение записей атрибутов
        void UpdateAttributeValuesFieldsForWork(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType);

        //Удаление записей значения отрибутов для работы
        void DeleteAttributValuesFieldsForWork(int WorkID);

        //Изменение ЗНАЧЕНИЙ атрибутов
        void UpdateAttributeValuesForWork(List<AttrValue> values);
    }
}
