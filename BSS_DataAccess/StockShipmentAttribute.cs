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
    
    public partial class StockShipmentAttribute
    {
        public int Id { get; set; }
        public int StockShipmentId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual StockShipment StockShipment { get; set; }
    }
}
