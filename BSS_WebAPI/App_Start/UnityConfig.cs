using Microsoft.Practices.Unity;                                                                              
using System.Web.Http;                                                                                        
using Unity.WebApi;                                                                                           
using BSS_WebAPI.Models.DTO;                                                                                   
using BSS_WebAPI.Repositories;                                                                                 
                                                                                                              
namespace BSS_WebAPI                                                                                           
{                                                                                                             
    public static class UnityConfig                                                                           
    {                                                                                                         
        public static void RegisterComponents()                                                               
        {                                                                                                     
            var container = new UnityContainer();                                                             
                                                                                                              

            container.RegisterType<IDataAccessRepository<Attribute, int>, AttributeRepository>(); 

            container.RegisterType<IDataAccessRepository<Attributelov, int>, AttributelovRepository>(); 

            container.RegisterType<IDataAccessRepository<Category, int>, CategoryRepository>(); 

            container.RegisterType<IDataAccessRepository<Country, int>, CountryRepository>(); 

            container.RegisterType<IDataAccessRepository<Globalsetting, int>, GlobalsettingRepository>(); 

            container.RegisterType<IDataAccessRepository<Manager, int>, ManagerRepository>(); 

            container.RegisterType<IDataAccessRepository<Product, int>, ProductRepository>(); 

            container.RegisterType<IDataAccessRepository<Productattribute, int>, ProductattributeRepository>(); 

            container.RegisterType<IDataAccessRepository<Provider, int>, ProviderRepository>(); 

            container.RegisterType<IDataAccessRepository<Stock, int>, StockRepository>(); 

            container.RegisterType<IDataAccessRepository<Stockpurchase, int>, StockpurchaseRepository>(); 

            container.RegisterType<IDataAccessRepository<Stocksale, int>, StocksaleRepository>(); 

            container.RegisterType<IDataAccessRepository<Stocksaledispute, int>, StocksaledisputeRepository>(); 

            container.RegisterType<IDataAccessRepository<Stockshipment, int>, StockshipmentRepository>(); 

            container.RegisterType<IDataAccessRepository<Stockshipmentattribute, int>, StockshipmentattributeRepository>(); 

            container.RegisterType<IDataAccessRepository<Superadmin, int>, SuperadminRepository>(); 

            container.RegisterType<IDataAccessRepository<User, int>, UserRepository>(); 

           GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);    
        }                                                                                                     
    }                                                                                                         
}                                                                                                             
