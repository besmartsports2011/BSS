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
    public partial class StockshipmentController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Stockshipment, int> _repository;                          
        public StockshipmentController(IDataAccessRepository<Stockshipment, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stockshipments")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stockshipments/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stockshipments/create/")]                                               
        public IHttpActionResult Post(Stockshipment stockshipment)                                        
        {                                                                            
            _repository.Post(stockshipment);                                                    
            return Json(stockshipment);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stockshipments/update/")]                                               
        public IHttpActionResult Put(Stockshipment stockshipment)                                         
        {                                                                            
            _repository.Put(stockshipment.Id, stockshipment);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stockshipments/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/stockshipments/{id}/stockshipmentattributes/filter/stockshipmentid")]                                                       
public IHttpActionResult GetStockshipmentattributeByStockshipment_Filter_stockshipmentid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockShipmentAttribute, Stockshipmentattribute>("StockShipmentAttribute", "StockShipmentId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
