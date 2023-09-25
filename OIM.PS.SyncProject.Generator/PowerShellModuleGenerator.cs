using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    public class PowerShellModuleGenerator
    {
        private string _nameSpace;
        private string _className;        
        private static Random _ran = new Random();
        private PSSyncMetadata _meta = new PSSyncMetadata();

        public PowerShellModuleGenerator(PSSyncMetadata metadata)
        {
            this._nameSpace = metadata.Namespace;
            this._className = metadata.ClassName;
            _meta = metadata;            
        }

        public string GenerateModule()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTemp = new StringBuilder();

            sb.Append(GenerateClasses());

            //P.S. Declaring class variables
            sb.AppendLine("#==Connection values==========================");
            foreach (var item in _meta.Parameters) //_parameters.Keys)
            {
                sb.AppendLine($"$script:_{item.ParamName.ToLower()} = $null;");
            }

            //===Function SetParameters ====================================
            sb.AppendLine("");
            sb.AppendLine("function SetParameters");
            sb.AppendLine("{");
            
            sb.AppendLine("    param(");

            sbTemp.Clear();
            foreach (var item in _meta.Parameters) //_parameters.Keys)
            {
                sbTemp.AppendLine("        [parameter(Mandatory=$false, ValueFromPipelineByPropertyName=$true)]");
                sbTemp.AppendLine("        [ValidateNotNullOrEmpty()]");
                sbTemp.AppendLine($"        [string]${item.ParamName},");
                sbTemp.AppendLine("");
            }

            sb.AppendLine(sbTemp.ToString().TrimEnd().TrimEnd(','));
            sb.AppendLine("    )");

            sb.AppendLine("");

            //Assigning values to script variables
            foreach (var item in _meta.Parameters) 
            {
                sb.AppendLine($"    $script:_{item.ParamName.ToLower()} = ${item.ParamName}");
            }
                        
            sb.AppendLine("");
            foreach (var item in _meta.SyncClasses) //_classes.Keys)
            {
                sb.AppendLine($"    Populate{item.ClassName}s;");
            }

            sb.AppendLine("}");
            //P.S. END Function SetParameters ====================================

            //P.S. Disconnect TODO - FIX ============================================
            sb.AppendLine("");
            sb.AppendLine("function Disconnect()");
            sb.AppendLine("{");
            
            foreach (var item in _meta.Parameters)
            {
                sb.AppendLine($"    $script:_{item.ParamName.ToLower()} = $null");
            }

            sb.AppendLine("}");
            sb.AppendLine("");

            //P.S. Classes ======================================================
            foreach (var item in _meta.SyncClasses)
            {
                sb.AppendLine($"#======== {item.ClassName.ToUpper()} ============================");

                sb.AppendLine(GenerateMethods(item));

                sb.AppendLine($"#======== END {item.ClassName.ToUpper()} ============================");
                sb.AppendLine("");
            }
            //P.S. END Classes ==================================================

            sb.AppendLine("Export-ModuleMember -Function *");

            string ret =  sb.ToString();

            return ret;
            
        }

        private string GenerateClasses()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in _meta.SyncClasses) 
            {
                sb.AppendLine($"class {item.ClassName}");
                sb.AppendLine("{");

                foreach (var prop in item.Properties) 
                {
                    if (prop.IsMultivalue)
                    {
                        sb.AppendLine($"    [{prop.DataTypeClass}[]]${prop.PropertyName};");
                    }
                    else
                    {
                        sb.AppendLine($"    [{prop.DataTypeClass}]${prop.PropertyName};");
                    }
                }

                sb.AppendLine("}");
                sb.AppendLine("");
            }

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

       

        private string GenerateMethods(SyncClass synClass)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder tmpSb = new StringBuilder();
            StringBuilder sbPrimProps = new StringBuilder();
            StringBuilder sbProps = new StringBuilder();
            string lclClass = $"ob{synClass.ClassName.ToLower()}";
            //string strPrinKeyName = "";
            string strParams = "";
            string strPrimParams = "";

            List<GenClassProp> props = synClass.Properties; 

            List<GenClassProp> primProps = synClass.Properties.Where(q => q.IsPrimaryKey).ToList();


            foreach (var item in primProps)
            {                
                sbPrimProps.AppendLine("        [parameter(Mandatory=$false, ValueFromPipelineByPropertyName=$true)]");
                sbPrimProps.AppendLine("        [ValidateNotNullOrEmpty()]");
                sbPrimProps.AppendLine($"        [{item.DataTypeClass.ToLower()}]${item.PropertyName},");
                sbPrimProps.AppendLine("");
            }

            foreach (var item in props)
            {                
                sbProps.AppendLine("        [parameter(Mandatory=$false, ValueFromPipelineByPropertyName=$true)]");
                sbProps.AppendLine("        [ValidateNotNullOrEmpty()]");
                if (item.IsMultivalue)
                {
                    sbProps.AppendLine($"        [{item.DataTypeClass.ToLower()}[]]${item.PropertyName},");
                }
                else
                {
                    sbProps.AppendLine($"        [{item.DataTypeClass.ToLower()}]${item.PropertyName},");
                }
                sbProps.AppendLine("");
            }

            strParams = sbProps.ToString().TrimEnd().TrimEnd(',');
            strPrimParams = sbPrimProps.ToString().TrimEnd().TrimEnd(',');


            sb.AppendLine($"function {synClass.ClassName}GetAll()");
            sb.AppendLine("{");
            sb.AppendLine("");
            sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s;");
            sb.AppendLine("");
            sb.AppendLine("}");
            sb.AppendLine("");
            
            //Get...
            //Work with multiple Primary Keys

            
            sb.AppendLine($"function {synClass.ClassName}Get");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strPrimParams}");
            sb.AppendLine("    )");
            sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s | Where-Object {{$_.{primProps[0].PropertyName} -eq ${primProps[0].PropertyName} }}");
            sb.AppendLine("");
            sb.AppendLine("}");
            
            
            sb.AppendLine("");
                        
            sb.AppendLine($"function {synClass.ClassName}Create");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strParams}");
            sb.AppendLine("    )");
            sb.AppendLine("");
            sb.AppendLine($"    ${synClass.ClassName.ToLower()} = [{synClass.ClassName}]::new()");

            foreach (var item in synClass.Properties)
            {
                if (item.DataType == DataTypes.Int)
                {
                    if (item.IsPrimaryKey)
                    {
                        sb.AppendLine($"    if({synClass.ClassName.ToLower()}.{item.PropertyName} = 0)");
                        sb.AppendLine("    {");
                        sb.AppendLine($"        ${synClass.ClassName.ToLower()}.{item.PropertyName} = Get-Random");
                        sb.AppendLine("    }");
                        sb.AppendLine("    else");
                        sb.AppendLine("    {");
                        sb.AppendLine($"        ${synClass.ClassName.ToLower()}.{item.PropertyName} =  ${item.PropertyName}");
                        sb.AppendLine("    }");
                    }
                    else
                    {
                        sb.AppendLine($"${synClass.ClassName.ToLower()}.{item.PropertyName} = ${item.PropertyName}");
                    }
                }
                else if (item.DataType == DataTypes.String)
                {
                    if (item.IsPrimaryKey)
                    {
                        sb.AppendLine($"    if([string]::IsNullOrEmpty(${item.PropertyName}))");
                        sb.AppendLine("    {");
                        sb.AppendLine($"        ${synClass.ClassName.ToLower()}.{item.PropertyName} = [System.guid]::NewGuid().toString()");
                        sb.AppendLine("    }");
                        sb.AppendLine("    else");
                        sb.AppendLine("    {");
                        sb.AppendLine($"        ${synClass.ClassName.ToLower()}.{item.PropertyName} =  ${item.PropertyName}");
                        sb.AppendLine("    }");
                    }
                    else
                    {
                        sb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = ${item.PropertyName}");
                    }
                }
                else
                {
                    if (item.IsPrimaryKey)
                    {
                        throw new Exception($"Data Type {primProps[0].DataType} is not valid for primary key");
                    }
                    else
                    {
                        sb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = ${item.PropertyName}");
                    }
                    
                }
            }
            sb.AppendLine("");
            sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s.Add(${synClass.ClassName.ToLower()})");
            sb.AppendLine("");
            sb.AppendLine($"    ${synClass.ClassName.ToLower()}");    

            sb.AppendLine("");
            
            sb.AppendLine("}");
            sb.AppendLine("");
                        
            sb.AppendLine($"function {synClass.ClassName}Update");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strParams}");
            sb.AppendLine("    )");
            sb.AppendLine("");
            sb.AppendLine($"  ${synClass.ClassName.ToLower()} =  $script:{synClass.ClassName.ToLower()}s | Where-Object {{$_.{primProps[0].PropertyName} -eq ${primProps[0].PropertyName} }}");
            sb.AppendLine("");

            foreach (var item in synClass.Properties)
            {
                if(item.IsPrimaryKey)
                {
                    continue;
                }

                string dataType = "";

                if(item.IsMultivalue)
                {
                    dataType = $"{item.DataType.ToString()}[]";
                }
                else
                {
                    dataType = $"{item.DataType.ToString()}";
                }

                sb.AppendLine($"  if ($PSBoundParameters.ContainsKey('{item.PropertyName}')){{${synClass.ClassName.ToLower()}.{item.PropertyName} = [{dataType}]$PSBoundParameters['{item.PropertyName}']}}");
            }

            sb.AppendLine("}");
            sb.AppendLine("");
                        
            sb.AppendLine($"function {synClass.ClassName}Delete");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strPrimParams}");
            sb.AppendLine("    )");
            sb.AppendLine($"  ${synClass.ClassName.ToLower()} =  $script:{synClass.ClassName.ToLower()}s | Where-Object {{$_.{primProps[0].PropertyName} -eq ${primProps[0].PropertyName} }}");
            sb.AppendLine($"  if(${synClass.ClassName.ToLower()})");
            sb.AppendLine("  {");
            sb.AppendLine($"      $script:{synClass.ClassName.ToLower()}s.Remove(${synClass.ClassName.ToLower()})");
            sb.AppendLine("  }");
            sb.AppendLine("}");
            sb.AppendLine("");


            sb.AppendLine("#P.S. Local methods =========================");
          
            //P.S. populate values            
            sb.AppendLine($"function Populate{synClass.ClassName}s()");
            sb.AppendLine("{");
            sb.AppendLine("");
            sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s = New-Object Collections.Generic.List[{synClass.ClassName}]");
            
            for (int i = 0; i < 5; i++)
            {
             
                sb.AppendLine($"    ${synClass.ClassName.ToLower()} = [{synClass.ClassName}]::new()");
                
                tmpSb.Clear();
                tmpSb.AppendLine("");

                props = _meta.GetClassByName(synClass.ClassName).Properties;

                foreach (var item in props) //_classes[className])                    
                {                    
                    if (item.IsPrimaryKey)
                    {
                        if (item.DataType == DataTypes.String)
                        {
                            tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = [System.guid]::NewGuid().toString()");
                        }
                        else if (item.DataType == DataTypes.Int)
                        {
                            tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = Get-Random");
                        }
                    }
                    else
                    {                        
                        if (item.DataType == DataTypes.String)
                        {
                            if (item.IsMultivalue)
                            {
                                tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = @('Test{item.PropertyName}{i.ToString()}','Test{item.PropertyName}{i.ToString()}{i.ToString()}')");
                            }
                            else
                            {
                                tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = 'Test{item.PropertyName}{i.ToString()}'");
                            }
                        }
                        else if (item.DataType == DataTypes.Int)
                        {
                            tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = {i}");
                        }
                        else if (item.DataType == DataTypes.Bool)
                        {
                            tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = 1");
                        }
                        else if (item.DataType == DataTypes.DateTime)
                        {
                            tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = [DateTime]::UtcNow.AddMonths({i}) ");
                        }
                    }
                }
                //P.S. Remove final ','
                sb.AppendLine(tmpSb.ToString().TrimEnd().TrimEnd(','));
                sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s.Add(${synClass.ClassName.ToLower()})");

                sb.AppendLine("");
                sb.AppendLine("");
                
            }

            sb.AppendLine("");
            sb.AppendLine("}");

            var ret = sb.ToString();

            return sb.ToString();
        }

        
    }
}
