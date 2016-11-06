using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class StockRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Stock, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Stock> Get()                                          
        {                                                                     
            var entityList = Context.Stocks;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.Stock, Models.DTO.Stock>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Stock Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.Stocks.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.Stock, Models.DTO.Stock>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Stock dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stock, BSS_DataAccess.Stock>(dto);                            
            Context.Stocks.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Stock dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stock, BSS_DataAccess.Stock>(dto);                            
                                                                               
            var dbEntity = Context.Stocks.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.ProductId = entity.ProductId; dbEntity.Quantity = entity.Quantity; dbEntity.Soldout = entity.Soldout; dbEntity.ManagerId = entity.ManagerId; dbEntity.HasAssigned = entity.HasAssigned; dbEntity.Manager = entity.Manager; dbEntity.Product = entity.Product;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.Stocks.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.Stocks.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
