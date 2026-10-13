using System.Text;
using OIM.PS.SyncProject.Common;
using OIM.PS.SyncProject.Generator;

namespace OIM.PS.SyncProject.Web.Helpers;

internal sealed class ConnectorScaffoldGenerator(PSSyncMetadata metadata)
{
	private const string ImplementationTodo = "TODO - Add code for interacting with target system here";

	public string GeneratePowerShellModule(string folderPath)
	{
		var output = new StringBuilder();
		foreach (var syncClass in metadata.SyncClasses)
		{
			output.AppendLine($"class {syncClass.ClassName}");
			output.AppendLine("{");
			foreach (var property in syncClass.Properties)
			{
				output.AppendLine($"    [{PropertyType(property)}]${property.PropertyName};");
			}
			output.AppendLine("}");
			output.AppendLine();
		}

		var connectionMetadata = new PSSyncMetadata
		{
			Namespace = metadata.Namespace,
			ClassName = metadata.ClassName,
			Parameters = metadata.Parameters,
			SyncClasses = []
		};
		var connectionModule = new PowerShellModuleGenerator(connectionMetadata).GenerateModule(folderPath);
		var loggingStart = connectionModule.IndexOf("Function Write-Log {", StringComparison.Ordinal);
		if (loggingStart < 0)
		{
			throw new InvalidOperationException("The generated PowerShell module is missing its logging function.");
		}
		output.Append(connectionModule.AsSpan(0, loggingStart));
		foreach (var syncClass in metadata.SyncClasses)
		{
			output.AppendLine($"#======== {syncClass.ClassName.ToUpper()} ============================");
			AppendPowerShellMethod(output, $"{syncClass.ClassName}GetAll", []);
			AppendPowerShellMethod(output, $"{syncClass.ClassName}Get", PrimaryKeys(syncClass), keyParameters: true);
			AppendPowerShellMethod(output, $"{syncClass.ClassName}Create", syncClass.Properties);
			AppendPowerShellMethod(output, $"{syncClass.ClassName}Update", syncClass.Properties);
			AppendPowerShellMethod(output, $"{syncClass.ClassName}Delete", PrimaryKeys(syncClass), keyParameters: true);
			output.AppendLine($"#======== END {syncClass.ClassName.ToUpper()} ============================");
			output.AppendLine();
		}
		output.Append(connectionModule.AsSpan(loggingStart));
		return output.ToString();
	}

	private static void AppendPowerShellMethod(StringBuilder output, string name, List<GenClassProp> properties, bool keyParameters = false)
	{
		output.AppendLine($"function {name}" + (properties.Count == 0 ? "()" : ""));
		output.AppendLine("{");
		if (properties.Count > 0)
		{
			output.AppendLine("    param(");
			var parameters = properties.Select(property =>
			{
				var validation = property.IsMandatory || property.IsPrimaryKey || property.IsCombinedPrimaryKey
					? "        [ValidateNotNullOrEmpty()]" + Environment.NewLine : "";
				var type = keyParameters ? property.DataTypeClass : PropertyType(property);
				return "        [parameter(Mandatory=$false, ValueFromPipelineByPropertyName=$true)]" + Environment.NewLine
					+ validation + $"        [{type.ToLowerInvariant()}]${property.PropertyName}";
			});
			output.AppendLine(string.Join("," + Environment.NewLine, parameters));
			output.AppendLine("    )");
		}
		output.AppendLine($"    # {ImplementationTodo}");
		output.AppendLine("    throw \"Not Implemented\"");
		output.AppendLine("}");
		output.AppendLine();
	}

	public string GenerateNetImplementation()
	{
		var output = new StringBuilder();
		output.AppendLine("using System;");
		output.AppendLine("using System.Collections;");
		output.AppendLine("using System.Collections.Generic;");
		output.AppendLine("using System.Linq;");
		output.AppendLine($"namespace {metadata.Namespace}");
		output.AppendLine("{");
		output.AppendLine($"    public class {metadata.ClassName}Implement");
		output.AppendLine("    {");
		foreach (var parameter in metadata.Parameters)
		{
			output.AppendLine($"        private string _{parameter.ParamName.ToLowerInvariant()};");
		}
		var constructorParameters = metadata.Parameters.Select(parameter => $"string {parameter.ParamName}")
			.Append("string testFolder = null");
		output.AppendLine($"        public {metadata.ClassName}Implement({string.Join(", ", constructorParameters)})");
		output.AppendLine("        {");
		foreach (var parameter in metadata.Parameters)
		{
			output.AppendLine($"            _{parameter.ParamName.ToLowerInvariant()} = {parameter.ParamName};");
		}
		output.AppendLine("        }");
		output.AppendLine("        public void Disconnect()");
		output.AppendLine("        {");
		foreach (var parameter in metadata.Parameters)
		{
			output.AppendLine($"            _{parameter.ParamName.ToLowerInvariant()} = null;");
		}
		output.AppendLine("        }");
		foreach (var syncClass in metadata.SyncClasses)
		{
			var name = syncClass.ClassName;
			var keys = string.Join(", ", PrimaryKeys(syncClass).Select(property => $"{property.DataTypeClass} {property.PropertyName}"));
			AppendNetMethod(output, $"List<{name}> {name}GetAll()");
			AppendNetMethod(output, $"{name} {name}Get({keys})");
			AppendNetMethod(output, $"{name} {name}Create({name} ob{name.ToLowerInvariant()})");
			AppendNetMethod(output, $"{name} {name}Update({(keys.Length > 0 ? keys + ", " : "")}System.Collections.Hashtable updates)");
			AppendNetMethod(output, $"void {name}Delete({keys})");
		}
		output.AppendLine("        public static string ObjectToString(object obj)");
		output.AppendLine("        {");
		output.AppendLine("            return string.Join(Environment.NewLine, obj.GetType().GetFields().Select(field => $\"{field.Name}: {field.GetValue(obj)}\"));");
		output.AppendLine("        }");
		output.AppendLine("    }");
		foreach (var syncClass in metadata.SyncClasses)
		{
			output.AppendLine($"    public class {syncClass.ClassName}");
			output.AppendLine("    {");
			foreach (var property in syncClass.Properties)
			{
				output.AppendLine($"        public {PropertyType(property)} {property.PropertyName};");
			}
			output.AppendLine("    }");
		}
		output.AppendLine("}");
		return output.ToString();
	}

	private static void AppendNetMethod(StringBuilder output, string signature)
	{
		output.AppendLine($"        public {signature}");
		output.AppendLine("        {");
		output.AppendLine($"            // {ImplementationTodo}");
		output.AppendLine("            throw new NotImplementedException(\"Not Implemented\");");
		output.AppendLine("        }");
	}

	public string GenerateNetExample()
	{
		var output = new StringBuilder();
		output.AppendLine("using System;");
		output.AppendLine("using System.Collections;");
		output.AppendLine($"using {metadata.Namespace};");
		output.AppendLine();
		output.AppendLine($"namespace {metadata.Namespace}.Test");
		output.AppendLine("{");
		output.AppendLine("    internal static class Program");
		output.AppendLine("    {");
		output.AppendLine("        private static void Main()");
		output.AppendLine("        {");
		output.AppendLine("            // TODO - Set connection values before running this example.");
		var arguments = string.Join(", ", metadata.Parameters.Select(parameter => $"{parameter.ParamName}: \"TODO\""));
		output.AppendLine($"            var connector = new {metadata.ClassName}({arguments});");
		output.AppendLine("            try");
		output.AppendLine("            {");
		foreach (var syncClass in metadata.SyncClasses)
		{
			var name = syncClass.ClassName;
			var keyArguments = string.Join(", ", PrimaryKeys(syncClass).Select(property => $"objects[0].{property.PropertyName}"));
			output.AppendLine("                {");
			output.AppendLine($"                    var objects = connector.{name}GetAll();");
			output.AppendLine("                    if (objects.Count > 0)");
			output.AppendLine("                    {");
			output.AppendLine($"                        var item = connector.{name}Get({keyArguments});");
			output.AppendLine("                        if (item != null)");
			output.AppendLine("                        {");
			output.AppendLine("                            var updates = new Hashtable();");
			var editable = EditableProperty(syncClass);
			if (editable != null)
			{
				output.AppendLine($"                            item.{editable.PropertyName} = {ExampleValue(editable, powerShell: false)};");
				output.AppendLine($"                            updates[\"{editable.PropertyName}\"] = item.{editable.PropertyName};");
			}
			var updateArguments = PrimaryKeys(syncClass).Select(property => $"item.{property.PropertyName}").Append("updates");
			output.AppendLine($"                            item = connector.{name}Update({string.Join(", ", updateArguments)});");
			output.AppendLine("                        }");
			output.AppendLine("                    }");
			output.AppendLine("                }");
		}
		output.AppendLine("            }");
		output.AppendLine("            finally");
		output.AppendLine("            {");
		output.AppendLine("                connector.Disconnect();");
		output.AppendLine("            }");
		output.AppendLine("        }");
		output.AppendLine("    }");
		output.AppendLine("}");
		return output.ToString();
	}

	public string GeneratePowerShellExample()
	{
		var output = new StringBuilder();
		output.AppendLine("$ErrorActionPreference = 'Stop'");
		output.AppendLine($"Import-Module (Join-Path $PSScriptRoot '{metadata.Namespace}Module.psm1') -Force");
		output.AppendLine("# TODO - Set connection values before running this example.");
		output.AppendLine("$connectionValues = @{");
		foreach (var parameter in metadata.Parameters.Where(parameter => parameter.ParamName != "logToFile" && parameter.ParamName != "logFolder"))
		{
			output.AppendLine($"    {parameter.ParamName} = 'TODO'");
		}
		output.AppendLine("}");
		output.AppendLine("SetParameters @connectionValues");
		output.AppendLine("try {");
		foreach (var syncClass in metadata.SyncClasses)
		{
			var name = syncClass.ClassName;
			var keyArguments = string.Join(" ", PrimaryKeys(syncClass).Select(property => $"-{property.PropertyName} $objects[0].{property.PropertyName}"));
			output.AppendLine($"    $objects = @({name}GetAll)");
			output.AppendLine("    if ($objects.Count -gt 0) {");
			output.AppendLine($"        $item = {name}Get {keyArguments}");
			output.AppendLine("        if ($null -ne $item) {");
			output.AppendLine("            $updates = @{");
			foreach (var key in PrimaryKeys(syncClass))
			{
				output.AppendLine($"                {key.PropertyName} = $item.{key.PropertyName}");
			}
			output.AppendLine("            }");
			var editable = EditableProperty(syncClass);
			if (editable != null)
			{
				output.AppendLine($"            $item.{editable.PropertyName} = {ExampleValue(editable, powerShell: true)}");
				output.AppendLine($"            $updates['{editable.PropertyName}'] = $item.{editable.PropertyName}");
			}
			output.AppendLine($"            $item = {name}Update @updates");
			output.AppendLine("        }");
			output.AppendLine("    }");
		}
		output.AppendLine("}");
		output.AppendLine("finally {");
		output.AppendLine("    Disconnect");
		output.AppendLine("}");
		return output.ToString();
	}

	private static List<GenClassProp> PrimaryKeys(SyncClass syncClass) =>
		syncClass.Properties.Where(property => property.IsPrimaryKey || property.IsCombinedPrimaryKey).ToList();

	private static GenClassProp? EditableProperty(SyncClass syncClass) =>
		syncClass.Properties.Where(property => !property.IsPrimaryKey && !property.IsCombinedPrimaryKey
			&& !property.IncludeInCombinedPrimaryKey && !property.IsAutoFill)
			.OrderByDescending(property => property.IsDisplay).ThenBy(property => property.OrderNumber).FirstOrDefault();

	private static string PropertyType(GenClassProp property) => property.DataTypeClass + (property.IsMultivalue ? "[]" : "");

	private static string ExampleValue(GenClassProp property, bool powerShell)
	{
		var scalar = property.DataType switch
		{
			DataTypes.Int => "1",
			DataTypes.Bool => powerShell ? "$true" : "true",
			DataTypes.DateTime => powerShell ? "(Get-Date)" : "DateTime.UtcNow",
			_ => "\"Updated value\""
		};
		return !property.IsMultivalue ? scalar : powerShell ? $"@({scalar})" : $"new {property.DataTypeClass}[] {{ {scalar} }}";
	}
}