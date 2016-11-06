using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class SuperadminRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Superadmin, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Superadmin> Get()                                          
        {                                                                     
            var entityList = Context.SuperAdmins;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.SuperAdmin, Models.DTO.Superadmin>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Superadmin Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.SuperAdmins.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.SuperAdmin, Models.DTO.Superadmin>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Superadmin dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Superadmin, BSS_DataAccess.SuperAdmin>(dto);                            
            Context.SuperAdmins.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Superadmin dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Superadmin, BSS_DataAccess.SuperAdmin>(dto);                            
                                                                               
            var dbEntity = Context.SuperAdmins.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.UserId = entity.UserId; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.User = entity.User;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.SuperAdmins.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.SuperAdmins.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
