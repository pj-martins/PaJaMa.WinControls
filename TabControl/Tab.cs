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
	internal partial class Tab : UserControl
	{
		public bool IsSelected { get; set; }
		public event EventHandler TabSelected;
		public event EventHandler TabRemoving;

		private bool _allowRemove;
		public bool AllowRemove
		{
			get { return _allowRemove; }
			set
			{
				_allowRemove = value;
				redraw();
			}
		}

		private TabPage _tabPage;
		public TabPage TabPage
		{
			get { return _tabPage; }
			set
			{
				_tabPage = value;
				_tabPage.TabTextChanged += delegate (object sender, EventArgs e) { redraw(); };
				redraw();
			}
		}

		public Tab()
		{
			InitializeComponent();
			lblTabText.Text = string.Empty;
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}

		private void redraw()
		{
			this.SuspendLayout();
			btnRemove.Visible = AllowRemove;
			this.lblTabText.Text = _tabPage.Text;
			this.Width = this.lblTabText.Width + (AllowRemove ? btnRemove.Width : 0) + 3;
			this.ResumeLayout();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var color = IsSelected ? Color.White : SystemColors.Control;
			using (SolidBrush brush = new SolidBrush(color))
				e.Graphics.FillRectangle(brush, ClientRectangle);
			// e.Graphics.DrawRectangle(Pens.Yellow, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
			var pen = new Pen(SystemColors.ActiveBorder);
			e.Graphics.DrawLine(pen, 0, 0, ClientSize.Width - 1, 0);
			e.Graphics.DrawLine(pen, 0, 0, 0, ClientSize.Height - 1);
			e.Graphics.DrawLine(pen, ClientSize.Width - 1, 0, ClientSize.Width - 1, ClientSize.Height - 1);
		}

		private void ctrl_Click(object sender, EventArgs e)
		{
			TabSelected?.Invoke(this, e);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			TabRemoving?.Invoke(this, e);
		}
	}
}
