﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public partial class WinProgressBox : UserControl
	{
		public bool AllowCancel
		{
			get { return btnCancel.Visible; }
			set { btnCancel.Visible = value; }
		}

		public BackgroundWorker BackgroundWorker
		{
			get;
			set;
		}

		public string ProgressText
		{
			get { return lblProgress.Text; }
			set { lblProgress.Text = value; }
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
					lblProgress.Visible = true;
					lblProgress.Text = e.UserState.ToString();
					this.Refresh();
				}
			}
		}

		public WinProgressBox()
		{
			InitializeComponent();
		}

		public event EventHandler Cancel;

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (BackgroundWorker != null && BackgroundWorker.IsBusy)
			{
				btnCancel.Enabled = false;
			}
			if (BackgroundWorker != null && BackgroundWorker.WorkerSupportsCancellation)
			{
				BackgroundWorker.CancelAsync();
			}
			if (Cancel != null)
			{
				Cancel(this, e);
			}
		}

		private Form _progressForm = null;
		public static DialogResult ShowProgress(BackgroundWorker worker, string text = "", IWin32Window owner = null,
			bool allowCancel = false, ProgressBarStyle progressBarStyle = ProgressBarStyle.Blocks)
		{
			return new WinProgressBox().Show(worker, text, owner, allowCancel, progressBarStyle);
		}

		public DialogResult Show(BackgroundWorker worker, string text = "", IWin32Window owner = null,
			bool allowCancel = false, ProgressBarStyle progressBarStyle = ProgressBarStyle.Blocks)
		{
			_progressForm = new Form();
			_progressForm.ControlBox = false;
			_progressForm.Size = new Size(400, 100);
			_progressForm.StartPosition = FormStartPosition.CenterScreen;
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

			_progressForm.Controls.Add(this);


			this.BackgroundWorker.RunWorkerAsync();
			if (owner != null)
			{
				return _progressForm.ShowDialog(owner);
			}
			else
			{
				return _progressForm.ShowDialog();
			}

		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			progMain.Value = (e.Cancelled ? 0 : 100);
			btnCancel.Enabled = true;
			if (_progressForm != null)
			{
				_progressForm.DialogResult = (e.Cancelled ? DialogResult.Cancel : DialogResult.OK);
				_progressForm.Close();
				_progressForm.Dispose();
			}
		}
	}
}
