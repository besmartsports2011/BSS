using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class ProductattributeRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Productattribute, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Productattribute> Get()                                          
        {                                                                     
            var entityList = Context.ProductAttributes;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.ProductAttribute, Models.DTO.Productattribute>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Productattribute Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.ProductAttributes.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.ProductAttribute, Models.DTO.Productattribute>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Productattribute dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Productattribute, BSS_DataAccess.ProductAttribute>(dto);                            
            Context.ProductAttributes.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Productattribute dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Productattribute, BSS_DataAccess.ProductAttribute>(dto);                            
                                                                               
            var dbEntity = Context.ProductAttributes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.ProductId = entity.ProductId; dbEntity.AttributeId = entity.AttributeId; dbEntity.Value = entity.Value; dbEntity.Attribute = entity.Attribute; dbEntity.Product = entity.Product;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.ProductAttributes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.ProductAttributes.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
