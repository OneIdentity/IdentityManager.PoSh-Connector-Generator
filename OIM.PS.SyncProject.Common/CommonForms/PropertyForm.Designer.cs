
namespace OIM.PS.SyncProject.Common.CommonForms
{
	partial class PropertyForm
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
			components = new System.ComponentModel.Container();
			btnCancel = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			chkIsUniqueKey = new System.Windows.Forms.CheckBox();
			chkIsDisplay = new System.Windows.Forms.CheckBox();
			chkIsMandatory = new System.Windows.Forms.CheckBox();
			chkIsAutoFill = new System.Windows.Forms.CheckBox();
			chkIsPrimaryKey = new System.Windows.Forms.CheckBox();
			label4 = new System.Windows.Forms.Label();
			cmbDataType = new System.Windows.Forms.ComboBox();
			label3 = new System.Windows.Forms.Label();
			txtPropertyName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			chkIsMultiValue = new System.Windows.Forms.CheckBox();
			chkIsRevision = new System.Windows.Forms.CheckBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			lblManyToMany = new System.Windows.Forms.Label();
			chkIncludeInPrimary = new System.Windows.Forms.CheckBox();
			chkCombinedPrimary = new System.Windows.Forms.CheckBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// btnCancel
			// 
			btnCancel.Location = new System.Drawing.Point(501, 142);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(86, 31);
			btnCancel.TabIndex = 22;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// btnSave
			// 
			btnSave.Location = new System.Drawing.Point(409, 142);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(86, 31);
			btnSave.TabIndex = 21;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// chkIsUniqueKey
			// 
			chkIsUniqueKey.AutoSize = true;
			chkIsUniqueKey.Location = new System.Drawing.Point(258, 281);
			chkIsUniqueKey.Name = "chkIsUniqueKey";
			chkIsUniqueKey.Size = new System.Drawing.Size(112, 24);
			chkIsUniqueKey.TabIndex = 36;
			chkIsUniqueKey.Text = "IsUniqueKey";
			chkIsUniqueKey.UseVisualStyleBackColor = true;
			// 
			// chkIsDisplay
			// 
			chkIsDisplay.AutoSize = true;
			chkIsDisplay.Location = new System.Drawing.Point(135, 250);
			chkIsDisplay.Name = "chkIsDisplay";
			chkIsDisplay.Size = new System.Drawing.Size(90, 24);
			chkIsDisplay.TabIndex = 35;
			chkIsDisplay.Text = "IsDisplay";
			chkIsDisplay.UseVisualStyleBackColor = true;
			// 
			// chkIsMandatory
			// 
			chkIsMandatory.AutoSize = true;
			chkIsMandatory.Location = new System.Drawing.Point(135, 281);
			chkIsMandatory.Name = "chkIsMandatory";
			chkIsMandatory.Size = new System.Drawing.Size(113, 24);
			chkIsMandatory.TabIndex = 34;
			chkIsMandatory.Text = "IsMandatory";
			chkIsMandatory.UseVisualStyleBackColor = true;
			// 
			// chkIsAutoFill
			// 
			chkIsAutoFill.AutoSize = true;
			chkIsAutoFill.Location = new System.Drawing.Point(18, 281);
			chkIsAutoFill.Name = "chkIsAutoFill";
			chkIsAutoFill.Size = new System.Drawing.Size(92, 24);
			chkIsAutoFill.TabIndex = 33;
			chkIsAutoFill.Text = "IsAutoFill";
			chkIsAutoFill.UseVisualStyleBackColor = true;
			// 
			// chkIsPrimaryKey
			// 
			chkIsPrimaryKey.AutoSize = true;
			chkIsPrimaryKey.Location = new System.Drawing.Point(18, 250);
			chkIsPrimaryKey.Name = "chkIsPrimaryKey";
			chkIsPrimaryKey.Size = new System.Drawing.Size(115, 24);
			chkIsPrimaryKey.TabIndex = 32;
			chkIsPrimaryKey.Text = "IsPrimaryKey";
			chkIsPrimaryKey.UseVisualStyleBackColor = true;
			chkIsPrimaryKey.CheckedChanged += chkIsPrimaryKey_CheckedChanged;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(409, 67);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(76, 20);
			label4.TabIndex = 31;
			label4.Text = "Data Type";
			// 
			// cmbDataType
			// 
			cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbDataType.FormattingEnabled = true;
			cmbDataType.Items.AddRange(new object[] { "String", "Bool", "DateTime", "Int" });
			cmbDataType.Location = new System.Drawing.Point(409, 91);
			cmbDataType.Name = "cmbDataType";
			cmbDataType.Size = new System.Drawing.Size(178, 28);
			cmbDataType.TabIndex = 30;
			cmbDataType.SelectedIndexChanged += cmbDataType_SelectedIndexChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(16, 67);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(109, 20);
			label3.TabIndex = 29;
			label3.Text = "Property Name";
			// 
			// txtPropertyName
			// 
			txtPropertyName.Location = new System.Drawing.Point(16, 92);
			txtPropertyName.Name = "txtPropertyName";
			txtPropertyName.Size = new System.Drawing.Size(380, 27);
			txtPropertyName.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			label1.Location = new System.Drawing.Point(184, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(174, 37);
			label1.TabIndex = 37;
			label1.Text = "Add Property";
			// 
			// chkIsMultiValue
			// 
			chkIsMultiValue.AutoSize = true;
			chkIsMultiValue.Location = new System.Drawing.Point(259, 250);
			chkIsMultiValue.Name = "chkIsMultiValue";
			chkIsMultiValue.Size = new System.Drawing.Size(111, 24);
			chkIsMultiValue.TabIndex = 38;
			chkIsMultiValue.Text = "IsMultiValue";
			chkIsMultiValue.UseVisualStyleBackColor = true;
			// 
			// chkIsRevision
			// 
			chkIsRevision.AutoSize = true;
			chkIsRevision.Location = new System.Drawing.Point(376, 281);
			chkIsRevision.Name = "chkIsRevision";
			chkIsRevision.Size = new System.Drawing.Size(96, 24);
			chkIsRevision.TabIndex = 39;
			chkIsRevision.Text = "IsRevision";
			chkIsRevision.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(lblManyToMany);
			groupBox1.Controls.Add(chkIncludeInPrimary);
			groupBox1.Controls.Add(chkCombinedPrimary);
			groupBox1.Location = new System.Drawing.Point(20, 140);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(383, 88);
			groupBox1.TabIndex = 40;
			groupBox1.TabStop = false;
			groupBox1.Text = "Many-To-Many (Membership)";
			// 
			// lblManyToMany
			// 
			lblManyToMany.AutoSize = true;
			lblManyToMany.BackColor = System.Drawing.Color.DeepSkyBlue;
			lblManyToMany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblManyToMany.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			lblManyToMany.ForeColor = System.Drawing.Color.Red;
			lblManyToMany.Location = new System.Drawing.Point(356, 23);
			lblManyToMany.Name = "lblManyToMany";
			lblManyToMany.Size = new System.Drawing.Size(20, 27);
			lblManyToMany.TabIndex = 2;
			lblManyToMany.Text = "!";
			toolTip1.SetToolTip(lblManyToMany, "Many-to-Many record configuration");
			lblManyToMany.Click += lblManyToMany_Click;
			// 
			// chkIncludeInPrimary
			// 
			chkIncludeInPrimary.Location = new System.Drawing.Point(164, 26);
			chkIncludeInPrimary.Name = "chkIncludeInPrimary";
			chkIncludeInPrimary.Size = new System.Drawing.Size(186, 60);
			chkIncludeInPrimary.TabIndex = 1;
			chkIncludeInPrimary.Text = "Field to Include in Combined Primary Key\r\n";
			chkIncludeInPrimary.UseVisualStyleBackColor = true;
			chkIncludeInPrimary.CheckedChanged += chkIncludeInPrimary_CheckedChanged;
			// 
			// chkCombinedPrimary
			// 
			chkCombinedPrimary.Location = new System.Drawing.Point(12, 26);
			chkCombinedPrimary.Name = "chkCombinedPrimary";
			chkCombinedPrimary.Size = new System.Drawing.Size(158, 60);
			chkCombinedPrimary.TabIndex = 0;
			chkCombinedPrimary.Text = "Combined (virtual) Primary Key";
			chkCombinedPrimary.UseVisualStyleBackColor = true;
			chkCombinedPrimary.CheckedChanged += chkCombinedPrimary_CheckedChanged;
			// 
			// PropertyForm
			// 
			AcceptButton = btnSave;
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new System.Drawing.Size(610, 325);
			Controls.Add(groupBox1);
			Controls.Add(chkIsRevision);
			Controls.Add(chkIsMultiValue);
			Controls.Add(label1);
			Controls.Add(chkIsUniqueKey);
			Controls.Add(chkIsDisplay);
			Controls.Add(chkIsMandatory);
			Controls.Add(chkIsAutoFill);
			Controls.Add(chkIsPrimaryKey);
			Controls.Add(label4);
			Controls.Add(cmbDataType);
			Controls.Add(label3);
			Controls.Add(txtPropertyName);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PropertyForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Property";
			Load += PropertyForm_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.CheckBox chkIsUniqueKey;
		private System.Windows.Forms.CheckBox chkIsDisplay;
		private System.Windows.Forms.CheckBox chkIsMandatory;
		private System.Windows.Forms.CheckBox chkIsAutoFill;
		private System.Windows.Forms.CheckBox chkIsPrimaryKey;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbDataType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPropertyName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkIsMultiValue;
		private System.Windows.Forms.CheckBox chkIsRevision;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkCombinedPrimary;
		private System.Windows.Forms.CheckBox chkIncludeInPrimary;
		private System.Windows.Forms.Label lblManyToMany;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}