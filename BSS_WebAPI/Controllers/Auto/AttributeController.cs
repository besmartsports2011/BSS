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
    public partial class AttributeController : ApiController                                
    {                                                                                
        private IDataAccessRepository<Attribute, int> _repository;                          
        public AttributeController(IDataAccessRepository<Attribute, int> repository)              
        {                                                                            
            _repository = repository;                                                 
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/attributes")]                                                       
        public IHttpActionResult Get()                                                
        {                                                                            
            var list = _repository.Get();                                             
            #if DEBUG                                                                 
               list.ToList().ForEach(x => x.SetLinks());                               
            #endif                                                                    
            return Json(list);                                                         
        }                                                                            
                                                                                      
        [HttpGet]                                                                     
        [Route("api/v1/attributes/{id}")]                                                
        public IHttpActionResult Get(int id)                                          
        {                                                                            
            return Json(_repository.Get(id));                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/attributes/create/")]                                               
        public IHttpActionResult Post(Attribute attribute)                                        
        {                                                                            
            _repository.Post(attribute);                                                    
            return Json(attribute);                                                         
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/attributes/update/")]                                               
        public IHttpActionResult Put(Attribute attribute)                                         
        {                                                                            
            _repository.Put(attribute.Id, attribute);                                             
            return Json("OK");                                                      
        }                                                                            
                                                                                      
        [HttpPost]                                                                    
        [Route("api/v1/attributes/delete/")]                                               
        public IHttpActionResult Delete(int id)                                       
        {                                                                            
            _repository.Delete(id);                                                   
            return Json(HttpStatusCode.NoContent);                                    
        }                                                                            
                                                                                      
                                                                                                           
[HttpGet]                                                                                          
[Route("api/v1/attributes/{id}/attributelovs/filter/attibuteid")]                                                       
public IHttpActionResult GetAttributelovByAttribute_Filter_attibuteid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.AttributeLov, Attributelov>("AttributeLov", "AttibuteId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/attributes/{id}/productattributes/filter/attributeid")]                                                       
public IHttpActionResult GetProductattributeByAttribute_Filter_attributeid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.ProductAttribute, Productattribute>("ProductAttribute", "AttributeId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                                                    
[HttpGet]                                                                                          
[Route("api/v1/attributes/{id}/stockshipmentattributes/filter/attributeid")]                                                       
public IHttpActionResult GetStockshipmentattributeByAttribute_Filter_attributeid (int id)                                            
{                                                                                                 
    var list = _repository.GetXByFK<BSS_DataAccess.StockShipmentAttribute, Stockshipmentattribute>("StockShipmentAttribute", "AttributeId", id.ToString());                
    #if DEBUG                                                                                      
        list.ToList().ForEach(x => x.SetLinks());                                                  
    #endif                                                                                         
    return Json(list);                                                                             
}                                                                            
                                                                                      
    }                                                                                
}                                                                                    
