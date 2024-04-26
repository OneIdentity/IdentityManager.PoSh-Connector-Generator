//using Aspose.Zip;
using Ionic.Zip;
using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
    public class DotNetProjectCreator
    {
        private string _outputPath = "";
        private string _className = "OIMPSSyncConnectorClassFacade";
        private string _nameSpace = "OIMPSSyncConnectorFacade";                
        private string projectName = "PowerShell.NetConnector";               
        private string myZip = @".\#.npr";
        
        public DotNetProjectCreator(PSSyncMetadata metadata, string outputPath)
        {
            _outputPath = outputPath;
            _className = metadata.ClassName;
            _nameSpace = metadata.Namespace;
        }

        public void BuildProject()
        {
			//using (FileStream zipFile = File.Open(myZip, FileMode.Open))
			//{
			//    // Decrypt using password
			//    using (var archive = new Archive(zipFile, new ArchiveLoadOptions() { DecryptionPassword = "#$nosecrets!!" }))
			//    {
			//        // Extract files to folder
			//        archive.ExtractToDirectory(_outputPath);
			//    }
			//}

			using (ZipFile zip = ZipFile.Read(myZip))
			{
				foreach (ZipEntry e in zip)
				{
					e.ExtractWithPassword(_outputPath, "#$nosecrets!!");
				}
			}



			//Main project file.
			string projText = File.ReadAllText($"{_outputPath}\\{projectName}\\{projectName}\\{projectName}.csproj");
            projText = projText.Replace("<NamespacePlaceholder>", _nameSpace);
            projText = projText.Replace("<AssembluPlaceholder>", _nameSpace);
            projText = projText.Replace("<MainClass>", _nameSpace);
            projText = projText.Replace("<ImplementClass>", $"{_nameSpace}Implement");
            File.WriteAllText($"{_outputPath}\\{projectName}\\{projectName}\\{projectName}.csproj", projText);

            //Test projectFile
            string projTest = File.ReadAllText($"{_outputPath}\\{projectName}\\{projectName}_TEST\\{projectName}_TEST.csproj");
            projTest = projTest.Replace("<TestProgram>", _nameSpace + "_TEST");
            File.WriteAllText($"{_outputPath}\\{projectName}\\{projectName}_TEST\\{projectName}_TEST.csproj", projTest);

            //Copy files.
            string sourceFile = System.IO.Path.Combine(_outputPath, _nameSpace + ".cs");
            string destFile = System.IO.Path.Combine($"{_outputPath}\\{projectName}\\{projectName}", _nameSpace + ".cs");
            System.IO.File.Copy(sourceFile, destFile, true);

            sourceFile = System.IO.Path.Combine(_outputPath, _nameSpace + "Implement.cs");
            destFile = System.IO.Path.Combine($"{_outputPath}\\{projectName}\\{projectName}", _nameSpace + "Implement.cs");
            System.IO.File.Copy(sourceFile, destFile, true);

            sourceFile = System.IO.Path.Combine(_outputPath, _nameSpace + "_TEST.cs");
            destFile = System.IO.Path.Combine($"{_outputPath}\\{projectName}\\{projectName}_TEST", _nameSpace + "_TEST.cs");
            System.IO.File.Copy(sourceFile, destFile, true);

        }
    }
}