using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class StocksaleRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Stocksale, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Stocksale> Get()                                          
        {                                                                     
            var entityList = Context.StockSales;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.StockSale, Models.DTO.Stocksale>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Stocksale Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.StockSales.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.StockSale, Models.DTO.Stocksale>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Stocksale dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stocksale, BSS_DataAccess.StockSale>(dto);                            
            Context.StockSales.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Stocksale dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stocksale, BSS_DataAccess.StockSale>(dto);                            
                                                                               
            var dbEntity = Context.StockSales.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.ProductId = entity.ProductId; dbEntity.QuantitySold = entity.QuantitySold; dbEntity.Currency = entity.Currency; dbEntity.SalePrice = entity.SalePrice; dbEntity.CourierCharges = entity.CourierCharges; dbEntity.PaypalCharges = entity.PaypalCharges; dbEntity.ProviderCharges = entity.ProviderCharges; dbEntity.ProfitAfterAllCharges = entity.ProfitAfterAllCharges; dbEntity.ProviderId = entity.ProviderId; dbEntity.ManagerId = entity.ManagerId; dbEntity.Date = entity.Date; dbEntity.Note = entity.Note; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.IsActive = entity.IsActive; dbEntity.Status = entity.Status; dbEntity.CustomerId = entity.CustomerId; dbEntity.CustomerUrl = entity.CustomerUrl; dbEntity.Product = entity.Product;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.StockSales.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.StockSales.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
