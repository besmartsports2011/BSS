using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class UserRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.User, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.User> Get()                                          
        {                                                                     
            var entityList = Context.Users;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.User, Models.DTO.User>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.User Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Users.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.User, Models.DTO.User>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.User dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.User, BSS_DataAccess.User>(dto);                            
            Context.Users.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.User dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.User, BSS_DataAccess.User>(dto);                            
                                                                               
            var dbEntity = Context.Users.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.FirstName = entity.FirstName; dbEntity.MiddleName = entity.MiddleName; dbEntity.LastName = entity.LastName; dbEntity.Email = entity.Email; dbEntity.UserName = entity.UserName; dbEntity.Password = entity.Password; dbEntity.AddressLine_1 = entity.AddressLine_1; dbEntity.AddressLine_2 = entity.AddressLine_2; dbEntity.Province = entity.Province; dbEntity.City = entity.City; dbEntity.Country = entity.Country; dbEntity.ZipCode = entity.ZipCode; dbEntity.Phone = entity.Phone; dbEntity.Mobile = entity.Mobile; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.PhotoPath = entity.PhotoPath; dbEntity.IsEmailVerified = entity.IsEmailVerified; dbEntity.IsActive = entity.IsActive; dbEntity.Token = entity.Token;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Users.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Users.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
