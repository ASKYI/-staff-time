using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Model.Interfaces
{
    public interface ITimeTableWork
    {
        //Возвращает планируемое время за дату
        double Read_TimeByDate(DateTime dt);
        void Update(DateTime dt, double tm);
    }
}
