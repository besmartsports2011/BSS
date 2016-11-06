using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Stockshipmentattribute : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Int32 Stockshipmentid  { get; set; } 
        public Int32 Attributeid  { get; set; } 
        public String Value  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/stockshipmentattributes/{0}", Id));
                
        }
    }
}