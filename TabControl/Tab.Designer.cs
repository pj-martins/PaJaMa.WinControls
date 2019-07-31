namespace PaJaMa.WinControls.TabControl
{
	partial class Tab
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
			this.components = new System.ComponentModel.Container();
			this.btnRemove = new System.Windows.Forms.Button();
			this.lblTabText = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolCloseOthers = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.BackColor = System.Drawing.Color.Transparent;
			this.btnRemove.FlatAppearance.BorderSize = 0;
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemove.Location = new System.Drawing.Point(36, 1);
			this.btnRemove.Margin = new System.Windows.Forms.Padding(0);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnRemove.Size = new System.Drawing.Size(18, 21);
			this.btnRemove.TabIndex = 0;
			this.btnRemove.TabStop = false;
			this.btnRemove.Text = "x";
			this.btnRemove.UseCompatibleTextRendering = true;
			this.btnRemove.UseVisualStyleBackColor = false;
			this.btnRemove.Visible = false;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// lblTabText
			// 
			this.lblTabText.AutoSize = true;
			this.lblTabText.BackColor = System.Drawing.Color.Transparent;
			this.lblTabText.Location = new System.Drawing.Point(3, 5);
			this.lblTabText.Name = "lblTabText";
			this.lblTabText.Size = new System.Drawing.Size(35, 13);
			this.lblTabText.TabIndex = 1;
			this.lblTabText.Text = "label1";
			this.lblTabText.Click += new System.EventHandler(this.ctrl_Click);
			this.lblTabText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tab_MouseMove);
			this.lblTabText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tab_MouseUp);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCloseOthers});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(142, 26);
			// 
			// toolCloseOthers
			// 
			this.toolCloseOthers.Name = "toolCloseOthers";
			this.toolCloseOthers.Size = new System.Drawing.Size(141, 22);
			this.toolCloseOthers.Text = "Close &Others";
			this.toolCloseOthers.Click += new System.EventHandler(this.toolCloseOthers_Click);
			// 
			// Tab
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Controls.Add(this.lblTabText);
			this.Controls.Add(this.btnRemove);
			this.Name = "Tab";
			this.Size = new System.Drawing.Size(55, 20);
			this.Click += new System.EventHandler(this.ctrl_Click);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Tab_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Tab_DragEnter);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tab_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tab_MouseUp);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label lblTabText;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolCloseOthers;
	}
}
