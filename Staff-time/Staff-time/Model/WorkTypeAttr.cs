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
    
    public partial class WorkTypeAttr
    {
        public int ID { get; set; }
        public int WorkTypeID { get; set; }
        public int AttrID { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
