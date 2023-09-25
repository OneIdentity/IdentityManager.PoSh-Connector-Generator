
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkIsUniqueKey = new System.Windows.Forms.CheckBox();
            this.chkIsDisplay = new System.Windows.Forms.CheckBox();
            this.chkIsMandatory = new System.Windows.Forms.CheckBox();
            this.chkIsAutoFill = new System.Windows.Forms.CheckBox();
            this.chkIsPrimaryKey = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIsMultiValue = new System.Windows.Forms.CheckBox();
            this.chkIsRevision = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(462, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 29);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(369, 179);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 29);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkIsUniqueKey
            // 
            this.chkIsUniqueKey.AutoSize = true;
            this.chkIsUniqueKey.Location = new System.Drawing.Point(258, 164);
            this.chkIsUniqueKey.Name = "chkIsUniqueKey";
            this.chkIsUniqueKey.Size = new System.Drawing.Size(104, 23);
            this.chkIsUniqueKey.TabIndex = 36;
            this.chkIsUniqueKey.Text = "IsUniqueKey";
            this.chkIsUniqueKey.UseVisualStyleBackColor = true;
            // 
            // chkIsDisplay
            // 
            this.chkIsDisplay.AutoSize = true;
            this.chkIsDisplay.Location = new System.Drawing.Point(135, 135);
            this.chkIsDisplay.Name = "chkIsDisplay";
            this.chkIsDisplay.Size = new System.Drawing.Size(82, 23);
            this.chkIsDisplay.TabIndex = 35;
            this.chkIsDisplay.Text = "IsDisplay";
            this.chkIsDisplay.UseVisualStyleBackColor = true;
            // 
            // chkIsMandatory
            // 
            this.chkIsMandatory.AutoSize = true;
            this.chkIsMandatory.Location = new System.Drawing.Point(135, 164);
            this.chkIsMandatory.Name = "chkIsMandatory";
            this.chkIsMandatory.Size = new System.Drawing.Size(106, 23);
            this.chkIsMandatory.TabIndex = 34;
            this.chkIsMandatory.Text = "IsMandatory";
            this.chkIsMandatory.UseVisualStyleBackColor = true;
            // 
            // chkIsAutoFill
            // 
            this.chkIsAutoFill.AutoSize = true;
            this.chkIsAutoFill.Location = new System.Drawing.Point(18, 164);
            this.chkIsAutoFill.Name = "chkIsAutoFill";
            this.chkIsAutoFill.Size = new System.Drawing.Size(84, 23);
            this.chkIsAutoFill.TabIndex = 33;
            this.chkIsAutoFill.Text = "IsAutoFill";
            this.chkIsAutoFill.UseVisualStyleBackColor = true;
            // 
            // chkIsPrimaryKey
            // 
            this.chkIsPrimaryKey.AutoSize = true;
            this.chkIsPrimaryKey.Location = new System.Drawing.Point(18, 135);
            this.chkIsPrimaryKey.Name = "chkIsPrimaryKey";
            this.chkIsPrimaryKey.Size = new System.Drawing.Size(107, 23);
            this.chkIsPrimaryKey.TabIndex = 32;
            this.chkIsPrimaryKey.Text = "IsPrimaryKey";
            this.chkIsPrimaryKey.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 19);
            this.label4.TabIndex = 31;
            this.label4.Text = "Data Type";
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Items.AddRange(new object[] {
            "String",
            "Bool",
            "DateTime",
            "Int"});
            this.cmbDataType.Location = new System.Drawing.Point(409, 86);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(138, 27);
            this.cmbDataType.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 29;
            this.label3.Text = "Property Name";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(16, 87);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(354, 26);
            this.txtPropertyName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(184, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 30);
            this.label1.TabIndex = 37;
            this.label1.Text = "Add Property";
            // 
            // chkIsMultiValue
            // 
            this.chkIsMultiValue.AutoSize = true;
            this.chkIsMultiValue.Location = new System.Drawing.Point(259, 135);
            this.chkIsMultiValue.Name = "chkIsMultiValue";
            this.chkIsMultiValue.Size = new System.Drawing.Size(103, 23);
            this.chkIsMultiValue.TabIndex = 38;
            this.chkIsMultiValue.Text = "IsMultiValue";
            this.chkIsMultiValue.UseVisualStyleBackColor = true;
            // 
            // chkIsRevision
            // 
            this.chkIsRevision.AutoSize = true;
            this.chkIsRevision.Location = new System.Drawing.Point(369, 135);
            this.chkIsRevision.Name = "chkIsRevision";
            this.chkIsRevision.Size = new System.Drawing.Size(88, 23);
            this.chkIsRevision.TabIndex = 39;
            this.chkIsRevision.Text = "IsRevision";
            this.chkIsRevision.UseVisualStyleBackColor = true;
            // 
            // PropertyForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(562, 221);
            this.Controls.Add(this.chkIsRevision);
            this.Controls.Add(this.chkIsMultiValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkIsUniqueKey);
            this.Controls.Add(this.chkIsDisplay);
            this.Controls.Add(this.chkIsMandatory);
            this.Controls.Add(this.chkIsAutoFill);
            this.Controls.Add(this.chkIsPrimaryKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDataType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPropertyName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Property";
            this.Load += new System.EventHandler(this.PropertyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}