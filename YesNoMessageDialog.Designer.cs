namespace PaJaMa.WinControls
{
	partial class YesNoMessageDialog
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnYesAll = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.btnNoAll = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.btnYes);
			this.panel1.Controls.Add(this.btnYesAll);
			this.panel1.Controls.Add(this.btnNo);
			this.panel1.Controls.Add(this.btnNoAll);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 124);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(478, 27);
			this.panel1.TabIndex = 0;
			// 
			// btnYes
			// 
			this.btnYes.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnYes.Location = new System.Drawing.Point(103, 0);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 27);
			this.btnYes.TabIndex = 4;
			this.btnYes.Tag = "Yes";
			this.btnYes.Text = "Yes";
			this.btnYes.UseVisualStyleBackColor = true;
			this.btnYes.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnYesAll
			// 
			this.btnYesAll.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnYesAll.Location = new System.Drawing.Point(178, 0);
			this.btnYesAll.Name = "btnYesAll";
			this.btnYesAll.Size = new System.Drawing.Size(75, 27);
			this.btnYesAll.TabIndex = 3;
			this.btnYesAll.Tag = "YesToAll";
			this.btnYesAll.Text = "Yes (All)";
			this.btnYesAll.UseVisualStyleBackColor = true;
			this.btnYesAll.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnNo
			// 
			this.btnNo.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnNo.Location = new System.Drawing.Point(253, 0);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(75, 27);
			this.btnNo.TabIndex = 2;
			this.btnNo.Tag = "No";
			this.btnNo.Text = "No";
			this.btnNo.UseVisualStyleBackColor = true;
			this.btnNo.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnNoAll
			// 
			this.btnNoAll.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnNoAll.Location = new System.Drawing.Point(328, 0);
			this.btnNoAll.Name = "btnNoAll";
			this.btnNoAll.Size = new System.Drawing.Size(75, 27);
			this.btnNoAll.TabIndex = 1;
			this.btnNoAll.Tag = "NoToAll";
			this.btnNoAll.Text = "No (All)";
			this.btnNoAll.UseVisualStyleBackColor = true;
			this.btnNoAll.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCancel.Location = new System.Drawing.Point(403, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 27);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Tag = "Cancel";
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btn_Click);
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(15, 13);
			this.lblMessage.MaximumSize = new System.Drawing.Size(451, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(35, 13);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = "label1";
			// 
			// YesNoMessageDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(478, 151);
			this.ControlBox = false;
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "YesNoMessageDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MessageDialog";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnYesAll;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Button btnNoAll;
	}
}