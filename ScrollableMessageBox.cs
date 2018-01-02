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
		public ScrollableMessageBox()
		{
			InitializeComponent();
		}

		public static DialogResult ShowDialog(string[] lines, string caption = "")
		{
			return show(lines, caption, true);
		}

		public static void Show(string[] lines, string caption = "")
		{
			show(lines, caption, false);
		}

		private static DialogResult show(string[] lines, string caption, bool isDialog)
		{
			var msg = new ScrollableMessageBox();
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
				return msg.ShowDialog();
			msg.Show();
			return DialogResult.None;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void ScrollableMessageBox_Load(object sender, EventArgs e)
		{
			btnOK.Select();
		}
	}
}
