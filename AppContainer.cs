using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PaJaMa.WinControls
{
	public partial class AppContainer : UserControl
	{
		public AppContainer()
		{
			InitializeComponent();
		}

		[DllImport("user32.dll")]
		private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
		private static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

		[DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hwnd, uint Msg, long wParam, long lParam);
		
		private const int GWL_STYLE = (-16);
		private const int WS_VISIBLE = 0x10000000;
		private const int WM_CLOSE = 0x10;
		
		private IntPtr _appHandle = IntPtr.Zero;
		
		private string _exeName = string.Empty;
		public string EXEName
		{
			get { return _exeName; }
			set
			{
				_exeName = value;
				HostApplication();
			}
		}

		private void HostApplication()
		{
			this.SuspendLayout();
			Process p = new Process();
			ProcessStartInfo psi = new ProcessStartInfo(this.EXEName);
			psi.WindowStyle = ProcessWindowStyle.Normal;
			p.StartInfo = psi;
			p.Start();
			this.ResumeLayout();
			p.WaitForInputIdle(3000);
			_appHandle = p.MainWindowHandle;
			IntPtr old = SetParent(_appHandle, this.Handle);
			SetWindowLong(_appHandle, GWL_STYLE, WS_VISIBLE);
			MoveWindow(_appHandle, 0, 0, this.Width, this.Height, true);
		}

		protected override void OnResize(EventArgs e)
		{
			if (_appHandle != IntPtr.Zero)
			{
				MoveWindow(_appHandle, 0, 0, this.Width, this.Height, true);
			}
			base.OnResize(e);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (_appHandle != IntPtr.Zero)
			{
				PostMessage(_appHandle, WM_CLOSE, 0, 0);
				System.Threading.Thread.Sleep(1000);
				_appHandle = IntPtr.Zero;
			}
			base.OnHandleDestroyed(e);
		}
	}
}
