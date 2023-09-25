
namespace OIM.PS.SyncProject.GeneratorUI
{
	partial class MainGeneratorForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			btnGenerate = new System.Windows.Forms.Button();
			textBoxOutputFile = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			butnAddParameter = new System.Windows.Forms.Button();
			butnDelete = new System.Windows.Forms.Button();
			dataGridViewClasses = new System.Windows.Forms.DataGridView();
			ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Properties = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label2 = new System.Windows.Forms.Label();
			btnDeleteClass = new System.Windows.Forms.Button();
			btnAddClass = new System.Windows.Forms.Button();
			btnFile = new System.Windows.Forms.Button();
			txtNamespace = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			txtClassName = new System.Windows.Forms.TextBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			btnEdit = new System.Windows.Forms.Button();
			label5 = new System.Windows.Forms.Label();
			btnImportMetadata = new System.Windows.Forms.Button();
			btnGenerateDefinition = new System.Windows.Forms.Button();
			chkVisualStudio = new System.Windows.Forms.CheckBox();
			btnGeneratePSDefinition = new System.Windows.Forms.Button();
			tabGenerate = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			tabPage2 = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridViewClasses).BeginInit();
			tabGenerate.SuspendLayout();
			tabPage1.SuspendLayout();
			tabPage2.SuspendLayout();
			SuspendLayout();
			// 
			// btnGenerate
			// 
			btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnGenerate.BackColor = System.Drawing.Color.DarkKhaki;
			btnGenerate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			btnGenerate.Location = new System.Drawing.Point(517, 93);
			btnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new System.Drawing.Size(180, 30);
			btnGenerate.TabIndex = 7;
			btnGenerate.Text = "Save Metadata";
			btnGenerate.UseVisualStyleBackColor = false;
			btnGenerate.Click += butnGenerate_Click;
			// 
			// textBoxOutputFile
			// 
			textBoxOutputFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxOutputFile.Location = new System.Drawing.Point(124, 463);
			textBoxOutputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			textBoxOutputFile.Name = "textBoxOutputFile";
			textBoxOutputFile.ReadOnly = true;
			textBoxOutputFile.Size = new System.Drawing.Size(577, 23);
			textBoxOutputFile.TabIndex = 6;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(26, 136);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(66, 15);
			label1.TabIndex = 3;
			label1.Text = "Parameters";
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ParamName, Description });
			dataGridView1.Location = new System.Drawing.Point(26, 156);
			dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersWidth = 51;
			dataGridView1.RowTemplate.Height = 25;
			dataGridView1.Size = new System.Drawing.Size(472, 97);
			dataGridView1.TabIndex = 4;
			// 
			// ParamName
			// 
			ParamName.DataPropertyName = "ParamName";
			ParamName.FillWeight = 150F;
			ParamName.HeaderText = "Name";
			ParamName.MinimumWidth = 6;
			ParamName.Name = "ParamName";
			ParamName.ReadOnly = true;
			ParamName.Width = 150;
			// 
			// Description
			// 
			Description.DataPropertyName = "Description";
			Description.FillWeight = 250F;
			Description.HeaderText = "Description";
			Description.MinimumWidth = 6;
			Description.Name = "Description";
			Description.ReadOnly = true;
			Description.Width = 250;
			// 
			// butnAddParameter
			// 
			butnAddParameter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			butnAddParameter.Location = new System.Drawing.Point(342, 128);
			butnAddParameter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			butnAddParameter.Name = "butnAddParameter";
			butnAddParameter.Size = new System.Drawing.Size(75, 22);
			butnAddParameter.TabIndex = 2;
			butnAddParameter.Text = "Add";
			butnAddParameter.UseVisualStyleBackColor = true;
			butnAddParameter.Click += butnAddParameter_Click;
			// 
			// butnDelete
			// 
			butnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			butnDelete.Location = new System.Drawing.Point(423, 128);
			butnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			butnDelete.Name = "butnDelete";
			butnDelete.Size = new System.Drawing.Size(75, 22);
			butnDelete.TabIndex = 3;
			butnDelete.Text = "Delete";
			butnDelete.UseVisualStyleBackColor = true;
			butnDelete.Click += butnDelete_Click;
			// 
			// dataGridViewClasses
			// 
			dataGridViewClasses.AllowUserToAddRows = false;
			dataGridViewClasses.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dataGridViewClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewClasses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ClassName, Properties });
			dataGridViewClasses.Location = new System.Drawing.Point(26, 291);
			dataGridViewClasses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			dataGridViewClasses.Name = "dataGridViewClasses";
			dataGridViewClasses.RowHeadersWidth = 51;
			dataGridViewClasses.RowTemplate.Height = 25;
			dataGridViewClasses.Size = new System.Drawing.Size(675, 166);
			dataGridViewClasses.TabIndex = 8;
			// 
			// ClassName
			// 
			ClassName.DataPropertyName = "ClassName";
			ClassName.FillWeight = 150F;
			ClassName.HeaderText = "Class";
			ClassName.MinimumWidth = 6;
			ClassName.Name = "ClassName";
			ClassName.ReadOnly = true;
			ClassName.Width = 150;
			// 
			// Properties
			// 
			Properties.DataPropertyName = "PropertyNames";
			Properties.FillWeight = 450F;
			Properties.HeaderText = "Properties";
			Properties.MinimumWidth = 6;
			Properties.Name = "Properties";
			Properties.ReadOnly = true;
			Properties.Width = 450;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(26, 272);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(45, 15);
			label2.TabIndex = 7;
			label2.Text = "Classes";
			// 
			// btnDeleteClass
			// 
			btnDeleteClass.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnDeleteClass.Location = new System.Drawing.Point(626, 262);
			btnDeleteClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnDeleteClass.Name = "btnDeleteClass";
			btnDeleteClass.Size = new System.Drawing.Size(75, 22);
			btnDeleteClass.TabIndex = 5;
			btnDeleteClass.Text = "Delete";
			btnDeleteClass.UseVisualStyleBackColor = true;
			btnDeleteClass.Click += btnDeleteClass_Click;
			// 
			// btnAddClass
			// 
			btnAddClass.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnAddClass.Location = new System.Drawing.Point(464, 262);
			btnAddClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnAddClass.Name = "btnAddClass";
			btnAddClass.Size = new System.Drawing.Size(75, 22);
			btnAddClass.TabIndex = 4;
			btnAddClass.Text = "Add";
			btnAddClass.UseVisualStyleBackColor = true;
			btnAddClass.Click += btnAddClass_Click;
			// 
			// btnFile
			// 
			btnFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnFile.Location = new System.Drawing.Point(26, 460);
			btnFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnFile.Name = "btnFile";
			btnFile.Size = new System.Drawing.Size(92, 22);
			btnFile.TabIndex = 11;
			btnFile.Text = "Output Folder";
			btnFile.UseVisualStyleBackColor = true;
			btnFile.Click += btnFile_Click;
			// 
			// txtNamespace
			// 
			txtNamespace.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtNamespace.Location = new System.Drawing.Point(107, 62);
			txtNamespace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			txtNamespace.Name = "txtNamespace";
			txtNamespace.Size = new System.Drawing.Size(391, 23);
			txtNamespace.TabIndex = 0;
			txtNamespace.Text = "OIMPSSyncConnector";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(26, 67);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(69, 15);
			label3.TabIndex = 13;
			label3.Text = "Namespace";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(26, 96);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 15);
			label4.TabIndex = 15;
			label4.Text = "Class Name";
			// 
			// txtClassName
			// 
			txtClassName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtClassName.Location = new System.Drawing.Point(107, 92);
			txtClassName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			txtClassName.Name = "txtClassName";
			txtClassName.Size = new System.Drawing.Size(391, 23);
			txtClassName.TabIndex = 1;
			txtClassName.Text = "OIMPSSyncConnectorClass";
			// 
			// toolTip1
			// 
			toolTip1.IsBalloon = true;
			toolTip1.ShowAlways = true;
			toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// btnEdit
			// 
			btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnEdit.Location = new System.Drawing.Point(545, 262);
			btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new System.Drawing.Size(75, 22);
			btnEdit.TabIndex = 16;
			btnEdit.Text = "Edit";
			btnEdit.UseVisualStyleBackColor = true;
			btnEdit.Click += btnEdit_Click;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			label5.Location = new System.Drawing.Point(124, -2);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(489, 45);
			label5.TabIndex = 17;
			label5.Text = "PoweShell Sync Project Metadata";
			// 
			// btnImportMetadata
			// 
			btnImportMetadata.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnImportMetadata.Location = new System.Drawing.Point(517, 62);
			btnImportMetadata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnImportMetadata.Name = "btnImportMetadata";
			btnImportMetadata.Size = new System.Drawing.Size(180, 22);
			btnImportMetadata.TabIndex = 18;
			btnImportMetadata.Text = "Import Metadata";
			btnImportMetadata.UseVisualStyleBackColor = true;
			btnImportMetadata.Click += btnImportMetadata_Click;
			// 
			// btnGenerateDefinition
			// 
			btnGenerateDefinition.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnGenerateDefinition.BackColor = System.Drawing.SystemColors.ActiveCaption;
			btnGenerateDefinition.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			btnGenerateDefinition.Location = new System.Drawing.Point(5, 14);
			btnGenerateDefinition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnGenerateDefinition.Name = "btnGenerateDefinition";
			btnGenerateDefinition.Size = new System.Drawing.Size(166, 34);
			btnGenerateDefinition.TabIndex = 19;
			btnGenerateDefinition.Text = "Generate .Net Definition";
			btnGenerateDefinition.UseVisualStyleBackColor = false;
			btnGenerateDefinition.Click += btnGenerateDefinition_Click;
			// 
			// chkVisualStudio
			// 
			chkVisualStudio.Location = new System.Drawing.Point(17, 52);
			chkVisualStudio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			chkVisualStudio.Name = "chkVisualStudio";
			chkVisualStudio.Size = new System.Drawing.Size(121, 40);
			chkVisualStudio.TabIndex = 20;
			chkVisualStudio.Text = "Build Visual Studio Project";
			chkVisualStudio.UseVisualStyleBackColor = true;
			// 
			// btnGeneratePSDefinition
			// 
			btnGeneratePSDefinition.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnGeneratePSDefinition.BackColor = System.Drawing.SystemColors.ActiveCaption;
			btnGeneratePSDefinition.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			btnGeneratePSDefinition.Location = new System.Drawing.Point(5, 14);
			btnGeneratePSDefinition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			btnGeneratePSDefinition.Name = "btnGeneratePSDefinition";
			btnGeneratePSDefinition.Size = new System.Drawing.Size(163, 74);
			btnGeneratePSDefinition.TabIndex = 21;
			btnGeneratePSDefinition.Text = "Generate PowerShell Definition";
			btnGeneratePSDefinition.UseVisualStyleBackColor = false;
			btnGeneratePSDefinition.Click += btnGeneratePSDefinition_Click;
			// 
			// tabGenerate
			// 
			tabGenerate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			tabGenerate.Controls.Add(tabPage1);
			tabGenerate.Controls.Add(tabPage2);
			tabGenerate.Location = new System.Drawing.Point(517, 128);
			tabGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			tabGenerate.Name = "tabGenerate";
			tabGenerate.SelectedIndex = 0;
			tabGenerate.Size = new System.Drawing.Size(184, 124);
			tabGenerate.TabIndex = 22;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(chkVisualStudio);
			tabPage1.Controls.Add(btnGenerateDefinition);
			tabPage1.Location = new System.Drawing.Point(4, 24);
			tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			tabPage1.Size = new System.Drawing.Size(176, 96);
			tabPage1.TabIndex = 0;
			tabPage1.Text = ".NET";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(btnGeneratePSDefinition);
			tabPage2.Location = new System.Drawing.Point(4, 24);
			tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			tabPage2.Size = new System.Drawing.Size(176, 96);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "PowerShell";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// MainGeneratorForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(738, 511);
			Controls.Add(tabGenerate);
			Controls.Add(btnImportMetadata);
			Controls.Add(label5);
			Controls.Add(btnEdit);
			Controls.Add(label4);
			Controls.Add(txtClassName);
			Controls.Add(label3);
			Controls.Add(txtNamespace);
			Controls.Add(btnFile);
			Controls.Add(btnDeleteClass);
			Controls.Add(btnAddClass);
			Controls.Add(dataGridViewClasses);
			Controls.Add(label2);
			Controls.Add(butnDelete);
			Controls.Add(butnAddParameter);
			Controls.Add(dataGridView1);
			Controls.Add(label1);
			Controls.Add(textBoxOutputFile);
			Controls.Add(btnGenerate);
			Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			MaximizeBox = false;
			MinimumSize = new System.Drawing.Size(571, 460);
			Name = "MainGeneratorForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "XML Generator";
			Load += Form1_Load;
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridViewClasses).EndInit();
			tabGenerate.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.TextBox textBoxOutputFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button butnAddParameter;
		private System.Windows.Forms.Button butnDelete;
		private System.Windows.Forms.DataGridView dataGridViewClasses;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnDeleteClass;
		private System.Windows.Forms.Button btnAddClass;
		private System.Windows.Forms.Button btnFile;
		private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Description;
		private System.Windows.Forms.TextBox txtNamespace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtClassName;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Properties;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnImportMetadata;
		private System.Windows.Forms.Button btnGenerateDefinition;
		private System.Windows.Forms.CheckBox chkVisualStudio;
		private System.Windows.Forms.Button btnGeneratePSDefinition;
		private System.Windows.Forms.TabControl tabGenerate;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
	}
}

