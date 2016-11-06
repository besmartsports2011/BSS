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
    public partial class UserController : ApiController                                
    {                                                                                
        private IDataAccessRepository<User, int> _repository;                          
        public UserController(IDataAccessRepository<User, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/users")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/users/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/users/create/")]                                               
        public IHttpActionResult Post(User user)                                        
        {                                                                            
            _repository.Post(user);                                                    
            return Json(user);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/users/update/")]                                               
        public IHttpActionResult Put(User user)                                         
        {                                                                            
            _repository.Put(user.Id, user);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/users/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/users/{id}/managers/filter/userid")]                                                       
public IHttpActionResult GetManagerByUser_Filter_userid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.Manager, Manager>("Manager", "UserId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/users/{id}/superadmins/filter/userid")]                                                       
public IHttpActionResult GetSuperadminByUser_Filter_userid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.SuperAdmin, Superadmin>("SuperAdmin", "UserId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
