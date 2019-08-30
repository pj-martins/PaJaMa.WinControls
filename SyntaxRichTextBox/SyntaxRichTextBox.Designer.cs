﻿namespace PaJaMa.WinControls.SyntaxRichTextBox
{
	partial class SyntaxRichTextBox
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
			this.txtQuery = new System.Windows.Forms.RichTextBox();
			this.txtLines = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtQuery
			// 
			this.txtQuery.AcceptsTab = true;
			this.txtQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtQuery.Location = new System.Drawing.Point(35, 0);
			this.txtQuery.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(782, 644);
			this.txtQuery.TabIndex = 2;
			this.txtQuery.Text = "";
			this.txtQuery.VScroll += new System.EventHandler(this.TxtQuery_VScroll);
			this.txtQuery.SizeChanged += new System.EventHandler(this.TxtQuery_SizeChanged);
			this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
			this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
			this.txtQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyUp);
			this.txtQuery.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtQuery_MouseUp);
			// 
			// txtLines
			// 
			this.txtLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLines.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtLines.Location = new System.Drawing.Point(0, 0);
			this.txtLines.Margin = new System.Windows.Forms.Padding(0, 3, 10, 0);
			this.txtLines.Multiline = true;
			this.txtLines.Name = "txtLines";
			this.txtLines.ReadOnly = true;
			this.txtLines.Size = new System.Drawing.Size(35, 644);
			this.txtLines.TabIndex = 3;
			this.txtLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.txtQuery);
			this.panel1.Controls.Add(this.txtLines);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(821, 648);
			this.panel1.TabIndex = 4;
			// 
			// SyntaxRichTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "SyntaxRichTextBox";
			this.Size = new System.Drawing.Size(821, 648);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtQuery;
		private System.Windows.Forms.TextBox txtLines;
		private System.Windows.Forms.Panel panel1;
	}
}