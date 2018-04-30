using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;

namespace Staff_time.ViewModel
{
    public class WorkViewModel
    {
        public Work Work { get; set; }

        public WorkViewModel(Work work)
        {
            Work = work;
        }
    }
}
