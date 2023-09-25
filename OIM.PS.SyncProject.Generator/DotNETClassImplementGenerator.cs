using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    public class DotNETClassImplementGenerator
    {
        private string _nameSpace;
        private string _classNameImplement;        
        private static Random _ran = new Random();
        private PSSyncMetadata _meta = new PSSyncMetadata();

        public DotNETClassImplementGenerator(PSSyncMetadata metadata)                                        
        {
            this._nameSpace = metadata.Namespace;
            this._classNameImplement = metadata.ClassName + "Implement";
            _meta = metadata;            
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
            sb.AppendLine($"    public class {_classNameImplement}");
            sb.AppendLine("    {");
            sb.AppendLine("");

            //P.S. Declaring class variables
            sb.AppendLine("        //Connection values");
            foreach (var item in _meta.Parameters) 
            {
                sb.AppendLine($"        private string _{item.ParamName.ToLower()} = null;");
            }

            sb.AppendLine("");
            sb.AppendLine("        //TEST Values");
            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"        private List<{item.ClassName}> _{item.ClassName.ToLower()}s = null;");
            }

            sb.AppendLine("");
            sb.AppendLine("");

            //P.S. Constructor
            sb.Append($"        public {_classNameImplement}(");
            sbTemp.Clear();
            //Adding parameters
            foreach (var item in _meta.Parameters) 
            {
                sbTemp.Append($"string {item.ParamName},");
            }
            sb.AppendLine(sbTemp.ToString().TrimEnd(',') + ")");
            sb.AppendLine("        {");
            sb.AppendLine("");

            foreach (var item in _meta.Parameters) 
            {
                sb.AppendLine($"            _{item.ParamName.ToLower()} = {item.ParamName};");
            }
            sb.AppendLine("");
            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"            Populate{item.ClassName}s();");
            }

            sb.AppendLine("        }");
            //P.S. END of constructor ===============================================

            sb.AppendLine("");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Put some code to do necessary cleanup.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        public void Disconnect()");
            sb.AppendLine("        {");
            sb.AppendLine("");

            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"            if(_{item.ClassName.ToLower()}s != null)");
                sb.AppendLine("            {");
                sb.AppendLine($"                _{item.ClassName.ToLower()}s.Clear();");
                sb.AppendLine($"                _{item.ClassName.ToLower()}s = null;");
                sb.AppendLine("            }");
                sb.AppendLine("");
            }

            sb.AppendLine("        }");
            sb.AppendLine("");

            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"        //======== {item.ClassName.ToUpper()} ============================");

                sb.AppendLine(GenerateMethods(item));

                sb.AppendLine($"        //======== END {item.ClassName.ToUpper()} ============================");
                sb.AppendLine("");
            }

            sb.AppendLine(GenerateToString());

            sb.AppendLine("");
            sb.AppendLine("    }");
            sb.AppendLine("");

            sb.AppendLine("    //P.S. Generated Classes");

            sb.AppendLine(GenerateClasses());

            sb.AppendLine("}");
            return sb.ToString();
        }

        private string GenerateToString()
        {
            StringBuilder sb = new StringBuilder();

            //P.S. Populate To String
            sb.AppendLine("        public static string ObjectToString(object obj)");
            sb.AppendLine("        {");
            sb.AppendLine("            StringBuilder sb = new StringBuilder();");
            sb.AppendLine("");
            sb.AppendLine("            var bindingFlags = System.Reflection.BindingFlags.Instance |");
            sb.AppendLine("                                    System.Reflection.BindingFlags.NonPublic |");
            sb.AppendLine("                                    System.Reflection.BindingFlags.Public;");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("            List <KeyValuePair<string, string>> listValues = obj.GetType()");
            sb.AppendLine("                                    .GetFields(bindingFlags)");
            sb.AppendLine("                                    .Where(value => value != null)");
            sb.AppendLine("                                    .Select(field => new KeyValuePair<string, string>(field.Name, field.GetValue(obj).ToString()))");
            sb.AppendLine("                                    .ToList();");

            sb.AppendLine("            sb.AppendLine(obj.GetType().Name + \":\");");

            sb.AppendLine("            foreach (var item in listValues)");
            sb.AppendLine("            {");
            sb.AppendLine("                // Note that you need to cast to string on objects that don't support ToSting() native! Maybe a new method to cast.");
            sb.AppendLine("                sb.Append(item.Key);");
            sb.AppendLine("                sb.Append(\":\");");
            sb.AppendLine("                sb.AppendLine(item.Value);");
            sb.AppendLine("            }");
            sb.AppendLine("            return sb.ToString();");
            sb.AppendLine("        }");
            sb.AppendLine("");

            return sb.ToString();
        }

        private string GenerateClasses()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"    public class {item.ClassName}");
                sb.AppendLine("    {");

                foreach (var prop in item.Properties) 
                {
                    if (prop.IsMultivalue)
                    {
                        sb.AppendLine($"       public {prop.DataTypeClass}[] {prop.PropertyName};");
                    }
                    else
                    {
                        sb.AppendLine($"       public {prop.DataTypeClass} {prop.PropertyName};");
                    }
                }

                sb.AppendLine("    }");
                sb.AppendLine("");
            }

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
            

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public List<{synClass.ClassName}> {synClass.ClassName}GetAll()");
            sb.AppendLine("        {");
            sb.AppendLine("");
            sb.AppendLine($"            return _{synClass.ClassName.ToLower()}s;");
            sb.AppendLine("");
            sb.AppendLine("        }");
            sb.AppendLine("");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// <summary>");

            //Work with multiple Primary Keys

            if (primProps.Count == 1)
            {
                sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Get({primProps[0].DataTypeClass} {strPrimParamsInd})");
                sb.AppendLine("        {");
                sb.AppendLine("");
                sb.AppendLine($"            return Get{synClass.ClassName}ById({primProps[0].PropertyName});");
                sb.AppendLine("");
                sb.AppendLine("        }");
            }
            else
            {                
                sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Get({strPrimParams})");
                sb.AppendLine("        {");
                sb.AppendLine("");
                sb.AppendLine($"            return Get{synClass.ClassName}ById({strPrimParamsInd});");
                sb.AppendLine("");
                sb.AppendLine("        }");
            }
            
            sb.AppendLine("");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Create({synClass.ClassName} {lclClass})");
            sb.AppendLine("        {");
            sb.AppendLine("");


            if (primProps.Count == 1)
            {
               
                if (primProps[0].DataType == DataTypes.Int)
                {
                    sb.AppendLine($"            if ({lclClass}.{strPrimParamsInd} == 0)");
                    sb.AppendLine("            {");
                    var val = (new Random()).Next();
                    sb.AppendLine($"                {lclClass}.{strPrimParamsInd} = {val};"); 
                }
                else if (primProps[0].DataType == DataTypes.String)
                {
                    sb.AppendLine($"            if (string.IsNullOrEmpty({lclClass}.{strPrimParamsInd}))");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                {lclClass}.{strPrimParamsInd} = Guid.NewGuid().ToString();");
                }
                else
                {
                    throw new Exception($"Data Type {primProps[0].DataType} is not valid for primary key");
                }
                
                
                sb.AppendLine("            }");
            }
            else
            {
                sbPrim.Clear();
                sbPrim2.Clear();

                foreach (var item in primProps)
                {
                    if (item.DataType == DataTypes.Int)
                    {
                        sb.AppendLine($"            if ({lclClass}.{item.PropertyName} == 0)");
                        sb.AppendLine("            {");
                        var val = (new Random()).Next();
                        sb.AppendLine($"                {lclClass}.{item.PropertyName} = {val};");
                    }
                    else if (item.DataType == DataTypes.String)
                    {
                        sb.AppendLine($"            if (string.IsNullOrEmpty({lclClass}.{item.PropertyName}))");
                        sb.AppendLine("            {");
                        sb.AppendLine($"                {lclClass}.{item.PropertyName} = Guid.NewGuid().ToString();");
                    }
                    else
                    {
                        throw new Exception($"Data Type {primProps[0].DataType} is not valid for primary key");
                    }
                    
                    sb.AppendLine("            }");
                    sb.AppendLine("");
                }
            }

            sb.AppendLine("");
            sb.AppendLine($"            _{synClass.ClassName.ToLower()}s.Add({lclClass});");
            sb.AppendLine($"            return {lclClass};");
            sb.AppendLine("        }");
            sb.AppendLine("");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// Hashtable holds a list of modified fields and their new values");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Update({strPrimParams}, System.Collections.Hashtable updates)");
            sb.AppendLine("        {");
            sb.AppendLine($"            var {lclClass} = Get{synClass.ClassName}ById({strPrimParamsInd});");
            sb.AppendLine("");
            sb.AppendLine($"            if ({lclClass} == null)");
            sb.AppendLine("            {");
            sb.AppendLine("                throw new Exception($\"Unable to find " + synClass + " by IDs \");");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine($"            Update{synClass.ClassName}({lclClass}, updates);");
            sb.AppendLine($"            return {lclClass};");
            sb.AppendLine("        }");
            sb.AppendLine("");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public void {synClass.ClassName}Delete({strPrimParams})");
            sb.AppendLine("        {");
            sb.AppendLine("");
            sb.AppendLine($"            var {lclClass} = Get{synClass.ClassName}ById({strPrimParamsInd});");
            sb.AppendLine("");
            sb.AppendLine($"            if ({lclClass} != null)");
            sb.AppendLine("            {");
            sb.AppendLine($"                _{synClass.ClassName.ToLower()}s.Remove({lclClass});");
            sb.AppendLine("            }");
            sb.AppendLine("");
            sb.AppendLine("        }");
            sb.AppendLine("");


            sb.AppendLine("        //P.S. Local methods =========================");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// Hashtable holds a list of modified fields and their new values");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        private void Update{synClass.ClassName}({synClass.ClassName} {lclClass}, System.Collections.Hashtable updates)");
            sb.AppendLine("        {");

            props = synClass.Properties; 

            int counter = 0;
            foreach (var item in props) 
            {
                if (item.IsAutoFill || item.IsPrimaryKey)
                {
                    continue;
                }

                string part = "";
                if (item.DataType == DataTypes.Bool)
                {
                    part = $"{lclClass}.{item.PropertyName} = bool.Parse(updates[\"{item.PropertyName}\"].ToString());";
                }
                else if (item.DataType == DataTypes.DateTime)
                {
                    part = $"{lclClass}.{item.PropertyName} = DateTime.Parse(updates[\"{item.PropertyName}\"].ToString());";
                }
                else if (item.DataType == DataTypes.Int)
                {
                    part = $"{lclClass}.{item.PropertyName} = int.Parse(updates[\"{item.PropertyName}\"].ToString());";
                }
                else
                {
                    if (item.IsMultivalue)
                    {
                        part = $"{lclClass}.{item.PropertyName} = (string[])updates[\"{item.PropertyName}\"];";
                    }
                    else
                    {
                        part = $"{lclClass}.{item.PropertyName} = updates[\"{item.PropertyName}\"].ToString();";
                    }
                }

                sb.AppendLine($"            if (updates.ContainsKey(\"{item.PropertyName}\")) {{ {part} }};");

                counter++;
            }

            if(counter == 0)
            {
                sb.AppendLine("            //There are no non-primary fields to update in this class.");                
            }


            sb.AppendLine("        }");
            sb.AppendLine("");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// Put real implementation here");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        private {synClass.ClassName} Get{synClass.ClassName}ById({strPrimParams})");
            sb.AppendLine("        {");
            sb.AppendLine("");

            
            tmpSb.Clear();
            foreach (var prop in primProps)
            {
                if (prop.DataType == DataTypes.String)
                {
                    tmpSb.AppendLine($" q.{prop.PropertyName}.Equals({prop.PropertyName}, StringComparison.OrdinalIgnoreCase) && ");
                }
                else if(prop.DataType == DataTypes.Int)
                {
                    tmpSb.AppendLine($" q.{prop.PropertyName} == {prop.PropertyName} && ");
                }
            }
            var tmpStr = tmpSb.ToString().TrimEnd().TrimEnd('&').TrimEnd('&');

            sb.AppendLine($"            return _{synClass.ClassName.ToLower()}s.Where(q => {tmpStr}).FirstOrDefault();");


            
            sb.AppendLine("");
            sb.AppendLine("        }");

            //P.S. populate values
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only!");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        private void Populate{synClass.ClassName}s()");
            sb.AppendLine("        {");
            sb.AppendLine("");
            sb.AppendLine($"           _{synClass.ClassName.ToLower()}s = new List<{synClass.ClassName}>()");
            sb.AppendLine("            {");

            for (int i = 0; i < 5; i++)
            {
                sb.AppendLine($"                new {synClass.ClassName}()");
                sb.AppendLine("                {");

                tmpSb.Clear();
                tmpSb.AppendLine("");

                props = _meta.GetClassByName(synClass.ClassName).Properties;

                foreach (var item in props) //_classes[className])                    
                {
                    if (item.IsPrimaryKey)
                    {
                        if (item.DataType == DataTypes.String)
                        {
                            tmpSb.AppendLine($"                    {item.PropertyName} = Guid.NewGuid().ToString(),");
                        }
                        else if (item.DataType == DataTypes.Int)
                        {
                            tmpSb.AppendLine($"                    {item.PropertyName} = {i},");
                        }
                    }
                    else
                    {

                        if (item.DataType == DataTypes.String)
                        {
                            if (item.IsMultivalue)
                            {
                                tmpSb.AppendLine($"                    {item.PropertyName} = new List<string>{{\"Test{item.PropertyName + i.ToString()}\"}}.ToArray(),"); //  \"Test{item.PropertyName}{i.ToString()}\",");
                            }
                            else
                            {
                                tmpSb.AppendLine($"                    {item.PropertyName} = \"Test{item.PropertyName}{i.ToString()}\",");
                            }
                        }
                        else if (item.DataType == DataTypes.Int)
                        {
                            tmpSb.AppendLine($"                    {item.PropertyName} = {i},");
                        }
                        else if (item.DataType == DataTypes.Bool)
                        {
                            tmpSb.AppendLine($"                    {item.PropertyName} = false,");
                        }
                        else if (item.DataType == DataTypes.DateTime)
                        {
                            tmpSb.AppendLine($"                    {item.PropertyName} = " +
                             $"DateTime.Parse(\"{ DateTime.MinValue.Add(TimeSpan.FromTicks((long)(_ran.NextDouble() * DateTime.MaxValue.Ticks)))}\"),");
                        }
                    }
                }
                //P.S. Remove final ','
                sb.AppendLine(tmpSb.ToString().TrimEnd().TrimEnd(','));
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("                },");
            }

            sb.AppendLine("");
            sb.AppendLine("            };");
            sb.AppendLine("        }");

            return sb.ToString();
        }

        
    }
}
