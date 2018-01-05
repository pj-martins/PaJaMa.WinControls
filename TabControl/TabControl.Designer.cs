namespace PaJaMa.WinControls.TabControl
{
	partial class TabControl
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
			this.pnlTabs = new System.Windows.Forms.Panel();
			this.pnlPages = new System.Windows.Forms.Panel();
			this.pnlAdd = new System.Windows.Forms.Panel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlTabs.SuspendLayout();
			this.pnlAdd.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTabs
			// 
			this.pnlTabs.Controls.Add(this.pnlAdd);
			this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTabs.Location = new System.Drawing.Point(0, 0);
			this.pnlTabs.Name = "pnlTabs";
			this.pnlTabs.Size = new System.Drawing.Size(617, 23);
			this.pnlTabs.TabIndex = 0;
			// 
			// pnlPages
			// 
			this.pnlPages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPages.Location = new System.Drawing.Point(0, 23);
			this.pnlPages.Name = "pnlPages";
			this.pnlPages.Size = new System.Drawing.Size(617, 433);
			this.pnlPages.TabIndex = 0;
			// 
			// pnlAdd
			// 
			this.pnlAdd.Controls.Add(this.btnAdd);
			this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlAdd.Location = new System.Drawing.Point(0, 0);
			this.pnlAdd.Name = "pnlAdd";
			this.pnlAdd.Size = new System.Drawing.Size(20, 23);
			this.pnlAdd.TabIndex = 0;
			this.pnlAdd.Visible = false;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(2, 1);
			this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnAdd.Size = new System.Drawing.Size(18, 22);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = false;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// TabControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlPages);
			this.Controls.Add(this.pnlTabs);
			this.Name = "TabControl";
			this.Size = new System.Drawing.Size(617, 456);
			this.pnlTabs.ResumeLayout(false);
			this.pnlAdd.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTabs;
		private System.Windows.Forms.Panel pnlPages;
		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.Button btnAdd;
	}
}
