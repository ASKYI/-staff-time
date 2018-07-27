using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IWorkWork
    {
        //Создает работу и пустые поля значений атрибутов
        void Create_Work(Work work);

        //Возвращает список правильно созданных (верный тип) работ (с загрузкой задач)
        List<Work> Read_AllWorks();

        //Возвращает список правильно созданных (верный тип) работ за определенную дату - дата начала (с загрузкой задач)
        List<int> Read_WorksForDate(DateTime date); // todo странно, что Read_AllWorks возвращает объекты, а Read_WorksForDate возвращает идентификаторы 

        //Возвращает список правильно созданных (верный тип) работ для определенной задачи (с загрузкой задач)
        List<int> Read_WorksForTask(int taskID);

        //Возвращает работу по ID
        Work Read_WorkByID(int workID);

        //Обнволение работы
        void Update_Work(Work work);

        //Удаление работы
        void Delete_Work(int workID);
    }
}
