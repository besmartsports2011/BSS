using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Category : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Int32? Parentid  { get; set; } 
        public String Name  { get; set; } 
        public String Note  { get; set; } 
        public String Qualifiedname  { get; set; } 
        public Boolean Isdeleted  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/categories/{0}", Id));
              
                                    Links.Add("products:categoryid",  string.Format(@"http://localhost:63330/api/v1/categories/{0}/products/filter/categoryid" ,Id));    
        }
    }
}