using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public class FormSettings
	{
		public int MainFormLeft { get; set; }
		public int MainFormTop { get; set; }
		public bool MainFormMaximized { get; set; }
		public int MainFormWidth { get; set; }
		public int MainFormHeight { get; set; }

		public static void LoadSettings(Form form)
		{
			var formSettings = SettingsHelper.GetUserSettings<FormSettings>(form.GetType().Name);
			if (formSettings != null)
			{
				var minLeft = Screen.AllScreens.Select(s => s.Bounds.X).Min();
				var minTop = Screen.AllScreens.Select(s => s.Bounds.Y).Min();
				if (formSettings.MainFormLeft < minLeft)
					formSettings.MainFormLeft = minLeft;

				if (formSettings.MainFormTop < minTop)
					formSettings.MainFormTop = minTop;

				form.DesktopLocation = new Point(formSettings.MainFormLeft, formSettings.MainFormTop);
				if (formSettings.MainFormMaximized)
					form.WindowState = FormWindowState.Maximized;
				else
				{
					if (formSettings.MainFormHeight > 0)
						form.Height = formSettings.MainFormHeight;
					if (formSettings.MainFormWidth > 0)
						form.Width = formSettings.MainFormWidth;
				}
			}
		}

		public static void SaveSettings(Form form)
		{
			if (form.WindowState == FormWindowState.Minimized) return;
			var formSettings = SettingsHelper.GetUserSettings<FormSettings>() ?? new FormSettings();
			formSettings.MainFormLeft = form.DesktopLocation.X;
			formSettings.MainFormTop = form.DesktopLocation.Y;
			if (form.WindowState == FormWindowState.Maximized)
				formSettings.MainFormMaximized = true;
			else
			{
				formSettings.MainFormMaximized = false;
				formSettings.MainFormWidth = form.Width;
				formSettings.MainFormHeight = form.Height;
			}
			SettingsHelper.SaveUserSettings<FormSettings>(formSettings, form.GetType().Name);
		}
	}
}
