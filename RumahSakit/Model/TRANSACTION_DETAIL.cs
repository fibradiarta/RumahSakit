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
    
    public partial class TRANSACTION_DETAIL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRANSACTION_DETAIL()
        {
            this.DOCTORs = new HashSet<DOCTOR>();
            this.MEDICINEs = new HashSet<MEDICINE>();
            this.NURSEs = new HashSet<NURSE>();
        }
    
        public int TRANSACTION_DETAIL_ID { get; set; }
        public Nullable<int> ROOM_ID { get; set; }
        public Nullable<int> TRANSACTION_ID { get; set; }
        public Nullable<System.DateTime> ARRIVAL_DATE { get; set; }
        public Nullable<System.DateTime> LEAVING_DATE { get; set; }
        public Nullable<double> TOTAL_PRICE { get; set; }
        public Nullable<double> PRICE_ROOM { get; set; }
        public Nullable<int> QTY_MEDICINE { get; set; }
    
        public virtual ROOM ROOM { get; set; }
        public virtual TRANSACTION TRANSACTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCTOR> DOCTORs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICINE> MEDICINEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NURSE> NURSEs { get; set; }
    }
}
