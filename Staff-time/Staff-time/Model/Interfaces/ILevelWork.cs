using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ILevelWork
    {
        //Возвращает список правильно созданных (верный тип) задач в правильном порядке
        Dictionary<string, int> Read_AllLevels();
        List<LEVEL> Read_AllLevelsLowerMe();
    }
}
