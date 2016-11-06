using AutoMapper;                                                                          
                                                                                           
namespace BSS_WebAPI.EntityAutoMapper                                                       
{                                                                                          
    class ManagerMapperProfile : Profile                                                       
    {                                                                                      
        protected override void Configure()                                                
        {                                                                                  
            // Forward Mapping                                                             
            SourceMemberNamingConvention = new PascalCaseNamingConvention();               
            DestinationMemberNamingConvention = new MyUpperUnderscoreNamingConvention();   
            CreateMap<Models.DTO.Manager, BSS_DataAccess.Manager>();                                                         
                                                                                           
            // Reverse Mapping                                                             
            SourceMemberNamingConvention = new MyUpperUnderscoreNamingConvention();        
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();          
            CreateMap<BSS_DataAccess.Manager, Models.DTO.Manager>();                                                         
                                                                                           
            // Mapper.AssertConfigurationIsValid();                                        
        }                                                                                  
    }                                                                                      
}                                                                                          
