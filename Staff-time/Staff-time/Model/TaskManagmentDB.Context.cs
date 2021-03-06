﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Staff_time.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TaskManagmentDBEntities : DbContext
    {
        public TaskManagmentDBEntities()
            : base("name=TaskManagmentDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttrValue> AttrValues { get; set; }
        public virtual DbSet<TaskTypeProp> TaskTypeProps { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<WorkTypeAttr> WorkTypeAttrs { get; set; }
        public virtual DbSet<WorkType> WorkTypes { get; set; }
        public virtual DbSet<LEVEL> LEVELS { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<PropValue> PropValues { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<PropertiesList> PropertiesLists { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<ListsValue> ListsValues { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<WorkTimeRange> WorkTimeRanges { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<LogTable> LogTables { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<WorkAbsence> WorkAbsences { get; set; }
    
        public virtual int RepareUserTree(Nullable<int> taskId)
        {
            var taskIdParameter = taskId.HasValue ?
                new ObjectParameter("taskId", taskId) :
                new ObjectParameter("taskId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RepareUserTree", taskIdParameter);
        }
    
        public virtual int UpdateTaskIndexNumbersAfterAppend(Nullable<int> nextIndexNumber)
        {
            var nextIndexNumberParameter = nextIndexNumber.HasValue ?
                new ObjectParameter("NextIndexNumber", nextIndexNumber) :
                new ObjectParameter("NextIndexNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateTaskIndexNumbersAfterAppend", nextIndexNumberParameter);
        }
    
        public virtual int GenerateTaskResults2(Nullable<System.DateTime> d)
        {
            var dParameter = d.HasValue ?
                new ObjectParameter("d", d) :
                new ObjectParameter("d", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerateTaskResults2", dParameter);
        }
    
        public virtual int GenerateTaskResults2All()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerateTaskResults2All");
        }
    
        public virtual int GenerateTaskResults2Changed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerateTaskResults2Changed");
        }
    
        public virtual int TaskDuplicate(Nullable<int> taskIDFrom, Nullable<int> taskIDTo, Nullable<int> userID)
        {
            var taskIDFromParameter = taskIDFrom.HasValue ?
                new ObjectParameter("TaskIDFrom", taskIDFrom) :
                new ObjectParameter("TaskIDFrom", typeof(int));
    
            var taskIDToParameter = taskIDTo.HasValue ?
                new ObjectParameter("TaskIDTo", taskIDTo) :
                new ObjectParameter("TaskIDTo", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TaskDuplicate", taskIDFromParameter, taskIDToParameter, userIDParameter);
        }
    }
}
