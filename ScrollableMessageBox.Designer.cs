namespace PaJaMa.WinControls
{
	partial class ScrollableMessageBox
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
			this.btnOK = new System.Windows.Forms.Button();
			this.txtLines = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnYesAll = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.btnNoAll = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnOK.Location = new System.Drawing.Point(45, 0);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 27);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btn_Click);
			// 
			// txtLines
			// 
			this.txtLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLines.BackColor = System.Drawing.Color.White;
			this.txtLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLines.Location = new System.Drawing.Point(12, 12);
			this.txtLines.Multiline = true;
			this.txtLines.Name = "txtLines";
			this.txtLines.ReadOnly = true;
			this.txtLines.Size = new System.Drawing.Size(471, 287);
			this.txtLines.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.btnYes);
			this.panel1.Controls.Add(this.btnYesAll);
			this.panel1.Controls.Add(this.btnNo);
			this.panel1.Controls.Add(this.btnNoAll);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 313);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(495, 27);
			this.panel1.TabIndex = 2;
			// 
			// btnYes
			// 
			this.btnYes.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnYes.Location = new System.Drawing.Point(120, 0);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 27);
			this.btnYes.TabIndex = 8;
			this.btnYes.Tag = "Yes";
			this.btnYes.Text = "Yes";
			this.btnYes.UseVisualStyleBackColor = true;
			this.btnYes.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnYesAll
			// 
			this.btnYesAll.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnYesAll.Location = new System.Drawing.Point(195, 0);
			this.btnYesAll.Name = "btnYesAll";
			this.btnYesAll.Size = new System.Drawing.Size(75, 27);
			this.btnYesAll.TabIndex = 7;
			this.btnYesAll.Tag = "YesToAll";
			this.btnYesAll.Text = "Yes (All)";
			this.btnYesAll.UseVisualStyleBackColor = true;
			this.btnYesAll.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnNo
			// 
			this.btnNo.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnNo.Location = new System.Drawing.Point(270, 0);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(75, 27);
			this.btnNo.TabIndex = 6;
			this.btnNo.Tag = "No";
			this.btnNo.Text = "No";
			this.btnNo.UseVisualStyleBackColor = true;
			this.btnNo.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnNoAll
			// 
			this.btnNoAll.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnNoAll.Location = new System.Drawing.Point(345, 0);
			this.btnNoAll.Name = "btnNoAll";
			this.btnNoAll.Size = new System.Drawing.Size(75, 27);
			this.btnNoAll.TabIndex = 5;
			this.btnNoAll.Tag = "NoToAll";
			this.btnNoAll.Text = "No (All)";
			this.btnNoAll.UseVisualStyleBackColor = true;
			this.btnNoAll.Click += new System.EventHandler(this.btn_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCancel.Location = new System.Drawing.Point(420, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 27);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Tag = "Cancel";
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btn_Click);
			// 
			// MessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(495, 340);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.txtLines);
			this.Name = "MessageBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.ScrollableMessageBox_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtLines;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnYesAll;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Button btnNoAll;
		private System.Windows.Forms.Button btnCancel;
	}
}