using OIM.PS.SyncProject.Common;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace OIM.PS.SyncProject.Generator
{
    public class DotNetProjectCreator
    {
        private readonly PSSyncMetadata _metadata;
        private readonly string _outputPath;
        private readonly string _ns;

        public DotNetProjectCreator(PSSyncMetadata metadata, string outputPath)
        {
            _metadata = metadata;
            _outputPath = outputPath;
            _ns = metadata.Namespace;
        }

        public void BuildProject()
        {
            string libProject  = _ns;
            string testProject = $"{_ns}.Test";

            string libFolder  = Path.Combine(_outputPath, libProject);
            string testFolder = Path.Combine(_outputPath, testProject);

            Directory.CreateDirectory(libFolder);
            Directory.CreateDirectory(testFolder);

            // Solution file
            WriteFile(_outputPath, $"{_ns}.sln", GenerateSolutionFile(libProject, testProject));

            // Library project
            WriteFile(libFolder, $"{libProject}.csproj", GenerateLibraryCsproj());

            string netClass     = new DotNETClassGenerator(_metadata).GenerateDotNetClass();
            string netClassImpl = new DotNETClassImplementGenerator(_metadata).GenerateDotNetClass();
            WriteFile(libFolder, $"{_ns}.cs",          netClass);
            WriteFile(libFolder, $"{_ns}Implement.cs", netClassImpl);

            var def    = new PowerShellConnectorGeneratorNet(_metadata).GetConnectorDefinition();
            string xml = SerializeXml(def);
            WriteFile(libFolder, $"{_ns}_NET.xml", xml);

            // Test (console) project
            WriteFile(testFolder, $"{testProject}.csproj", GenerateConsoleCsproj(libProject));

            string netTestClass = new DotNETTestClassGenerator(_metadata).GenerateDotNetClass();
            WriteFile(testFolder, $"{_ns}_TEST.cs", netTestClass);

            // Test data JSON files
            JsonFilesGenerator.PopulateJSONFile(_metadata.SyncClasses, _outputPath);
        }

        private static void WriteFile(string folder, string fileName, string content)
        {
            File.WriteAllText(Path.Combine(folder, fileName), content, Encoding.UTF8);
        }

        private string GenerateSolutionFile(string libProject, string testProject)
        {
            string libGuid  = Guid.NewGuid().ToString("B").ToUpper();
            string testGuid = Guid.NewGuid().ToString("B").ToUpper();
            const string slnGuid = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";

            return
$@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{slnGuid}"") = ""{testProject}"", ""{testProject}\{testProject}.csproj"", ""{testGuid}""
EndProject
Project(""{slnGuid}"") = ""{libProject}"", ""{libProject}\{libProject}.csproj"", ""{libGuid}""
EndProject
Global
GlobalSection(SolutionConfigurationPlatforms) = preSolution
Debug|Any CPU = Debug|Any CPU
Release|Any CPU = Release|Any CPU
EndGlobalSection
GlobalSection(ProjectConfigurationPlatforms) = postSolution
{libGuid}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
{libGuid}.Debug|Any CPU.Build.0 = Debug|Any CPU
{libGuid}.Release|Any CPU.ActiveCfg = Release|Any CPU
{libGuid}.Release|Any CPU.Build.0 = Release|Any CPU
{testGuid}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
{testGuid}.Debug|Any CPU.Build.0 = Debug|Any CPU
{testGuid}.Release|Any CPU.ActiveCfg = Release|Any CPU
{testGuid}.Release|Any CPU.Build.0 = Release|Any CPU
EndGlobalSection
EndGlobal
";
        }

        private string GenerateLibraryCsproj()
        {
            return
$@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <RootNamespace>{_ns}</RootNamespace>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>";
        }

        private string GenerateConsoleCsproj(string libProject)
        {
            return
$@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <RootNamespace>{_ns}.Test</RootNamespace>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <StartArguments>$(SolutionDir)</StartArguments>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include=""..\{libProject}\{libProject}.csproj"" />
  </ItemGroup>

</Project>";
        }

        private static string SerializeXml(PowershellConnectorDefinition def)
        {
            var serializer = new XmlSerializer(def.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using var writer = new StringWriter();
            serializer.Serialize(writer, def, ns);
            return writer.ToString()
                .Replace("&lt;", "<")
                .Replace("&gt;", ">");
        }
    }
}