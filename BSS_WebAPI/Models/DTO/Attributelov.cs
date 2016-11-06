using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Attributelov : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Int32 Attibuteid  { get; set; } 
        public String Value  { get; set; } 
        public Int32 Sortorder  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/attributelovs/{0}", Id));
                
        }
    }
}