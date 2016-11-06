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
    public class CommandGenerator
    {
        public string EntityNamespace { get; set; }
        public string DataAccessAssemblyName { get; set; }
        public string DataModelToExclude { get; set; }

        public void GenerateCommands()
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


                Console.WriteLine(string.Format(@"http://localhost:63820/api/v1/{0}", lowerPluralEntityName));
                Console.WriteLine(string.Format(@"http://localhost:63820/api/v1/{0}/1", lowerPluralEntityName));
                Console.WriteLine("");
            }
        }
    }

}