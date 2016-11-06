using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Country : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public String Name  { get; set; } 
        public String Shortname  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/countries/{0}", Id));
              
                                    Links.Add("managers:countryid",  string.Format(@"http://localhost:63330/api/v1/countries/{0}/managers/filter/countryid" ,Id));    
        }
    }
}