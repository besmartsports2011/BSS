using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class ProviderRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Provider, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Provider> Get()                                          
        {                                                                     
            var entityList = Context.Providers;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Provider, Models.DTO.Provider>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Provider Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Providers.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Provider, Models.DTO.Provider>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Provider dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Provider, BSS_DataAccess.Provider>(dto);                            
            Context.Providers.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Provider dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Provider, BSS_DataAccess.Provider>(dto);                            
                                                                               
            var dbEntity = Context.Providers.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.Name = entity.Name; dbEntity.WebsiteUrl = entity.WebsiteUrl; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Providers.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Providers.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
