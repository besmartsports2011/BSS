using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Stockshipment : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public Int32? Stocksaleid  { get; set; } 
        public Int32? Stocksaledisputeid  { get; set; } 
        public Int32 Productid  { get; set; } 
        public Decimal Quantityshipped  { get; set; } 
        public Int32 Managerid  { get; set; } 
        public DateTime Date  { get; set; } 
        public String Note  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public String Status  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/stockshipments/{0}", Id));
              
                                    Links.Add("stockshipmentattributes:stockshipmentid",  string.Format(@"http://localhost:63330/api/v1/stockshipments/{0}/stockshipmentattributes/filter/stockshipmentid" ,Id));    
        }
    }
}