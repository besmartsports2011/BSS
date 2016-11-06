using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class StockshipmentRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Stockshipment, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Stockshipment> Get()                                          
        {                                                                     
            var entityList = Context.StockShipments;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.StockShipment, Models.DTO.Stockshipment>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Stockshipment Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.StockShipments.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.StockShipment, Models.DTO.Stockshipment>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Stockshipment dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stockshipment, BSS_DataAccess.StockShipment>(dto);                            
            Context.StockShipments.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Stockshipment dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stockshipment, BSS_DataAccess.StockShipment>(dto);                            
                                                                               
            var dbEntity = Context.StockShipments.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.Guid = entity.Guid; dbEntity.StockSaleId = entity.StockSaleId; dbEntity.StockSaleDisputeId = entity.StockSaleDisputeId; dbEntity.ProductId = entity.ProductId; dbEntity.QuantityShipped = entity.QuantityShipped; dbEntity.ManagerId = entity.ManagerId; dbEntity.Date = entity.Date; dbEntity.Note = entity.Note; dbEntity.Cr_By = entity.Cr_By; dbEntity.Cr_At = entity.Cr_At; dbEntity.Up_By = entity.Up_By; dbEntity.Up_At = entity.Up_At; dbEntity.Status = entity.Status; dbEntity.Product = entity.Product; dbEntity.StockSale = entity.StockSale; dbEntity.StockSaleDispute = entity.StockSaleDispute;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.StockShipments.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.StockShipments.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
