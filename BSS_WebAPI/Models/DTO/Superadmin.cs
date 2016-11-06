using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Superadmin : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Int32 Userid  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/superadmins/{0}", Id));
                
        }
    }
}