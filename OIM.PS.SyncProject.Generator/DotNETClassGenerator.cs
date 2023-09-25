using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    public class DotNETClassGenerator
    {
        private string _nameSpace;
        private string _className;
        //private Dictionary<string, string> _parameters;
        //private Dictionary<string, GenClsProp[]> _classes;
        private static Random _ran = new Random();
        private PSSyncMetadata _meta = new PSSyncMetadata();

        public DotNETClassGenerator(PSSyncMetadata metadata)
                                        //Dictionary<string, string> parameters,
                                        //Dictionary<string, GenClsProp[]> classes)
        {
            this._nameSpace = metadata.Namespace;
            this._className = metadata.ClassName;
            _meta = metadata;
            //this._parameters = parameters;
            //this._classes = classes;
        }

        public string GenerateDotNetClass()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTemp = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("");
            sb.AppendLine($"namespace {_nameSpace}");
            sb.AppendLine("{");
            sb.AppendLine("");
            sb.AppendLine($"    public class {_className}");
            sb.AppendLine("    {");
            sb.AppendLine("");

              
            sb.AppendLine($"        private {_className}Implement _impl = null;");

            sb.AppendLine("");

            //P.S. Constructor
            sb.Append($"        public {_className}(");
            sbTemp.Clear();
            //Adding parameters
            foreach (var item in _meta.Parameters) 
            {
                sbTemp.Append($"string {item.ParamName},");
            }
            sb.AppendLine(sbTemp.ToString().TrimEnd(',') + ")");
            sb.AppendLine("        {");
            sb.AppendLine("");

            sb.Append($"            _impl = new {_className}Implement(");
            sbTemp.Clear();
            foreach (var item in _meta.Parameters) 
            {
                sbTemp.Append($"{item.ParamName}, ");
            }
            sb.Append($"{sbTemp.ToString().Trim().TrimEnd(',')});");


            sb.AppendLine("");
            
            sb.AppendLine("        }");
            //P.S. END of constructor ===============================================

            sb.AppendLine("");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Put some code to do necessary cleanup.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        public void Disconnect()");
            sb.AppendLine("        {");
            sb.AppendLine("");

            //TODO - Add code for disconnect;
            sb.Append($"            _impl.Disconnect();");

            sb.AppendLine("        }");
            sb.AppendLine("");

            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"        //======== {item.ClassName.ToUpper()} ============================");

                sb.AppendLine(GenerateMethods(item));

                sb.AppendLine($"        //======== END {item.ClassName.ToUpper()} ========================");
                sb.AppendLine("");
            }

            sb.AppendLine(GenerateToString());

            sb.AppendLine("");
            sb.AppendLine("    }");
            sb.AppendLine("");           

            sb.AppendLine("}");
            return sb.ToString();
        }

        private string GenerateToString()
        {
            StringBuilder sb = new StringBuilder();

            //P.S. Populate To String
            sb.AppendLine("        public static string ObjectToString(object obj)");
            sb.AppendLine("        {");
            sb.AppendLine($"            return {_className}Implement.ObjectToString(obj);"); 
            sb.AppendLine("        }");
            sb.AppendLine("");

            return sb.ToString();
        }

        

        private string GenerateMethods(SyncClass synClass)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder tmpSb = new StringBuilder();
            StringBuilder sbPrim = new StringBuilder();
            StringBuilder sbPrim2 = new StringBuilder();
            string lclClass = $"ob{synClass.ClassName.ToLower()}";
            //string lclId = "";
            string strPrimParams = "";
            string strPrimParamsInd = "";

            List<GenClassProp> props = synClass.Properties; 

            List<GenClassProp> primProps = synClass.Properties.Where(q => q.IsPrimaryKey).ToList();

            
            foreach (var item in primProps)
            {
                sbPrim.Append($"{item.DataTypeClass} {item.PropertyName},");
                sbPrim2.Append($"{item.PropertyName},");
            }

            strPrimParams = sbPrim.ToString().TrimEnd().TrimEnd(',');
            strPrimParamsInd = sbPrim2.ToString().TrimEnd().TrimEnd(',');
            
            
            sb.AppendLine($"        public List<{synClass.ClassName}> {synClass.ClassName}GetAll()");
            sb.AppendLine("        {");
            sb.AppendLine("");
            sb.AppendLine($"            return _impl.{synClass.ClassName}GetAll();");
            sb.AppendLine("");
            sb.AppendLine("        }");
            sb.AppendLine("");



            //Work with multiple Primary Keys
            
            if (primProps.Count == 1)
            {
                sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Get({primProps[0].DataTypeClass} {strPrimParamsInd})");
                sb.AppendLine("        {");
                sb.AppendLine("");
                sb.AppendLine($"            return _impl.{synClass.ClassName}Get({primProps[0].PropertyName});");
                sb.AppendLine("");
                sb.AppendLine("        }");
            }
            else
            {                
                sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Get({strPrimParams})");
                sb.AppendLine("        {");
                sb.AppendLine("");
                sb.AppendLine($"            return _impl.{synClass.ClassName}Get({strPrimParamsInd});");
                sb.AppendLine("");
                sb.AppendLine("        }");
            }
            
            sb.AppendLine("");
                        
            sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Create({synClass.ClassName} {lclClass})");
            sb.AppendLine("        {");
            sb.AppendLine("");
            sb.AppendLine($"            return _impl.{synClass.ClassName}Create({lclClass});");
            sb.AppendLine("        }");
            sb.AppendLine("");
                        
            sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Update({strPrimParams}, System.Collections.Hashtable updates)");
            sb.AppendLine("        {");            
            sb.AppendLine($"            return _impl.{synClass.ClassName}Update({strPrimParamsInd}, updates);");
            sb.AppendLine("        }");
            sb.AppendLine("");
                        
            sb.AppendLine($"        public void {synClass.ClassName}Delete({strPrimParams})");
            sb.AppendLine("        {");
            sb.AppendLine("");
            sb.AppendLine($"            _impl.{synClass.ClassName}Delete({strPrimParamsInd});");
            sb.AppendLine("");
            sb.AppendLine("        }");
            sb.AppendLine("");

            return sb.ToString();
        }

        
    }
}
