using Staff_time.Model;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.Helpers
{
    public class MessageWorkObject
    {
        public WorkCommandEnum _commandType { get; set; }
        public Work _work { get; set; }
        public DateTime dt { get; set; }
        public MessageWorkObject(WorkCommandEnum type_, Work work_, DateTime dt_)
        {
            _commandType = type_;
            _work = work_;
            dt = dt_;
        }
    }
}
