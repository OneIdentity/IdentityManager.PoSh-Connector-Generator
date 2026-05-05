using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using OIM.PS.SyncProject.Common;
using OIM.PS.SyncProject.Generator;
using OIM.PS.SyncProject.Web.Helpers;
using OIM.PS.SyncProject.Web.Models;

namespace OIM.PS.SyncProject.Web.Controllers;

public class GeneratorController : Controller
{
	private const string MetaKey = "PSSyncMetadata";
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		WriteIndented = true,
		PropertyNameCaseInsensitive = true,
		Converters = { new JsonStringEnumConverter() }
	};

	private PSSyncMetadata GetMetadata()
	{
		return HttpContext.Session.Get<PSSyncMetadata>(MetaKey) ?? new PSSyncMetadata
		{
			Parameters = new List<Param>(),
			SyncClasses = new List<SyncClass>()
		};
	}

	private void SaveMetadata(PSSyncMetadata meta)
	{
		HttpContext.Session.Set(MetaKey, meta);
	}

	private GeneratorViewModel ToViewModel(PSSyncMetadata meta)
	{
		return new GeneratorViewModel
		{
			Namespace = meta.Namespace ?? "",
			ClassName = meta.ClassName ?? "",
			Parameters = meta.Parameters ?? new List<Param>(),
			SyncClasses = meta.SyncClasses ?? new List<SyncClass>()
		};
	}

	public IActionResult Index()
	{
		var meta = GetMetadata();
		if (string.IsNullOrEmpty(meta.Namespace)) meta.Namespace = "OIMPSSyncConnector";
		if (string.IsNullOrEmpty(meta.ClassName)) meta.ClassName = "OIMPSSyncConnectorClass";
		SaveMetadata(meta);
		return View(ToViewModel(meta));
	}

	public IActionResult NewProject()
	{
		HttpContext.Session.Remove(MetaKey);
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult UpdateBasic(string @namespace, string className)
	{
		var meta = GetMetadata();
		meta.Namespace = @namespace;
		meta.ClassName = className;
		SaveMetadata(meta);
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult AddParameter(AddParamModel model)
	{
		if (string.IsNullOrWhiteSpace(model.ParamName))
		{
			TempData["Error"] = "Parameter name is required.";
			return RedirectToAction(nameof(Index));
		}

		var meta = GetMetadata();
		meta.Parameters ??= new List<Param>();
		meta.Parameters.Add(new Param(model.ParamName, model.Description, model.IsSensibleData)
		{
			DataType = model.DataType
		});
		SaveMetadata(meta);
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult DeleteParameter(int index)
	{
		var meta = GetMetadata();
		if (meta.Parameters != null && index >= 0 && index < meta.Parameters.Count)
		{
			meta.Parameters.RemoveAt(index);
			SaveMetadata(meta);
		}
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult AddClass(AddClassModel model)
	{
		if (string.IsNullOrWhiteSpace(model.ClassName))
		{
			TempData["Error"] = "Class name is required.";
			return RedirectToAction(nameof(Index));
		}

		var meta = GetMetadata();
		meta.SyncClasses ??= new List<SyncClass>();

		var props = model.Properties?
			.Where(p => !string.IsNullOrWhiteSpace(p.PropertyName))
			.Select(p => new GenClassProp
			{
				PropertyName = p.PropertyName,
				DataType = p.DataType,
				IsPrimaryKey = p.IsPrimaryKey,
				IsUniqueKey = p.IsUniqueKey,
				IsMandatory = p.IsMandatory,
				IsAutoFill = p.IsAutoFill,
				IsDisplay = p.IsDisplay,
				IsRevision = p.IsRevision,
				IsMultivalue = p.IsMultivalue,
				OrderNumber = p.OrderNumber,
				IsCombinedPrimaryKey = p.IsCombinedPrimaryKey,
				IncludeInCombinedPrimaryKey = p.IncludeInCombinedPrimaryKey
			}).ToList() ?? new List<GenClassProp>();

		meta.SyncClasses.Add(new SyncClass(model.ClassName, props) { IsManyToMany = model.IsManyToMany });
		SaveMetadata(meta);
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult DeleteClass(int index)
	{
		var meta = GetMetadata();
		if (meta.SyncClasses != null && index >= 0 && index < meta.SyncClasses.Count)
		{
			meta.SyncClasses.RemoveAt(index);
			SaveMetadata(meta);
		}
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult EditClass(int index, AddClassModel model)
	{
		if (string.IsNullOrWhiteSpace(model.ClassName))
		{
			TempData["Error"] = "Class name is required.";
			return RedirectToAction(nameof(Index));
		}

		var meta = GetMetadata();
		if (meta.SyncClasses == null || index < 0 || index >= meta.SyncClasses.Count)
		{
			TempData["Error"] = "Invalid class index.";
			return RedirectToAction(nameof(Index));
		}

		var props = model.Properties?
			.Where(p => !string.IsNullOrWhiteSpace(p.PropertyName))
			.Select(p => new GenClassProp
			{
				PropertyName = p.PropertyName,
				DataType = p.DataType,
				IsPrimaryKey = p.IsPrimaryKey,
				IsUniqueKey = p.IsUniqueKey,
				IsMandatory = p.IsMandatory,
				IsAutoFill = p.IsAutoFill,
				IsDisplay = p.IsDisplay,
				IsRevision = p.IsRevision,
				IsMultivalue = p.IsMultivalue,
				OrderNumber = p.OrderNumber,
				IsCombinedPrimaryKey = p.IsCombinedPrimaryKey,
				IncludeInCombinedPrimaryKey = p.IncludeInCombinedPrimaryKey
			}).ToList() ?? new List<GenClassProp>();

		meta.SyncClasses[index] = new SyncClass(model.ClassName, props) { IsManyToMany = model.IsManyToMany };
		SaveMetadata(meta);
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult MoveParameter(int index, string direction)
	{
		var meta = GetMetadata();
		if (meta.Parameters == null) return RedirectToAction(nameof(Index));
		int target = direction == "up" ? index - 1 : index + 1;
		if (index >= 0 && index < meta.Parameters.Count && target >= 0 && target < meta.Parameters.Count)
		{
			(meta.Parameters[index], meta.Parameters[target]) = (meta.Parameters[target], meta.Parameters[index]);
			SaveMetadata(meta);
		}
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult MoveClass(int index, string direction)
	{
		var meta = GetMetadata();
		if (meta.SyncClasses == null) return RedirectToAction(nameof(Index));
		int target = direction == "up" ? index - 1 : index + 1;
		if (index >= 0 && index < meta.SyncClasses.Count && target >= 0 && target < meta.SyncClasses.Count)
		{
			(meta.SyncClasses[index], meta.SyncClasses[target]) = (meta.SyncClasses[target], meta.SyncClasses[index]);
			SaveMetadata(meta);
		}
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult SaveMetadata(string @namespace, string className)
	{
		var meta = GetMetadata();
		meta.Namespace = @namespace;
		meta.ClassName = className;
		SaveMetadata(meta);

		string json = JsonSerializer.Serialize(meta, JsonOptions);
		string fileName = (meta.Namespace?.Trim() ?? "metadata") + "Metadata.json";
		return File(Encoding.UTF8.GetBytes(json), "application/json", fileName);
	}

	[HttpPost]
	public IActionResult ImportMetadata(IFormFile file)
	{
		if (file == null || file.Length == 0)
		{
			TempData["Error"] = "Please select a JSON file.";
			return RedirectToAction(nameof(Index));
		}

		using var reader = new StreamReader(file.OpenReadStream());
		string json = reader.ReadToEnd();
		var meta = JsonSerializer.Deserialize<PSSyncMetadata>(json, JsonOptions);
		if (meta != null)
		{
			meta.Parameters ??= new List<Param>();
			meta.SyncClasses ??= new List<SyncClass>();
			SaveMetadata(meta);
		}
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public IActionResult GeneratePowerShell(string @namespace, string className)
	{
		var meta = GetMetadata();
		meta.Namespace = @namespace;
		meta.ClassName = className;
		SaveMetadata(meta);

		if (string.IsNullOrWhiteSpace(meta.Namespace) || string.IsNullOrWhiteSpace(meta.ClassName))
		{
			TempData["Error"] = "Namespace and Class Name are required.";
			return RedirectToAction(nameof(Index));
		}

		string ns = meta.Namespace.Trim();

		string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
		Directory.CreateDirectory(tempFolder);

		try
		{
			using var memoryStream = new MemoryStream();
			using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
			{
				string metaJson = JsonSerializer.Serialize(meta, JsonOptions);
				AddZipEntry(archive, $"{ns}Metadata.json", metaJson);

				var defPS = new PowerShellConnectorGeneratorPS(meta).GetConnectorDefinition();
				string xmlPS = SerializeXml(defPS);
				AddZipEntry(archive, $"{ns}_PS.xml", xmlPS);

				string psModule = new PowerShellModuleGenerator(meta).GenerateModule(tempFolder);
				AddZipEntry(archive, $"{ns}Module.psm1", psModule);

				// Add test data JSON files generated by PowerShellModuleGenerator
				foreach (var jsonFile in Directory.GetFiles(tempFolder, "*.json"))
				{
					string content = System.IO.File.ReadAllText(jsonFile);
					AddZipEntry(archive, Path.GetFileName(jsonFile), content);
				}
			}

			memoryStream.Position = 0;
			return File(memoryStream.ToArray(), "application/zip", $"{ns}_PS_Generated.zip");
		}
		finally
		{
			Directory.Delete(tempFolder, recursive: true);
		}
	}

	[HttpPost]
	public IActionResult GenerateNet(string @namespace, string className, bool generateSolution = false)
	{
		var meta = GetMetadata();
		meta.Namespace = @namespace;
		meta.ClassName = className;
		SaveMetadata(meta);

		if (string.IsNullOrWhiteSpace(meta.Namespace) || string.IsNullOrWhiteSpace(meta.ClassName))
		{
			TempData["Error"] = "Namespace and Class Name are required.";
			return RedirectToAction(nameof(Index));
		}

		string ns = meta.Namespace.Trim();

		string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
		Directory.CreateDirectory(tempFolder);

		try
		{
			string netClass = new DotNETClassGenerator(meta).GenerateDotNetClass();
			string netClassImpl = new DotNETClassImplementGenerator(meta).GenerateDotNetClass();
			string netTestClass = new DotNETTestClassGenerator(meta).GenerateDotNetClass();
			var defNet = new PowerShellConnectorGeneratorNet(meta).GetConnectorDefinition();
			string xmlNet = SerializeXml(defNet);
			string metaJson = JsonSerializer.Serialize(meta, JsonOptions);

			// Generate test data JSON files (same mechanism as PowerShell generator)
			JsonFilesGenerator.PopulateJSONFile(meta.SyncClasses, tempFolder);

			using var memoryStream = new MemoryStream();
			using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
			{
				if (generateSolution)
				{
					string libProject = $"{ns}";
					string testProject = $"{ns}.Test";
					string slnFolder = $"{ns}_Solution";

					// Solution file
					string slnContent = GenerateSolutionFile(ns, libProject, testProject);
					AddZipEntry(archive, $"{slnFolder}/{ns}.sln", slnContent);

					// Library project
					string libCsproj = GenerateLibraryCsproj(ns);
					AddZipEntry(archive, $"{slnFolder}/{libProject}/{libProject}.csproj", libCsproj);
					AddZipEntry(archive, $"{slnFolder}/{libProject}/{ns}.cs", netClass);
					AddZipEntry(archive, $"{slnFolder}/{libProject}/{ns}Implement.cs", netClassImpl);

					// Test (console) project
					string testCsproj = GenerateConsoleCsproj(ns, libProject);
					AddZipEntry(archive, $"{slnFolder}/{testProject}/{testProject}.csproj", testCsproj);
					AddZipEntry(archive, $"{slnFolder}/{testProject}/{ns}_TEST.cs", netTestClass);

					// Additional files
					AddZipEntry(archive, $"{slnFolder}/{ns}Metadata.json", metaJson);
					AddZipEntry(archive, $"{slnFolder}/{libProject}/{ns}_NET.xml", xmlNet);

					// Add test data JSON files to solution folder
					foreach (var jsonFile in Directory.GetFiles(tempFolder, "*.json"))
					{
						string content = System.IO.File.ReadAllText(jsonFile);
						AddZipEntry(archive, $"{slnFolder}/{Path.GetFileName(jsonFile)}", content);
					}
				}
				else
				{
					AddZipEntry(archive, $"{ns}Metadata.json", metaJson);
					AddZipEntry(archive, $"{ns}.cs", netClass);
					AddZipEntry(archive, $"{ns}Implement.cs", netClassImpl);
					AddZipEntry(archive, $"{ns}_TEST.cs", netTestClass);
					AddZipEntry(archive, $"{ns}_NET.xml", xmlNet);

					// Add test data JSON files
					foreach (var jsonFile in Directory.GetFiles(tempFolder, "*.json"))
					{
						string content = System.IO.File.ReadAllText(jsonFile);
						AddZipEntry(archive, Path.GetFileName(jsonFile), content);
					}
				}
			}

			memoryStream.Position = 0;
			string zipName = generateSolution ? $"{ns}_NET_Solution.zip" : $"{ns}_NET_Generated.zip";
			return File(memoryStream.ToArray(), "application/zip", zipName);
		}
		finally
		{
			Directory.Delete(tempFolder, recursive: true);
		}
	}

	private static string GenerateSolutionFile(string ns, string libProject, string testProject)
	{
		string libGuid = Guid.NewGuid().ToString("B").ToUpper();
		string testGuid = Guid.NewGuid().ToString("B").ToUpper();
		string slnGuid = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";

		return $"""

			Microsoft Visual Studio Solution File, Format Version 12.00
			# Visual Studio Version 17
			VisualStudioVersion = 17.0.31903.59
			MinimumVisualStudioVersion = 10.0.40219.1
			Project("{slnGuid}") = "{libProject}", "{libProject}\{libProject}.csproj", "{libGuid}"
			EndProject
			Project("{slnGuid}") = "{testProject}", "{testProject}\{testProject}.csproj", "{testGuid}"
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
			""";
	}

	private static string GenerateLibraryCsproj(string ns)
	{
		return $"""
			<Project Sdk="Microsoft.NET.Sdk">

			  <PropertyGroup>
				<TargetFramework>net10.0</TargetFramework>
				<RootNamespace>{ns}</RootNamespace>
				<Nullable>enable</Nullable>
				<ImplicitUsings>enable</ImplicitUsings>
			  </PropertyGroup>

			</Project>
			""";
	}

	private static string GenerateConsoleCsproj(string ns, string libProject)
	{
		return $"""
			<Project Sdk="Microsoft.NET.Sdk">

			  <PropertyGroup>
				<OutputType>Exe</OutputType>
				<TargetFramework>net10.0</TargetFramework>
				<RootNamespace>{ns}.Test</RootNamespace>
				<Nullable>enable</Nullable>
				<ImplicitUsings>enable</ImplicitUsings>
			  </PropertyGroup>

			  <ItemGroup>
				<ProjectReference Include="..\{libProject}\{libProject}.csproj" />
			  </ItemGroup>

			</Project>
			""";
	}

	private static void AddZipEntry(ZipArchive archive, string fileName, string content)
	{
		var entry = archive.CreateEntry(fileName, CompressionLevel.Fastest);
		using var writer = new StreamWriter(entry.Open(), Encoding.UTF8);
		writer.Write(content);
	}

	private static string SerializeXml(PowershellConnectorDefinition def)
	{
		var xmlSerializer = new XmlSerializer(def.GetType());
		var ns = new XmlSerializerNamespaces();
		ns.Add("", "");

		using var textWriter = new StringWriter();
		xmlSerializer.Serialize(textWriter, def, ns);
		string ret = textWriter.ToString();
		ret = ret.Replace("&lt;", "<").Replace("&gt;", ">");
		return ret;
	}
}
