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
    
    public partial class TaskTypeProp
    {
        public int ID { get; set; }
        public int TaskTypeID { get; set; }
        public int PropID { get; set; }
    
        public virtual Property Property { get; set; }
        public virtual TaskType TaskType { get; set; }
    }
}
