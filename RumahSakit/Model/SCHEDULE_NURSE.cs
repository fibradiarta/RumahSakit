//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RumahSakit.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SCHEDULE_NURSE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SCHEDULE_NURSE()
        {
            this.NURSEs = new HashSet<NURSE>();
        }
    
        public int SCHEDULE_NURSE_ID { get; set; }
        public Nullable<System.DateTime> DATES { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NURSE> NURSEs { get; set; }
    }
}