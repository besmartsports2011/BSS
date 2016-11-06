using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class ProductRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Product, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Product> Get()                                          
        {                                                                     
            var entityList = Context.Products;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Product, Models.DTO.Product>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Product Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Products.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Product, Models.DTO.Product>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Product dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Product, BSS_DataAccess.Product>(dto);                            
            Context.Products.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Product dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Product, BSS_DataAccess.Product>(dto);                            
                                                                               
            var dbEntity = Context.Products.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.CategoryId = entity.CategoryId; dbEntity.EAN = entity.EAN; dbEntity.Name = entity.Name; dbEntity.Description = entity.Description; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive; dbEntity.Category = entity.Category;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Products.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Products.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
