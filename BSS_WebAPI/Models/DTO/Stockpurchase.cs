using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Stockpurchase : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public DateTime Date  { get; set; } 
        public Int32 Productid  { get; set; } 
        public Decimal Quantity  { get; set; } 
        public Decimal Costprice  { get; set; } 
        public String Suppliername  { get; set; } 
        public String Supplierdetail  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/stockpurchases/{0}", Id));
                
        }
    }
}