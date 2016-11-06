using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Product : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public Int32? Categoryid  { get; set; } 
        public String Ean  { get; set; } 
        public String Name  { get; set; } 
        public String Description  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/products/{0}", Id));
              
                                    Links.Add("productattributes:productid",  string.Format(@"http://localhost:63330/api/v1/products/{0}/productattributes/filter/productid" ,Id));  
                                    Links.Add("stocks:productid",  string.Format(@"http://localhost:63330/api/v1/products/{0}/stocks/filter/productid" ,Id));  
                                    Links.Add("stockpurchases:productid",  string.Format(@"http://localhost:63330/api/v1/products/{0}/stockpurchases/filter/productid" ,Id));  
                                    Links.Add("stocksales:productid",  string.Format(@"http://localhost:63330/api/v1/products/{0}/stocksales/filter/productid" ,Id));  
                                    Links.Add("stocksaledisputes:productid",  string.Format(@"http://localhost:63330/api/v1/products/{0}/stocksaledisputes/filter/productid" ,Id));  
                                    Links.Add("stockshipments:productid",  string.Format(@"http://localhost:63330/api/v1/products/{0}/stockshipments/filter/productid" ,Id));    
        }
    }
}