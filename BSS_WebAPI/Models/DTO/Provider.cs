using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Provider : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public String Name  { get; set; } 
        public String Websiteurl  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/providers/{0}", Id));
              
                                    Links.Add("managers:providerid",  string.Format(@"http://localhost:63330/api/v1/providers/{0}/managers/filter/providerid" ,Id));    
        }
    }
}