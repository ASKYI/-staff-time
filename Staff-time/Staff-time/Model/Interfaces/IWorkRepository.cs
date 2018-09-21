using System;
using System.Collections.Generic;

namespace Staff_time.Model.Interfaces
{
    public interface IWorkRepository  // done: переименован интерфейс, функции, убраны лишние библиотеки
    {
        //Создает работу и пустые поля значений атрибутов
        void AddWork(Work work);

        //Возвращает список правильно созданных (верный тип) работ (с загрузкой задач)
        List<Work> GetAllWorks();

        //Возвращает список правильно созданных (верный тип) работ за определенную дату - дата начала (с загрузкой задач)
        List<int> GetWorksForDate(DateTime date);

        //Возвращает список правильно созданных (верный тип) работ для определенной задачи (с загрузкой задач)
        List<int> GetWorksForTask(int taskID);

        //Возвращает работу по ID
        Work GetWorkByID(int workID);

        //Обнволение работы
        void UpdateWork(Work work);

        //Удаление работы
        void DeleteWork(int workID);
    }
}
