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
		public Tab Tab { get; internal set; }
		public string TooltipText { get; set; }

		internal event EventHandler TabTextChanged;

		private string _text;
		public override string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				TabTextChanged?.Invoke(this, new EventArgs());
			}
		}

        public TabPage(string text) : this()
		{
			this.Text = text;
		}

		public TabPage()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(SystemColors.Control))
				e.Graphics.FillRectangle(brush, ClientRectangle);

			var pen = new Pen(SystemColors.ActiveBorder);
			e.Graphics.DrawLine(pen, 0, 0, this.Tab.Left, 0);
			e.Graphics.DrawLine(pen, this.Tab.Left + this.Tab.Width, 0, ClientSize.Width - 1, 0);
			e.Graphics.DrawLine(pen, 0, 0, 0, ClientSize.Height - 1);
			e.Graphics.DrawLine(pen, ClientSize.Width - 1, 0, ClientSize.Width - 1, ClientSize.Height - 1);
			e.Graphics.DrawLine(pen, 0, ClientSize.Height - 1, ClientSize.Width - 1, ClientSize.Height - 1);
		}
	}
}
