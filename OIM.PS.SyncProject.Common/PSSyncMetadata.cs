using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Common
{
    public class PSSyncMetadata
    {

        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public List<Param> Parameters { get; set; }
        public List<SyncClass> SyncClasses { get; set; }

        public SyncClass GetClassByName(string ClassName)
        {
            return SyncClasses.Where(q => q.ClassName.Equals(ClassName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();            
        }
    }
}
