using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class CategoryRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Category, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Category> Get()                                          
        {                                                                     
            var entityList = Context.Categories;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Category, Models.DTO.Category>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Category Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Categories.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Category, Models.DTO.Category>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Category dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Category, BSS_DataAccess.Category>(dto);                            
            Context.Categories.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Category dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Category, BSS_DataAccess.Category>(dto);                            
                                                                               
            var dbEntity = Context.Categories.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.ParentId = entity.ParentId; dbEntity.Name = entity.Name; dbEntity.Note = entity.Note; dbEntity.QualifiedName = entity.QualifiedName; dbEntity.IsDeleted = entity.IsDeleted; dbEntity.Category2 = entity.Category2;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Categories.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Categories.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
