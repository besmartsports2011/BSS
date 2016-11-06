using System;
namespace BSS_WebAPI.Models.DTO
{
    public  class User : BaseDTO
    {  
        public Int32 Id  { get; set; } 
        public Guid Guid  { get; set; } 
        public String Firstname  { get; set; } 
        public String Middlename  { get; set; } 
        public String Lastname  { get; set; } 
        public String Email  { get; set; } 
        public String Username  { get; set; } 
        public String Password  { get; set; } 
        public String Addressline1  { get; set; } 
        public String Addressline2  { get; set; } 
        public String Province  { get; set; } 
        public String City  { get; set; } 
        public String Country  { get; set; } 
        public String Zipcode  { get; set; } 
        public String Phone  { get; set; } 
        public String Mobile  { get; set; } 
        public Int32 CrBy  { get; set; } 
        public DateTime CrAt  { get; set; } 
        public Int32? UpBy  { get; set; } 
        public DateTime? UpAt  { get; set; } 
        public String Photopath  { get; set; } 
        public Boolean Isemailverified  { get; set; } 
        public Boolean Isactive  { get; set; } 
        public String Token  { get; set; }
        public void SetLinks()
        { 
             Links.Add("self", string.Format(@"http://localhost:63330/api/v1/users/{0}", Id));
              
                                    Links.Add("managers:userid",  string.Format(@"http://localhost:63330/api/v1/users/{0}/managers/filter/userid" ,Id));  
                                    Links.Add("superadmins:userid",  string.Format(@"http://localhost:63330/api/v1/users/{0}/superadmins/filter/userid" ,Id));    
        }
    }
}