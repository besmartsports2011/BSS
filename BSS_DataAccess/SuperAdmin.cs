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
    
    public partial class SuperAdmin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Cr_By { get; set; }
        public System.DateTime Cr_At { get; set; }
    
        public virtual User User { get; set; }
    }
}