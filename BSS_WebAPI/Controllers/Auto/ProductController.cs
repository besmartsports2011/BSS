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
    public partial class ProductController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Product, int> _repository;                          
        public ProductController(IDataAccessRepository<Product, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/products")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/products/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/products/create/")]                                               
        public IHttpActionResult Post(Product product)                                        
        {                                                                            
            _repository.Post(product);                                                    
            return Json(product);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/products/update/")]                                               
        public IHttpActionResult Put(Product product)                                         
        {                                                                            
            _repository.Put(product.Id, product);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/products/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/products/{id}/productattributes/filter/productid")]                                                       
public IHttpActionResult GetProductattributeByProduct_Filter_productid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.ProductAttribute, Productattribute>("ProductAttribute", "ProductId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/products/{id}/stocks/filter/productid")]                                                       
public IHttpActionResult GetStockByProduct_Filter_productid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.Stock, Stock>("Stock", "ProductId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/products/{id}/stockpurchases/filter/productid")]                                                       
public IHttpActionResult GetStockpurchaseByProduct_Filter_productid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockPurchase, Stockpurchase>("StockPurchase", "ProductId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/products/{id}/stocksales/filter/productid")]                                                       
public IHttpActionResult GetStocksaleByProduct_Filter_productid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockSale, Stocksale>("StockSale", "ProductId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/products/{id}/stocksaledisputes/filter/productid")]                                                       
public IHttpActionResult GetStocksaledisputeByProduct_Filter_productid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockSaleDispute, Stocksaledispute>("StockSaleDispute", "ProductId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/products/{id}/stockshipments/filter/productid")]                                                       
public IHttpActionResult GetStockshipmentByProduct_Filter_productid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockShipment, Stockshipment>("StockShipment", "ProductId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
