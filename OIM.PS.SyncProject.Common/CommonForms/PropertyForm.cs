using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OIM.PS.SyncProject.Common.CommonForms
{
    public partial class PropertyForm : Form
    {
        GenClassProp _prop = null;

        public PropertyForm()
        {
            InitializeComponent();
        }

        public static GenClassProp AddPProperty()
        {
            GenClassProp ret = null;
            PropertyForm frm = new PropertyForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ret = frm._prop;
            }

            return ret;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _prop = new GenClassProp()
            {
                DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), cmbDataType.Text),
                PropertyName = txtPropertyName.Text,
                IsPrimaryKey = chkIsPrimaryKey.Checked,
                IsAutoFill = chkIsAutoFill.Checked,
                IsDisplay = chkIsDisplay.Checked,
                IsMandatory = chkIsMandatory.Checked,
                IsUniqueKey = chkIsUniqueKey.Checked,
                IsMultivalue = chkIsMultiValue.Checked,
                IsRevision = chkIsRevision.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PropertyForm_Load(object sender, EventArgs e)
        {
            cmbDataType.Text = "String";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
