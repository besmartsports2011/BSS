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
    public partial class StocksaleController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Stocksale, int> _repository;                          
        public StocksaleController(IDataAccessRepository<Stocksale, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stocksales")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/stocksales/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stocksales/create/")]                                               
        public IHttpActionResult Post(Stocksale stocksale)                                        
        {                                                                            
            _repository.Post(stocksale);                                                    
            return Json(stocksale);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stocksales/update/")]                                               
        public IHttpActionResult Put(Stocksale stocksale)                                         
        {                                                                            
            _repository.Put(stocksale.Id, stocksale);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/stocksales/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/stocksales/{id}/stocksaledisputes/filter/stocksaleid")]                                                       
public IHttpActionResult GetStocksaledisputeByStocksale_Filter_stocksaleid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockSaleDispute, Stocksaledispute>("StockSaleDispute", "StockSaleId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/stocksales/{id}/stockshipments/filter/stocksaleid")]                                                       
public IHttpActionResult GetStockshipmentByStocksale_Filter_stocksaleid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockShipment, Stockshipment>("StockShipment", "StockSaleId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
