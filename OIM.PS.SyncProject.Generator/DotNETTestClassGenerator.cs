using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    public class DotNETTestClassGenerator
    {
        private string _nameSpace;
        private string _className;
        private string _origClassName;
        
        private PSSyncMetadata _meta = new PSSyncMetadata();

        public DotNETTestClassGenerator(PSSyncMetadata metadata)
        {
            this._nameSpace = metadata.Namespace + "_TEST";
            this._className = metadata.ClassName + "Program";
            this._origClassName = metadata.ClassName;

            _meta = metadata;            
        }

        public string GenerateDotNetClass()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTemp = new StringBuilder();
            string className = "";
            string strIdParam = "";
            string strDisplayParam = "";

            while (true)
            {
                var testClass = _meta.SyncClasses.Where(q => q.Properties.Exists(z => z.IsPrimaryKey == true)).FirstOrDefault();
                className = testClass.ClassName;
                var idParam = testClass.Properties.Where(q => q.IsPrimaryKey).FirstOrDefault();
                if(idParam == null)
                {
                    continue;
                }

                strIdParam = idParam.PropertyName;

                var displayParam = testClass.Properties.Where(q => q.IsDisplay).FirstOrDefault();
                if (displayParam == null)
                {
                    displayParam = testClass.Properties.Where(q => q.IsPrimaryKey == false && q.DataType == DataTypes.String).FirstOrDefault();
                    if(displayParam == null)
                    {
                        continue;
                    }
                }

                strDisplayParam = displayParam.PropertyName;
                break;
            }

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine($"using {_meta.Namespace};");
            sb.AppendLine("");

            sb.AppendLine($"namespace {_nameSpace}");
            sb.AppendLine("{");
            sb.AppendLine("");

            sb.AppendLine($"    public class {_className}");
            sb.AppendLine("    {");
            sb.AppendLine("");

            sb.AppendLine("        static void Main(string[] args)");
            sb.AppendLine("        {");

            ////P.S. Declaring class variables
            sb.AppendLine("            //Connection values");
            foreach (var item in _meta.Parameters) //_parameters.Keys)
            {
                sb.AppendLine($"            string {item.ParamName} = \"test{item.ParamName.ToLower()}\";");
                sbTemp.Append(item.ParamName + ", ");
            }

            sb.AppendLine("");
            sb.Append($"            {_origClassName} conn = new {_origClassName}({sbTemp.ToString().Trim().TrimEnd(',')}");
            sb.Append(");");

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine($"            //Get all {className}s");
            sb.AppendLine($"            var obj{className}s = conn.{className}GetAll();");
            sb.AppendLine("");
            sb.AppendLine($"            //Get one {className}");
            sb.AppendLine($"            var obj{className} = conn.{className}Get(obj{className}s[0].{strIdParam});");
            sb.AppendLine("");
            sb.AppendLine($"            Console.WriteLine({_origClassName}.ObjectToString(obj{className}));");
            sb.AppendLine("");
            sb.AppendLine($"            //Update {className}");
            sb.AppendLine($"            var hash = new System.Collections.Hashtable();");
            sb.AppendLine($"            hash.Add(\"{strDisplayParam}\", obj{className}.{strDisplayParam} + \"-Test\");");
            sb.AppendLine("");
            sb.AppendLine($"            obj{className} = conn.{className}Update(obj{className}.{strIdParam}, hash);");
            sb.AppendLine("");
            sb.AppendLine($"            Console.WriteLine({_origClassName}.ObjectToString(obj{className}));");
            sb.AppendLine("");
            sb.AppendLine($"            Console.ReadLine();");
            sb.AppendLine("");
            sb.AppendLine("        }");
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
            sb.AppendLine($"            return {_className}.ObjectToString(obj);"); 
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
