using AutoMapper;                                                                          
                                                                                           
namespace BSS_WebAPI.EntityAutoMapper                                                       
{                                                                                          
    class UserMapperProfile : Profile                                                       
    {                                                                                      
        protected override void Configure()                                                
        {                                                                                  
            // Forward Mapping                                                             
            SourceMemberNamingConvention = new PascalCaseNamingConvention();               
            DestinationMemberNamingConvention = new MyUpperUnderscoreNamingConvention();   
            CreateMap<Models.DTO.User, BSS_DataAccess.User>();                                                         
                                                                                           
            // Reverse Mapping                                                             
            SourceMemberNamingConvention = new MyUpperUnderscoreNamingConvention();        
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();          
            CreateMap<BSS_DataAccess.User, Models.DTO.User>();                                                         
                                                                                           
            // Mapper.AssertConfigurationIsValid();                                        
        }                                                                                  
    }                                                                                      
}                                                                                          
