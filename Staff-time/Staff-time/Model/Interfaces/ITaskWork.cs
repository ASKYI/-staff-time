using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ITaskWork
    {
        void Create_Task(Task task);

        //Добавить задачу в избранное
        void Create_TaskToFave(int taskID, int curUserID);

        //Возвращает список правильно созданных (верный тип) задач в правильном порядке
        List<Task> Read_AllTasks();

        //Возвращает список ID корневых задач
        List<int> Read_RootTasks();

        //Возвращает список ID избранных задач пользователя
        List<int> Read_FaveTasks(int curUser);

        //Возвращает список ID подзадач для данной задачи
        List<int> Read_ChildTasks(int taskID);

        //Обновление задачи
        void Update_Task(Task task);

        //Удаление задачи. Родителем потомка становятся родитель удаляемой задачи, работы удаляются.
        void Delete_Task(int taskID);

        //Убрать задачу из избранного
        void Delete_TaskFromFave(int taskID);
    }
}
