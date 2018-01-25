namespace PaJaMa.WinControls
{
    partial class WinStatusBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.btnOK = new System.Windows.Forms.Button();
			this.progMain = new System.Windows.Forms.ProgressBar();
			this.txtLines = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnOK.Location = new System.Drawing.Point(417, 1);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 27);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Visible = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// progMain
			// 
			this.progMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progMain.Location = new System.Drawing.Point(3, 4);
			this.progMain.Name = "progMain";
			this.progMain.Size = new System.Drawing.Size(489, 23);
			this.progMain.TabIndex = 2;
			// 
			// txtLines
			// 
			this.txtLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLines.BackColor = System.Drawing.Color.White;
			this.txtLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLines.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtLines.Location = new System.Drawing.Point(12, 12);
			this.txtLines.Multiline = true;
			this.txtLines.Name = "txtLines";
			this.txtLines.ReadOnly = true;
			this.txtLines.Size = new System.Drawing.Size(462, 226);
			this.txtLines.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.progMain);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 244);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(495, 30);
			this.panel1.TabIndex = 5;
			// 
			// WinStatusBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(495, 274);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.txtLines);
			this.Name = "WinStatusBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ProgressBar progMain;
		private System.Windows.Forms.TextBox txtLines;
		private System.Windows.Forms.Panel panel1;
	}
}
