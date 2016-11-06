using AutoMapper;                                                                          
                                                                                           
namespace BSS_WebAPI.EntityAutoMapper                                                       
{                                                                                          
    class StockMapperProfile : Profile                                                       
    {                                                                                      
        protected override void Configure()                                                
        {                                                                                  
            // Forward Mapping                                                             
            SourceMemberNamingConvention = new PascalCaseNamingConvention();               
            DestinationMemberNamingConvention = new MyUpperUnderscoreNamingConvention();   
            CreateMap<Models.DTO.Stock, BSS_DataAccess.Stock>();                                                         
                                                                                           
            // Reverse Mapping                                                             
            SourceMemberNamingConvention = new MyUpperUnderscoreNamingConvention();        
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();          
            CreateMap<BSS_DataAccess.Stock, Models.DTO.Stock>();                                                         
                                                                                           
            // Mapper.AssertConfigurationIsValid();                                        
        }                                                                                  
    }                                                                                      
}                                                                                          
