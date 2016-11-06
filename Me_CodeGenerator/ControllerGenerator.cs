using BSS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSS_CodeGenerator
{
    public class ControllerGenerator
    {
        public string ControllersFolder { get; set; } 
        public string EntityNamespace { get; set; } 
        public string DataAccessAssemblyName { get; set; }
        public string DataModelToExclude { get; set; }

        public void GenerateWebApiControllers()
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

                var lowerSingleEntityName = pascalNameOfClass.ToLower(); //user
                var lowerPluralEntityName = pluralEntityName.ToLower().Replace("_", "-"); //users

                

                //FK Functions handling
                string fkMethods = "";
                List<string> collectionsHandled = new List<string>();
                foreach (var property in theType.GetProperties())
                {
                    string typeName = Util.GetTypeDisplayName(property.PropertyType);

                    if (typeName.StartsWith("ICollection"))
                    {
                        if(collectionsHandled.Contains(typeName)) { continue; }
                        else { collectionsHandled.Add(typeName); }

                        var fkTableName = typeName.Replace("ICollection<", "").Replace(">", "");//USER
                        var pluralFk = fkTableName.Pluralize(); //USERs

                        var pascalFkClass = Util.ToPascalCase(fkTableName); //User

                        var lowerSingleFkName = pascalFkClass.ToLower(); //user
                        var lowerPluralFkName = pluralFk.ToLower().Replace("_", "-"); //users

                        string fkColumnName = null;


                        var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                        var storageMetadata = ((EntityConnection)objectContext.Connection).GetMetadataWorkspace().GetItems(DataSpace.SSpace);
                        var entityProps = (from s in storageMetadata where s.BuiltInTypeKind == BuiltInTypeKind.EntityType select s as EntityType);
                        var personRightStorageMetadata = (from m in entityProps where m.Name == fkTableName select m).Single();
                        foreach (var item in personRightStorageMetadata.Properties)
                        {
                            Console.WriteLine(item.Name);

                            var fkeys = ((EntityConnection)objectContext.Connection).GetMetadataWorkspace().GetItems<AssociationType>(DataSpace.CSpace).Where(a => a.IsForeignKey);
                            //check if the table has any foreign constraints for that column
                            var fknames = fkeys.Where(x => x.ReferentialConstraints[0].ToRole.Name == fkTableName).Where(x => x.ReferentialConstraints[0].ToProperties[0].Name == item.Name);
                            //Get the corresponding reference entity column name
                            foreach (var fkname in fknames)
                            {
                                var refConstraint = fkname.ReferentialConstraints[0];
                                if(refConstraint.FromRole.Name == nameOfEntity)
                                {
                                    fkColumnName = refConstraint.ToProperties[0].Name;

                                    fkMethods += string.Format(
"                                                                                                   \r\n" +
"[HttpGet]                                                                                          \r\n" +
"[Route(\"api/v1/{0}/{{id}}/{1}/filter/{9}\")]                                                       \r\n" +
"public IHttpActionResult Get{4}By{2}_Filter_{6} (int id)                                            \r\n" +
"{{                                                                                                 \r\n" +
"    var list = _repository.GetXByFK<{8}.{3}, {5}>(\"{3}\", \"{7}\", id.ToString());                \r\n" +
"    #if DEBUG                                                                                      \r\n" +
"        list.ToList().ForEach(x => x.SetLinks());                                                  \r\n" +
"    #endif                                                                                         \r\n" +
"    return Json(list);                                                                             \r\n" +
"}} "
, lowerPluralEntityName, lowerPluralFkName, pascalNameOfClass, fkTableName, 
pascalFkClass, pascalFkClass, fkColumnName.ToLower(), fkColumnName, 
DataAccessAssemblyName, fkColumnName.ToLower().Replace("_", "-"));


                                }
                            }
                            }

                        }
 


                    
                }
                    //FK Functions handling





                var csFile = ControllersFolder + pascalNameOfClass + "Controller.cs";
                using (StreamWriter sw = new StreamWriter(csFile))
                {
                    sw.Write(string.Format(
    "using System.Net;                                                                     \r\n" +
    "using System.Web.Http;                                                                \r\n" +
    "using System.Web.Http.Cors;                                                           \r\n" +
    "using BSS_WebAPI.Auth;                                                                 \r\n" +
    "using BSS_WebAPI.Models.DTO;                                                           \r\n" +
    "using System.Linq;                                                                    \r\n" +
    "                                                                                      \r\n" +
    "namespace BSS_WebAPI.Controllers.Auto                                                  \r\n" +
    "{{                                                                                    \r\n" +
    "    [EnableCors(\"*\", \"*\", \"*\")]                                                 \r\n" +
    "    //[CustomAuthorize]                                                               \r\n" +
    "    public partial class {0}Controller : ApiController                                \r\n" +
    "    {{                                                                                \r\n" +
    "        private IDataAccessRepository<{0}, int> _repository;                          \r\n" +
    "        public {0}Controller(IDataAccessRepository<{0}, int> repository)              \r\n" +
    "        {{                                                                            \r\n" +
    "            _repository = repository;                                                 \r\n" +
    "        }}                                                                            \r\n" +
    "                                                                                      \r\n" +
    "        [HttpGet]                                                                     \r\n" +
    "        [Route(\"api/v1/{2}\")]                                                       \r\n" +
    "        public IHttpActionResult Get()                                                \r\n" +
    "        {{                                                                            \r\n" +
    "            var list = _repository.Get();                                             \r\n" +
    "            #if DEBUG                                                                 \r\n" +
    "               list.ToList().ForEach(x => x.SetLinks());                               \r\n" +
    "            #endif                                                                    \r\n" +
    "            return Json(list);                                                         \r\n" +
    "        }}                                                                            \r\n" +
    "                                                                                      \r\n" +
    "        [HttpGet]                                                                     \r\n" +
    "        [Route(\"api/v1/{2}/{{id}}\")]                                                \r\n" +
    "        public IHttpActionResult Get(int id)                                          \r\n" +
    "        {{                                                                            \r\n" +
    "            return Json(_repository.Get(id));                                         \r\n" +
    "        }}                                                                            \r\n" +
    "                                                                                      \r\n" +
    "        [HttpPost]                                                                    \r\n" +
    "        [Route(\"api/v1/{2}/create/\")]                                               \r\n" +
    "        public IHttpActionResult Post({0} {1})                                        \r\n" +
    "        {{                                                                            \r\n" +
    "            _repository.Post({1});                                                    \r\n" +
    "            return Json({1});                                                         \r\n" +
    "        }}                                                                            \r\n" +
    "                                                                                      \r\n" +
    "        [HttpPost]                                                                    \r\n" +
    "        [Route(\"api/v1/{2}/update/\")]                                               \r\n" +
    "        public IHttpActionResult Put({0} {1})                                         \r\n" +
    "        {{                                                                            \r\n" +
    "            _repository.Put({1}.Id, {1});                                             \r\n" +
    "            return Json(\"OK\");                                                      \r\n" +
    "        }}                                                                            \r\n" +
    "                                                                                      \r\n" +
    "        [HttpPost]                                                                    \r\n" +
    "        [Route(\"api/v1/{2}/delete/\")]                                               \r\n" +
    "        public IHttpActionResult Delete(int id)                                       \r\n" +
    "        {{                                                                            \r\n" +
    "            _repository.Delete(id);                                                   \r\n" +
    "            return Json(HttpStatusCode.NoContent);                                    \r\n" +
    "        }}                                                                            \r\n" +
    "                                                                                      \r\n" +
    "        {3}                                                                           \r\n" +
    "                                                                                      \r\n" +
    "    }}                                                                                \r\n" +
    "}}                                                                                    \r\n" 

        , pascalNameOfClass
        , lowerSingleEntityName
        , lowerPluralEntityName
        , fkMethods
        )
    );


                }

                Util.IncludeInProject(csFile.Replace(@"..\..\..\BSS_WebAPI\",""));

            }
        }
         

    }

}
