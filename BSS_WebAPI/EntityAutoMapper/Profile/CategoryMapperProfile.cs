using AutoMapper;                                                                          
                                                                                           
namespace BSS_WebAPI.EntityAutoMapper                                                       
{                                                                                          
    class CategoryMapperProfile : Profile                                                       
    {                                                                                      
        protected override void Configure()                                                
        {                                                                                  
            // Forward Mapping                                                             
            SourceMemberNamingConvention = new PascalCaseNamingConvention();               
            DestinationMemberNamingConvention = new MyUpperUnderscoreNamingConvention();   
            CreateMap<Models.DTO.Category, BSS_DataAccess.Category>();                                                         
                                                                                           
            // Reverse Mapping                                                             
            SourceMemberNamingConvention = new MyUpperUnderscoreNamingConvention();        
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();          
            CreateMap<BSS_DataAccess.Category, Models.DTO.Category>();                                                         
                                                                                           
            // Mapper.AssertConfigurationIsValid();                                        
        }                                                                                  
    }                                                                                      
}                                                                                          
