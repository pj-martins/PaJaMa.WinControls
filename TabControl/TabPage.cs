using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls.TabControl
{
	[Serializable()]
	public partial class TabPage : Panel
	{
		public bool IsSelected { get; set; }
		public override string Text { get; set; }
		public int TabLeft { get; set; }
		public int TabRight { get; set; }

		public TabPage(string text) : this()
		{
			this.Text = text;
		}

		public TabPage()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(SystemColors.Control))
				e.Graphics.FillRectangle(brush, ClientRectangle);

			var pen = new Pen(SystemColors.ActiveBorder);
			e.Graphics.DrawLine(pen, 0, 0, TabLeft, 0);
			e.Graphics.DrawLine(pen, TabRight, 0, ClientSize.Width - 1, 0);
			e.Graphics.DrawLine(pen, 0, 0, 0, ClientSize.Height - 1);
			e.Graphics.DrawLine(pen, ClientSize.Width - 1, 0, ClientSize.Width - 1, ClientSize.Height - 1);
			e.Graphics.DrawLine(pen, 0, ClientSize.Height - 1, ClientSize.Width - 1, ClientSize.Height - 1);
		}
	}
}
