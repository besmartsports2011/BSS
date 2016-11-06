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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Managers = new HashSet<Manager>();
            this.SuperAdmins = new HashSet<SuperAdmin>();
        }
    
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AddressLine_1 { get; set; }
        public string AddressLine_2 { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int Cr_By { get; set; }
        public System.DateTime Cr_At { get; set; }
        public Nullable<int> Up_By { get; set; }
        public Nullable<System.DateTime> Up_At { get; set; }
        public string PhotoPath { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manager> Managers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuperAdmin> SuperAdmins { get; set; }
    }
}
