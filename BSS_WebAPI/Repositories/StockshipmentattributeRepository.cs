using System.Collections.Generic;                                              
using System.Linq;                                                             
using BSS_DataAccess;                                                           
using Microsoft.Practices.Unity;                                               
using AutoMapper;                                                              
using BSS_WebAPI.Models.DTO;                                                    
                                                                               
namespace BSS_WebAPI.Repositories                                               
{                                                                             
    public class StockshipmentattributeRepository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.Stockshipmentattribute, int> 
    {                                                                         
        //[Dependency]                                                           
        //public BeSmartSportsEntities Context { get; set; }                        
                                                                               
        public IEnumerable<Models.DTO.Stockshipmentattribute> Get()                                          
        {                                                                     
            var entityList = Context.StockShipmentAttributes;                                      
            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.StockShipmentAttribute, Models.DTO.Stockshipmentattribute>).ToList();    
            return dtoList;                                                    
        }                                                                     
                                                                               
        public Models.DTO.Stockshipmentattribute Get(int id)                                                 
        {                                                                     
            var dbEntity = Context.StockShipmentAttributes.Find(id);                               
            var dto = Mapper.Map<BSS_DataAccess.StockShipmentAttribute, Models.DTO.Stockshipmentattribute>(dbEntity);                          
            return dto;                                                        
        }                                                                     
                                                                               
        public void Post(Models.DTO.Stockshipmentattribute dto)                                              
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stockshipmentattribute, BSS_DataAccess.StockShipmentAttribute>(dto);                            
            Context.StockShipmentAttributes.Add(entity);                                           
            Context.SaveChanges();                                             
            dto.Id = entity.Id;                                                
        }                                                                     
                                                                               
        public void Put(int id, Models.DTO.Stockshipmentattribute dto)                                       
        {                                                                     
            var entity = Mapper.Map<Models.DTO.Stockshipmentattribute, BSS_DataAccess.StockShipmentAttribute>(dto);                            
                                                                               
            var dbEntity = Context.StockShipmentAttributes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                dbEntity.Id = entity.Id; dbEntity.StockShipmentId = entity.StockShipmentId; dbEntity.AttributeId = entity.AttributeId; dbEntity.Value = entity.Value; dbEntity.Attribute = entity.Attribute; dbEntity.StockShipment = entity.StockShipment;                                                             
                                                                               
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
                                                                               
        public void Delete(int id)                                             
        {                                                                     
            var dbEntity = Context.StockShipmentAttributes.Find(id);                               
            if (dbEntity != null)                                              
            {                                                                 
                Context.StockShipmentAttributes.Remove(dbEntity);                                  
                Context.SaveChanges();                                         
            }                                                                 
        }                                                                     
    }                                                                         
}                                                                             
