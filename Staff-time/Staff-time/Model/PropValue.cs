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
    using Staff_time.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class PropValue
    {
        public int ID { get; set; }
        public int DataType { get; set; }
        public int PropID { get; set; }
        public int TaskID { get; set; }
        public string ValueText { get; set; }
        public Nullable<int> ValueInt { get; set; }
        public Nullable<System.DateTime> ValueDate { get; set; }
        public Nullable<System.TimeSpan> ValueTime { get; set; }

        public virtual Task Task { get; set; }
        public virtual Property Property { get; set; }
    }
}
