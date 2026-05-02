using OIM.PS.SyncProject.Common;
using OIM.PS.SyncProject.Common.CommonForms;
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
	public partial class SyncClassForm : Form
	{
		public SyncClassForm()
		{
			InitializeComponent();
		}

		private SyncClass _ret = new SyncClass();
		public List<GenClassProp> _props = new List<GenClassProp>();
		private BindingSource _bindingSource = new BindingSource();

		public static SyncClass AddSyncClass()
		{
			SyncClass ret = null;
			SyncClassForm frm = new SyncClassForm();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				ret = new SyncClass(frm.txtName.Text, frm._props);
			}

			return ret;
		}

		public static SyncClass EditSyncClass(SyncClass selSyncClass)
		{
			SyncClass ret = null;

			SyncClassForm frm = new SyncClassForm();
			frm._props = selSyncClass.Properties;
			frm.txtName.Text = selSyncClass.ClassName;
			frm._bindingSource.DataSource = frm._props.OrderBy(q => q.OrderNumber).ToList();
			frm._bindingSource.ResetBindings(false);
			frm.chkManyToMany.Checked = selSyncClass.IsManyToMany;

			//frm.ClearControls();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				ret = new SyncClass(frm.txtName.Text, frm._props);
				ret.IsManyToMany = frm.chkManyToMany.Checked;
			}

			return ret;
		}

		private void PSyncClassForm_Load(object sender, EventArgs e)
		{
			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.AutoSize = false;

			dataGridView1.DataSource = _bindingSource;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			//P.S. We allow multiple primary keys. For example UserInGroup relation.

			GenClassProp prop = PropertyForm.AddPProperty();

			if (prop == null)
			{
				return;
			}

			if (_props.Exists(q => q.PropertyName.Equals(prop.PropertyName, StringComparison.OrdinalIgnoreCase)))
			{
				MessageBox.Show($"Property {prop.PropertyName} already exists");
				return;
			}

			var count = _props.Count();
			prop.OrderNumber = count + 1;

			_props.Add(prop);

			_bindingSource.DataSource = _props.OrderBy(q => q.OrderNumber).ToList();
			_bindingSource.ResetBindings(false);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtName.Text))
			{
				MessageBox.Show("Name is a mandatory field");
				return;
			}

			if (txtName.Text.EndsWith("s", StringComparison.OrdinalIgnoreCase))
			{
				MessageBox.Show("Class name should not be plural");
				return;
			}

			if (_props.Count < 2)
			{
				MessageBox.Show("Class must contain more than one property");
				return;
			}

			if (!_props.Exists(q => q.IsPrimaryKey) && !_props.Exists(q => q.IsCombinedPrimaryKey))
			{
				MessageBox.Show("At least one property must be marked as Primary Key or Combined Primary Key");
				return;
			}

			if (_props.Exists(q => q.IsCombinedPrimaryKey) && !_props.Exists(q => q.IncludeInCombinedPrimaryKey))
			{
				MessageBox.Show("At least one property must be marked as 'Included in Primary Key' if exists property 'Combined Primary Key'");
				return;
			}

			if (_props.Exists(q => q.IsCombinedPrimaryKey) && _props.Exists(q => q.IsPrimaryKey))
			{
				MessageBox.Show("Can't contain both 'Primary Key' and 'Combined primary key' fields");
				return;
			}

			if (_props.Exists(q => q.IsCombinedPrimaryKey) && !chkManyToMany.Checked)
			{
				MessageBox.Show("One of the fields is marked 'Combined Primary Key'. Please check 'Is Many To Many record' checkbox");
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DataGridView dgv = dataGridView1;
			try
			{
				int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
				DataGridViewRow selectedRow = dgv.Rows[rowIndex];

				_props.Remove(selectedRow.DataBoundItem as GenClassProp);

				_bindingSource.DataSource = _props.OrderBy(q => q.OrderNumber).ToList();
				_bindingSource.ResetBindings(false);

				int counter = 1;
				foreach (var item in _props)
				{
					item.OrderNumber = counter;
					counter++;
				}
			}
			catch { }


		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			DataGridView dgv = dataGridView1;
			try
			{
				int totalRows = dgv.Rows.Count;
				// get index of the row for the selected cell
				int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
				if (rowIndex == 0)
					return;

				DataGridViewRow selectedRow = dgv.Rows[rowIndex];

				var ordNum = (selectedRow.DataBoundItem as GenClassProp).OrderNumber;

				//(selectedRow.DataBoundItem as GenClsProp).OrderNumber = (selectedRow.DataBoundItem as GenClsProp).OrderNumber - 1;
				var prev = _props.Where(q => q.OrderNumber == ordNum - 1).FirstOrDefault();
				if (prev == null)
				{
					return;
				}

				prev.OrderNumber = ordNum;
				(selectedRow.DataBoundItem as GenClassProp).OrderNumber = ordNum - 1;

				_bindingSource.DataSource = _props.OrderBy(q => q.OrderNumber).ToList();
				_bindingSource.ResetBindings(false);

				dgv.ClearSelection();
				dgv.Rows[rowIndex - 1].Cells[0].Selected = true;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}



		private void btnDown_Click(object sender, EventArgs e)
		{
			DataGridView dgv = dataGridView1;
			try
			{
				int totalRows = dgv.Rows.Count;
				// get index of the row for the selected cell
				int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
				if (rowIndex == totalRows - 1)
					return;


				DataGridViewRow selectedRow = dgv.Rows[rowIndex];

				var ordNum = (selectedRow.DataBoundItem as GenClassProp).OrderNumber;

				//(selectedRow.DataBoundItem as GenClsProp).OrderNumber = (selectedRow.DataBoundItem as GenClsProp).OrderNumber - 1;
				var next = _props.Where(q => q.OrderNumber == ordNum + 1).FirstOrDefault();
				if (next == null)
				{
					return;
				}

				next.OrderNumber = ordNum;
				(selectedRow.DataBoundItem as GenClassProp).OrderNumber = ordNum + 1;

				_bindingSource.DataSource = _props.OrderBy(q => q.OrderNumber).ToList();
				_bindingSource.ResetBindings(false);

				dgv.ClearSelection();
				dgv.Rows[rowIndex + 1].Cells[0].Selected = true;

			}
			catch { }
		}

		private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			////For editing
			//int rowIndex = e.RowIndex;
			//DataGridViewRow selectedRow = (sender as DataGridView).Rows[rowIndex];

			//GenClsProp currentClass = selectedRow.DataBoundItem as GenClsProp;

			//txtPropertyName.Text = currentClass.PropertyName;
			//cmbDataType.Text = currentClass.DataType.ToString();
			//chkIsPrimaryKey.Checked = currentClass.IsPrimaryKey;
			//chkIsAutoFill.Checked = currentClass.IsAutoFill;
			//chkIsDisplay.Checked = currentClass.IsDisplay;
			//chkIsMandatory.Checked = currentClass.IsMandatory;
			//chkIsUniqueKey.Checked = currentClass.IsUniqueKey;
			//txtPropertyName.Focus();
		}


	}
}
