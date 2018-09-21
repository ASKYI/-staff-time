using System;
using System.Collections.Generic;

namespace Staff_time.Model.Repositories
{
    public interface ITaskRepository // done: переименован интерфейс, функции, убраны лишние библиотеки
    {
        void AddTask(Task task);

        //Добавить задачу в избранное
        void AddTaskToFave(int taskID, int curUserID);

        //Возвращает список правильно созданных (верный тип) задач в правильном порядке
        List<Task> GetAllTasks();

        //Возвращает список ID корневых задач
        List<int> GetRootTasks();

        //Возвращает список ID избранных задач пользователя
        List<int> GetFaveTasks(int curUser);

        //Возвращает список ID подзадач для данной задачи
        List<int> GetChildTasks(int taskID);

        //Обновление задачи
        void UpdateTask(Task task);

        //Удаление задачи. Родителем потомка становятся родитель удаляемой задачи, работы удаляются.
        void DeleteTask(int taskID);

        //Убрать задачу из избранного
        void DeleteTaskFromFave(int taskID);
    }
}
