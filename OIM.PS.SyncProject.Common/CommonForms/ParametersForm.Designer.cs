
namespace OIM.PS.SyncProject.Common
{
	partial class ParametersForm
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
			label3 = new System.Windows.Forms.Label();
			txtDescription = new System.Windows.Forms.TextBox();
			btnSave = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			chkIsSensibleData = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			// 
			// txtName
			// 
			txtName.Location = new System.Drawing.Point(116, 49);
			txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(318, 27);
			txtName.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			label1.Location = new System.Drawing.Point(128, 12);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(224, 32);
			label1.TabIndex = 1;
			label1.Text = "Add new parameter";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(57, 53);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(49, 20);
			label2.TabIndex = 2;
			label2.Text = "Name";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(25, 92);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(85, 20);
			label3.TabIndex = 4;
			label3.Text = "Description";
			// 
			// txtDescription
			// 
			txtDescription.Location = new System.Drawing.Point(116, 88);
			txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			txtDescription.Name = "txtDescription";
			txtDescription.Size = new System.Drawing.Size(318, 27);
			txtDescription.TabIndex = 3;
			// 
			// btnSave
			// 
			btnSave.Location = new System.Drawing.Point(248, 157);
			btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(86, 31);
			btnSave.TabIndex = 5;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnCancel
			// 
			btnCancel.Location = new System.Drawing.Point(348, 157);
			btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(86, 31);
			btnCancel.TabIndex = 6;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// chkIsSensibleData
			// 
			chkIsSensibleData.AutoSize = true;
			chkIsSensibleData.Location = new System.Drawing.Point(116, 122);
			chkIsSensibleData.Name = "chkIsSensibleData";
			chkIsSensibleData.Size = new System.Drawing.Size(72, 24);
			chkIsSensibleData.TabIndex = 7;
			chkIsSensibleData.Text = "Secret";
			chkIsSensibleData.UseVisualStyleBackColor = true;
			// 
			// ParametersForm
			// 
			AcceptButton = btnSave;
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new System.Drawing.Size(462, 203);
			Controls.Add(chkIsSensibleData);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(label3);
			Controls.Add(txtDescription);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(txtName);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			MaximizeBox = false;
			Name = "ParametersForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "New Parameter";
			Load += ParametersForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkIsSensibleData;
	}
}