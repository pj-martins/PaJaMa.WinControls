using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public partial class ScrollableMessageBox : Form
	{
		public new PromptResult DialogResult { get; private set; }

		public ScrollableMessageBox()
		{
			InitializeComponent();
			btnYes.Tag = PromptResult.Yes;
			btnYesAll.Tag = PromptResult.YesToAll;
			btnOK.Tag = PromptResult.OK;
			btnCancel.Tag = PromptResult.Cancel;
			btnNo.Tag = PromptResult.No;
			btnNoAll.Tag = PromptResult.NoToAll;
		}

		public static PromptResult ShowDialog(string text, string caption = "", ScrollableMessageBoxButtons buttons = ScrollableMessageBoxButtons.OK)
		{
			return show(text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries), caption, true, buttons);
		}

		public static PromptResult ShowDialog(string[] lines, string caption = "", ScrollableMessageBoxButtons buttons = ScrollableMessageBoxButtons.OK)
		{
			return show(lines, caption, true, buttons);
		}

		public static void Show(string text, string caption = "")
		{
			show(text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries), caption, false);
		}

		public static void Show(string[] lines, string caption = "")
		{
			show(lines, caption, false);
		}

		private static PromptResult show(string[] lines, string caption, bool isDialog, ScrollableMessageBoxButtons buttons = ScrollableMessageBoxButtons.OK)
		{
			var msg = new ScrollableMessageBox();
			msg.btnYes.Visible = msg.btnNo.Visible =
				buttons == ScrollableMessageBoxButtons.YesNo || buttons == ScrollableMessageBoxButtons.YesNoCancel || buttons == ScrollableMessageBoxButtons.YesAllNoAll || buttons == ScrollableMessageBoxButtons.YesAllNoAllCancel;
			msg.btnYesAll.Visible = msg.btnNoAll.Visible =
				buttons == ScrollableMessageBoxButtons.YesAllNoAll || buttons == ScrollableMessageBoxButtons.YesAllNoAllCancel;
			msg.btnOK.Visible = buttons == ScrollableMessageBoxButtons.OK || buttons == ScrollableMessageBoxButtons.OKCancel;
			msg.btnCancel.Visible = buttons == ScrollableMessageBoxButtons.OKCancel || buttons == ScrollableMessageBoxButtons.YesAllNoAllCancel || buttons == ScrollableMessageBoxButtons.YesNoCancel;
			msg.txtLines.Lines = lines;
			msg.Text = caption;
			var height = lines.Length * 13;
			if (height > 800)
			{
				height = 800;
				msg.txtLines.ScrollBars = ScrollBars.Vertical;
			}
			msg.Height = (msg.Height - msg.txtLines.Height) + height + 7;
			if (isDialog)
			{
				msg.ShowDialog();
				return msg.DialogResult;
			}
			msg.Show();
			return PromptResult.None;
		}

		private void btn_Click(object sender, EventArgs e)
		{
			var btn = sender as Button;
			var result = (PromptResult)btn.Tag;
			DialogResult = result;
			this.Close();
		}

		private void ScrollableMessageBox_Load(object sender, EventArgs e)
		{
			btnOK.Select();
		}
	}

	public enum ScrollableMessageBoxButtons
	{
		OK,
		OKCancel,
		YesNo,
		YesNoCancel,
		YesAllNoAllCancel,
		YesAllNoAll,
	}
}
