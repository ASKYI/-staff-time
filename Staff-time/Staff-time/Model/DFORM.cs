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
    
    public partial class DFORM
    {
        public int NUM_REC { get; set; }
        public string CLASS { get; set; }
        public string NAME { get; set; }
        public byte[] DOCFORM { get; set; }
        public byte[] DOCVARS { get; set; }
        public string DESCR { get; set; }
        public Nullable<System.DateTime> DT { get; set; }
        public string USERID { get; set; }
        public Nullable<bool> FINISHLOCK { get; set; }
        public byte[] DOCVIS { get; set; }
    }
}
