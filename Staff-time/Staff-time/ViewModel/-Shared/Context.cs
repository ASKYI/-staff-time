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
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

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
        public static ILevelWork levelWork;
        public static ITimeTableWork timeTableWork;
        public static IProcedureWork procedureWork;
        public static IRequestWork requestWork;
        public static IPropertyWork propertyWork;
        public static IListWork listWork;
        public static ILogWork logWork;

        public static bool IsContextExist() { return _context != null; }

        public static bool _init_tracker = false;

        public static string GetConnString()
        {
            return _context.Database.Connection.ConnectionString;
        }
        public static void ReloadContext()
        {
            var refreshableObjects = _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).Select(c => c.Entity).ToList();
            var context = ((IObjectContextAdapter)_context).ObjectContext;
            context.Refresh(RefreshMode.StoreWins, refreshableObjects);

            TasksVM.Init_Full_tracker = false;
            TasksVM.InitFullTree();
            //_init_tracker = false;
            //Init();
        }
        public static void Init()
        {
            if (_init_tracker)
                return;
            _init_tracker = true;

            _context = new TaskManagmentDBEntities();
            _context.Configuration.AutoDetectChangesEnabled = false;

              taskWork = _context;
            workWork = _context;
            attrWork = _context;
            typesWork = _context;
            usersWork = _context;
            levelWork = _context;
            timeTableWork = _context;
            procedureWork = _context;
            requestWork = _context;
            propertyWork = _context;
            listWork = _context;
            logWork = _context;
        }
    }
}
