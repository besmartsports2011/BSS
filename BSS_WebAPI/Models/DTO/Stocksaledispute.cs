using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Stocksaledispute : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public Int32 Stocksaleid  { get; set; } 
        public Int32 Productid  { get; set; } 
        public Decimal Quantityclaimed  { get; set; } 
        public Decimal Quantitysent  { get; set; } 
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
        public String Status  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/stocksaledisputes/{0}", Id));
              
                                    Links.Add("stockshipments:stocksaledisputeid",  string.Format(@"http://localhost:63330/api/v1/stocksaledisputes/{0}/stockshipments/filter/stocksaledisputeid" ,Id));    
        }
    }
}