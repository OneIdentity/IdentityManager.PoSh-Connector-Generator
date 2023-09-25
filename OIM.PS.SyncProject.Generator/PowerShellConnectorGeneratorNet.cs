using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    /// <summary>
    /// This file generates XML for use with PowerShell Module - .psm1 file.
    /// </summary>
    public class PowerShellConnectorGeneratorNet
    {
        //private List<Param> parameters;
        //private Dictionary<string, GenClassProp[]> classes;
        public PSSyncMetadata _meta = new PSSyncMetadata();
        public PowerShellConnectorGeneratorNet(PSSyncMetadata metadata) //List<Param> parameters, Dictionary<string, GenClassProp[]> classes)
        {
            _meta = metadata;
            //this.parameters = parameters;
            //this.classes = classes;
        }

        public PowershellConnectorDefinition GetConnectorDefinition()
        {
            PCDefClass pcClass = null;

            PowershellConnectorDefinition def = new PowershellConnectorDefinition();
            def.Id = "SampleGeneratedConnector";
            def.Description = "Powershell Generated Connector using .Net dll";
            def.Version = "1.0";

            def.PluginAssemblies = new List<PCDefPluginAssembly>()
            {
                //new PCDefPluginAssembly()
            };

            def.ConnectionParameters = new List<PCDefConnectionParameter>();
            def.ConnectionParameters.Add(new PCDefConnectionParameter() { Name = "FolderContainingDLLs", Description = "Path to folder containing dlls" });
            def.ConnectionParameters.Add(new PCDefConnectionParameter() { Name = "CommaSeparatedDLLNames", Description = "Names of connector dlls" });
            def.ConnectionParameters.Add(new PCDefConnectionParameter() { Name = "Namespace", Description = "Namespace of the connector dll" });
            def.ConnectionParameters.Add(new PCDefConnectionParameter() { Name = "ClassName", Description = "Main class name of the connector dll" });

            foreach (var item in _meta.Parameters)
            {
                def.ConnectionParameters.Add(new PCDefConnectionParameter()
                {
                    Name = item.ParamName,
                    Description = item.Description,
                    IsSensibleData = item.IsSensibleData,
                    IsSensibleDataSpecified = true
                });
            }


            def.Initialization = new PCDefInitialization();
            def.Initialization.PredefinedCommands = new List<PCDefInitializationCommand>();
            def.Initialization.CustomCommands = new List<PCDefInitializationCustomCommand>(); //!!!!!!
            def.Initialization.EnvironmentInitialization = new PCDefInitializationEnvironmentInitialization();

            //==================CustomCommands=============================================================
            string conUtils = GetConnectUtils();

            def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
            {
                Name = "ConnectUtils",
                Value = conUtils
            });

            string disconUtils = GetDisconnectUtils();

            def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
            {
                Name = "DisconnectUtils",
                Value = disconUtils
            });

            //============= Generate Custom Commands per list of classes ===================================
            GenerateCustomCommands(def);
            //==============================================================================================

            GenerateEnvironmentInitializationConnect(def);

            //===============================================================================================
            GenerateEnvironmentInitializationDisconnect(def);

            //===============================================================================================

            def.Schema = new List<PCDefClass>();

            foreach (var cls in _meta.SyncClasses)
            {
                //P.S. We are not using predefined commands yet.
                //GeneratePreDefinedCommands(def, cls);

                //============================================================

                pcClass = new PCDefClass();
                pcClass.Name = cls.ClassName;
                pcClass.Properties = new List<PCDefClassProperty>();

                foreach (var item in cls.Properties.OrderBy(q=>q.OrderNumber).ToList())
                {
                    PCDefClassProperty prop = CreateProperty(item);

                    PropertyModifiedBy(cls.ClassName, item, prop);
                    PropertyCommandMapping(cls.ClassName, item, prop);
                    PropertyReturnBindings(cls.ClassName, item, prop);

                    pcClass.Properties.Add(prop);
                }

                //P.S. ========= Read Configuration ========================================


                ReadConfiguration(pcClass, cls.ClassName);


                //P.S. ==============Method Configuration ============================================================

                pcClass.MethodConfiguration = new List<PCDefClassMethod>() { };
                //{
                AddMethodConfigurationInsert(pcClass, cls.ClassName);
                AddMethodConfigurationUpdate(pcClass, cls.ClassName);
                AddMethodConfigurationDelete(pcClass, cls.ClassName);

                def.Schema.Add(pcClass);
            }

            return def;
        }


        private static void AddMethodConfigurationDelete(PCDefClass pcClass, string cls)
        {
            pcClass.MethodConfiguration.Add(new PCDefClassMethod()
            {
                Name = "Delete",
                CommandSequence = new PCDefClassMethodCommandSequence()
                {
                    Item = new List<PCDefClassMethodCommandSequenceItem>()
                            {
                                new PCDefClassMethodCommandSequenceItem()
                                {
                                    Command = $"{cls}Delete",
                                    //Condition = "Add Condition Here",
                                    Order = 1
                                }
                                //P.S. If we need multiple commands to delet an item - add them here.
                                //,
                                //new PCDefClassMethodCommandSequenceItem()
                                //{
                                //    Command = "Add Command Here",
                                //    Condition = "Add Condition Here",
                                //    Order = 2
                                //}
                            }
                }
            });
        }

        private static void AddMethodConfigurationUpdate(PCDefClass pcClass, string cls)
        {
            pcClass.MethodConfiguration.Add(new PCDefClassMethod()
            {
                Name = "Update",
                CommandSequence = new PCDefClassMethodCommandSequence()
                {
                    Item = new List<PCDefClassMethodCommandSequenceItem>()
                            {
                                new PCDefClassMethodCommandSequenceItem()
                                {
                                    Command = $"{cls}Update",
                                    Order = 1
                                    //Condition = "Add Condition Here",

                                    //P.S. If we need some global parameters - put them here
                                    //,SetParameter = new List<GenericItemSetParameter>()
                                    //{
                                    //    new GenericItemSetParameter()
                                    //    {

                                    //    }
                                    //}
                                }
                            }
                }
            });
        }

        private static void AddMethodConfigurationInsert(PCDefClass pcClass, string cls)
        {
            pcClass.MethodConfiguration.Add(new PCDefClassMethod()
            {
                Name = "Insert",
                CommandSequence = new PCDefClassMethodCommandSequence()
                {
                    Item = new List<PCDefClassMethodCommandSequenceItem>()
                            {
                                new PCDefClassMethodCommandSequenceItem()
                                {
                                    //P.S. For parametes - we don't need to pass data values
                                    //They come from mapping.
                                    //We need to pass here some global values like authentication or so...
                                    Command = $"{cls}Insert",
                                    Order = 1                                    
                                }
                            }
                }
            });
        }

        private static void ReadConfiguration(PCDefClass pcClass, string cls)
        {
            pcClass.ReadConfiguration = new PCDefClassReadConfiguration()
            {
                ListingCommand = new PCDefClassReadConfigurationListingCommand()
                {
                    //P.S. - No parameters needed here as all values are passed via mapping
                    Command = $"{cls}GetAll"
                },

                //P.S. this command returns individual record when item from GetAll is selected
                CommandSequence = new PCDefClassReadConfigurationCommandSequence()
                {
                    Item = new List<PCDefClassReadConfigurationCommandSequenceItem>()
                        {
                            new PCDefClassReadConfigurationCommandSequenceItem()
                            {
                                Command = $"{cls}Get",
                                Order = 1                                
                            }
                        }
                }
            };
        }

        private static PCDefClassProperty CreateProperty(GenClassProp item)
        {
            var prop = new PCDefClassProperty
            {
                Name = item.PropertyName,
                DataType = item.DataType.ToString(),
                IsMandatory = item.IsMandatory,
                IsAutoFill = item.IsAutoFill,
                IsAutoFillSpecified = true,
                IsUniqueKey = item.IsUniqueKey,
                IsUniqueKeySpecified = true,
                IsDisplay = item.IsDisplay,
                IsMultivalue = item.IsMultivalue,
                IsRevision = item.IsRevision,
                Items = new List<object>()   
            };

            //P.S. Primary key is always unique
            if (item.IsPrimaryKey)
            {
                prop.IsUniqueKey = true;
                prop.IsUniqueKeySpecified = true;
            }

            return prop;
        }

        private static void PropertyReturnBindings(string cls, GenClassProp item, PCDefClassProperty prop)
        {
            //P.S. Return binding does not connect to XxxDelete
            var bind = new List<PCDefClassPropertyReturnBindingsBind>()
                    {
                        new PCDefClassPropertyReturnBindingsBind()
                        {
                            CommandResultOf = $"{cls}GetAll",
                            Path = item.PropertyName
                        },
                        new PCDefClassPropertyReturnBindingsBind()
                        {
                            CommandResultOf = $"{cls}Get",
                            Path = item.PropertyName
                        },
                        new PCDefClassPropertyReturnBindingsBind()
                        {
                            CommandResultOf = $"{cls}Insert",
                            Path = item.PropertyName
                        },
                        new PCDefClassPropertyReturnBindingsBind()
                        {
                            CommandResultOf = $"{cls}Update",
                            Path = item.PropertyName
                        }
                    };

            prop.Items.Add(new PCDefClassPropertyReturnBindings()
            {
                Bind = bind
            });
        }

        private static void PropertyCommandMapping(string cls, GenClassProp item, PCDefClassProperty prop)
        {
            //P.S. - Command mapping. All fields map to commands except:
            //Primary Key - gets mapped to Update, Delete, Get
            //Auto Filled - don't get mapped to commands at all except if it is a Primary Key.
            //GetAll - no parameters get mapped.
            var map = new List<PCDefClassPropertyCommandMappingsMap>() { };

            if (!(item.IsAutoFill || item.IsPrimaryKey))
            {
                map.Add(new PCDefClassPropertyCommandMappingsMap()
                {
                    ToCommand = $"{cls}Update",
                    Parameter = item.PropertyName
                });

                map.Add(new PCDefClassPropertyCommandMappingsMap()
                {
                    ToCommand = $"{cls}Insert",
                    Parameter = item.PropertyName
                });
            }

            //P.S. "id" is mapped to Delete command. Nothing else
            if (item.IsPrimaryKey == true)
            {
                map.Add(new PCDefClassPropertyCommandMappingsMap()
                {
                    ToCommand = $"{cls}Delete",
                    Parameter = item.PropertyName
                });

                map.Add(new PCDefClassPropertyCommandMappingsMap()
                {
                    ToCommand = $"{cls}Get",
                    Parameter = item.PropertyName
                });

                map.Add(new PCDefClassPropertyCommandMappingsMap()
                {
                    ToCommand = $"{cls}Update",
                    Parameter = item.PropertyName
                });
            }

            if (map.Count > 0)
            {
                prop.Items.Add(new PCDefClassPropertyCommandMappings()
                {
                    Map = map
                });
            }
        }

        private static void PropertyModifiedBy(string cls, GenClassProp item, PCDefClassProperty prop)
        {
            //P.S. Only command XxxUpdate updates values.
            //But Auto Filled and PrimaryKey values don't get updated.            
            if (!(item.IsAutoFill || item.IsPrimaryKey))
            {
                prop.Items.Add(new PCDefClassPropertyModifiedBy()
                {
                    ModBy = new List<PCDefClassPropertyModifiedByModBy>()
                            {
                                new PCDefClassPropertyModifiedByModBy()
                                {
                                    Command = $"{cls}Update"
                                }
                            }
                });
            }
        }

        private static void GeneratePreDefinedCommands(PowershellConnectorDefinition def, string cls)
        {
            def.Initialization.PredefinedCommands.AddRange(new List<PCDefInitializationCommand>()
                {
                    new PCDefInitializationCommand()
                    {
                        Name = $"{cls}Insert"
                    },
                    new PCDefInitializationCommand()
                    {
                        Name = $"{cls}Update"
                    },
                    new PCDefInitializationCommand()
                    {
                        Name = $"{cls}Delete"
                    },
                    new PCDefInitializationCommand()
                    {
                        Name = $"{cls}GetAll"
                    },
                    new PCDefInitializationCommand()
                    {
                        Name = $"{cls}Get"
                    }
                });
        }

        private static void GenerateEnvironmentInitializationDisconnect(PowershellConnectorDefinition def)
        {
            def.Initialization.EnvironmentInitialization.Disconnect = new PCDefInitializationEnvironmentInitializationDisconnect
            {
                CommandSequence = new PCDefInitializationEnvironmentInitializationDisconnectCommandSequence()
                {
                    Item = new PCDefInitializationEnvironmentInitializationDisconnectCommandSequenceItem()
                    {
                        Command = "DisconnectUtils",
                        Order = 0
                        //P.S. if we need to pass a parameter to DisconnectUtils = we can do so here.
                        //,
                        //SetParameter = new List<GenericItemSetParameter>()
                        //{
                        //    new GenericItemSetParameter()
                        //    {
                        //        Param = "URL",
                        //        Source = "ConnectionParameter",
                        //        Value = "URL"
                        //    }
                        //}
                    }
                }
            };
        }

        private void GenerateEnvironmentInitializationConnect(PowershellConnectorDefinition def)
        {
            def.Initialization.EnvironmentInitialization.Connect = new PCDefInitializationEnvironmentInitializationConnect()
            {
                CommandSequence = new PCDefInitializationEnvironmentInitializationConnectCommandSequence()
                {
                    Item = new PCDefInitializationEnvironmentInitializationConnectCommandSequenceItem()
                    {
                        Command = "ConnectUtils",
                        Order = 0,
                        SetParameter = new List<PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter>()
                        {
                            new PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter()
                            {
                                 Param = "FolderContainingDLLs",
                                 Source = "ConnectionParameter",
                                 Value = "FolderContainingDLLs"
                            },
                             new PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter()
                            {
                                 Param = "CommaSeparatedDLLNames",
                                 Source = "ConnectionParameter",
                                 Value = "CommaSeparatedDLLNames"
                            },
                            new PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter()
                            {
                                 Param = "Namespace",
                                 Source = "ConnectionParameter",
                                 Value = "Namespace"
                            },
                            new PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter()
                            {
                                 Param = "ClassName",
                                 Source = "ConnectionParameter",
                                 Value = "ClassName"
                            }
                        }
                    }
                }
            };

            //P.S. Additional Parameters
            foreach (var item in _meta.Parameters)
            {
                def.Initialization.EnvironmentInitialization.Connect.CommandSequence.Item.SetParameter.Add(
                    new PCDefInitializationEnvironmentInitializationConnectCommandSequenceItemSetParameter()
                    {
                        Param = item.ParamName,
                        Source = "ConnectionParameter",
                        Value = item.ParamName
                    }
               );
            }
        }

        private void GenerateCustomCommands(PowershellConnectorDefinition def)
        {
            foreach (var cls in _meta.SyncClasses)
            {
                //For class with 2 primary keys

                def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
                {
                    Name = $"{cls.ClassName}GetAll",
                    Value = GenerateGetAll(cls.ClassName)
                });

                def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
                {
                    Name = $"{cls.ClassName}Get",
                    Value = GenerateGet(cls.ClassName)
                });

                //Check if there are no
                def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
                {
                    Name = $"{cls.ClassName}Insert",
                    Value = GenerateInsert(cls.ClassName, cls.Properties) //, cls.IsJoinClass)
                });

                def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
                {
                    Name = $"{cls.ClassName}Update",
                    Value = GenerateUpdate(cls.ClassName, cls.Properties)
                });

                def.Initialization.CustomCommands.Add(new PCDefInitializationCustomCommand()
                {
                    Name = $"{cls.ClassName}Delete",
                    Value = GenerateDelete(cls.ClassName)
                });
            }
        }

        private string GetDisconnectUtils()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine("");
            sb.AppendLine("           $global:connector.Disconnect() ");
            sb.Append("        ]]> ");

            return sb.ToString();
        }

        private string GetConnectUtils()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine("        param( ");
            sb.AppendLine("           [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");
            sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
            sb.AppendLine("           [String]$FolderContainingDLLs, ");
            sb.AppendLine("");
            sb.AppendLine("           [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");
            sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
            sb.AppendLine("           [String]$CommaSeparatedDLLNames, ");
            sb.AppendLine("");
            sb.AppendLine("           [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");
            sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
            sb.AppendLine("           [string]$Namespace, ");
            sb.AppendLine("");
            sb.AppendLine("           [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");
            sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
            sb.Append("           [string]$ClassName ");

            if (_meta.Parameters.Count == 0)
            {
                sb.AppendLine("");
                sb.AppendLine("");
            }
            else
            {
                sb.AppendLine(",");
                sb.AppendLine(GenerateParameters(_meta.Parameters));
            }

            sb.AppendLine("        )  ");
            sb.AppendLine("");


            //=====================================================================================
            //sb.AppendLine("        [Reflection.Assembly]::LoadFile($PathToDll) ");
            sb.AppendLine("        $path = $FolderContainingDLLs");
            sb.AppendLine("        $arDLLs = $CommaSeparatedDLLNames.Split(\",\")");
            sb.AppendLine("        foreach ($dll in $arDLLs)");
            sb.AppendLine("        {");
            sb.AppendLine("            $fullName = Join-Path -Path $path -ChildPath $dll.Trim()");
            sb.AppendLine("            [Reflection.Assembly]::LoadFile($fullName)      ");
            sb.AppendLine("        }");



            //==================================================================================
            sb.AppendLine("");
            sb.AppendLine("        $FQTN = $Namespace + \".\" + $ClassName ");

            var argList = GenerateArgList(_meta.Parameters);

            sb.AppendLine("        $global:connector = New-Object -TypeName $FQTN -ArgumentList " + argList);
            sb.AppendLine("        $global:namespace = $Namespace ");
            sb.Append("        ]]> ");

            return sb.ToString();
        }

        private string GenerateGet(string className)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDll = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine("           param( ");
            
            SyncClass cls = _meta.GetClassByName(className);
            List<GenClassProp> props = cls.Properties.Where(q => q.IsPrimaryKey).ToList();
            if (props == null || props.Count == 0)
            {
                throw new Exception($"There must be one parameter, marked as 'Primary Key' in class {className}");
            }

            //All primary keys
            foreach (var prop in props)
            {
                sb.AppendLine("              [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");

                if (prop.IsMandatory || prop.IsPrimaryKey)
                {
                    sb.AppendLine("              [ValidateNotNullOrEmpty()] ");
                }

                sb.Append($"              [{prop.DataType}]${prop.PropertyName} ");
                sb.AppendLine(",");
                sb.AppendLine("");
                //--------------------
                sbDll.Append($"${prop.PropertyName},");
            }

            //P.S. Code to delete last comma.
            string temp = sb.ToString().TrimEnd().TrimEnd(',');
            sb.Clear();
            sb.AppendLine(temp);

            string tempDll = sbDll.ToString().TrimEnd().TrimEnd(',');

            sb.AppendLine("");
            sb.AppendLine("           )  ");
            sb.AppendLine($"             $global:connector.{className}Get({tempDll}) ");
            sb.Append("         ]]> ");

            return sb.ToString();
        }

        private string GenerateGetAll(string className)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine($"             $global:connector.{className}GetAll() ");
            sb.Append("         ]]> ");

            return sb.ToString();
        }

        private string GenerateArgList(List<Param> parameters)
        {
            //$URL,$Username,$Password
            StringBuilder sb = new StringBuilder();
            foreach (var parameter in _meta.Parameters)
            {
                sb.Append("$" + parameter.ParamName + ",");
            }

            return sb.ToString().TrimEnd(',');
        }

        private string GenerateParameters(List<Param> parameters)
        {
            int counter = 0;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");

            foreach (var parameter in _meta.Parameters)
            {
                counter++;

                sb.AppendLine("           [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");
                sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
                sb.Append($"           [string]${parameter.ParamName} ");

                if (counter < _meta.Parameters.Count)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine("");
                }

                sb.AppendLine("");
            }

            return sb.ToString();
        }

        private string GenerateUpdate(string className, List<GenClassProp> fields)
        {
            //string idFieldName = "";
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDll = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine("        param( ");

            foreach (var field in fields)
            {                
                //P.S. We do need to pass Primary Key to the command.
                //But we don't need to pass AutoFill fields
                //because we don't update them.
                if (field.IsAutoFill && !field.IsPrimaryKey)
                {
                    continue;
                }

                if(field.IsPrimaryKey)
                {
                    //idFieldName = field.PropertyName;
                    sbDll.Append($"${field.PropertyName},");
                }

                sb.AppendLine("           [parameter(Mandatory =$false, ValueFromPipelineByPropertyName =$true)] ");

                if (field.IsMandatory || field.IsPrimaryKey)
                {
                    sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
                }                

                if (field.IsMultivalue)
                {
                    sb.Append($"           [{field.DataType}[]]${field.PropertyName}");
                }
                else
                {
                    sb.Append($"           [{field.DataType}]${field.PropertyName}");
                }
                
                sb.AppendLine(",");
                
                sb.AppendLine("");
            }

            //P.S. Code to delete last comma.
            string temp = sb.ToString().TrimEnd().TrimEnd(',');
            sb.Clear();
            sb.AppendLine(temp);

            string tempDll = sbDll.ToString().TrimEnd().TrimEnd(',');

            sb.AppendLine("        )  ");
            sb.AppendLine($"            $global:connector.{className}Update({tempDll}, $PSBoundParameters) ");
            sb.Append("      ]]> ");

            return sb.ToString();
        }

        private string GenerateDelete(string className)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDll = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine("           param( ");
            
            SyncClass cls = _meta.GetClassByName(className);
            List<GenClassProp> props = cls.Properties.Where(q => q.IsPrimaryKey).ToList();
            if (props == null || props.Count == 0)
            {
                throw new Exception($"There must be one parameter, marked as 'Primary Key' in class {className}");
            }

            foreach (var prop in props)
            {
                sb.AppendLine("              [parameter(Mandatory =$true, ValueFromPipelineByPropertyName =$true)] ");

                if (prop.IsMandatory || prop.IsPrimaryKey)
                {
                    sb.AppendLine("              [ValidateNotNullOrEmpty()] ");
                }

                sb.Append($"              [{prop.DataType}]${prop.PropertyName} ");
                sb.AppendLine(",");
                sb.AppendLine("");
                //--------------------
                sbDll.Append($"${prop.PropertyName},");
            }

            //P.S. Code to delete last comma.
            string temp = sb.ToString().TrimEnd().TrimEnd(',');
            sb.Clear();
            sb.AppendLine(temp);

            string tempDll = sbDll.ToString().TrimEnd().TrimEnd(',');

            sb.AppendLine("");
            sb.AppendLine("           )  ");
            sb.AppendLine($"             $global:connector.{className}Delete({tempDll}) ");
            sb.Append("         ]]> ");

            return sb.ToString();
        }

        //P.S. For joint class we need to create new class with PrimaryKeys.
        private string GenerateInsert(string className, List<GenClassProp> fields) //, bool isJointClass)
        {            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("        <![CDATA[");
            sb.AppendLine("          param( ");

            foreach (var field in fields)
            {                
                //P.S. We don't need to pass Auto Generated ke if it is not a Primary Key to 'Create' function
                if (field.IsAutoFill && !field.IsPrimaryKey)
                {
                    continue;                    
                }

                sb.AppendLine("           [parameter(Mandatory =$false, ValueFromPipelineByPropertyName =$true)] ");

                if (field.IsMandatory || field.IsPrimaryKey)
                {
                    sb.AppendLine("           [ValidateNotNullOrEmpty()] ");
                }

                if (field.IsMultivalue)
                {
                    sb.Append($"           [{field.DataType}[]]${field.PropertyName}");
                }
                else
                {
                    sb.Append($"           [{field.DataType}]${field.PropertyName}");
                }

                sb.AppendLine(",");
                
                sb.AppendLine("");
            }

            //P.S. Code to delete last comma.
            string temp = sb.ToString().TrimEnd().TrimEnd(',');
            sb.Clear();
            sb.AppendLine(temp);

            sb.AppendLine("         )  ");
            sb.AppendLine("");
            sb.AppendLine($"         $cls = $global:namespace + \".{className}\" ");
            sb.AppendLine("");
            sb.AppendLine("         $inst = New-Object -TypeName $cls ");

            foreach (var field in fields)
            {
                //P.S. We don't need to pass Auto Generated ke if it is not a Primary Key to 'Create' function
                if (field.IsAutoFill && !field.IsPrimaryKey)
                {
                    continue;                    
                }

                sb.AppendLine($"         $inst.{field.PropertyName}=${field.PropertyName}");                
            }

            sb.AppendLine("");
            sb.AppendLine($"         $global:connector.{className}Create($inst) ");
            sb.Append("        ]]> ");

            return sb.ToString();
        }

    }


}
