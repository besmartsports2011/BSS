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
    public partial class SuperadminController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Superadmin, int> _repository;                          
        public SuperadminController(IDataAccessRepository<Superadmin, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/superadmins")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/superadmins/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/superadmins/create/")]                                               
        public IHttpActionResult Post(Superadmin superadmin)                                        
        {                                                                            
            _repository.Post(superadmin);                                                    
            return Json(superadmin);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/superadmins/update/")]                                               
        public IHttpActionResult Put(Superadmin superadmin)                                         
        {                                                                            
            _repository.Put(superadmin.Id, superadmin);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/superadmins/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                   
                                                                                      
    }                                                                                
}                                                                                    
