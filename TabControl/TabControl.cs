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
	[Serializable]
	public partial class TabControl : UserControl
	{
		public event TabEventHandler TabClosing;
		public event TabEventHandler TabAdding;
		public event TabEventHandler TabChanged;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingList<TabPage> TabPages { get; set; }
		private List<Tab> _tabs;
		private int _visibleTabStart = 0;

		private TabPage _selectedTab;
		public TabPage SelectedTab
		{
			get { return _selectedTab; }
			set
			{
				_selectedTab = value;
				redraw();
			}
		}

		private bool _allowAdd;
		[DefaultValue(false)]
		public bool AllowAdd
		{
			get { return _allowAdd; }
			set
			{
				_allowAdd = value;
				pnlAdd.Visible = value;
			}
		}

		private bool _allowRemove;
		[DefaultValue(false)]
		public bool AllowRemove
		{
			get { return _allowRemove; }
			set
			{
				_allowRemove = value;
				foreach (var tab in _tabs)
				{
					tab.AllowRemove = value;
				}
			}
		}

		public TabControl()
		{
			InitializeComponent();
			_tabs = new List<Tab>();
			TabPages = new BindingList<TabPage>();
			TabPages.ListChanged += TabPages_ListChanged;
		}

		private void TabPages_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (DesignMode) return;
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				_selectedTab = TabPages[e.NewIndex];
				var newTab = new Tab() { TabPage = _selectedTab, AllowRemove = this.AllowRemove };
				newTab.TabSelected += NewTab_TabSelected;
				newTab.TabRemoving += NewTab_TabRemoving;
				_tabs.Insert(e.NewIndex, newTab);
				redrawTabs();
			}
		}

		private void redrawTabs()
		{
			pnlTabs.Controls.Clear();
			if (AllowAdd)
			{
				pnlTabs.Controls.Add(pnlAdd);
			}

			for (int i = 0; i < _tabs.Count; i++)
			{
				_tabs[i].Visible = i >= _visibleTabStart;
			}
			var copy = _tabs.ToList();
			copy.Reverse();
			foreach (var tab in copy)
			{
				tab.Dock = DockStyle.Left;
				pnlTabs.Controls.Add(tab);
			}
			if (_visibleTabStart > 0)
				pnlLeft.Visible = pnlRight.Visible = true;
			else if (pnlTabs.Controls.Count > 0)
			{
				var rightMostX = pnlTabs.Controls[0].Left + pnlTabs.Controls[0].Width;
				pnlLeft.Visible = pnlRight.Visible = rightMostX > this.Width;
			}
		}

		private void NewTab_TabRemoving(object sender, EventArgs e)
		{
			var tab = sender as Tab;
			TabClosing?.Invoke(this, new TabEventArgs(tab.TabPage));
			int index = TabPages.IndexOf(tab.TabPage);
			TabPages.Remove(tab.TabPage);
			if (tab.TabPage == SelectedTab)
			{
				pnlPages.Controls.Clear();
				tab.TabPage.Dispose();
			}
			_tabs.Remove(tab);
			pnlTabs.Controls.Remove(tab);
			if (index < 0) index = 0;
			if (_tabs.Count > index)
				SelectedTab = _tabs[index].TabPage;
			else if (index > 0)
				SelectedTab = _tabs[index - 1].TabPage;
		}

		private void NewTab_TabSelected(object sender, EventArgs e)
		{
			SelectedTab = (sender as Tab).TabPage;
			TabChanged?.Invoke(this, new TabEventArgs(SelectedTab));
		}

		private void redraw()
		{
			pnlAdd.Visible = AllowAdd;
			pnlPages.Controls.Clear();
			foreach (var tab in _tabs)
			{
				tab.IsSelected = false;
				tab.Invalidate();
			}
			var selectedTab = _tabs.FirstOrDefault(t => t.TabPage == _selectedTab);
			if (selectedTab == null) return;

			selectedTab.IsSelected = true;
			selectedTab.TabPage.TabLeft = selectedTab.Left;
			selectedTab.TabPage.TabRight = selectedTab.Left + selectedTab.Width;
			selectedTab.TabPage.Dock = DockStyle.Fill;
			selectedTab.Invalidate();
			pnlPages.Controls.Add(selectedTab.TabPage);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			var tabPage = new TabPage("New Tab");
			TabAdding?.Invoke(this, new TabEventArgs(tabPage));
			this.TabPages.Add(tabPage);
		}

		private void btnLeft_Click(object sender, EventArgs e)
		{
			if (_visibleTabStart > 0)
			{
				_visibleTabStart--;
				redrawTabs();
			}
		}

		private void btnRight_Click(object sender, EventArgs e)
		{
			var rightMostX = pnlTabs.Controls[0].Left + pnlTabs.Controls[0].Width;
			if (rightMostX > this.Width)
			{
				_visibleTabStart++;
				redrawTabs();
			}
		}

		private void TabControl_Load(object sender, EventArgs e)
		{
			this.ParentForm.ResizeEnd += delegate (object sender2, EventArgs e2) { redrawTabs(); };
		}
	}

	public delegate void TabEventHandler(object sender, TabEventArgs e);
	public class TabEventArgs : EventArgs
	{
		public TabPage TabPage { get; private set; }

		public TabEventArgs(TabPage tabPage)
		{
			this.TabPage = tabPage;
		}
	}
}
