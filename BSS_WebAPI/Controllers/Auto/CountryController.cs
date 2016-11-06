using System.Net;                                                                     
using System.Web.Http;                                                                
using System.Web.Http.Cors;                                                           
using BSS_WebAPI.Auth;                                                                 
using BSS_WebAPI.Models.DTO;                                                           
using System.Linq;                                                                    
                                                                                      
namespace BSS_WebAPI.Controllers.Auto                                                  
{                                                                                    
    [EnableCors("*", "*", "*")]                                                 
    //[CustomAuthorize]                                                               
    public partial class CountryController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Country, int> _repository;                          
        public CountryController(IDataAccessRepository<Country, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/countries")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/countries/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/countries/create/")]                                               
        public IHttpActionResult Post(Country country)                                        
        {                                                                            
            _repository.Post(country);                                                    
            return Json(country);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/countries/update/")]                                               
        public IHttpActionResult Put(Country country)                                         
        {                                                                            
            _repository.Put(country.Id, country);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/countries/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/countries/{id}/managers/filter/countryid")]                                                       
public IHttpActionResult GetManagerByCountry_Filter_countryid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.Manager, Manager>("Manager", "CountryId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
