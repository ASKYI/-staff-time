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
    
    public partial class Work
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Work()
        {
            this.AttrValues = new HashSet<AttrValue>();
        }
    
        public string WorkName { get; set; }
        public int ID { get; set; }
        public int TaskID { get; set; }
        public Nullable<int> WorkTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttrValue> AttrValues { get; set; }
        public virtual Task Task { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
