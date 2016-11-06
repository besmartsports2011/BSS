using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class AttributelovRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Attributelov, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Attributelov> Get()                                          
        {                                                                     
            var entityList = Context.AttributeLovs;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.AttributeLov, Models.DTO.Attributelov>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Attributelov Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.AttributeLovs.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.AttributeLov, Models.DTO.Attributelov>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Attributelov dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Attributelov, BSS_DataAccess.AttributeLov>(dto);                            
            Context.AttributeLovs.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Attributelov dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Attributelov, BSS_DataAccess.AttributeLov>(dto);                            
                                                                               
            var dbEntity = Context.AttributeLovs.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.AttibuteId = entity.AttibuteId; dbEntity.Value = entity.Value; dbEntity.SortOrder = entity.SortOrder; dbEntity.Attribute = entity.Attribute;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.AttributeLovs.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.AttributeLovs.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
