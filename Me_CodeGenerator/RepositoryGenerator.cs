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
    public class RepositoryGenerator
    {   
        public string ReposFolder { get; set; }
        public string EntityNamespace { get; set; }
        public string DataAccessAssemblyName { get; set; }
        public string DataModelToExclude { get; set; }

        public void GenerateDTORepositories()
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


                var csFile = ReposFolder + pascalNameOfClass + "Repository.cs";
                using (StreamWriter sw = new StreamWriter(csFile))
                {
                    sw.Write(string.Format(
    "using System.Collections.Generic;                                              \r\n" +
    "using System.Linq;                                                             \r\n" +
    "using BSS_DataAccess;                                                           \r\n" +
    "using Microsoft.Practices.Unity;                                               \r\n" +
    "using AutoMapper;                                                              \r\n" +
    "using BSS_WebAPI.Models.DTO;                                                    \r\n" +
    "                                                                               \r\n" +
    "namespace BSS_WebAPI.Repositories                                               \r\n" +
    "{{                                                                             \r\n" +
    "    public class {0}Repository : BaseDataAccessRepository, IDataAccessRepository<Models.DTO.{0}, int> \r\n" +
    "    {{                                                                         \r\n" +
    "        //[Dependency]                                                           \r\n" +
    "        //public BeSmartSportsEntities Context {{ get; set; }}                        \r\n" +
    "                                                                               \r\n" +
    "        public IEnumerable<Models.DTO.{0}> Get()                                          \r\n" +
    "        {{                                                                     \r\n" +
    "            var entityList = Context.{2};                                      \r\n" +
    "            var dtoList = entityList.Select(Mapper.Map<BSS_DataAccess.{1}, Models.DTO.{0}>).ToList();    \r\n" +
    "            return dtoList;                                                    \r\n" +
    "        }}                                                                     \r\n" +
    "                                                                               \r\n" +
    "        public Models.DTO.{0} Get(int id)                                                 \r\n" +
    "        {{                                                                     \r\n" +
    "            var dbEntity = Context.{2}.Find(id);                               \r\n" +
    "            var dto = Mapper.Map<BSS_DataAccess.{1}, Models.DTO.{0}>(dbEntity);                          \r\n" +
    "            return dto;                                                        \r\n" +
    "        }}                                                                     \r\n" +
    "                                                                               \r\n" +
    "        public void Post(Models.DTO.{0} dto)                                              \r\n" +
    "        {{                                                                     \r\n" +
    "            var entity = Mapper.Map<Models.DTO.{0}, BSS_DataAccess.{1}>(dto);                            \r\n" +
    "            Context.{2}.Add(entity);                                           \r\n" +
    "            Context.SaveChanges();                                             \r\n" +
    "            dto.Id = entity.Id;                                                \r\n" +
    "        }}                                                                     \r\n" +
    "                                                                               \r\n" +
    "        public void Put(int id, Models.DTO.{0} dto)                                       \r\n" +
    "        {{                                                                     \r\n" +
    "            var entity = Mapper.Map<Models.DTO.{0}, BSS_DataAccess.{1}>(dto);                            \r\n" +
    "                                                                               \r\n" +
    "            var dbEntity = Context.{2}.Find(id);                               \r\n" +
    "            if (dbEntity != null)                                              \r\n" +
    "            {{                                                                 \r\n" +
    "                {3}                                                            \r\n" +
    "                                                                               \r\n" +
    "                Context.SaveChanges();                                         \r\n" +
    "            }}                                                                 \r\n" +
    "        }}                                                                     \r\n" +
    "                                                                               \r\n" +
    "        public void Delete(int id)                                             \r\n" +
    "        {{                                                                     \r\n" +
    "            var dbEntity = Context.{2}.Find(id);                               \r\n" +
    "            if (dbEntity != null)                                              \r\n" +
    "            {{                                                                 \r\n" +
    "                Context.{2}.Remove(dbEntity);                                  \r\n" +
    "                Context.SaveChanges();                                         \r\n" +
    "            }}                                                                 \r\n" +
    "        }}                                                                     \r\n" +
    "    }}                                                                         \r\n" +
    "}}                                                                             \r\n"
    , pascalNameOfClass
    , nameOfEntity
    , pluralEntityName
    , updateCopy )
    );


                    





                }

                Util.IncludeInProject(csFile.Replace(@"..\..\..\BSS_WebAPI\", ""));
            }
        }




    }

}
