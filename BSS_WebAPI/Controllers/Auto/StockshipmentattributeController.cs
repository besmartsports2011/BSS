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
    public partial class StockshipmentattributeController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Stockshipmentattribute, int> _repository;                          
        public StockshipmentattributeController(IDataAccessRepository<Stockshipmentattribute, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stockshipmentattributes")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stockshipmentattributes/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stockshipmentattributes/create/")]                                               
        public IHttpActionResult Post(Stockshipmentattribute stockshipmentattribute)                                        
        {                                                                            
            _repository.Post(stockshipmentattribute);                                                    
            return Json(stockshipmentattribute);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stockshipmentattributes/update/")]                                               
        public IHttpActionResult Put(Stockshipmentattribute stockshipmentattribute)                                         
        {                                                                            
            _repository.Put(stockshipmentattribute.Id, stockshipmentattribute);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stockshipmentattributes/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                   
                                                                                      
    }                                                                                
}                                                                                    
