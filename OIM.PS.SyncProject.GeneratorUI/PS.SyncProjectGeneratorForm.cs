using Newtonsoft.Json;
using OIM.PS.SyncProject.Common;
using OIM.PS.SyncProject.Generator;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class PSSyncProjectGeneratorForm : Form
    {
        

        public PSSyncProjectGeneratorForm()
        {
            InitializeComponent();           
        }

        StringBuilder sb = new StringBuilder();
                
        private BindingSource bindingSourceParams = new BindingSource();
        private BindingSource bindingSourceClasses = new BindingSource();

        //public Dictionary<string, string> parameters = new Dictionary<string, string>();
        //public Dictionary<string, GenClassProp[]> classes = new Dictionary<string, GenClassProp[]>();

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
        }


        

        private void btnFile_Click(object sender, EventArgs e)
        {            
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "Sync Connector Metadata|*.json";
                saveFileDialog1.Title = "Save Connector";

                if (!string.IsNullOrEmpty(textBoxOutputFile.Text))
                {
                    saveFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxOutputFile.Text);
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBoxOutputFile.Text = saveFileDialog1.FileName;
                }
            }
        }
               
        
        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dataGridViewClasses.SelectedCells)
            {
                if (oneCell.Selected)
                {
                    dataGridViewClasses.Rows.RemoveAt(oneCell.RowIndex);
                }
            }
        }

        //============================================================================================

        private void butnGenerate_Click(object sender, EventArgs e)
        {
            string[] props = null;

            string nameSpace = txtNamespace.Text;
            string className = txtClassName.Text;
            _meta.Parameters.Clear();
            _meta.SyncClasses.Clear();

            #region P.S. TEST Code - commented out
            ////TODO - Comment out
            //_meta.Parameters.Add("URL", "URL to API server");
            //_meta.Parameters.Add("Username", "Username to login to API server");
            //_meta.Parameters.Add("Password", "Password to API server");

            //_meta.SyncClasses.Add("User", (new List<GenClassProp>()
            //{
            //    new GenClassProp()
            //    {
            //        PropertyName = "ID",
            //        DataType = DataTypes.String,
            //        IsPrimaryKey = true,
            //        IsAutoFill = true
            //    },
            //    new GenClassProp(){ PropertyName = "FirstName" },
            //    new GenClassProp() {PropertyName="LastName"},
            //    new GenClassProp() {PropertyName="UserName", IsMandatory = true, IsDisplay = true},
            //    new GenClassProp() {PropertyName="IsActive", IsMandatory = true, DataType = DataTypes.Bool},
            //    new GenClassProp() {PropertyName="StartDate", IsMandatory = true, DataType = DataTypes.DateTime}
            //}).ToArray());

            //_meta.SyncClasses.Add("Group", (new List<GenClassProp>() 
            //{
            //    new GenClassProp()
            //    {
            //        PropertyName = "GroupID", DataType = DataTypes.String, IsPrimaryKey = true, IsAutoFill = true
            //    },
            //    new GenClassProp(){ PropertyName = "GroupName", IsMandatory = true, IsDisplay = true },
            //    new GenClassProp() {PropertyName="Description"},
            //    new GenClassProp() {PropertyName="IsActive", IsMandatory = true, DataType = DataTypes.Bool}
            //}).ToArray());

            //_meta.SyncClasses.Add("Thingy", (new List<GenClassProp>() 
            //{
            //    new GenClassProp()
            //    {
            //        PropertyName = "ThingyId", DataType = DataTypes.String, IsPrimaryKey = true, IsAutoFill = true
            //    },
            //    new GenClassProp(){ PropertyName = "Name", IsMandatory = true, IsDisplay = true },
            //    new GenClassProp() {PropertyName="Description"},
            //    new GenClassProp() {PropertyName="Modification"}
            //}).ToArray());
            //=======================END OF TEST=================================================================== 
            #endregion

            _meta.Namespace = txtNamespace.Text;
            _meta.ClassName = txtClassName.Text;

            foreach (var item in bindingSourceParams)
            {
                _meta.Parameters.Add(item as Param);
            }

            foreach (var item in bindingSourceClasses)
            {
                _meta.SyncClasses.Add((item as SyncClass));                
            }

            #region P.S. Saving Metadata - Commented out
            //P.S. - This code generates and writes metadata.
            //string output = JsonConvert.SerializeObject(_meta, Newtonsoft.Json.Formatting.Indented);
            //using (TextWriter writer = new StreamWriter(textBoxOutputFile.Text.Trim()))
            //{
            //    writer.Write(output);
            //} 
            #endregion

            //=============Generate XML==========================================================
            PowerShellConnectorGenerator gen = new PowerShellConnectorGenerator(_meta);
            PowershellConnectorDefinition def = gen.GetConnectorDefinition();
            //P.S. For testing
            //PowershellConnectorDefinition def = new PowershellConnectorDefinition();

            //=============Generate Class==========================================================
            DotNETClassGenerator netGen = new DotNETClassGenerator(nameSpace,
                                                                    className,
                                                                    _meta);
            string netClass = netGen.GenerateDotNetClass();

            //==============Writing Files===========================================================

            string ret = null;

            //========Write XML File ===============================================
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(def.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            if (!File.Exists(textBoxOutputFile.Text.Trim()))
            {
                File.Create(textBoxOutputFile.Text.Trim()).Close();
            }

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, def, ns);
                ret = textWriter.ToString();
            }

            ret = ret.Replace("&lt;", "<").Replace("&gt;", ">");

            using (TextWriter writer = new StreamWriter(textBoxOutputFile.Text.Trim()))
            {
                writer.Write(ret);
            }

            //Write Class file to the same directory

            string path = Path.GetDirectoryName(textBoxOutputFile.Text);
            string classFile = Path.Combine(path, txtNamespace.Text + ".cs");

            if (!File.Exists(classFile))
            {
                File.Create(classFile).Close();
            }

            //P.S. Write .Net class file.
            using (TextWriter writer = new StreamWriter(classFile))
            {
                writer.Write(netClass);
            }
                        
            MessageBox.Show("Done!");
        }
                        

        private void Application_Idle(object sender, EventArgs e)
        {
            if (bindingSourceParams.Count > 0 &&
                    bindingSourceClasses.Count > 0 &&
                    !string.IsNullOrEmpty(textBoxOutputFile.Text) &&
                    !string.IsNullOrEmpty(txtClassName.Text) &&
                    !string.IsNullOrEmpty(txtNamespace.Text))
            {
                btnGenerate.Enabled = true;
                btnGenerate.BackColor = Color.LightBlue;
            }
            else
            {
                btnGenerate.Enabled = false;
                btnGenerate.BackColor = Color.Gray;
            }
        }

        private void btnImportMetadata_Click(object sender, EventArgs e)
        {
            _meta = MetadataImprtForm.ImportMetadata();

            if(_meta != null)
            {
                txtClassName.Text = _meta.ClassName;
                txtNamespace.Text = _meta.Namespace;

                bindingSourceParams.DataSource = _meta.Parameters.ToList();
                bindingSourceParams.ResetBindings(false);

                bindingSourceClasses.DataSource = _meta.SyncClasses.ToArray();
                bindingSourceClasses.ResetBindings(false);
            }
        }
    }
}
