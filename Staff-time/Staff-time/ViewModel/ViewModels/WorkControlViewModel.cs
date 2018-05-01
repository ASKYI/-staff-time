﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace Staff_time.ViewModel
{
    public class WorkControlViewModel : MainViewModel
    {
        public string a;
        private Work _work;
        public Work Work
        {
            get { return _work; }
            set
            {
                SetField(ref _work, value);
            }
        }
        public WorkControlViewModel(Work work)
        {
            Work = work;
            a = "Hello"; 
        }
    }
}
