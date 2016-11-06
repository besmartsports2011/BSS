using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class GlobalsettingRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Globalsetting, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Globalsetting> Get()                                          
        {                                                                     
            var entityList = Context.GlobalSettings;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.GlobalSetting, Models.DTO.Globalsetting>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Globalsetting Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.GlobalSettings.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.GlobalSetting, Models.DTO.Globalsetting>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Globalsetting dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Globalsetting, BSS_DataAccess.GlobalSetting>(dto);                            
            Context.GlobalSettings.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Globalsetting dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Globalsetting, BSS_DataAccess.GlobalSetting>(dto);                            
                                                                               
            var dbEntity = Context.GlobalSettings.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Name = entity.Name; dbEntity.Value = entity.Value;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.GlobalSettings.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.GlobalSettings.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
