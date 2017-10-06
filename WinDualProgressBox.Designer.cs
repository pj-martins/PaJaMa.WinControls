namespace PaJaMa.WinControls
{
	partial class WinDualProgressBox
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
			this.progIndividual = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblProgress = new System.Windows.Forms.Label();
			this.progTotal = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// progIndividual
			// 
			this.progIndividual.Location = new System.Drawing.Point(15, 34);
			this.progIndividual.Name = "progIndividual";
			this.progIndividual.Size = new System.Drawing.Size(528, 23);
			this.progIndividual.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(468, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblProgress
			// 
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(12, 9);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(0, 13);
			this.lblProgress.TabIndex = 2;
			// 
			// progTotal
			// 
			this.progTotal.Location = new System.Drawing.Point(15, 77);
			this.progTotal.Name = "progTotal";
			this.progTotal.Size = new System.Drawing.Size(528, 23);
			this.progTotal.TabIndex = 3;
			// 
			// frmProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 157);
			this.ControlBox = false;
			this.Controls.Add(this.progTotal);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.progIndividual);
			this.Name = "frmProgress";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progIndividual;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.ProgressBar progTotal;
	}
}