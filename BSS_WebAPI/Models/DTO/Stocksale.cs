using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Stocksale : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public Int32 Productid  { get; set; } 
        public Decimal Quantitysold  { get; set; } 
        public String Currency  { get; set; } 
        public Decimal Saleprice  { get; set; } 
        public Decimal Couriercharges  { get; set; } 
        public Decimal Paypalcharges  { get; set; } 
        public Decimal Providercharges  { get; set; } 
        public Decimal Profitafterallcharges  { get; set; } 
        public String Providerid  { get; set; } 
        public Int32 Managerid  { get; set; } 
        public DateTime Date  { get; set; } 
        public String Note  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; } 
        public String Status  { get; set; } 
        public String Customerid  { get; set; } 
        public String Customerurl  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/stocksales/{0}", Id));
              
                                    Links.Add("stocksaledisputes:stocksaleid",  string.Format(@"http://localhost:63330/api/v1/stocksales/{0}/stocksaledisputes/filter/stocksaleid" ,Id));  
                                    Links.Add("stockshipments:stocksaleid",  string.Format(@"http://localhost:63330/api/v1/stocksales/{0}/stockshipments/filter/stocksaleid" ,Id));    
        }
    }
}