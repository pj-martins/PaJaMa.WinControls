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
			this.pnlAdd = new System.Windows.Forms.Panel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.btnLeft = new System.Windows.Forms.Button();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.btnRight = new System.Windows.Forms.Button();
			this.pnlPages = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnlTabs.SuspendLayout();
			this.pnlAdd.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.pnlRight.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTabs
			// 
			this.pnlTabs.AllowDrop = true;
			this.pnlTabs.Controls.Add(this.pnlAdd);
			this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTabs.Location = new System.Drawing.Point(0, 0);
			this.pnlTabs.Name = "pnlTabs";
			this.pnlTabs.Size = new System.Drawing.Size(577, 23);
			this.pnlTabs.TabIndex = 0;
			this.pnlTabs.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlTabs_DragDrop);
			this.pnlTabs.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlTabs_DragEnter);
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
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.btnLeft);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlLeft.Location = new System.Drawing.Point(577, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(20, 23);
			this.pnlLeft.TabIndex = 2;
			this.pnlLeft.Visible = false;
			// 
			// btnLeft
			// 
			this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLeft.Location = new System.Drawing.Point(2, 1);
			this.btnLeft.Margin = new System.Windows.Forms.Padding(0);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnLeft.Size = new System.Drawing.Size(18, 22);
			this.btnLeft.TabIndex = 1;
			this.btnLeft.Text = "<";
			this.btnLeft.UseVisualStyleBackColor = false;
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// pnlRight
			// 
			this.pnlRight.Controls.Add(this.btnRight);
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlRight.Location = new System.Drawing.Point(597, 0);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(20, 23);
			this.pnlRight.TabIndex = 1;
			this.pnlRight.Visible = false;
			// 
			// btnRight
			// 
			this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRight.Location = new System.Drawing.Point(2, 1);
			this.btnRight.Margin = new System.Windows.Forms.Padding(0);
			this.btnRight.Name = "btnRight";
			this.btnRight.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnRight.Size = new System.Drawing.Size(18, 22);
			this.btnRight.TabIndex = 1;
			this.btnRight.Text = ">";
			this.btnRight.UseVisualStyleBackColor = false;
			this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
			// 
			// pnlPages
			// 
			this.pnlPages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPages.Location = new System.Drawing.Point(0, 23);
			this.pnlPages.Name = "pnlPages";
			this.pnlPages.Padding = new System.Windows.Forms.Padding(1);
			this.pnlPages.Size = new System.Drawing.Size(617, 433);
			this.pnlPages.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pnlTabs);
			this.panel1.Controls.Add(this.pnlLeft);
			this.panel1.Controls.Add(this.pnlRight);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(617, 23);
			this.panel1.TabIndex = 1;
			// 
			// TabControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlPages);
			this.Controls.Add(this.panel1);
			this.Name = "TabControl";
			this.Size = new System.Drawing.Size(617, 456);
			this.Load += new System.EventHandler(this.TabControl_Load);
			this.pnlTabs.ResumeLayout(false);
			this.pnlAdd.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.pnlRight.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlPages;
		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Panel pnlRight;
		private System.Windows.Forms.Button btnRight;
		private System.Windows.Forms.Panel pnlTabs;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Panel panel1;
	}
}
