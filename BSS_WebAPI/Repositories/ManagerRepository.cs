using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class ManagerRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Manager, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Manager> Get()                                          
        {                                                                     
            var entityList = Context.Managers;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Manager, Models.DTO.Manager>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Manager Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Managers.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Manager, Models.DTO.Manager>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Manager dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Manager, BSS_DataAccess.Manager>(dto);                            
            Context.Managers.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Manager dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Manager, BSS_DataAccess.Manager>(dto);                            
                                                                               
            var dbEntity = Context.Managers.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.UserId = entity.UserId; dbEntity.ProviderId = entity.ProviderId; dbEntity.CountryId = entity.CountryId; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive; dbEntity.Country = entity.Country; dbEntity.Provider = entity.Provider; dbEntity.User = entity.User;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Managers.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Managers.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
