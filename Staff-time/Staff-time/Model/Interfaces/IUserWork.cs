using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface IUserWork
    {
        //Возвращает список правильно созданных (верный тип) задач в правильном порядке
        List<User> Read_AllUsers();
        string GetUserNameByID(int _userID);
    }
}
