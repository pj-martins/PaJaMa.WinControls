using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public partial class YesNoMessageDialog : Form
	{
		public new YesNoMessageDialogResult DialogResult { get; private set; }

		public YesNoMessageDialog()
		{
			InitializeComponent();
		}

		private void btn_Click(object sender, EventArgs e)
		{
			var btn = sender as Button;
			var result = (YesNoMessageDialogResult)Enum.Parse(typeof(YesNoMessageDialogResult), btn.Tag.ToString());
			closeDialog(result);
		}

		private void closeDialog(YesNoMessageDialogResult result)
		{
			DialogResult = result;
			this.Close();
		}

		public static YesNoMessageDialogResult Show(string message, string caption, bool showYes = true, bool showYesToAll = true,
			bool showNo = true, bool showNoToAll = true, bool showCancel = true)
		{
			using (var dlg = new YesNoMessageDialog())
			{
				dlg.Text = caption;
				dlg.lblMessage.Text = message;
				dlg.btnYes.Visible = showYes;
				dlg.btnYesAll.Visible = showYesToAll;
				dlg.btnNo.Visible = showNo;
				dlg.btnNoAll.Visible = showNoToAll;
				dlg.btnCancel.Visible = showCancel;
				dlg.ShowDialog();
				return dlg.DialogResult;
			}
		}
	}

	public enum YesNoMessageDialogResult
	{
		Yes,
		YesToAll,
		No,
		NoToAll,
		Cancel
	}
}
