using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSS_CodeGenerator
{
    public class TargetProject
    {

        private static TargetProject _instance = null;

        public Project project = null;

        private TargetProject()
        {
          
                project = new Project(
                      CsProjectPath,
                      null,
                      null,
                      ProjectCollection.GlobalProjectCollection,
                      ProjectLoadSettings.Default);
           

        }

        public static string CsProjectPath {get; set; }
        public static TargetProject Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TargetProject();
                }
                return _instance;
            }
        }
    }
}
