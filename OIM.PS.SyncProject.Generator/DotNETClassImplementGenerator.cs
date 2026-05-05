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
            sb.AppendLine("using System.IO;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine("using System.Text.Json.Serialization;");
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
            sb.AppendLine("        //JSON file paths");
            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"        private string _{item.ClassName.ToLower()}FilePath = null;");
            }
            sb.AppendLine("");
            sb.AppendLine("        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions");
            sb.AppendLine("        {");
            sb.AppendLine("            WriteIndented = true,");
            sb.AppendLine("            PropertyNameCaseInsensitive = true,");
            sb.AppendLine("            IncludeFields = true");
            sb.AppendLine("        };");

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
            sb.AppendLine(sbTemp.ToString() + "string testFolder = null)");
            sb.AppendLine("        {");
            sb.AppendLine("");

            foreach (var item in _meta.Parameters) 
            {
                sb.AppendLine($"            _{item.ParamName.ToLower()} = {item.ParamName};");
            }
            sb.AppendLine("");
            sb.AppendLine("            // Set JSON file paths - use testFolder if provided, otherwise use executable directory");
            sb.AppendLine("            string _jsonDir = !string.IsNullOrEmpty(testFolder) ? testFolder : AppDomain.CurrentDomain.BaseDirectory;");
            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"            _{item.ClassName.ToLower()}FilePath = Path.Combine(_jsonDir, \"{item.ClassName.ToLower()}.json\");");
            }

            sb.AppendLine("        }");
            //P.S. END of constructor ===============================================

            sb.AppendLine("");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Put some code to do necessary cleanup.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        public void Disconnect()");
            sb.AppendLine("        {");
            sb.AppendLine("            // Nothing to clean up when using file-based storage");
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
            sb.AppendLine("            return JsonSerializer.Serialize(obj, _jsonOptions);");
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
            string strPrimParams = "";
            string strPrimParamsInd = "";

            List<GenClassProp> props = synClass.Properties; 

            List<GenClassProp> primProps = synClass.Properties.Where(q => q.IsPrimaryKey || q.IsCombinedPrimaryKey).ToList();


            foreach (var item in primProps)
            {
                sbPrim.Append($"{item.DataTypeClass} {item.PropertyName},");
                sbPrim2.Append($"{item.PropertyName},");
            }

            strPrimParams = sbPrim.ToString().TrimEnd().TrimEnd(',');
            strPrimParamsInd = sbPrim2.ToString().TrimEnd().TrimEnd(',');


            // ReadAll helper
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Reads all records from JSON file.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        private List<{synClass.ClassName}> ReadAll{synClass.ClassName}s()");
            sb.AppendLine("        {");
            sb.AppendLine($"            if (File.Exists(_{synClass.ClassName.ToLower()}FilePath))");
            sb.AppendLine("            {");
            sb.AppendLine($"                string json = File.ReadAllText(_{synClass.ClassName.ToLower()}FilePath);");
            sb.AppendLine($"                return JsonSerializer.Deserialize<List<{synClass.ClassName}>>(json, _jsonOptions) ?? new List<{synClass.ClassName}>();");
            sb.AppendLine("            }");
            sb.AppendLine($"            return new List<{synClass.ClassName}>();");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // SaveAll helper
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Saves all records to JSON file.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        private void SaveAll{synClass.ClassName}s(List<{synClass.ClassName}> items)");
            sb.AppendLine("        {");
            sb.AppendLine($"            string json = JsonSerializer.Serialize(items, _jsonOptions);");
            sb.AppendLine($"            File.WriteAllText(_{synClass.ClassName.ToLower()}FilePath, json);");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // GetAll
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only! Put real implementation here.");
            sb.AppendLine("        /// Reads all records from JSON file on every call.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public List<{synClass.ClassName}> {synClass.ClassName}GetAll()");
            sb.AppendLine("        {");
            sb.AppendLine($"            return ReadAll{synClass.ClassName}s();");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // Get by ID
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only! Put real implementation here.");
            sb.AppendLine("        /// <summary>");

            // Build the LINQ Where clause for finding by primary key(s)
            tmpSb.Clear();
            foreach (var prop in primProps)
            {
                if (prop.DataType == DataTypes.String)
                {
                    tmpSb.Append($"q.{prop.PropertyName}.Equals({prop.PropertyName}, StringComparison.OrdinalIgnoreCase) && ");
                }
                else if (prop.DataType == DataTypes.Int)
                {
                    tmpSb.Append($"q.{prop.PropertyName} == {prop.PropertyName} && ");
                }
            }
            var whereClause = tmpSb.ToString().TrimEnd().TrimEnd('&').TrimEnd('&').TrimEnd();

            if (primProps.Count == 1)
            {
                sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Get({primProps[0].DataTypeClass} {strPrimParamsInd})");
            }
            else
            {
                sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Get({strPrimParams})");
            }
            sb.AppendLine("        {");
            sb.AppendLine($"            var items = ReadAll{synClass.ClassName}s();");
            sb.AppendLine($"            return items.Where(q => {whereClause}).FirstOrDefault();");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // Create
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only! Put real implementation here.");
            sb.AppendLine("        /// Reads all from file, adds new record, saves back to file.");
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
            sb.AppendLine($"            var items = ReadAll{synClass.ClassName}s();");
            sb.AppendLine($"            items.Add({lclClass});");
            sb.AppendLine($"            SaveAll{synClass.ClassName}s(items);");
            sb.AppendLine($"            return {lclClass};");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // Update
            string updateSig = string.IsNullOrEmpty(strPrimParams)
                ? "System.Collections.Hashtable updates"
                : $"{strPrimParams}, System.Collections.Hashtable updates";
            string updateCall = string.IsNullOrEmpty(strPrimParamsInd)
                ? "updates"
                : $"{strPrimParamsInd}, updates";

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only! Put real implementation here.");
            sb.AppendLine("        /// Reads all from file, updates the matching record, saves back to file.");
            sb.AppendLine("        /// Hashtable holds a list of modified fields and their new values.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public {synClass.ClassName} {synClass.ClassName}Update({updateSig})");
            sb.AppendLine("        {");
            sb.AppendLine($"            var items = ReadAll{synClass.ClassName}s();");
            sb.AppendLine($"            var {lclClass} = items.Where(q => {whereClause}).FirstOrDefault();");
            sb.AppendLine("");
            sb.AppendLine($"            if ({lclClass} == null)");
            sb.AppendLine("            {");
            sb.AppendLine("                throw new Exception($\"Unable to find " + synClass.ClassName + " by IDs \");");
            sb.AppendLine("            }");
            sb.AppendLine("");

            // Apply updates
            props = synClass.Properties;
            int counter = 0;
            foreach (var item in props)
            {
                if (item.IsAutoFill || item.IsPrimaryKey || item.IsCombinedPrimaryKey || item.IncludeInCombinedPrimaryKey)
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

            if (counter == 0)
            {
                sb.AppendLine("            //There are no non-primary fields to update in this class.");
            }

            sb.AppendLine("");
            sb.AppendLine($"            SaveAll{synClass.ClassName}s(items);");
            sb.AppendLine($"            return {lclClass};");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // Delete
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Demo only! Put real implementation here.");
            sb.AppendLine("        /// Reads all from file, removes the matching record, saves back to file.");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        public void {synClass.ClassName}Delete({strPrimParams})");
            sb.AppendLine("        {");
            sb.AppendLine($"            var items = ReadAll{synClass.ClassName}s();");
            sb.AppendLine($"            var {lclClass} = items.Where(q => {whereClause}).FirstOrDefault();");
            sb.AppendLine("");
            sb.AppendLine($"            if ({lclClass} != null)");
            sb.AppendLine("            {");
            sb.AppendLine($"                items.Remove({lclClass});");
            sb.AppendLine($"                SaveAll{synClass.ClassName}s(items);");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("");

            return sb.ToString();
        }

        
    }
}
