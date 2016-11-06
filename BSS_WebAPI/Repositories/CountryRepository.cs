using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class CountryRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Country, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Country> Get()                                          
        {                                                                     
            var entityList = Context.Countries;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Country, Models.DTO.Country>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Country Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Countries.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Country, Models.DTO.Country>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Country dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Country, BSS_DataAccess.Country>(dto);                            
            Context.Countries.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Country dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Country, BSS_DataAccess.Country>(dto);                            
                                                                               
            var dbEntity = Context.Countries.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.Name = entity.Name; dbEntity.ShortName = entity.ShortName; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Countries.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Countries.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
