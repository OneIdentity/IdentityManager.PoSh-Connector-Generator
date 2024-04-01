using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OIM.PS.SyncProject.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Resources.ResXFileRef;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;

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

		//P.S. 3-18-2024 -> Start changes to generate JSOn files and functionality to read/write

		public string GenerateModule(string folderPath)
        {
			StringBuilder sb = new StringBuilder();
            StringBuilder sbTemp = new StringBuilder();
						
            sb.Append(GenerateClasses());

            //P.S. Declaring class variables
            sb.AppendLine("#==Connection values==========================");

			if (_meta.Parameters.Where(q => q.ParamName == "logToFile").FirstOrDefault() == null)
			{
				_meta.Parameters.Add(new Param("logToFile", "If value is 'Y' or 'True' or '1' - will additionally write log information to file"));
			}

			if (_meta.Parameters.Where(q => q.ParamName == "logFolder").FirstOrDefault() == null)
			{
				_meta.Parameters.Add(new Param("logFolder", "Folder where log files will be located"));
			}			

			foreach (var item in _meta.Parameters) //_parameters.Keys)
            {
                sb.AppendLine($"$script:_{item.ParamName} = $null;");
            }

			sb.AppendLine($"$script:_logFile = $null;");

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

				if (item.ParamName == "logToFile")
				{
					sbTemp.AppendLine($"        [string]${item.ParamName} = \"False\",");
				}
				else if (item.ParamName == "logFolder")
				{
					sbTemp.AppendLine($"        [string]${item.ParamName},");
				}
				else
				{
					sbTemp.AppendLine($"        [string]${item.ParamName},");
				}
				
                sbTemp.AppendLine("");
            }

			

            sb.AppendLine(sbTemp.ToString().TrimEnd().TrimEnd(','));
            sb.AppendLine("    )");

            sb.AppendLine("");

            //Assigning values to script variables
            foreach (var item in _meta.Parameters) 
            {
				if (item.ParamName == "logToFile")
				{
					//sb.AppendLine($"    $script:_{item.ParamName} = $false");
					sb.AppendLine("");
					sb.AppendLine("    if ($logToFile -eq \"1\" -or $logToFile -eq \"Y\" -or $logToFile -eq \"True\"){");
					sb.AppendLine("");
					sb.AppendLine("       $script:_logToFile = $true");
					sb.AppendLine("");
					sb.AppendLine("       if([string]::IsNullOrEmpty($logFolder)){");
					sb.AppendLine("         $script:_logFolder = \"$PSScriptRoot\\Log\"");
					sb.AppendLine("       }else{");
					sb.AppendLine("         $script:_logFolder = $logFolder");
					sb.AppendLine("       }");
					sb.AppendLine("");
					sb.AppendLine($"      New-Item -ItemType Directory -Force -Path $script:_logFolder");
					sb.AppendLine("");
					sb.AppendLine($"      $script:_logFile =  Join-Path -Path $script:_logFolder -ChildPath \"{_meta.Namespace}_$((Get-Date).ToString(\"yyyy-MM-dd_HH-mm-ss-fff\")).log\"");
					sb.AppendLine("");
					sb.AppendLine("    }else{");
					sb.AppendLine("      $script:_logToFile = $false");
					sb.AppendLine("    }");
					sb.AppendLine("");
				}
				else if (item.ParamName == "logFolder") {
					//do nothing, it is used above.
				}
				else
				{
					sb.AppendLine($"    $script:_{item.ParamName} = ${item.ParamName}");
				}				
            }
			
            sb.AppendLine("");

			//P.S. 3-18-2024 -> Don't need to populate values here.
			//--> They will be present in JSON files.
			//
			//foreach (var item in _meta.SyncClasses) //_classes.Keys)
			//{
			//    sb.AppendLine($"    Populate{item.ClassName}s;");
			//}

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

			//P.S. 3-18-2024 -> Add paths to the JSON files
			foreach (var item in _meta.SyncClasses)
			{
				sb.AppendLine("");
				sb.AppendLine($"$script:{item.ClassName}FilePath = \"$PSScriptRoot\\{item.ClassName.ToLower()}.json\"");

				sb.AppendLine("");
				sb.AppendLine($"if (-not(Test-Path $script:{item.ClassName}FilePath))");
				sb.AppendLine("{");
				sb.AppendLine("  # Create an empty JSON file if it doesn't exist");
				sb.AppendLine("  @{ } " + $"| ConvertTo-Json | Set-Content -Path $script:{item.ClassName.ToLower()}FilePath");
				sb.AppendLine("}");
			}
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

			//P.S. Add Logging function
			sb.AppendLine(GenerateLoggingFunction());

			JsonFilesGenerator.PopulateJSONFile(_meta.SyncClasses, folderPath);

			sb.AppendLine("Export-ModuleMember -Function *");

            string ret =  sb.ToString();

			return ret;
            
        }

		private string GenerateLoggingFunction()
		{
			StringBuilder sb = new StringBuilder();
			//========================================================================================
			sb.AppendLine("Function Write-Log {");
			sb.AppendLine("  Param(");
			sb.AppendLine("     [Parameter(Mandatory =$True)]");
			sb.AppendLine("     [string]$Message,");
			sb.AppendLine("     [Parameter(Mandatory =$False)]");
			sb.AppendLine("     [ValidateSet(\"INFO\", \"WARN\", \"ERROR\", \"FATAL\", \"DEBUG\")]");
			sb.AppendLine("     [String]$Level = \"INFO\"");
			sb.AppendLine("  )");
			sb.AppendLine("");			
			sb.AppendLine("  $Stamp = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\")");
			sb.AppendLine("  $Line = \"$Stamp - $Level - $Message\"");
			sb.AppendLine("");
			sb.AppendLine("  if($Level -eq \"ERROR\"){");
			sb.AppendLine("     $Line = \"!!!==================`n\" + $Line + \"`n!!!==================\"");
			sb.AppendLine("  }");
			sb.AppendLine("");
			sb.AppendLine("  if($script:_logToFile) {");
			sb.AppendLine("     $mutexName = \"PSMutex\"");
			sb.AppendLine("     $mutex = New-Object 'Threading.Mutex' $false, $mutexName");
			sb.AppendLine("");
			sb.AppendLine("     # Grab the mutex. Will block until this process has it.");
			sb.AppendLine("     $mutex.WaitOne() | Out-Null");
			sb.AppendLine("     try {");
			sb.AppendLine("        Add-Content $script:_logFile -Value $Line");
			sb.AppendLine("     }");
			sb.AppendLine("     finally {");
			sb.AppendLine("        $mutex.ReleaseMutex() ");
			sb.AppendLine("     }");
			sb.AppendLine("  }");
			sb.AppendLine("");
			sb.AppendLine("  if($Level -eq \"INFO\"){");
			sb.AppendLine("     Write-Information -MessageData $Line -InformationAction Continue");
			sb.AppendLine("  }elseif($Level -eq \"WARN\"){");
			sb.AppendLine("     Write-Warning -Message $Line -InformationAction Continue");
			sb.AppendLine("  }elseif($Level -eq \"ERROR\" -or $Level -eq \"FATAL\"){");
			sb.AppendLine("     Write-Error -Message $Line -InformationAction Continue");
			sb.AppendLine("  }elseif($Level -eq \"DEBUG\"){");
			sb.AppendLine("     Write-Debug -Message $Line -InformationAction Continue");
			sb.AppendLine("  }");
			sb.AppendLine("}");

			//========================================================================================

			return sb.ToString();
		}

		

		//private void ProcessManyToManyClass(string filePath, Dictionary<string, List<string>> classIDs, SyncClass synClass)
		//{
		//	StringBuilder tmpSb = new StringBuilder();
		//	var objects = new List<Dictionary<string, object>>();

		//	var combinedIDs = classIDs[synClass.ClassName];

		//	foreach (var id in combinedIDs)
		//	{
		//		var obj = new Dictionary<string, object>();
		//		var props = _meta.GetClassByName(synClass.ClassName).Properties.OrderBy(q => q.OrderNumber);

		//		foreach (var item in props)
		//		{
		//			if(item.IsCombinedPrimaryKey)
		//			{
		//				obj.Add($"{item.PropertyName}", id);
		//			}
		//			else
		//			{
						
		//			}
		//		}
		//	}
			
		//}

		
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

            List<GenClassProp> primProps = synClass.Properties.Where(q => q.IsPrimaryKey || q.IsCombinedPrimaryKey).ToList();


            foreach (var item in primProps)
            {                
                sbPrimProps.AppendLine("        [parameter(Mandatory=$false, ValueFromPipelineByPropertyName=$true)]");

				if(item.IsMandatory || item.IsPrimaryKey || item.IsCombinedPrimaryKey)
				{
					sbPrimProps.AppendLine("        [ValidateNotNullOrEmpty()]");
				}     
				
                sbPrimProps.AppendLine($"        [{item.DataTypeClass.ToLower()}]${item.PropertyName},");
                sbPrimProps.AppendLine("");
            }

            foreach (var item in props)
            {                
                sbProps.AppendLine("        [parameter(Mandatory=$false, ValueFromPipelineByPropertyName=$true)]");

				if (item.IsMandatory || item.IsPrimaryKey || item.IsCombinedPrimaryKey)
				{
					sbProps.AppendLine("        [ValidateNotNullOrEmpty()]");
				}

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
			sb.AppendLine("    #===== This method's implementation is for demonstration purposes only. ======================");
			sb.AppendLine("");
			sb.AppendLine($"    ${synClass.ClassName.ToLower()}s = $null;");
			sb.AppendLine("    try { ");
			sb.AppendLine($"      $jsonContent = Get-Content -Raw -Path $script:{synClass.ClassName.ToLower()}FilePath");
			sb.AppendLine($"      ${synClass.ClassName.ToLower()}s = $jsonContent | ConvertFrom-Json");
			sb.AppendLine("    }");
			sb.AppendLine("    catch");
			sb.AppendLine("    {");
			sb.AppendLine("      $message = $_");
			sb.AppendLine($"      Write-Log -Message \"{synClass.ClassName}GetAll - `n$message\" -Level ERROR");
			sb.AppendLine($"      throw \"ERROR in {synClass.ClassName}GetAll - `n$message\"");
			sb.AppendLine("    }");
			sb.AppendLine($"    return ${synClass.ClassName.ToLower()}s");
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
			sb.AppendLine("");
			sb.AppendLine("    #===== This method's implementation is for demonstration purposes only. ======================");
			sb.AppendLine("");
			sb.AppendLine($"    return {synClass.ClassName}GetAll | Where-Object {{$_.{primProps[0].PropertyName} -eq ${primProps[0].PropertyName} }}");
            sb.AppendLine("");
            sb.AppendLine("}");
            
            
            sb.AppendLine("");
			//======================== CREATE =============================================================                       
            sb.AppendLine($"function {synClass.ClassName}Create");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strParams}");
            sb.AppendLine("    )");
            sb.AppendLine("");
			sb.AppendLine("");
			sb.AppendLine("    #===== This method's implementation is for demonstration purposes only. ======================");
			sb.AppendLine("");
			sb.AppendLine($"    ${synClass.ClassName.ToLower()} = [{synClass.ClassName}]::new()");

			//P.S. --> For some reason Hashtable - @{} produces error in Create method. Have to use Class::new();
			//sb.AppendLine($"    ${synClass.ClassName.ToLower()} = " + "@{}");

			foreach (var item in synClass.Properties)
            {
                if (item.DataType == DataTypes.Int)
                {
                    if (item.IsPrimaryKey)
                    {
                        sb.AppendLine($"    if(${synClass.ClassName.ToLower()}.{item.PropertyName} -eq 0)");
                        sb.AppendLine("    {");
                        sb.AppendLine($"      ${synClass.ClassName.ToLower()}.{item.PropertyName} = Get-Random");
                        sb.AppendLine("    }");
                        sb.AppendLine("    else");
                        sb.AppendLine("    {");
                        sb.AppendLine($"      ${synClass.ClassName.ToLower()}.{item.PropertyName} =  ${item.PropertyName}");
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
                        sb.AppendLine($"      ${synClass.ClassName.ToLower()}.{item.PropertyName} = [System.guid]::NewGuid().toString()");
                        sb.AppendLine("    }");
                        sb.AppendLine("    else");
                        sb.AppendLine("    {");
                        sb.AppendLine($"      ${synClass.ClassName.ToLower()}.{item.PropertyName} =  ${item.PropertyName}");
                        sb.AppendLine("    }");
                    }
					else if (item.IsCombinedPrimaryKey)
					{
						//P.S. this is for the Many-to-Many objects for which we create a combined primary key.
						sb.AppendLine($"    if([string]::IsNullOrEmpty(${item.PropertyName}))");
						sb.AppendLine("    {");
						sb.AppendLine($"      ${synClass.ClassName.ToLower()}.{item.PropertyName} = " + BuildCombinedPrimaryKeyPoSh(synClass.Properties));
						sb.AppendLine("    }");
						sb.AppendLine("    else");
						sb.AppendLine("    {");
						sb.AppendLine($"      ${synClass.ClassName.ToLower()}.{item.PropertyName} =  ${item.PropertyName}");
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
			//sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s.Add(${synClass.ClassName.ToLower()})");
			sb.AppendLine($"    $ret{synClass.ClassName} = SafeWrite{synClass.ClassName}ToJson -FilePath $script:{synClass.ClassName.ToLower()}FilePath -operation 'create' -{synClass.ClassName.ToLower()} ${synClass.ClassName.ToLower()} ");

			sb.AppendLine("");
            sb.AppendLine($"    return $ret{synClass.ClassName}");    

            sb.AppendLine("");
            
            sb.AppendLine("}");
            sb.AppendLine("");
            //====================== UPDATE ==============================================================
            sb.AppendLine($"function {synClass.ClassName}Update");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strParams}");
            sb.AppendLine("    )");
            sb.AppendLine("");
			//sb.AppendLine($"  ${synClass.ClassName.ToLower()} =  $script:{synClass.ClassName.ToLower()}s | Where-Object {{$_.{primProps[0].PropertyName} -eq ${primProps[0].PropertyName} }}");
			sb.AppendLine("");
			sb.AppendLine("    #===== This method's implementation is for demonstration purposes only. ======================");
			sb.AppendLine("");
			sb.AppendLine($"  ${synClass.ClassName.ToLower()} = {synClass.ClassName}Get -{primProps[0].PropertyName} ${primProps[0].PropertyName}");
			sb.AppendLine("");

			foreach (var item in synClass.Properties)
            {
                if(item.IsPrimaryKey || item.IsCombinedPrimaryKey || item.IncludeInCombinedPrimaryKey)
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
			sb.AppendLine("");
			sb.AppendLine($"  $ret{synClass.ClassName} = SafeWrite{synClass.ClassName}ToJson -FilePath $script:{synClass.ClassName.ToLower()}FilePath -operation 'update' -{primProps[0].PropertyName} ${primProps[0].PropertyName}  -{synClass.ClassName.ToLower()} ${synClass.ClassName.ToLower()}");
			sb.AppendLine($"  return $ret{synClass.ClassName}");

			sb.AppendLine("}");
            sb.AppendLine("");
			//====================== DELETE ==============================================================

			sb.AppendLine($"function {synClass.ClassName}Delete");
            sb.AppendLine("{");
            sb.AppendLine("    param(");
            sb.AppendLine($"{strPrimParams}");
            sb.AppendLine("    )");
			sb.AppendLine("");
			sb.AppendLine("");
			sb.AppendLine("    #===== This method's implementation is for demonstration purposes only. ======================");
			sb.AppendLine("");
			sb.AppendLine($"     SafeWrite{synClass.ClassName}ToJson -FilePath $script:{synClass.ClassName.ToLower()}FilePath -operation 'delete' -{primProps[0].PropertyName} ${primProps[0].PropertyName} ${synClass.ClassName.ToLower()} | Out-Null");
            sb.AppendLine("}");
            sb.AppendLine("");

			//====================== Safe WRITE TO FILE ==============================================================

			sb.AppendLine("#P.S. Local methods =========================");
			sb.AppendLine("");
			sb.AppendLine("");
			sb.AppendLine("    #===== Please replace this method with your own implementation of target system calls. ======================");
			sb.AppendLine("");
			sb.AppendLine($"function SafeWrite{synClass.ClassName}ToJson " + "{");
			sb.AppendLine("   param(");
			sb.AppendLine("      [string]$FilePath,");
			sb.AppendLine("      [ValidateSet('update', 'delete', 'create')]");
			sb.AppendLine("      [string]$operation,");
			sb.AppendLine($"      [string]${primProps[0].PropertyName},");
			sb.AppendLine($"      [object]${synClass.ClassName.ToLower()}");
			sb.AppendLine($"   )");
			sb.AppendLine($"   $lock = [System.Threading.Mutex]::new($false, 'Global\\{synClass.ClassName}sFileLock')");
			sb.AppendLine("   try	{");
			sb.AppendLine("      # Attempt to acquire the lock");
			sb.AppendLine("      $lock.WaitOne() | Out-Null");
			sb.AppendLine("");
			sb.AppendLine($"      ${synClass.ClassName.ToLower()}s = {synClass.ClassName}GetAll");
			sb.AppendLine("");
			sb.AppendLine("      if ($operation -eq 'update' -or $operation -eq 'delete'){");
			sb.AppendLine("");
			sb.AppendLine($"         #Get all {synClass.ClassName.ToLower()}s where {primProps[0].PropertyName} not equal to parameter {primProps[0].PropertyName}");
			sb.AppendLine($"         ${synClass.ClassName.ToLower()}s = ${synClass.ClassName.ToLower()}s | Where-Object " + "{" + $"$_.{primProps[0].PropertyName} -ne ${primProps[0].PropertyName} " + "}");
			sb.AppendLine("      }");
			sb.AppendLine("");
			sb.AppendLine("      if ($operation -eq 'create'){");
			sb.AppendLine("");
			sb.AppendLine("         # Check record with same ID is not present");
			sb.AppendLine($"         $test{synClass.ClassName} = ${synClass.ClassName.ToLower()}s | Where-Object " + "{" + $"$_.{primProps[0].PropertyName} -eq ${synClass.ClassName.ToLower()}.{primProps[0].PropertyName}" + "}");
			sb.AppendLine($"         if ($test{synClass.ClassName})" + "{");
			sb.AppendLine($"            throw \"{synClass.ClassName} with {primProps[0].PropertyName} = '$(${synClass.ClassName.ToLower()}.{primProps[0].PropertyName})' already exists\"");
			sb.AppendLine("         }");
			sb.AppendLine("      }");
			sb.AppendLine("");
            sb.AppendLine("      # For operations 'update' and 'create' - add user to the list" );
			sb.AppendLine("      if ($operation -ne 'delete'){");
			sb.AppendLine($"         ${synClass.ClassName.ToLower()}s += ${synClass.ClassName.ToLower()} ");
			sb.AppendLine("      } ");       

			sb.AppendLine("");
			sb.AppendLine($"      $jsonData = ${synClass.ClassName.ToLower()}s | ConvertTo-Json -Depth 4");
			sb.AppendLine("      $jsonData | Set-Content -Path $FilePath");
			sb.AppendLine("");
			sb.AppendLine("   }");
			sb.AppendLine("   catch");
			sb.AppendLine("   {");
			sb.AppendLine("      $message = $_");
			sb.AppendLine("      Write-Host $message");
			sb.AppendLine("   }");
			sb.AppendLine("   finally");
			sb.AppendLine("   {");
			sb.AppendLine("      # Release the lock");
			sb.AppendLine("      $lock.ReleaseMutex()");
			sb.AppendLine("   }");
			sb.AppendLine("");
			sb.AppendLine($"   return ${synClass.ClassName.ToLower()}");
			sb.AppendLine("}");
			sb.AppendLine("");

			//P.S. 3-18-2024 -> Don't populate objects - they will be present in JSON file
			#region "OLD Commented Out code for populating objects"
			//P.S. populate values.            
			//sb.AppendLine($"function Populate{synClass.ClassName}s()");
			//         sb.AppendLine("{");
			//         sb.AppendLine("");
			//         sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s = New-Object Collections.Generic.List[{synClass.ClassName}]");

			//         for (int i = 0; i < 5; i++)
			//         {

			//             sb.AppendLine($"    ${synClass.ClassName.ToLower()} = [{synClass.ClassName}]::new()");

			//             tmpSb.Clear();
			//             tmpSb.AppendLine("");

			//             props = _meta.GetClassByName(synClass.ClassName).Properties;

			//             foreach (var item in props) //_classes[className])                    
			//             {                    
			//                 if (item.IsPrimaryKey)
			//                 {
			//                     if (item.DataType == DataTypes.String)
			//                     {
			//				tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = '" + Guid.NewGuid().ToString() + "'");
			//                     }
			//                     else if (item.DataType == DataTypes.Int)
			//                     {
			//                         tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = " + $"{i}{i}{i}{i}");
			//                     }
			//                 }
			//                 else
			//                 {                        
			//                     if (item.DataType == DataTypes.String)
			//                     {
			//                         if (item.IsMultivalue)
			//                         {
			//					//P.S. If property ends with 's' - like Groups - remove 's'.
			//					// Also number should be between 0 and 4 and two numbers should be different.
			//					var propName = item.PropertyName.Trim();
			//					if (propName.EndsWith('s'))
			//					{
			//						propName = propName.TrimEnd('s');
			//					}

			//					var num1 = GetRandomNumber(0, 5, null);
			//					var num2 = GetRandomNumber(0, 5, num1.ToString());

			//					tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = @('Test{propName}{num1.ToString()}','Test{propName}{num2.ToString()}')");
			//                         }
			//                         else
			//                         {
			//                             tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = 'Test{item.PropertyName}{i.ToString()}'");
			//                         }
			//                     }
			//                     else if (item.DataType == DataTypes.Int)
			//                     {
			//                         tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = {i}");
			//                     }
			//                     else if (item.DataType == DataTypes.Bool)
			//                     {
			//                         tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = 1");
			//                     }
			//                     else if (item.DataType == DataTypes.DateTime)
			//                     {
			//                         tmpSb.AppendLine($"    ${synClass.ClassName.ToLower()}.{item.PropertyName} = [DateTime]::UtcNow.AddMonths({i}) ");
			//                     }
			//                 }
			//             }
			//             //P.S. Remove final ','
			//             sb.AppendLine(tmpSb.ToString().TrimEnd().TrimEnd(','));
			//             sb.AppendLine($"    $script:{synClass.ClassName.ToLower()}s.Add(${synClass.ClassName.ToLower()})");

			//             sb.AppendLine("");
			//             sb.AppendLine("");

			//         }

			//         sb.AppendLine("");
			//         sb.AppendLine("}");
			#endregion

			var ret = sb.ToString();

            return sb.ToString();
        }

		private string BuildCombinedPrimaryKeyPoSh(List<GenClassProp> properties)
		{
			StringBuilder sb = new StringBuilder();
			var included = properties.Where(q=>q.IncludeInCombinedPrimaryKey).ToList().OrderBy(q=>q.OrderNumber);

			foreach (var item in included)
			{
				sb.Append($"${item.PropertyName}|");
			}

			var ret = "\"" + sb.ToString().TrimEnd('|') + "\"";

			return ret;
		}

		
	}
}
