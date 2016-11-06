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
    public partial class StocksaledisputeController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Stocksaledispute, int> _repository;                          
        public StocksaledisputeController(IDataAccessRepository<Stocksaledispute, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stocksaledisputes")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stocksaledisputes/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stocksaledisputes/create/")]                                               
        public IHttpActionResult Post(Stocksaledispute stocksaledispute)                                        
        {                                                                            
            _repository.Post(stocksaledispute);                                                    
            return Json(stocksaledispute);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stocksaledisputes/update/")]                                               
        public IHttpActionResult Put(Stocksaledispute stocksaledispute)                                         
        {                                                                            
            _repository.Put(stocksaledispute.Id, stocksaledispute);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stocksaledisputes/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/stocksaledisputes/{id}/stockshipments/filter/stocksaledisputeid")]                                                       
public IHttpActionResult GetStockshipmentByStocksaledispute_Filter_stocksaledisputeid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockShipment, Stockshipment>("StockShipment", "StockSaleDisputeId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
