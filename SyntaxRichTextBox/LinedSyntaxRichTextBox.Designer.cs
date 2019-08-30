namespace PaJaMa.WinControls.SyntaxRichTextBox
{
	partial class LinedSyntaxRichTextBox
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
			this.txtLines = new System.Windows.Forms.TextBox();
			this.TextBox = new PaJaMa.WinControls.SyntaxRichTextBox.SyntaxRichTextBox();
			this.SuspendLayout();
			// 
			// txtLines
			// 
			this.txtLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLines.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtLines.Location = new System.Drawing.Point(0, 0);
			this.txtLines.Multiline = true;
			this.txtLines.Name = "txtLines";
			this.txtLines.ReadOnly = true;
			this.txtLines.Size = new System.Drawing.Size(45, 592);
			this.txtLines.TabIndex = 0;
			// 
			// TextBox
			// 
			this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBox.Location = new System.Drawing.Point(45, 0);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(609, 592);
			this.TextBox.TabIndex = 1;
			this.TextBox.Text = "";
			this.TextBox.VScroll += new System.EventHandler(this.SyntaxRichTextBox1_VScroll);
			this.TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
			// 
			// LinedSyntaxRichTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.TextBox);
			this.Controls.Add(this.txtLines);
			this.Name = "LinedSyntaxRichTextBox";
			this.Size = new System.Drawing.Size(654, 592);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtLines;
		public SyntaxRichTextBox TextBox;
	}
}
