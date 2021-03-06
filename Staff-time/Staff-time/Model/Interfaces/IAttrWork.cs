﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IAttrWork           // todo честно говоря последнее слово неочевидно, то ли это интерфейс для работ(work) над Attr, то ли это это цельный объект AttrWork . Является ли это репозиторием?
    {
        //При создании новой работы, создание пустых записей атрибутов для типа работы 
        void Create_AttrValuesFields_ForWork(Work work, WorkTypeEnum type);   // todo именование функций, знаки подчёркивания лишние, они ничего не добавляют . Можно в качестве руководства по именованию взять это http://rsdn.org/article/mag/200401/codestyle.XML

        //Возвращает значение всех атрибутов для задач (с загрузкой атрибутов)
        List<AttrValue> Read_AttrValues_ForWork(Work work);             // todo по ощущениям read не подразумевает возврат каких либо объектов, можеть быть точнее будет Get_...

        //При изменении типа работы, изменение записей атрибутов
        void Update_AttrValuesFields_ForWork(Work work, WorkTypeEnum oldType, WorkTypeEnum newType);

        //Удаление записей значения отрибутов для работы
        void Delete_AttrValuesFields_ForWork(int WorkID);

        //Изменение ЗНАЧЕНИЙ атрибутов
        void Update_AttrValues_ForWork(List<AttrValue> values);
    }
}
