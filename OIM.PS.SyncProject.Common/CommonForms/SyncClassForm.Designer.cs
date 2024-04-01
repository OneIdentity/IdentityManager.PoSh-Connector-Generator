
namespace OIM.PS.SyncProject.Common
{
	partial class SyncClassForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			txtName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			btnSave = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			btnAdd = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnUp = new System.Windows.Forms.Button();
			btnDown = new System.Windows.Forms.Button();
			chkManyToMany = new System.Windows.Forms.CheckBox();
			PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			IsPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsCombinedPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IncludeInCombinedPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsAutoFill = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsDisplay = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsMandatory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsUniqueKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsMultiValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			IsRevision = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// txtName
			// 
			txtName.Location = new System.Drawing.Point(14, 89);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(325, 27);
			txtName.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			label1.Location = new System.Drawing.Point(14, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(331, 32);
			label1.TabIndex = 1;
			label1.Text = "Add new class with properties";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 66);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(86, 20);
			label2.TabIndex = 2;
			label2.Text = "Class Name";
			// 
			// btnSave
			// 
			btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnSave.Location = new System.Drawing.Point(843, 576);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(86, 31);
			btnSave.TabIndex = 5;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.Location = new System.Drawing.Point(936, 576);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(86, 31);
			btnCancel.TabIndex = 6;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { PropertyName, DataType, IsPrimaryKey, IsCombinedPrimaryKey, IncludeInCombinedPrimaryKey, IsAutoFill, IsDisplay, IsMandatory, IsUniqueKey, IsMultiValue, IsRevision });
			dataGridView1.Location = new System.Drawing.Point(14, 126);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersWidth = 51;
			dataGridView1.RowTemplate.Height = 25;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(1005, 425);
			dataGridView1.TabIndex = 7;
			dataGridView1.RowEnter += dataGridView1_RowEnter;
			// 
			// btnAdd
			// 
			btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnAdd.Location = new System.Drawing.Point(785, 89);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new System.Drawing.Size(111, 31);
			btnAdd.TabIndex = 20;
			btnAdd.Text = "Add";
			btnAdd.UseVisualStyleBackColor = true;
			btnAdd.Click += btnAdd_Click;
			// 
			// btnDelete
			// 
			btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnDelete.Location = new System.Drawing.Point(903, 89);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(117, 31);
			btnDelete.TabIndex = 21;
			btnDelete.Text = "Delete Selected";
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// btnUp
			// 
			btnUp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnUp.Location = new System.Drawing.Point(497, 558);
			btnUp.Name = "btnUp";
			btnUp.Size = new System.Drawing.Size(111, 31);
			btnUp.TabIndex = 22;
			btnUp.Text = "^ Move Up ^";
			btnUp.UseVisualStyleBackColor = true;
			btnUp.Click += btnUp_Click;
			// 
			// btnDown
			// 
			btnDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnDown.Location = new System.Drawing.Point(620, 558);
			btnDown.Name = "btnDown";
			btnDown.Size = new System.Drawing.Size(111, 31);
			btnDown.TabIndex = 23;
			btnDown.Text = "v Move Down v";
			btnDown.UseVisualStyleBackColor = true;
			btnDown.Click += btnDown_Click;
			// 
			// chkManyToMany
			// 
			chkManyToMany.AutoSize = true;
			chkManyToMany.Location = new System.Drawing.Point(375, 92);
			chkManyToMany.Name = "chkManyToMany";
			chkManyToMany.Size = new System.Drawing.Size(192, 24);
			chkManyToMany.TabIndex = 24;
			chkManyToMany.Text = "Is Many-To-Many record";
			chkManyToMany.UseVisualStyleBackColor = true;
			// 
			// PropertyName
			// 
			PropertyName.DataPropertyName = "PropertyName";
			PropertyName.FillWeight = 150F;
			PropertyName.HeaderText = "Property Name";
			PropertyName.MinimumWidth = 6;
			PropertyName.Name = "PropertyName";
			PropertyName.Width = 150;
			// 
			// DataType
			// 
			DataType.DataPropertyName = "DataType";
			DataType.HeaderText = "Data Type";
			DataType.MinimumWidth = 6;
			DataType.Name = "DataType";
			DataType.Width = 125;
			// 
			// IsPrimaryKey
			// 
			IsPrimaryKey.DataPropertyName = "IsPrimaryKey";
			IsPrimaryKey.FalseValue = "false";
			IsPrimaryKey.FillWeight = 50F;
			IsPrimaryKey.HeaderText = "Primary Key";
			IsPrimaryKey.MinimumWidth = 6;
			IsPrimaryKey.Name = "IsPrimaryKey";
			IsPrimaryKey.TrueValue = "true";
			IsPrimaryKey.Width = 70;
			// 
			// IsCombinedPrimaryKey
			// 
			IsCombinedPrimaryKey.DataPropertyName = "IsCombinedPrimaryKey";
			IsCombinedPrimaryKey.FalseValue = "false";
			IsCombinedPrimaryKey.HeaderText = "Combined Primary Key";
			IsCombinedPrimaryKey.MinimumWidth = 6;
			IsCombinedPrimaryKey.Name = "IsCombinedPrimaryKey";
			IsCombinedPrimaryKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			IsCombinedPrimaryKey.TrueValue = "true";
			IsCombinedPrimaryKey.Width = 80;
			// 
			// IncludeInCombinedPrimaryKey
			// 
			IncludeInCombinedPrimaryKey.DataPropertyName = "IncludeInCombinedPrimaryKey";
			IncludeInCombinedPrimaryKey.FalseValue = "false";
			IncludeInCombinedPrimaryKey.HeaderText = "Included In Primary Key";
			IncludeInCombinedPrimaryKey.MinimumWidth = 6;
			IncludeInCombinedPrimaryKey.Name = "IncludeInCombinedPrimaryKey";
			IncludeInCombinedPrimaryKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			IncludeInCombinedPrimaryKey.TrueValue = "true";
			IncludeInCombinedPrimaryKey.Width = 70;
			// 
			// IsAutoFill
			// 
			IsAutoFill.DataPropertyName = "IsAutoFill";
			IsAutoFill.FalseValue = "false";
			IsAutoFill.FillWeight = 50F;
			IsAutoFill.HeaderText = "Auto Fill";
			IsAutoFill.MinimumWidth = 6;
			IsAutoFill.Name = "IsAutoFill";
			IsAutoFill.TrueValue = "true";
			IsAutoFill.Width = 50;
			// 
			// IsDisplay
			// 
			IsDisplay.DataPropertyName = "IsDisplay";
			IsDisplay.FalseValue = "false";
			IsDisplay.FillWeight = 50F;
			IsDisplay.HeaderText = "Display";
			IsDisplay.MinimumWidth = 6;
			IsDisplay.Name = "IsDisplay";
			IsDisplay.TrueValue = "true";
			IsDisplay.Width = 70;
			// 
			// IsMandatory
			// 
			IsMandatory.DataPropertyName = "IsMandatory";
			IsMandatory.FalseValue = "false";
			IsMandatory.FillWeight = 70F;
			IsMandatory.HeaderText = "Mandatory";
			IsMandatory.MinimumWidth = 6;
			IsMandatory.Name = "IsMandatory";
			IsMandatory.TrueValue = "true";
			IsMandatory.Width = 125;
			// 
			// IsUniqueKey
			// 
			IsUniqueKey.DataPropertyName = "IsUniqueKey";
			IsUniqueKey.FalseValue = "false";
			IsUniqueKey.FillWeight = 50F;
			IsUniqueKey.HeaderText = "Unique Key";
			IsUniqueKey.MinimumWidth = 6;
			IsUniqueKey.Name = "IsUniqueKey";
			IsUniqueKey.TrueValue = "true";
			IsUniqueKey.Width = 70;
			// 
			// IsMultiValue
			// 
			IsMultiValue.DataPropertyName = "IsMultiValue";
			IsMultiValue.FalseValue = "false";
			IsMultiValue.HeaderText = "Multi Value";
			IsMultiValue.MinimumWidth = 6;
			IsMultiValue.Name = "IsMultiValue";
			IsMultiValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			IsMultiValue.TrueValue = "true";
			IsMultiValue.Width = 70;
			// 
			// IsRevision
			// 
			IsRevision.DataPropertyName = "IsRevision";
			IsRevision.FalseValue = "false";
			IsRevision.HeaderText = "Revision";
			IsRevision.MinimumWidth = 6;
			IsRevision.Name = "IsRevision";
			IsRevision.TrueValue = "true";
			IsRevision.Width = 70;
			// 
			// SyncClassForm
			// 
			AcceptButton = btnUp;
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1037, 618);
			Controls.Add(chkManyToMany);
			Controls.Add(btnDown);
			Controls.Add(btnUp);
			Controls.Add(btnDelete);
			Controls.Add(btnAdd);
			Controls.Add(dataGridView1);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(txtName);
			Name = "SyncClassForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "New Class";
			Load += PSyncClassForm_Load;
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.CheckBox chkManyToMany;
		private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
		private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrimaryKey;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsCombinedPrimaryKey;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeInCombinedPrimaryKey;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsAutoFill;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsDisplay;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsMandatory;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsUniqueKey;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsMultiValue;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsRevision;
	}
}