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
    public class EntityToDtoGenerator
    {
        public string DtoFolder { get; set; }
        public string EntityNamespace { get; set;  }
        public string DataAccessAssemblyName { get; set; }
        public string DataModelToExclude { get; set; }
        public string UrlBase { get; set; }
        
        public void GenerateDTOs()
        {
           // this call is needed  to initialize the assembly
            var context = new BeSmartSportsEntities();

            var theAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().ToString().StartsWith(string.Format("{0}, Version", DataAccessAssemblyName) ))
                .First();

            var theTypes = theAssembly.GetTypes()
                      .Where(t => t.Namespace == EntityNamespace)
                      .ToList();

            foreach (var theType in theTypes)
            {
                var nameOfEntity = theType.Name;
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


                var csFile = DtoFolder + Util.ToPascalCase(nameOfEntity) + ".cs";
                using (StreamWriter sw = new StreamWriter(csFile))
                {
                    sw.Write(string.Format(
    @"using System;
namespace BSS_WebAPI.Models.DTO
{{
    public  class {0} : BaseDTO
    {{ ", pascalNameOfClass ));



                    //FK Functions handling
                    List<string> collectionsHandled = new List<string>();
                    string fkLinks = "";
                    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                    var storageMetadata = ((EntityConnection)objectContext.Connection).GetMetadataWorkspace().GetItems(DataSpace.SSpace);
                    var entityProps = (from s in storageMetadata where s.BuiltInTypeKind == BuiltInTypeKind.EntityType select s as EntityType);
                    foreach (var property in theType.GetProperties())
                    {
                        string typeName = Util.GetTypeDisplayName(property.PropertyType);

                        if (typeName.StartsWith("ICollection"))
                        {
                            if (collectionsHandled.Contains(typeName)) { continue; }
                            else { collectionsHandled.Add(typeName); }

                            var fkTableName = typeName.Replace("ICollection<", "").Replace(">", "");//USER
                            var pluralFk = fkTableName.Pluralize(); //USERs

                            var pascalFkClass = Util.ToPascalCase(fkTableName); //User

                            var lowerSingleFkName = pascalFkClass.ToLower(); //user
                            var lowerPluralFkName = pluralFk.ToLower().Replace("_", "-"); //users

                            string fkColumnName = null;


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
                                    if (refConstraint.FromRole.Name == nameOfEntity)
                                    {
                                        fkColumnName = refConstraint.ToProperties[0].Name;

                                        fkLinks += string.Format(@" 
                                    Links.Add(""{2}:{3}"",  string.Format(@""{0}/{1}/{{0}}/{2}/filter/{3}"" ,Id)); ",
                                    UrlBase, lowerPluralEntityName, lowerPluralFkName, fkColumnName.ToLower().Replace("_", "-"));

                                    }
                                }
                            }
                            continue;
                        }


                        //if (typeName == typeName.ToUpper()) //FKs
                        //    continue;

                        var propMetadata = (from m in entityProps where m.Name == nameOfEntity select m).Single();
                        foreach (var item in propMetadata.Properties)
                        {
                            Console.WriteLine(item.Name);
                            if (property.Name == item.Name) // purpose: exclude FKs
                            {
                                sw.Write(string.Format(@" 
        public {0} {1}  {{ get; set; }}", typeName,
                                            Util.ToPascalCase(property.Name)));
                                continue;
                            }
                        }

                    }

                    sw.Write(string.Format(@"
        public void SetLinks()
        {{ 
             Links.Add(""self"", string.Format(@""http://localhost:63330/api/v1/{0}/{{0}}"", Id));
             {1}   
        }}", lowerPluralEntityName, fkLinks));

            sw.Write(@"
    }
}");
                }

                Util.IncludeInProject(csFile.Replace(@"..\..\..\BSS_WebAPI\", ""));
            }
        }


    }
}
