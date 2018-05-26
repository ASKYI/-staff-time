using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IAttrWork
    {
        //При создании новой работы, создание пустых записей атрибутов для типа работы 
        void Create_AttrValuesFields_ForWork(int WorkID, WorkTypeEnum type);

        //Возвращает значение всех атрибутов для задач (с загрузкой атрибутов)
        List<AttrValue> Read_AttrValues_ForWork(Work work);

        //При изменении типа работы, изменение записей атрибутов
        void Update_AttrValuesFields_ForWork(int WorkID, WorkTypeEnum oldType, WorkTypeEnum newType);

        //Удаление записей значения отрибутов для работы
        void Delete_AttrValuesFields_ForWork(int WorkID);

        //Изменение ЗНАЧЕНИЙ атрибутов
        void Update_AttrValues_ForWork(List<AttrValue> values);
    }
}
