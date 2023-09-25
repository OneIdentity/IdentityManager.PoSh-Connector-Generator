using Newtonsoft.Json;
using OIM.PS.SyncProject.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OIM.PS.SyncProject.GeneratorUI
{
	public partial class MainGeneratorForm : Form
	{


		public MainGeneratorForm()
		{
			InitializeComponent();
		}

		StringBuilder sb = new StringBuilder();

		private BindingSource bindingSourceParams = new BindingSource();
		private BindingSource bindingSourceClasses = new BindingSource();
		private string _multiThreaderFileName = "";

		public PSSyncMetadata _meta = new PSSyncMetadata();

		private void Form1_Load(object sender, EventArgs e)
		{
			Application.Idle += Application_Idle;

			_meta.Parameters = new List<Param>();
			_meta.SyncClasses = new List<SyncClass>();

			//===PARAMETERS===============================================================
			// Initialize the DataGridView.
			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.AutoSize = false;
			dataGridView1.DataSource = bindingSourceParams;

			//===CLASSES=======================================================================
			dataGridViewClasses.AutoGenerateColumns = false;
			dataGridViewClasses.AutoSize = false;
			dataGridViewClasses.DataSource = bindingSourceClasses;

			txtNamespace.Focus();

			string appSettingValue = ConfigurationManager.AppSettings["FilesDirectory"];
			textBoxOutputFile.Text = appSettingValue;

			_multiThreaderFileName = ConfigurationManager.AppSettings["MultiThreader"];

			if (!File.Exists(_multiThreaderFileName))
			{
				tabGenerate.Visible = false;
			}
			else
			{
				try
				{
					var DLL = Assembly.LoadFrom(_multiThreaderFileName);
				}
				catch (Exception ex)
				{
					tabGenerate.Visible = false;
				}
			}
		}


		private void butnAddParameter_Click(object sender, EventArgs e)
		{
			var parm = ParametersForm.AddParameter();
			if (parm != null)
			{
				_meta.Parameters.Add(parm);

				bindingSourceParams.DataSource = _meta.Parameters.ToList();
				bindingSourceParams.ResetBindings(false);
			}
		}

		private void butnDelete_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
			{
				if (oneCell.Selected)
				{
					_meta.Parameters.RemoveAt(oneCell.RowIndex);
					bindingSourceParams.DataSource = _meta.Parameters.ToArray();
					bindingSourceParams.ResetBindings(false);
					break;

				}
			}
		}

		private void btnFile_Click(object sender, EventArgs e)
		{
			//P.S. Just select folder. File names will be hardcoded.

			using (var fbd = new FolderBrowserDialog())
			{
				if (!string.IsNullOrEmpty(textBoxOutputFile.Text))
				{
					if (Directory.Exists(textBoxOutputFile.Text))
					{
						fbd.RootFolder = Environment.SpecialFolder.Desktop;
						fbd.SelectedPath = textBoxOutputFile.Text;
					}
				}
				// 

				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					textBoxOutputFile.Text = fbd.SelectedPath;

					SaveOutputFileInSettings(fbd.SelectedPath);
				}
			}
		}

		private void SaveOutputFileInSettings(string selectedPath)
		{
			try
			{
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var settings = configFile.AppSettings.Settings;
				if (settings["FilesDirectory"] == null)
				{
					settings.Add("FilesDirectory", selectedPath);
				}
				else
				{
					settings["FilesDirectory"].Value = selectedPath;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error writing app settings");
			}
		}

		private void btnAddClass_Click(object sender, EventArgs e)
		{
			var syncClass = SyncClassForm.AddSyncClass();
			if (syncClass != null)
			{
				_meta.SyncClasses.Add(syncClass);

				bindingSourceClasses.DataSource = _meta.SyncClasses.ToArray();
				bindingSourceClasses.ResetBindings(false);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DataGridView dgv = dataGridViewClasses;

			SyncClass selSyncClass = null;
			try
			{
				int totalRows = dgv.Rows.Count;
				// get index of the row for the selected cell
				int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
				//if (rowIndex == totalRows - 1)
				//    return;


				DataGridViewRow selectedRow = dgv.Rows[rowIndex];

				selSyncClass = selectedRow.DataBoundItem as SyncClass;

				var syncClass = SyncClassForm.EditSyncClass(selSyncClass);
				if (syncClass != null)
				{
					foreach (DataGridViewRow item in dataGridViewClasses.Rows)
					{
						SyncClass rowData = item.DataBoundItem as SyncClass;
						if (rowData != null)
						{
							if (rowData.ClassName == syncClass.ClassName)
							{
								rowData = syncClass;
								break;
							}
						}
					}

					bindingSourceClasses.ResetBindings(false);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private void btnDeleteClass_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewCell oneCell in dataGridViewClasses.SelectedCells)
			{
				if (oneCell.Selected)
				{
					_meta.SyncClasses.RemoveAt(oneCell.RowIndex);
					bindingSourceClasses.DataSource = _meta.SyncClasses.ToArray();
					bindingSourceClasses.ResetBindings(false);
					break;
				}
			}
		}

		//============================================================================================
		/// <summary>
		/// Generate Metadata
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void butnGenerate_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxOutputFile.Text))
			{
				MessageBox.Show("Please select output folder");
				return;
			}


			_meta.Parameters.Clear();
			_meta.SyncClasses.Clear();

			_meta.Namespace = txtNamespace.Text;
			_meta.ClassName = txtClassName.Text;

			////P.S. TEST Metadata
			//_meta = TestMetadata.GetTestMetadata();

			PopulateMetadataFromForm();

			string path = Path.GetFullPath(textBoxOutputFile.Text);
			string metaFile = Path.Combine(path, txtNamespace.Text.Trim() + ".json");

			if (!File.Exists(metaFile))
			{
				File.Create(metaFile).Close();
			}



			string output = JsonConvert.SerializeObject(_meta, Newtonsoft.Json.Formatting.Indented);
			using (TextWriter writer = new StreamWriter(metaFile))
			{
				writer.Write(output);
			}


			MessageBox.Show("Done!");
		}

		private void btnGeneratePSDefinition_Click(object sender, EventArgs e)
		{
			if (!File.Exists(_multiThreaderFileName))
			{
				return;
			}

			if (string.IsNullOrEmpty(textBoxOutputFile.Text))
			{
				MessageBox.Show("Please select output folder");
			}

			_meta.Parameters.Clear();
			_meta.SyncClasses.Clear();

			_meta.Namespace = txtNamespace.Text;
			_meta.ClassName = txtClassName.Text;

			PopulateMetadataFromForm();

			if (!VerifyInitialPathExists(textBoxOutputFile.Text))
			{
				return;
			}

			string path = Path.GetFullPath(textBoxOutputFile.Text);
			string xmlFilePS = Path.Combine(path, txtNamespace.Text.Trim() + "_PS.xml");
			string psModuleFile = Path.Combine(path, txtNamespace.Text.Trim() + "Module.psm1");

			var DLL = Assembly.LoadFrom(_multiThreaderFileName);

			var typePowerShellXMLPS = DLL.GetType("OIM.PS.SyncProject.Generator.PowerShellConnectorGeneratorPS");
			var typePowerShellModule = DLL.GetType("OIM.PS.SyncProject.Generator.PowerShellModuleGenerator");

			//--------------------------------------------------------------------------------
			var ps = Activator.CreateInstance(typePowerShellXMLPS, new object[] { _meta });
			var method = typePowerShellXMLPS.GetMethod("GetConnectorDefinition");
			var def = (PowershellConnectorDefinition)method.Invoke(ps, null);
			string syncXML = SerializeXML(def);
			WriteStringToFile(xmlFilePS, syncXML);
			//--------------------------------------------------------------------------------
			var cg = Activator.CreateInstance(typePowerShellModule, new object[] { _meta });
			method = typePowerShellModule.GetMethod("GenerateModule");
			string strModule = method.Invoke(cg, null).ToString();
			WriteStringToFile(psModuleFile, strModule);
			//--------------------------------------------------------------------------------

			////string psXML = GenerateSyncProjectXML(DLL, "OIM.PS.SyncProject.Generator.PowerShellConnectorGeneratorPS");

			//string psModule = new PowerShellModuleGenerator(_meta).GenerateModule();
			//WriteStringToFile(psModuleFile, psModule);

			//PowershellConnectorDefinition def = new PowerShellConnectorGeneratorPS(_meta).GetConnectorDefinition();
			//string psXML = SerializeXML(def);
			//WriteStringToFile(xmlFilePS, psXML);

			MessageBox.Show("Done!");
		}

		private void btnGenerateDefinition_Click(object sender, EventArgs e)
		{
			if (!File.Exists(_multiThreaderFileName))
			{
				return;
			}

			if (string.IsNullOrEmpty(textBoxOutputFile.Text))
			{
				MessageBox.Show("Please select output folder");
			}

			_meta.Parameters.Clear();
			_meta.SyncClasses.Clear();

			_meta.Namespace = txtNamespace.Text;
			_meta.ClassName = txtClassName.Text;

			////P.S. TEST Metadata
			//_meta = TestMetadata.GetTestMetadata();

			//TODO - Uncomment
			PopulateMetadataFromForm();

			if (!VerifyInitialPathExists(textBoxOutputFile.Text))
			{
				return;
			}

			string path = Path.GetFullPath(textBoxOutputFile.Text);
			string classFile = Path.Combine(path, txtNamespace.Text.Trim() + ".cs");
			string classImplementFile = Path.Combine(path, txtNamespace.Text.Trim() + "Implement" + ".cs");
			string classTestFile = Path.Combine(path, txtNamespace.Text.Trim() + "_TEST" + ".cs");
			string xmlFileNet = Path.Combine(path, txtNamespace.Text.Trim() + "_NET.xml");




			//======== Create and instance of the generator ======================


			//////TODO - Uncomment real code ======================================
			var DLL = Assembly.LoadFrom(_multiThreaderFileName);

			var typeClassGenerator = DLL.GetType("OIM.PS.SyncProject.Generator.DotNETClassGenerator");
			var typeClassImplGenerator = DLL.GetType("OIM.PS.SyncProject.Generator.DotNETClassImplementGenerator");
			var typeTestClasslGenerator = DLL.GetType("OIM.PS.SyncProject.Generator.DotNETTestClassGenerator");
			var typeProjectGenerator = DLL.GetType("OIM.PS.SyncProject.Generator.DotNetProjectCreator");
			var typePowerShellXMLNet = DLL.GetType("OIM.PS.SyncProject.Generator.PowerShellConnectorGeneratorNet");

			//--------------------------------------------------------------            
			var ps = Activator.CreateInstance(typePowerShellXMLNet, new object[] { _meta });
			var method = typePowerShellXMLNet.GetMethod("GetConnectorDefinition");
			var def = (PowershellConnectorDefinition)method.Invoke(ps, null);
			string syncXML = SerializeXML(def);
			WriteStringToFile(xmlFileNet, syncXML);
			//--------------------------------------------------------------
			var cg = Activator.CreateInstance(typeClassGenerator, new object[] { _meta });
			method = typeClassGenerator.GetMethod("GenerateDotNetClass");
			string netClass = method.Invoke(cg, null).ToString();
			WriteStringToFile(classFile, netClass);
			//--------------------------------------------------------------
			var cig = Activator.CreateInstance(typeClassImplGenerator, new object[] { _meta, });
			method = typeClassImplGenerator.GetMethod("GenerateDotNetClass");
			string netClassImplem = method.Invoke(cig, null).ToString();
			WriteStringToFile(classImplementFile, netClassImplem);
			//--------------------------------------------------------------
			var tcg = Activator.CreateInstance(typeTestClasslGenerator, new object[] { _meta });
			method = typeTestClasslGenerator.GetMethod("GenerateDotNetClass");
			string netTestClass = method.Invoke(tcg, null).ToString();
			WriteStringToFile(classTestFile, netTestClass);
			//--------------------------------------------------------------
			if (chkVisualStudio.Checked)
			{
				var pg = Activator.CreateInstance(typeProjectGenerator, new object[] { _meta, path });
				method = typeProjectGenerator.GetMethod("BuildProject");
				method.Invoke(pg, null);
			}
			//////TODO - END Uncomment real code ======================================
			//==============================================================



			//string netClass = new DotNETClassGenerator(_meta).GenerateDotNetClass();

			//WriteStringToFile(classFile, netClass);

			//string netClassImplem = new DotNETClassImplementGenerator(_meta).GenerateDotNetClass();

			//WriteStringToFile(classImplementFile, netClassImplem);

			//string netTestClass = new DotNETTestClassGenerator(_meta).GenerateDotNetClass();

			//WriteStringToFile(classTestFile, netTestClass);

			//DotNetProjectCreator projCreator = new DotNetProjectCreator(_meta, path);
			//projCreator.BuildProject();

			////==============END Generate Class===========================================================

			MessageBox.Show("Done!");
		}

		private void WriteStringToFile(string classFile, string netClass)
		{
			if (!File.Exists(classFile))
			{
				File.Create(classFile).Close();
			}

			//P.S. Write .Net class file.
			using (TextWriter writer = new StreamWriter(classFile))
			{
				writer.Write(netClass);
			}
		}

		private string SerializeXML(PowershellConnectorDefinition def)
		{
			string ret;
			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(def.GetType());
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");

			using (StringWriter textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, def, ns);
				ret = textWriter.ToString();
			}

			ret = ret.Replace("&lt;", "<").Replace("&gt;", ">");
			return ret;
		}

		private bool VerifyInitialPathExists(string initialPath)
		{
			bool ret = true;

			if (!Directory.Exists(initialPath))
			{
				try
				{
					Directory.CreateDirectory(initialPath);
				}
				catch (Exception)
				{
					MessageBox.Show($"Directory '{initialPath}' doesn't exist and cannot be created.");
					ret = false;
				}
			}

			return ret;
		}

		private void PopulateMetadataFromForm()
		{
			foreach (var item in bindingSourceParams)
			{
				_meta.Parameters.Add(item as Param);
			}

			foreach (var item in bindingSourceClasses)
			{
				var synClass = item as SyncClass;

				_meta.SyncClasses.Add(synClass);
			}
		}

		private void btnImportMetadata_Click(object sender, EventArgs e)
		{
			_meta = MetadataImprtForm.ImportMetadata(textBoxOutputFile.Text);

			if (_meta != null)
			{
				txtClassName.Text = _meta.ClassName;
				txtNamespace.Text = _meta.Namespace;

				bindingSourceParams.DataSource = _meta.Parameters.ToList();
				bindingSourceParams.ResetBindings(false);

				bindingSourceClasses.DataSource = _meta.SyncClasses.ToArray();
				bindingSourceClasses.ResetBindings(false);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			//if (bindingSourceParams.Count > 0 &&
			//        bindingSourceClasses.Count > 0 &&
			//        !string.IsNullOrEmpty(textBoxOutputFile.Text) &&
			//        !string.IsNullOrEmpty(txtClassName.Text) &&
			//        !string.IsNullOrEmpty(txtNamespace.Text))
			//{
			//    btnGenerate.Enabled = true;
			//    btnGenerate.BackColor = Color.DarkKhaki;
			//    btnGenerateDefinition.Enabled = true;
			//    btnGenerateDefinition.BackColor = Color.LightBlue;
			//}
			//else
			//{
			//    btnGenerate.Enabled = false;
			//    btnGenerate.BackColor = Color.Gray;
			//    btnGenerateDefinition.Enabled = false;
			//    btnGenerateDefinition.BackColor = Color.Gray;
			//}
		}


	}
}
