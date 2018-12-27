using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Staff_time.Model;
using Staff_time.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace Staff_time.ViewModel
{
    public static class Context
    {
        private static TaskManagmentDBEntities _context;
        public static ITaskWork taskWork;
        public static IWorkWork workWork;
        public static IAttrWork attrWork;
        public static ITypesWork typesWork;
        public static IUserWork usersWork;

        private static bool _init_tracker = false;
        public static void Init()
        {
            if (_init_tracker)
                return;
            _init_tracker = true;

            _context = new TaskManagmentDBEntities();
            taskWork = _context;
            workWork = _context;
            attrWork = _context;
            typesWork = _context;
            usersWork = _context;
        }

        public static User TestUser;
        private static bool _init_testUser_tracker = false;
        public static User GetTestUser()
        {
            if (!_init_testUser_tracker)
            {
                TestUser = _context.Users.Where(u => u.ID == 1).FirstOrDefault();
                _init_testUser_tracker = true;
            }
            return TestUser;
        }
    }
}
