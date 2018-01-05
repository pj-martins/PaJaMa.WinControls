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
	public partial class TabControl : UserControl
	{
		public event TabEventHandler TabClosing;
		public event TabEventHandler TabAdding;

		public BindingList<TabPage> TabPages { get; set; }
		private List<Tab> _tabs;

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
				var newTab = new Tab() { TabPage = _selectedTab, AllowRemove = this.AllowAddRemove };
				newTab.TabSelected += NewTab_TabSelected;
				newTab.TabRemoving += NewTab_TabRemoving;
				_tabs.Insert(e.NewIndex, newTab);
				pnlTabs.Controls.Clear();
				if (AllowAddRemove)
				{
					pnlTabs.Controls.Add(pnlAdd);
				}
				var copy = _tabs.ToList();
				copy.Reverse();
				foreach (var tab in copy)
				{
					tab.Dock = DockStyle.Left;
					pnlTabs.Controls.Add(tab);
				}
				redraw();
			}
		}

		private void NewTab_TabRemoving(object sender, EventArgs e)
		{
			var tab = sender as Tab;
			TabClosing?.Invoke(this, new TabEventArgs(tab.TabPage));
			var index = TabPages.IndexOf(tab.TabPage);
			TabPages.Remove(tab.TabPage);
			if (tab.TabPage == SelectedTab)
			{
				pnlPages.Controls.Clear();
				tab.TabPage.Dispose();
			}
			_tabs.Remove(tab);
			pnlTabs.Controls.Remove(tab);
			if (_tabs.Count > index)
				SelectedTab = _tabs[index].TabPage;
			else if (index > 0)
				SelectedTab = _tabs[index - 1].TabPage;
		}

		private void NewTab_TabSelected(object sender, EventArgs e)
		{
			SelectedTab = (sender as Tab).TabPage;
		}

		private bool _allowAddRemove;
		[DefaultValue(false)]
		public bool AllowAddRemove
		{
			get { return _allowAddRemove; }
			set
			{
				_allowAddRemove = value;
				foreach (var tab in _tabs)
				{
					tab.AllowRemove = value;
				}
				pnlAdd.Visible = value;
			}
		}

		private void redraw()
		{
			pnlAdd.Visible = AllowAddRemove;
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
