﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.ViewModel
{
    public interface IWorkInBlockFactory
    {
        WorkInBlock CreateWorkInBlock(Model.Work work);
    }
}