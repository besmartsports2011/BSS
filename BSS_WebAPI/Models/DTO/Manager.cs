using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Manager : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Int32 Userid  { get; set; } 
        public Int32? Providerid  { get; set; } 
        public Int32? Countryid  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/managers/{0}", Id));
              
                                    Links.Add("stocks:managerid",  string.Format(@"http://localhost:63330/api/v1/managers/{0}/stocks/filter/managerid" ,Id));    
        }
    }
}