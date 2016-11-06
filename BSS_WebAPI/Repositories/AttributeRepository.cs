using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class AttributeRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Attribute, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Attribute> Get()                                          
        {                                                                     
            var entityList = Context.Attributes;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Attribute, Models.DTO.Attribute>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Attribute Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Attributes.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Attribute, Models.DTO.Attribute>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Attribute dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Attribute, BSS_DataAccess.Attribute>(dto);                            
            Context.Attributes.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Attribute dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Attribute, BSS_DataAccess.Attribute>(dto);                            
                                                                               
            var dbEntity = Context.Attributes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Name = entity.Name; dbEntity.ControlType = entity.ControlType; dbEntity.MaxLength = entity.MaxLength; dbEntity.Description = entity.Description; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Attributes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Attributes.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
