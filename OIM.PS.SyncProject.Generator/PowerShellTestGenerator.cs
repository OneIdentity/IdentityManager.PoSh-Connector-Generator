using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    public class PowerShellTestGenerator
    {
        private PSSyncMetadata _meta = new PSSyncMetadata();

        public PowerShellTestGenerator(PSSyncMetadata metadata)
        {
            _meta = metadata;
        }

        public string GenerateTestScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("#==============================================================================");
            sb.AppendLine($"# TEST Script for {_meta.Namespace} PowerShell Module");
            sb.AppendLine("#==============================================================================");
            sb.AppendLine("");
            sb.AppendLine("# Import the module");
            sb.AppendLine($"Import-Module .\\{_meta.Namespace}Module.psm1 -Force");
            sb.AppendLine("");

            // Connection parameters
            sb.AppendLine("#==Connection values==========================");
            foreach (var item in _meta.Parameters)
            {
                if (item.ParamName == "logToFile" || item.ParamName == "logFolder")
                    continue;
                sb.AppendLine($"${item.ParamName} = \"test{item.ParamName.ToLower()}\"");
            }
            sb.AppendLine("");

            // Set parameters
            sb.AppendLine("# Set connection parameters");
            StringBuilder sbParams = new StringBuilder();
            foreach (var item in _meta.Parameters)
            {
                if (item.ParamName == "logToFile" || item.ParamName == "logFolder")
                    continue;
                sbParams.Append($" -{item.ParamName} ${item.ParamName}");
            }
            sb.AppendLine($"SetParameters{sbParams}");
            sb.AppendLine("");

            // Generate GetAll and Get for each class
            foreach (var cls in _meta.SyncClasses)
            {
                var idProp = cls.Properties.Where(q => q.IsPrimaryKey).FirstOrDefault();
                if (idProp == null) continue;

                string cn = cls.ClassName;
                string varPlural = $"${cn.Substring(0, 1).ToLower()}{cn.Substring(1)}s";
                string varSingle = $"${cn.Substring(0, 1).ToLower()}{cn.Substring(1)}";

                sb.AppendLine($"${cn.ToLower()}s = {cn}GetAll");
                sb.AppendLine($"Write-Host (${cn.ToLower()}s | ConvertTo-Json -Depth 4)");
                sb.AppendLine("");
                sb.AppendLine($"${cn.ToLower()} = {cn}Get -{idProp.PropertyName} ${cn.ToLower()}s[2].{idProp.PropertyName}");
                sb.AppendLine($"Write-Host (${cn.ToLower()} | ConvertTo-Json -Depth 4)");
                sb.AppendLine("");
            }

            // Generate Create, Update, Delete for each class
            foreach (var cls in _meta.SyncClasses)
            {
                var idProp = cls.Properties.Where(q => q.IsPrimaryKey).FirstOrDefault();
                if (idProp == null) continue;

                string cn = cls.ClassName;
                string varNew = $"${cn.ToLower()}New";

                // Get non-PK properties for Create/Update params
                var editableProps = cls.Properties
                    .Where(q => !q.IsPrimaryKey && !q.IsAutoFill)
                    .OrderBy(q => q.OrderNumber)
                    .ToList();

                if (editableProps.Count == 0) continue;

                sb.AppendLine("#==================================================================================================");

                // Create
                sb.Append($"{varNew} = {cn}Create");
                GenerateParamList(sb, editableProps, isCreate: true);
                sb.AppendLine("");
                sb.AppendLine($"Write-Host  ({varNew} | ConvertTo-Json -Depth 4)");

                                // Update
                sb.Append($"{varNew} = {cn}Update -{idProp.PropertyName} {varNew}.{idProp.PropertyName}");
                GenerateParamList(sb, editableProps, isCreate: false);
                sb.AppendLine("");
                sb.AppendLine($"Write-Host  ({varNew} | ConvertTo-Json -Depth 4)");

                                sb.AppendLine("");
            }

            // Delete in reverse order
            sb.AppendLine("#==================================================================================================");
            var reversedClasses = _meta.SyncClasses.ToList();
            reversedClasses.Reverse();
            foreach (var cls in reversedClasses)
            {
                var idProp = cls.Properties.Where(q => q.IsPrimaryKey).FirstOrDefault();
                if (idProp == null) continue;

                var editableProps = cls.Properties.Where(q => !q.IsPrimaryKey && !q.IsAutoFill).ToList();
                if (editableProps.Count == 0) continue;

                string cn = cls.ClassName;
                string varNew = $"${cn.ToLower()}New";
                sb.AppendLine($"{cn}Delete -{idProp.PropertyName} {varNew}.{idProp.PropertyName}");
            }

            return sb.ToString();
        }

        private void GenerateParamList(StringBuilder sb, List<GenClassProp> props, bool isCreate)
        {
            string suffix = isCreate ? "" : " New";
            bool useLineContinuation = props.Count > 3;

            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                string paramValue = GetSampleValue(prop, suffix);

                if (i == 0)
                {
                    sb.Append($" -{prop.PropertyName} {paramValue}");
                }
                else if (useLineContinuation)
                {
                    sb.Append($" `\r\n                        -{prop.PropertyName} {paramValue}");
                }
                else
                {
                    sb.Append($" -{prop.PropertyName} {paramValue}");
                }
            }
        }

        private string GetSampleValue(GenClassProp prop, string suffix)
        {
            if (prop.IsMultivalue)
            {
                return $"@(\"value1\", \"value2\", \"value3\")";
            }

            switch (prop.DataType)
            {
                case DataTypes.Bool:
                    return "$true";
                case DataTypes.Int:
                    return "1";
                case DataTypes.DateTime:
                    return "(Get-Date)";
                default:
                    return $"'Test{prop.PropertyName}{suffix}'";
            }
        }
    }
}
