using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public class TabControl : System.Windows.Forms.TabControl
	{
		[DefaultValue(false)]
		public bool AllowAddRemove { get; set; }

		public event TabEventHandler TabClosing;
		public event TabEventHandler TabAdding;

		private TabPage _addPage;
		public TabControl()
		{
			this.ControlAdded += TabControl_ControlAdded;
			this.SelectedIndexChanged += TabControl_SelectedIndexChanged;
		}

		private int _activeIndex = -1;
		private bool _lockIndexChange = false;
		private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (AllowAddRemove)
			{
				if (_lockIndexChange) return;
				_lockIndexChange = true;

				var activeTab = this.SelectedTab;
				if (activeTab == _addPage)
				{
					var tab = new TabPage("New Tab");
					var args = new TabEventArgs(tab);
					TabAdding?.Invoke(this, args);
					if (args.Cancel)
					{
						if (_activeIndex >= 0)
							this.SelectedTab = this.TabPages[_activeIndex];
						_lockIndexChange = false;
						return;
					}
					this.TabPages.Add(tab);
					this.SelectedTab = tab;
				}
				else
					_activeIndex = this.TabPages.IndexOf(activeTab);

				_lockIndexChange = false;
			}
		}

		private bool _lockAdd = false;
		private void TabControl_ControlAdded(object sender, ControlEventArgs e)
		{
			if (AllowAddRemove && e.Control is TabPage)
			{
				if (_lockAdd) return;
				_lockAdd = true;
				if (_addPage == null)
				{
					_addPage = new TabPage("+");
					this.TabPages.Add(_addPage);
				}

				// move to end
				this.TabPages.Remove(_addPage);
				this.TabPages.Add(_addPage);

				e.Control.Text += "   x";
				_lockAdd = false;
			}
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			for (int i = 0; i < TabCount; i++)
			{
				var r = GetTabRect(i);
				var closeButton = new Rectangle(r.Right - 15, r.Top + 4, 14, 11);
				if (closeButton.Contains(e.Location))
				{
					var args = new TabEventArgs(TabPages[i]);
					TabClosing?.Invoke(this, args);
					if (args.Cancel) return;
					TabPages.RemoveAt(i);
					break;
				}
			}
		}
	}

	public delegate void TabEventHandler(object sender, TabEventArgs e);
	public class TabEventArgs : CancelEventArgs
	{
		public TabPage TabPage { get; private set; }

		public TabEventArgs(TabPage tabPage)
		{
			this.TabPage = tabPage;
		}
	}
}
