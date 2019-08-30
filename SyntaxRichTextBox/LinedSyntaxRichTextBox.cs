using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaJaMa.Common;

namespace PaJaMa.WinControls.SyntaxRichTextBox
{
	public partial class LinedSyntaxRichTextBox : UserControl
	{
		public LinedSyntaxRichTextBox()
		{
			InitializeComponent();
			_dispatcher = new DebounceDispatcher();
		}

		private bool _bounceScroll = false;
		private DebounceDispatcher _dispatcher;

		private void SyntaxRichTextBox1_VScroll(object sender, EventArgs e)
		{
			if (_bounceScroll)
			{
				_dispatcher.Debounce(300, x =>
				{
					this.Invoke(new Action(() =>
					{
						DrawLineNumbers();
						_bounceScroll = false;
					}));
				});
			}
			else
			{
				DrawLineNumbers();
			}
		}

		public void DrawLineNumbers()
		{
			txtLines.SuspendLayout();
			int firstIndex = TextBox.GetCharIndexFromPosition(new Point(0, 0));
			int firstLine = TextBox.GetLineFromCharIndex(firstIndex);
			int lastIndex = TextBox.GetCharIndexFromPosition(new Point(TextBox.ClientRectangle.Width, TextBox.ClientRectangle.Height));
			int lastLine = TextBox.GetLineFromCharIndex(lastIndex);
			txtLines.Text = string.Empty;
			for (int i = firstLine; i <= lastLine + 2; i++)
			{
				txtLines.Text += (i + 1) + " \r\n";
			}
			txtLines.ResumeLayout();
		}

		private void TextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				DrawLineNumbers();
			}
			else if (e.KeyCode == Keys.V && e.Control)
			{
				_bounceScroll = true;
			}
		}
	}
}
