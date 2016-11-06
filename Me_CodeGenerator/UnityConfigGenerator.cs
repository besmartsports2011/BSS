using BSS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSS_CodeGenerator
{
    public class UnityConfigGenerator
    {
        public string UnityConfigFolder { get; set; }
        public string EntityNamespace { get; set; }
        public string DataAccessAssemblyName { get; set; }
        public string DataModelToExclude { get; set; }

        public void Generate()
        {
            // this call is needed  to initialize the assembly
            var context = new BeSmartSportsEntities();

            var theAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().ToString().StartsWith(string.Format("{0}, Version", DataAccessAssemblyName)))
                .First();

            var theTypes = theAssembly.GetTypes()
                      .Where(t => t.Namespace == EntityNamespace)
                      .ToList();

            var csFile = UnityConfigFolder + "UnityConfig.cs";
            using (StreamWriter sw = new StreamWriter(csFile))
            {
                sw.Write(string.Format(
"using Microsoft.Practices.Unity;                                                                              \r\n" +
"using System.Web.Http;                                                                                        \r\n" +
"using Unity.WebApi;                                                                                           \r\n" +
"using BSS_WebAPI.Models.DTO;                                                                                   \r\n" +
"using BSS_WebAPI.Repositories;                                                                                 \r\n" +
"                                                                                                              \r\n" +
"namespace BSS_WebAPI                                                                                           \r\n" +
"{{                                                                                                             \r\n" +
"    public static class UnityConfig                                                                           \r\n" +
"    {{                                                                                                         \r\n" +
"        public static void RegisterComponents()                                                               \r\n" +
"        {{                                                                                                     \r\n" +
"            var container = new UnityContainer();                                                             \r\n" +
"                                                                                                              \r\n"
));



                foreach (var theType in theTypes)
                {
                    var nameOfEntity = theType.Name; //USER
                    if (nameOfEntity.ToLower() == "dbversion" || nameOfEntity.ToLower() == "sysdiagram")
                        continue;
                    if (nameOfEntity.ToLower() == DataModelToExclude.ToLower())
                        continue;
                    if (nameOfEntity.ToLower().EndsWith("_result")) //SPs
                        continue;

                    var pascalNameOfClass = Util.ToPascalCase(nameOfEntity); //User


                    sw.Write(string.Format(@"
            container.RegisterType<IDataAccessRepository<{0}, int>, {0}Repository>(); 
", pascalNameOfClass));
                }


                sw.Write(string.Format( 
                "\r\n           GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);    \r\n" +
    "        }}                                                                                                     \r\n" +
    "    }}                                                                                                         \r\n" +
    "}}                                                                                                             \r\n"

        ));


            }

            Util.IncludeInProject(csFile.Replace(@"..\..\..\BSS_WebAPI\", ""));

        }
    }

}