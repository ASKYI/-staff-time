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
    
    public partial class PropertiesList
    {
        public int PropID { get; set; }
        public string Value { get; set; }
    
        public virtual Property Property { get; set; }
    }
}
