//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Staff_time.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttrValue
    {
        public int ID { get; set; }
        public int AttrID { get; set; }
        public int WorkID { get; set; }
        public int DataType { get; set; }
        public string ValueText { get; set; }
        public Nullable<int> ValueInt { get; set; }
        public Nullable<System.DateTime> ValueDate { get; set; }
        public Nullable<System.TimeSpan> ValueTime { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual Work Work { get; set; }
    }
}
