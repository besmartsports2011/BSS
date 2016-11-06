using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Stock : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public Int32 Productid  { get; set; } 
        public Decimal Quantity  { get; set; } 
        public Decimal? Soldout  { get; set; } 
        public Int32? Managerid  { get; set; } 
        public Boolean? Hasassigned  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/stocks/{0}", Id));
                
        }
    }
}