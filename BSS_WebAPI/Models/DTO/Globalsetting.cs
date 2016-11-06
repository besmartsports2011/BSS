using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Globalsetting : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public String Name  { get; set; } 
        public String Value  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/globalsettings/{0}", Id));
                
        }
    }
}