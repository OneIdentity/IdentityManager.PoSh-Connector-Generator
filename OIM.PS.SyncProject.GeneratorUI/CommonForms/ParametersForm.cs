using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OIM.PS.SyncProject.Common
{
	public partial class ParametersForm : Form
	{
		public ParametersForm()
		{
			InitializeComponent();
		}

		public static Param AddParameter()
		{
			Param ret = null;
			ParametersForm frm = new ParametersForm();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				ret = new Param(frm.txtName.Text, frm.txtDescription.Text, frm.chkIsSensibleData.Checked);
			}

			return ret;
		}

		private void ParametersForm_Load(object sender, EventArgs e)
		{

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtName.Text))
			{
				MessageBox.Show("Name is a mandatory field");
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
