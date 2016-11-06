//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BSS_DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            this.Managers = new HashSet<Manager>();
        }
    
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Cr_By { get; set; }
        public System.DateTime Cr_At { get; set; }
        public Nullable<int> Up_By { get; set; }
        public Nullable<System.DateTime> Up_At { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manager> Managers { get; set; }
    }
}