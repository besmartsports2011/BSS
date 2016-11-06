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
    
    public partial class StockPurchase
    {
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public System.DateTime Date { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public string SupplierName { get; set; }
        public string SupplierDetail { get; set; }
        public int Cr_By { get; set; }
        public System.DateTime Cr_At { get; set; }
        public Nullable<int> Up_By { get; set; }
        public Nullable<System.DateTime> Up_At { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
