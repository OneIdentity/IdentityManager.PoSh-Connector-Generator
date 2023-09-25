using Newtonsoft.Json;
using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OIM.PS.SyncProject.Common
{
    public partial class MetadataImprtForm : Form
    {
        private PSSyncMetadata _meta = new PSSyncMetadata();
        private string _filename = "";

        public MetadataImprtForm()
        {
            InitializeComponent();
        }
                
        public static PSSyncMetadata ImportMetadata(string initialDirectory)
        {
            PSSyncMetadata ret = null;
            MetadataImprtForm frm = new MetadataImprtForm();
            frm.txtName.Text = initialDirectory;
            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ret = frm._meta;
            }

            return ret;
        }

        private void MetadataImprtForm_Load(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string strMeta = "";
                        
            try
            {
                strMeta = File.ReadAllText(_filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file '{_filename}': {ex.Message}.");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            try
            {
                _meta = JsonConvert.DeserializeObject<PSSyncMetadata>(strMeta);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deserializing file '{_filename}': {ex.Message}.");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            //string strMeta = "";
            OpenFileDialog openFileDialog1;

            using (openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "PS Sync Connector Metadata|*.json";
                openFileDialog1.Title = "Open Connector Metadata";

                if(string.IsNullOrEmpty(txtName.Text))
                {
                    openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                }
                else
                {
                    openFileDialog1.InitialDirectory = txtName.Text;
                }

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = openFileDialog1.FileName;
                    txtName.Text = _filename;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MetadataImprtForm_Shown(object sender, EventArgs e)
        {
            btnSelectFile.Focus();
        }
    }
}
