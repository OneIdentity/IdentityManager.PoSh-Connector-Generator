
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsAutoFill = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsDisplay = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsMandatory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsUniqueKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsMultiValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsRevision = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(14, 89);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(325, 27);
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add new class with properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Class Name";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(724, 604);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 31);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(817, 604);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 31);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.DataType,
            this.IsPrimaryKey,
            this.IsAutoFill,
            this.IsDisplay,
            this.IsMandatory,
            this.IsUniqueKey,
            this.IsMultiValue,
            this.IsRevision});
            this.dataGridView1.Location = new System.Drawing.Point(14, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(886, 453);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // PropertyName
            // 
            this.PropertyName.DataPropertyName = "PropertyName";
            this.PropertyName.FillWeight = 150F;
            this.PropertyName.HeaderText = "Property Name";
            this.PropertyName.MinimumWidth = 6;
            this.PropertyName.Name = "PropertyName";
            this.PropertyName.Width = 150;
            // 
            // DataType
            // 
            this.DataType.DataPropertyName = "DataType";
            this.DataType.HeaderText = "Data Type";
            this.DataType.MinimumWidth = 6;
            this.DataType.Name = "DataType";
            this.DataType.Width = 125;
            // 
            // IsPrimaryKey
            // 
            this.IsPrimaryKey.DataPropertyName = "IsPrimaryKey";
            this.IsPrimaryKey.FalseValue = "false";
            this.IsPrimaryKey.FillWeight = 50F;
            this.IsPrimaryKey.HeaderText = "Primary Key";
            this.IsPrimaryKey.MinimumWidth = 6;
            this.IsPrimaryKey.Name = "IsPrimaryKey";
            this.IsPrimaryKey.TrueValue = "true";
            this.IsPrimaryKey.Width = 50;
            // 
            // IsAutoFill
            // 
            this.IsAutoFill.DataPropertyName = "IsAutoFill";
            this.IsAutoFill.FalseValue = "false";
            this.IsAutoFill.FillWeight = 50F;
            this.IsAutoFill.HeaderText = "Auto Fill";
            this.IsAutoFill.MinimumWidth = 6;
            this.IsAutoFill.Name = "IsAutoFill";
            this.IsAutoFill.TrueValue = "true";
            this.IsAutoFill.Width = 50;
            // 
            // IsDisplay
            // 
            this.IsDisplay.DataPropertyName = "IsDisplay";
            this.IsDisplay.FalseValue = "false";
            this.IsDisplay.FillWeight = 50F;
            this.IsDisplay.HeaderText = "Display";
            this.IsDisplay.MinimumWidth = 6;
            this.IsDisplay.Name = "IsDisplay";
            this.IsDisplay.TrueValue = "true";
            this.IsDisplay.Width = 50;
            // 
            // IsMandatory
            // 
            this.IsMandatory.DataPropertyName = "IsMandatory";
            this.IsMandatory.FalseValue = "false";
            this.IsMandatory.FillWeight = 70F;
            this.IsMandatory.HeaderText = "Mandatory";
            this.IsMandatory.MinimumWidth = 6;
            this.IsMandatory.Name = "IsMandatory";
            this.IsMandatory.TrueValue = "true";
            this.IsMandatory.Width = 70;
            // 
            // IsUniqueKey
            // 
            this.IsUniqueKey.DataPropertyName = "IsUniqueKey";
            this.IsUniqueKey.FalseValue = "false";
            this.IsUniqueKey.FillWeight = 50F;
            this.IsUniqueKey.HeaderText = "Unique Key";
            this.IsUniqueKey.MinimumWidth = 6;
            this.IsUniqueKey.Name = "IsUniqueKey";
            this.IsUniqueKey.TrueValue = "true";
            this.IsUniqueKey.Width = 50;
            // 
            // IsMultiValue
            // 
            this.IsMultiValue.DataPropertyName = "IsMultiValue";
            this.IsMultiValue.FalseValue = "false";
            this.IsMultiValue.HeaderText = "Multi Value";
            this.IsMultiValue.MinimumWidth = 6;
            this.IsMultiValue.Name = "IsMultiValue";
            this.IsMultiValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsMultiValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsMultiValue.TrueValue = "true";
            this.IsMultiValue.Width = 125;
            // 
            // IsRevision
            // 
            this.IsRevision.DataPropertyName = "IsRevision";
            this.IsRevision.FalseValue = "false";
            this.IsRevision.HeaderText = "Revision";
            this.IsRevision.MinimumWidth = 6;
            this.IsRevision.Name = "IsRevision";
            this.IsRevision.TrueValue = "true";
            this.IsRevision.Width = 125;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(666, 89);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(111, 31);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(784, 89);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(117, 31);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(378, 586);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(111, 31);
            this.btnUp.TabIndex = 22;
            this.btnUp.Text = "^ Move Up ^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(501, 586);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(111, 31);
            this.btnDown.TabIndex = 23;
            this.btnDown.Text = "v Move Down v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // SyncClassForm
            // 
            this.AcceptButton = this.btnUp;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 646);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "SyncClassForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Class";
            this.Load += new System.EventHandler(this.PSyncClassForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAutoFill;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDisplay;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMandatory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsUniqueKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMultiValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsRevision;
    }
}