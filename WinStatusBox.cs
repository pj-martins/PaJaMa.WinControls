using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public partial class WinStatusBox : Form
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern bool HideCaret(IntPtr hWnd);

		public bool AllowCancel
		{
			get { return btnOK.Visible; }
			set { btnOK.Visible = value; }
		}

		public BackgroundWorker BackgroundWorker
		{
			get;
			set;
		}

		public int Progress
		{
			get { return progMain.Value; }
			set { progMain.Value = value; }
		}

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (BackgroundWorker != null && BackgroundWorker.WorkerReportsProgress)
			{
				progMain.Value = e.ProgressPercentage;
				if (e.UserState != null && e.UserState is String)
				{
					txtLines.Text += $"{e.UserState.ToString()}\r\n";
					this.Refresh();
				}
				else if (e.UserState != null && e.UserState is IEnumerable<string>)
				{
					txtLines.Text = string.Join("\r\n", ((IEnumerable<string>)e.UserState).ToArray());
					this.Refresh();
				}
			}
		}

		public WinStatusBox()
		{
			InitializeComponent();
			txtLines.GotFocus += (object sender, EventArgs e) => HideCaret(txtLines.Handle);
		}

		private void WinStatusBox_FormClosing(object sender, FormClosingEventArgs e)
		{
			throw new NotImplementedException();
		}

		public static void ShowProgress(BackgroundWorker worker, string text = "", IWin32Window owner = null,
			bool allowCancel = false, ProgressBarStyle progressBarStyle = ProgressBarStyle.Blocks)
		{
			new WinStatusBox().Show(worker, text, owner, allowCancel, progressBarStyle);
		}

		public void Show(BackgroundWorker worker, string text = "", IWin32Window owner = null,
			bool allowCancel = false, ProgressBarStyle progressBarStyle = ProgressBarStyle.Blocks)
		{
			worker.RunWorkerCompleted -= BackgroundWorker_RunWorkerCompleted;
			worker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
			worker.WorkerReportsProgress = true;
			worker.WorkerSupportsCancellation = true;
			worker.ProgressChanged -= BackgroundWorker_ProgressChanged;
			worker.ProgressChanged += BackgroundWorker_ProgressChanged;
			this.BackgroundWorker = worker;
			this.AllowCancel = allowCancel;
			this.Dock = DockStyle.Fill;
			this.progMain.Style = progressBarStyle;

			this.BackgroundWorker.RunWorkerAsync();
			if (owner != null)
				this.ShowDialog(owner);
			else
				this.ShowDialog();

		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			progMain.Visible = false;
			if (string.IsNullOrEmpty(txtLines.Text.Trim()))
				this.Close();
			else
				btnOK.Visible = true;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
