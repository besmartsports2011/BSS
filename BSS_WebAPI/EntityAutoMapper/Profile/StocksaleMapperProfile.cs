using AutoMapper;                                                                          
                                                                                           
namespace BSS_WebAPI.EntityAutoMapper                                                       
{                                                                                          
    class StocksaleMapperProfile : Profile                                                       
    {                                                                                      
        protected override void Configure()                                                
        {                                                                                  
            // Forward Mapping                                                             
            SourceMemberNamingConvention = new PascalCaseNamingConvention();               
            DestinationMemberNamingConvention = new MyUpperUnderscoreNamingConvention();   
            CreateMap<Models.DTO.Stocksale, BSS_DataAccess.StockSale>();                                                         
                                                                                           
            // Reverse Mapping                                                             
            SourceMemberNamingConvention = new MyUpperUnderscoreNamingConvention();        
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();          
            CreateMap<BSS_DataAccess.StockSale, Models.DTO.Stocksale>();                                                         
                                                                                           
            // Mapper.AssertConfigurationIsValid();                                        
        }                                                                                  
    }                                                                                      
}                                                                                          
