
namespace OIM.PS.SyncProject.GeneratorUI
{
    partial class PSSyncProjectGeneratorForm
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
            this.components = new System.ComponentModel.Container();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewClasses = new System.Windows.Forms.DataGridView();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Properties = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.btnImportMetadata = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGenerate.Location = new System.Drawing.Point(548, 158);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(152, 90);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.butnGenerate_Click);
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Location = new System.Drawing.Point(124, 462);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.ReadOnly = true;
            this.textBoxOutputFile.Size = new System.Drawing.Size(576, 23);
            this.textBoxOutputFile.TabIndex = 6;
            this.textBoxOutputFile.Text = "C:\\1Identity\\Projects\\TestProjects\\OneIMSyncGenerator\\Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Parameters";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamName,
            this.Description});
            this.dataGridView1.Location = new System.Drawing.Point(26, 155);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(472, 93);
            this.dataGridView1.TabIndex = 4;
            // 
            // ParamName
            // 
            this.ParamName.DataPropertyName = "ParamName";
            this.ParamName.FillWeight = 150F;
            this.ParamName.HeaderText = "Name";
            this.ParamName.Name = "ParamName";
            this.ParamName.ReadOnly = true;
            this.ParamName.Width = 150;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 250F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 250;
            // 
            // dataGridViewClasses
            // 
            this.dataGridViewClasses.AllowUserToAddRows = false;
            this.dataGridViewClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClasses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClassName,
            this.Properties});
            this.dataGridViewClasses.Location = new System.Drawing.Point(26, 276);
            this.dataGridViewClasses.Name = "dataGridViewClasses";
            this.dataGridViewClasses.RowTemplate.Height = 25;
            this.dataGridViewClasses.Size = new System.Drawing.Size(674, 180);
            this.dataGridViewClasses.TabIndex = 8;
            // 
            // ClassName
            // 
            this.ClassName.DataPropertyName = "ClassName";
            this.ClassName.FillWeight = 150F;
            this.ClassName.HeaderText = "Class";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            this.ClassName.Width = 150;
            // 
            // Properties
            // 
            this.Properties.DataPropertyName = "PropertyNames";
            this.Properties.FillWeight = 450F;
            this.Properties.HeaderText = "Properties";
            this.Properties.Name = "Properties";
            this.Properties.ReadOnly = true;
            this.Properties.Width = 450;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Classes";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(26, 461);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(92, 23);
            this.btnFile.TabIndex = 11;
            this.btnFile.Text = "Output Folder";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(107, 63);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(391, 23);
            this.txtNamespace.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Namespace";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Class Name";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(107, 92);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(391, 23);
            this.txtClassName.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(124, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(489, 45);
            this.label5.TabIndex = 17;
            this.label5.Text = "PoweShell Sync Project Metadata";
            // 
            // btnImportMetadata
            // 
            this.btnImportMetadata.BackColor = System.Drawing.Color.LightBlue;
            this.btnImportMetadata.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnImportMetadata.Location = new System.Drawing.Point(548, 62);
            this.btnImportMetadata.Name = "btnImportMetadata";
            this.btnImportMetadata.Size = new System.Drawing.Size(150, 90);
            this.btnImportMetadata.TabIndex = 18;
            this.btnImportMetadata.Text = "Import Metadata";
            this.btnImportMetadata.UseVisualStyleBackColor = false;
            this.btnImportMetadata.Click += new System.EventHandler(this.btnImportMetadata_Click);
            // 
            // PSSyncProjectGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 505);
            this.Controls.Add(this.btnImportMetadata);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.dataGridViewClasses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOutputFile);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PSSyncProjectGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XML Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClasses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridViewClasses;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnImportMetadata;
    }
}

