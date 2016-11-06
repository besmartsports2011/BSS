using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSS_CodeGenerator
{
    public static  class Util
    {

        public static string ToPascalCase(string str)
        {
            var myString = str.ToLower().Replace("_", " ");
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;
            myString = info.ToTitleCase(myString).Replace(" ", string.Empty);
            return myString;
        }

        public static string GetTypeDisplayName(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                return string.Format("{0}?", GetTypeDisplayName(t.GetGenericArguments()[0]));
            if (t.IsGenericType)
                return string.Format("{0}<{1}>",
                                     t.Name.Remove(t.Name.IndexOf('`')),
                                     string.Join(",", t.GetGenericArguments().Select(at => GetTypeDisplayName(at))));
            if (t.IsArray)
                return string.Format("{0}[{1}]",
                                     GetTypeDisplayName(t.GetElementType()),
                                     new string(',', t.GetArrayRank() - 1));
            return t.Name;
        }

        public static void IncludeInProject(string csFile)
        {
            
            Project project = TargetProject.Instance.project;

            var exists = project.GetItems("Compile").FirstOrDefault(x => x.EvaluatedInclude.ToLower() == csFile.ToLower()) != null;
            if (!exists)
            {
                project.AddItem("Compile", csFile);
                project.Save();
            }
        }



    }
}
