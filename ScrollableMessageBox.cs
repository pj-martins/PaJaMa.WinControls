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

		public static PromptResult ShowDialog(string text, string caption = "", params ScrollableMessageBoxButtons[] buttons)
		{
			return show(text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries), caption, true, buttons);
		}

		public static PromptResult ShowDialog(string[] lines, string caption = "", params ScrollableMessageBoxButtons[] buttons)
		{
			return show(lines, caption, true, buttons);
		}

		public static void Show(string text, string caption = "")
		{
			show(text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries), caption, false, ScrollableMessageBoxButtons.OK);
		}

		public static void Show(string[] lines, string caption = "")
		{
			show(lines, caption, false, ScrollableMessageBoxButtons.OK);
		}

		private static PromptResult show(string[] lines, string caption, bool isDialog, params ScrollableMessageBoxButtons[] buttons)
		{
			var msg = new ScrollableMessageBox();
			msg.btnYes.Visible = buttons.Contains(ScrollableMessageBoxButtons.Yes);
			msg.btnNo.Visible = buttons.Contains(ScrollableMessageBoxButtons.No);
			msg.btnYesAll.Visible = buttons.Contains(ScrollableMessageBoxButtons.YesToAll);
			msg.btnNoAll.Visible = buttons.Contains(ScrollableMessageBoxButtons.NoToAll);
			msg.btnOK.Visible = buttons.Contains(ScrollableMessageBoxButtons.OK) || buttons.Length < 1;
			msg.btnCancel.Visible = buttons.Contains(ScrollableMessageBoxButtons.Cancel);
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
		Yes,
		No,
		YesToAll,
		NoToAll,
		Cancel
	}
}
