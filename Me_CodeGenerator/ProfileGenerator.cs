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
    public class ProfileGenerator
    {
        public string ProfileFolder { get; set; }
        public string EntityNamespace { get; set; }
        public string DataAccessAssemblyName { get; set; }
        public string DataModelToExclude { get; set; }

        public void GenerateMapperProfiles()
        {
            // this call is needed  to initialize the assembly
            var context = new BeSmartSportsEntities();

            var theAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().ToString().StartsWith(string.Format("{0}, Version", DataAccessAssemblyName)))
                .First();

            var theTypes = theAssembly.GetTypes()
                      .Where(t => t.Namespace == EntityNamespace)
                      .ToList();

            foreach (var theType in theTypes)
            {
                var nameOfEntity = theType.Name; //USER
                if (nameOfEntity.ToLower() == "dbversion" || nameOfEntity.ToLower() == "sysdiagram")
                    continue;
                if (nameOfEntity.ToLower() == DataModelToExclude.ToLower())
                    continue;
                if (nameOfEntity.ToLower().EndsWith("_result")) //SPs
                    continue;

                var pluralEntityName = nameOfEntity.Pluralize(); //USERs

                var pascalNameOfClass = Util.ToPascalCase(nameOfEntity); //User

                string updateCopy = null;
                foreach (var property in theType.GetProperties())
                {
                    string typeName = Util.GetTypeDisplayName(property.PropertyType);

                    if (typeName.StartsWith("ICollection"))
                        continue;
                    if (typeName == typeName.ToUpper())
                        continue;

                    if (property.Name == "ID")
                        continue;

                    updateCopy += string.Format(@"dbEntity.{0} = entity.{0}; ", property.Name);
                }



                var csFile = ProfileFolder + pascalNameOfClass + "MapperProfile.cs";
                using (StreamWriter sw = new StreamWriter(csFile))
                {
                    sw.Write(string.Format(
    "using AutoMapper;                                                                          \r\n" +
    "                                                                                           \r\n" +
    "namespace BSS_WebAPI.EntityAutoMapper                                                       \r\n" +
    "{{                                                                                          \r\n" +
    "    class {0}MapperProfile : Profile                                                       \r\n" +
    "    {{                                                                                      \r\n" +
    "        protected override void Configure()                                                \r\n" +
    "        {{                                                                                  \r\n" +
    "            // Forward Mapping                                                             \r\n" +
    "            SourceMemberNamingConvention = new PascalCaseNamingConvention();               \r\n" +
    "            DestinationMemberNamingConvention = new MyUpperUnderscoreNamingConvention();   \r\n" +
    "            CreateMap<Models.DTO.{0}, BSS_DataAccess.{1}>();                                                         \r\n" +
    "                                                                                           \r\n" +
    "            // Reverse Mapping                                                             \r\n" +
    "            SourceMemberNamingConvention = new MyUpperUnderscoreNamingConvention();        \r\n" +
    "            DestinationMemberNamingConvention = new PascalCaseNamingConvention();          \r\n" +
    "            CreateMap<BSS_DataAccess.{1}, Models.DTO.{0}>();                                                         \r\n" +
    "                                                                                           \r\n" +
    "            // Mapper.AssertConfigurationIsValid();                                        \r\n" +
    "        }}                                                                                  \r\n" +
    "    }}                                                                                      \r\n" +
    "}}                                                                                          \r\n" 


    , pascalNameOfClass
    , nameOfEntity
    ) );




                }

                Util.IncludeInProject(csFile.Replace(@"..\..\..\BSS_WebAPI\", ""));
            }
        }




    }

}
