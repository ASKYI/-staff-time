using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ITaskWork  // todo функционал очень похож на репозиторий, странно что не фигурирует это имя Если используется паттерн, очень хорошо упоминать это в имени, т.к. это маркер другим программистам
    {
        void Create_Task(Task task);

        //Добавить задачу в избранное
        void Create_TaskToFave(int taskID, int curUserID);

        //Редактировать задачу в избранном, установить развернутость
        void Update_UserTaskExpended(List<TreeNode> nodes);

        //Возвращает список правильно созданных (верный тип) задач в правильном порядке
        List<Task> Read_AllTasks(List<int> existTaskIDs);

        //Возвращает список ID корневых задач
        List<int> Read_RootTasks();

        //Проверяет, есть ли в избранном задача
        bool IsFave(int taskID);

        //Возвращает все доп. поля задачи
        List<Property> GetAllProperties(int tasktTypeID);

        //Удаляем все значения доп. полей задачи из списка
        void DeleteProperties(List<PropValue> deleteList);

        //Проверяет, есть ли уже такая задача
        bool IsExist(int taskID, string taskName, int? parentTaskID);

        //меняет местами задачи в избранном пользователя
        void ReplaceUserTasks(Task task1, Task task2);

        //Проверяет, является ли задача у пользователя развернутой в дереве
        bool IsExpanded(int taskID, int userID);

        //Возвращает список ID избранных задач пользователя
        List<int> Read_FaveTasksID(int curUser);

        //Возвращает список избранных задач пользователя
        List<Task> Read_FaveTasks(int curUser);

        //Возвращает список ID подзадач для данной задачи
        List<int> Read_ChildTasks(int taskID);

        //Обновление задачи
        void Update_Task(Task task);

        //Удаление задачи. Родителем потомка становятся родитель удаляемой задачи, работы удаляются.
        bool Delete_Task(int taskID);

        //Убрать задачу из избранного
        void Delete_TaskFromFave(int taskID);

        //Возвращает все имена работ для этой задачи
        List<string> GetAllWorksNames(int taskID);
        //Индексацию сделать, чтобы правильная нумерация была
        void UpdateUserTaskIndexNumber(int taksID, int pos);
    }
}
