//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestTree.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropValue
    {
        public System.Guid PropValueID { get; set; }
        public System.Guid TaskID { get; set; }
        public System.Guid PropID { get; set; }
        public string Value { get; set; }
    
        public virtual Property Property { get; set; }
        public virtual Task Task { get; set; }
    }
}
