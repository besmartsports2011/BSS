using BSS_CodeGenerator;
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

namespace Me_CodeGenerator
{
    public class EntityAutoMapperGenerator
    {
        public string AutoMapperConfigFolder { get; set; }
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

            var csFile = AutoMapperConfigFolder + "AutoMapperConfiguration.cs";
            using (StreamWriter sw = new StreamWriter(csFile))
            {
                sw.Write(string.Format(
"using AutoMapper;                                      \r\n" +
"namespace BSS_WebAPI.EntityAutoMapper                   \r\n" +
"{{                                                  \r\n" +
"   public static class AutoMapperConfiguration    \r\n" +
"   {{                                              \r\n" +
"       public static void Configure()             \r\n" +
"       {{                                          \r\n" +
"           Mapper.Initialize(x =>                 \r\n" +
"           {{                                      \r\n" 
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
            x.AddProfile(new {0}MapperProfile()); ", pascalNameOfClass));
                }

                sw.Write(string.Format(
                 "\r\n          }});     \r\n" +
                 "      }}    \r\n" +
                 "  }}        \r\n" +
                 "}}            \r\n" 

        ));


            }
            Util.IncludeInProject(csFile.Replace(@"..\..\..\BSS_WebAPI\", ""));

        }
    }

}