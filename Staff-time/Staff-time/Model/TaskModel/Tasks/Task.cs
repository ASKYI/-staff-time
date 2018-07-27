using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Staff_time.Model
{
    public partial class Task : ICloneable
    {
        //Есть ли более умный способ написания конструктора копирования?
        // todo любой универсальный будет работать через рефлексию, что в разы дольше. Недостатки текущего решения - если добавятся поля надо не забыть и сюда их вписать
        public Task(Task task) 
        {
            this.ID = task.ID;
            this.TaskName = task.TaskName;
            this.TaskTypeID = task.TaskTypeID;
            this.ParentTaskID = task.ParentTaskID;
            this.Descriptions = task.Descriptions;
            this.IndexNumber = task.IndexNumber;

            this.TaskType = task.TaskType;
            this.Works = task.Works;
            this.PropValues = task.PropValues;
        }

        public object Clone()
        {
            return this.MemberwiseClone(); // todo недостаточная глубина клонирования, массивы останутся у двух объектов одни и те же  (смотреть пример ниже)
        }
    }
}



// class test : ICloneable
// {
//     public int k;
//     public List<int> li;
// 
//     public test(int z)
//     {
//         k = z;
//         li = new List<int>();
//         li.Add(k);
//     }
//     public object Clone()
//     {
//         return this.MemberwiseClone();
//     }
// }
// 
// 
// 
// class Program
// {
//     static void Main(string[] args)
//     {
// 
//         test t = new test(3);
// 
//         var s = (test)t.Clone();
//         s.li.Add(4);   // в объекте t.li тоже добавится элемент! хотя после clone связи между ними не должно быть
// 
//     }
// }
