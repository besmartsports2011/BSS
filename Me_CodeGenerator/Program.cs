using BSS_CodeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string entityNamespace = "BSS_DataAccess";
            const string dataAccessAssemblyName  = "BSS_DataAccess";
            const string dataModelToExclude = "BeSmartSportsEntities";
            const string urlBase = @"http://localhost:63330/api/v1";
            TargetProject.CsProjectPath = @"..\..\..\BSS_WebAPI\BSS_WebAPI.csproj";


            const string dtoFolder = @"..\..\..\BSS_WebAPI\Models\DTO\";
            new EntityToDtoGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude, UrlBase = urlBase, DtoFolder = dtoFolder }
            .GenerateDTOs();

            string reposFolder = @"..\..\..\BSS_WebAPI\Repositories\";
            new RepositoryGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude, ReposFolder = reposFolder }
            .GenerateDTORepositories();

            string controllersFolder = @"..\..\..\BSS_WebAPI\Controllers\Auto\";
            new ControllerGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude, ControllersFolder = controllersFolder }
            .GenerateWebApiControllers();

            string unityConfigFolder = @"..\..\..\BSS_WebAPI\App_Start\";
            new UnityConfigGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude, UnityConfigFolder = unityConfigFolder }
            .Generate();

            string profileFolder = @"..\..\..\BSS_WebAPI\EntityAutoMapper\Profile\";
            new ProfileGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude, ProfileFolder = profileFolder }
            .GenerateMapperProfiles();

            string autoMapperConfigFolder = @"..\..\..\BSS_WebAPI\EntityAutoMapper\";
            new EntityAutoMapperGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude, AutoMapperConfigFolder = autoMapperConfigFolder }
            .Generate();

            new CommandGenerator { EntityNamespace = entityNamespace, DataAccessAssemblyName = dataAccessAssemblyName, DataModelToExclude = dataModelToExclude }
            .GenerateCommands();


            Console.WriteLine("Program completed successfully.");
       //     Console.ReadLine();
        }
 
    }
}
