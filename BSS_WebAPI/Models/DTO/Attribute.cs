using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class Attribute : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public String Name  { get; set; } 
        public String Controltype  { get; set; } 
        public String Maxlength  { get; set; } 
        public String Description  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public Boolean Isactive  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/attributes/{0}", Id));
              
                                    Links.Add("attributelovs:attibuteid",  string.Format(@"http://localhost:63330/api/v1/attributes/{0}/attributelovs/filter/attibuteid" ,Id));  
                                    Links.Add("productattributes:attributeid",  string.Format(@"http://localhost:63330/api/v1/attributes/{0}/productattributes/filter/attributeid" ,Id));  
                                    Links.Add("stockshipmentattributes:attributeid",  string.Format(@"http://localhost:63330/api/v1/attributes/{0}/stockshipmentattributes/filter/attributeid" ,Id));    
        }
    }
}