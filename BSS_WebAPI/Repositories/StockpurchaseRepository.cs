using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class StockpurchaseRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Stockpurchase, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Stockpurchase> Get()                                          
        {                                                                     
            var entityList = Context.StockPurchases;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.StockPurchase, Models.DTO.Stockpurchase>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Stockpurchase Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.StockPurchases.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.StockPurchase, Models.DTO.Stockpurchase>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Stockpurchase dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stockpurchase, BSS_DataAccess.StockPurchase>(dto);                            
            Context.StockPurchases.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Stockpurchase dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stockpurchase, BSS_DataAccess.StockPurchase>(dto);                            
                                                                               
            var dbEntity = Context.StockPurchases.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.Date = entity.Date; dbEntity.ProductId = entity.ProductId; dbEntity.Quantity = entity.Quantity; dbEntity.CostPrice = entity.CostPrice; dbEntity.SupplierName = entity.SupplierName; dbEntity.SupplierDetail = entity.SupplierDetail; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive; dbEntity.Product = entity.Product;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.StockPurchases.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.StockPurchases.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
