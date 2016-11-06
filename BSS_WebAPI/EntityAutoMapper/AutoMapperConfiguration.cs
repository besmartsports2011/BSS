using AutoMapper;                                      
namespace BSS_WebAPI.EntityAutoMapper                   
{                                                  
   public static class AutoMapperConfiguration    
   {                                              
       public static void Configure()             
       {                                          
           Mapper.Initialize(x =>                 
           {                                      

            x.AddProfile(new AttributeMapperProfile()); 
            x.AddProfile(new AttributelovMapperProfile()); 
            x.AddProfile(new CategoryMapperProfile()); 
            x.AddProfile(new CountryMapperProfile()); 
            x.AddProfile(new GlobalsettingMapperProfile()); 
            x.AddProfile(new ManagerMapperProfile()); 
            x.AddProfile(new ProductMapperProfile()); 
            x.AddProfile(new ProductattributeMapperProfile()); 
            x.AddProfile(new ProviderMapperProfile()); 
            x.AddProfile(new StockMapperProfile()); 
            x.AddProfile(new StockpurchaseMapperProfile()); 
            x.AddProfile(new StocksaleMapperProfile()); 
            x.AddProfile(new StocksaledisputeMapperProfile()); 
            x.AddProfile(new StockshipmentMapperProfile()); 
            x.AddProfile(new StockshipmentattributeMapperProfile()); 
            x.AddProfile(new SuperadminMapperProfile()); 
            x.AddProfile(new UserMapperProfile()); 
          });     
      }    
  }        
}            
