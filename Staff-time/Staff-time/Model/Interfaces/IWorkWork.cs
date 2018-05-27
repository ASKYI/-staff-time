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
        List<Work> Read_WorksForDate(DateTime date);

        List<Work> Read_WorksForTask(int taskID);

        Work Read_WorkByID(int workID);

        void Update_Work(Work work);

        void Delete_Work(int workID);
    }
}
