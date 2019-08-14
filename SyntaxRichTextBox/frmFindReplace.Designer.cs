namespace PaJaMa.WinControls.SyntaxRichTextBox
{
	partial class frmFindReplace
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtFind = new System.Windows.Forms.TextBox();
			this.chkMatchCase = new System.Windows.Forms.CheckBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.chkReplace = new System.Windows.Forms.CheckBox();
			this.btnReplace = new System.Windows.Forms.Button();
			this.btnReplaceAll = new System.Windows.Forms.Button();
			this.chkMatchWholeWord = new System.Windows.Forms.CheckBox();
			this.chkRegex = new System.Windows.Forms.CheckBox();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.lblResults = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Find";
			// 
			// txtFind
			// 
			this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFind.Location = new System.Drawing.Point(87, 12);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(363, 20);
			this.txtFind.TabIndex = 1;
			this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtFind_KeyDown);
			// 
			// chkMatchCase
			// 
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.Location = new System.Drawing.Point(87, 64);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(83, 17);
			this.chkMatchCase.TabIndex = 3;
			this.chkMatchCase.Text = "Match Case";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFind.Location = new System.Drawing.Point(375, 126);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 23);
			this.btnFind.TabIndex = 9;
			this.btnFind.Text = "Find";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
			// 
			// txtReplace
			// 
			this.txtReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtReplace.Enabled = false;
			this.txtReplace.Location = new System.Drawing.Point(87, 38);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(363, 20);
			this.txtReplace.TabIndex = 2;
			// 
			// chkReplace
			// 
			this.chkReplace.AutoSize = true;
			this.chkReplace.Location = new System.Drawing.Point(15, 40);
			this.chkReplace.Name = "chkReplace";
			this.chkReplace.Size = new System.Drawing.Size(66, 17);
			this.chkReplace.TabIndex = 10;
			this.chkReplace.Text = "Replace";
			this.chkReplace.UseVisualStyleBackColor = true;
			this.chkReplace.CheckedChanged += new System.EventHandler(this.ChkReplace_CheckedChanged);
			// 
			// btnReplace
			// 
			this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReplace.Enabled = false;
			this.btnReplace.Location = new System.Drawing.Point(114, 126);
			this.btnReplace.Name = "btnReplace";
			this.btnReplace.Size = new System.Drawing.Size(75, 23);
			this.btnReplace.TabIndex = 6;
			this.btnReplace.Text = "Replace";
			this.btnReplace.UseVisualStyleBackColor = true;
			this.btnReplace.Click += new System.EventHandler(this.BtnReplace_Click);
			// 
			// btnReplaceAll
			// 
			this.btnReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReplaceAll.Enabled = false;
			this.btnReplaceAll.Location = new System.Drawing.Point(195, 126);
			this.btnReplaceAll.Name = "btnReplaceAll";
			this.btnReplaceAll.Size = new System.Drawing.Size(93, 23);
			this.btnReplaceAll.TabIndex = 7;
			this.btnReplaceAll.Text = "Replace All";
			this.btnReplaceAll.UseVisualStyleBackColor = true;
			this.btnReplaceAll.Click += new System.EventHandler(this.BtnReplaceAll_Click);
			// 
			// chkMatchWholeWord
			// 
			this.chkMatchWholeWord.AutoSize = true;
			this.chkMatchWholeWord.Location = new System.Drawing.Point(176, 64);
			this.chkMatchWholeWord.Name = "chkMatchWholeWord";
			this.chkMatchWholeWord.Size = new System.Drawing.Size(119, 17);
			this.chkMatchWholeWord.TabIndex = 4;
			this.chkMatchWholeWord.Text = "Match Whole Word";
			this.chkMatchWholeWord.UseVisualStyleBackColor = true;
			// 
			// chkRegex
			// 
			this.chkRegex.AutoSize = true;
			this.chkRegex.Location = new System.Drawing.Point(301, 64);
			this.chkRegex.Name = "chkRegex";
			this.chkRegex.Size = new System.Drawing.Size(122, 17);
			this.chkRegex.TabIndex = 5;
			this.chkRegex.Text = "Regular Expressions";
			this.chkRegex.UseVisualStyleBackColor = true;
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.Location = new System.Drawing.Point(294, 126);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(75, 23);
			this.btnPrevious.TabIndex = 8;
			this.btnPrevious.Text = "Previous";
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
			// 
			// lblResults
			// 
			this.lblResults.AutoSize = true;
			this.lblResults.Location = new System.Drawing.Point(12, 89);
			this.lblResults.Name = "lblResults";
			this.lblResults.Size = new System.Drawing.Size(0, 13);
			this.lblResults.TabIndex = 12;
			// 
			// frmFindReplace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(462, 161);
			this.Controls.Add(this.lblResults);
			this.Controls.Add(this.btnPrevious);
			this.Controls.Add(this.chkRegex);
			this.Controls.Add(this.chkMatchWholeWord);
			this.Controls.Add(this.btnReplaceAll);
			this.Controls.Add(this.btnReplace);
			this.Controls.Add(this.chkReplace);
			this.Controls.Add(this.txtReplace);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.chkMatchCase);
			this.Controls.Add(this.txtFind);
			this.Controls.Add(this.label1);
			this.Name = "frmFindReplace";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find & Replace";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFindReplace_FormClosing);
			this.Load += new System.EventHandler(this.FrmFindReplace_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFind;
		private System.Windows.Forms.CheckBox chkMatchCase;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.TextBox txtReplace;
		private System.Windows.Forms.Button btnReplace;
		private System.Windows.Forms.Button btnReplaceAll;
		private System.Windows.Forms.CheckBox chkMatchWholeWord;
		private System.Windows.Forms.CheckBox chkRegex;
		internal System.Windows.Forms.CheckBox chkReplace;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.Label lblResults;
	}
}