//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.PropValues = new HashSet<PropValue>();
            this.Tasks1 = new HashSet<Task>();
            this.UserTasks = new HashSet<UserTask>();
            this.Works = new HashSet<Work>();
        }
    
        public string TaskName { get; set; }
        public int TaskTypeID { get; set; }
        public int ID { get; set; }
        public Nullable<int> ParentTaskID { get; set; }
        public int WorkTypeID { get; set; }
        public string Descriptions { get; set; }
        public Nullable<int> IndexNumber { get; set; }
        public Nullable<int> LevelID { get; set; }
    
        public virtual LEVEL LEVEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropValue> PropValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks1 { get; set; }
        public virtual Task Task1 { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual WorkType WorkType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTask> UserTasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Work> Works { get; set; }
    }
}
