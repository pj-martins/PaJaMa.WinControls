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
        public event TabEventHandler TabClosed;
        public event TabEventHandler TabAdding;
        public event TabEventHandler TabAdded;
        public event TabEventHandler TabChanged;
        public event TabEventHandler TabOrderChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<TabPage> TabPages { get; set; }
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
                foreach (var tab in TabPages.Select(tp => tp.Tab))
                {
                    tab.AllowRemove = value;
                }
            }
        }

        [DefaultValue(false)]
        public bool WrapTabs { get; set; }

        private List<Panel> _wrapPanels = new List<Panel>();

        public TabControl()
        {
            InitializeComponent();
            TabPages = new BindingList<TabPage>();
            TabPages.ListChanged += TabPages_ListChanged;
        }

        private bool _lockAdd;
        private void TabPages_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (DesignMode || _lockAdd) return;
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                _selectedTab = TabPages[e.NewIndex];
                var newTab = new Tab() { TabPage = _selectedTab, AllowRemove = this.AllowRemove, TabControl = this };
                _selectedTab.Tab = newTab;
                newTab.TabSelected += NewTab_TabSelected;
                newTab.TabRemoving += NewTab_TabRemoving;
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

            if (WrapTabs)
            {
                foreach (var pnl in _wrapPanels)
                {
                    this.Controls.Remove(pnl);
                }
                _wrapPanels = new List<Panel>();
            }


            var tabs = TabPages.Select(tb => tb.Tab).ToList();
            for (int i = 0; i < tabs.Count; i++)
            {
                tabs[i].Visible = i >= _visibleTabStart;
            }
            var copy = tabs.ToList();
            if (WrapTabs)
            {
                int currentWidth = pnlAdd.Width;
                int panelIndex = 0;
                foreach (var tab in copy)
                {
                    tab.WrapPanelIndex = panelIndex;
                    currentWidth += tab.Width;
                    if (currentWidth > pnlTabs.Width)
                    {
                        tab.WrapPanelIndex = ++panelIndex;
                        currentWidth = 0;
                    }
                }
                for (int i = 0; i < panelIndex; i++)
                {
                    var panel = new Panel();
                    panel.Dock = DockStyle.Top;
                    panel.Height = 23;
                    this.Controls.Add(panel);
                    _wrapPanels.Add(panel);
                }
            }
            copy.Reverse();
            Panel lastPanel = pnlTabs;
            foreach (var tab in copy)
            {
                tab.Dock = DockStyle.Left;
                int panelIndex = _wrapPanels.Count - tab.WrapPanelIndex;
                var panel = panelIndex <= 0 ? pnlTabs : _wrapPanels[panelIndex - 1];
                panel.Controls.Add(tab);
            }
            if (!WrapTabs)
            {
                if (_visibleTabStart > 0)
                    pnlLeft.Visible = pnlRight.Visible = true;
                else if (pnlTabs.Controls.Count > 0)
                {
                    var rightMostX = pnlTabs.Controls[0].Left + pnlTabs.Controls[0].Width;
                    pnlLeft.Visible = pnlRight.Visible = rightMostX > this.Width;
                }
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
            pnlTabs.Controls.Remove(tab);
            if (index < 0) index = 0;
            if (TabPages.Count > index)
                SelectedTab = TabPages[index];
            else if (index > 0)
                SelectedTab = TabPages[index - 1];
            TabClosed?.Invoke(this, new TabEventArgs(tab.TabPage));
        }

        private void NewTab_TabSelected(object sender, EventArgs e)
        {
            if (SelectedTab == (sender as Tab).TabPage) return;
            SelectedTab = (sender as Tab).TabPage;
            TabChanged?.Invoke(this, new TabEventArgs(SelectedTab));
        }

        private void redraw()
        {
            pnlAdd.Visible = AllowAdd;
            pnlPages.Controls.Clear();
            foreach (var tab in TabPages.Select(t => t.Tab))
            {
                tab.IsSelected = false;
                tab.Invalidate();
            }
            if (_selectedTab == null) return;

            _selectedTab.Tab.IsSelected = true;
            _selectedTab.Dock = DockStyle.Fill;
            pnlPages.Controls.Add(_selectedTab);
            this.Invalidate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var tabPage = new TabPage() { Text = "New Tab" };
            var args = new TabEventArgs(tabPage);
            TabAdding?.Invoke(this, args);
            if (args.Cancel) return;
            this.TabPages.Add(tabPage);
            SelectedTab = tabPage;
            TabAdded?.Invoke(this, new TabEventArgs(tabPage));
            TabChanged?.Invoke(this, new TabEventArgs(tabPage));
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
            if (DesignMode) return;
            this.ParentForm.ResizeEnd += delegate (object sender2, EventArgs e2) { redrawTabs(); };
        }

        private void pnlTabs_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Tab).FullName))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void pnlTabs_DragDrop(object sender, DragEventArgs e)
        {
            ReorderTabs((Tab)e.Data.GetData(typeof(Tab).FullName), null);
        }

        internal void ReorderTabs(Tab source, Tab destination)
        {
            var srcPage = TabPages.First(t => t.Tab == source);
            int srcIndex = TabPages.IndexOf(srcPage);
            int destIndex = destination == null ? -1 : TabPages.IndexOf(TabPages.First(t => t.Tab == destination));
            if (srcIndex == destIndex) return;
            _lockAdd = true;
            TabPages.Remove(srcPage);
            if (destination == null)
            {
                TabPages.Add(srcPage);
            }
            else
            {
                if (srcIndex > destIndex)
                {
                    TabPages.Insert(destIndex, srcPage);
                }
                else if (destIndex >= TabPages.Count - 1)
                {
                    TabPages.Add(srcPage);
                }
                else
                {
                    TabPages.Insert(destIndex + 1, srcPage);
                }
            }
            redrawTabs();
            TabOrderChanged?.Invoke(this, new TabEventArgs(srcPage));
            _lockAdd = false;
        }

        internal void CloseOtherTabs(Tab tab)
        {
            for (int i = TabPages.Count - 1; i >= 0; i--)
            {
                var tabPage = TabPages[i];
                if (tabPage == tab.TabPage) continue;
                TabClosing?.Invoke(this, new TabEventArgs(tabPage));
                TabPages.Remove(tabPage);
                pnlTabs.Controls.Remove(tabPage.Tab);
                TabClosed?.Invoke(this, new TabEventArgs(tab.TabPage));
            }
        }
    }

    public delegate void TabEventHandler(object sender, TabEventArgs e);
    public class TabEventArgs : EventArgs
    {
        public TabPage TabPage { get; private set; }
        public bool Cancel { get; set; }

        public TabEventArgs(TabPage tabPage)
        {
            this.TabPage = tabPage;
        }
    }
}
