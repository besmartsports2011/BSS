using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class StocksaledisputeRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Stocksaledispute, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Stocksaledispute> Get()                                          
        {                                                                     
            var entityList = Context.StockSaleDisputes;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.StockSaleDispute, Models.DTO.Stocksaledispute>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Stocksaledispute Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.StockSaleDisputes.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.StockSaleDispute, Models.DTO.Stocksaledispute>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Stocksaledispute dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stocksaledispute, BSS_DataAccess.StockSaleDispute>(dto);                            
            Context.StockSaleDisputes.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Stocksaledispute dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stocksaledispute, BSS_DataAccess.StockSaleDispute>(dto);                            
                                                                               
            var dbEntity = Context.StockSaleDisputes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.StockSaleId = entity.StockSaleId; dbEntity.ProductId = entity.ProductId; dbEntity.QuantityClaimed = entity.QuantityClaimed; dbEntity.QuantitySent = entity.QuantitySent; dbEntity.CourierCharges = entity.CourierCharges; dbEntity.PaypalCharges = entity.PaypalCharges; dbEntity.ProviderCharges = entity.ProviderCharges; dbEntity.ProfitAfterAllCharges = entity.ProfitAfterAllCharges; dbEntity.ProviderId = entity.ProviderId; dbEntity.ManagerId = entity.ManagerId; dbEntity.Date = entity.Date; dbEntity.Note = entity.Note; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.Status = entity.Status; dbEntity.Product = entity.Product; dbEntity.StockSale = entity.StockSale;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.StockSaleDisputes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.StockSaleDisputes.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
