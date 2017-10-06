using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PaJaMa.WinControls.Explorer
{
	public partial class Explorer : UserControl
	{
		private bool _userSelecting = true;
		private Dictionary<string, int> _icons = new Dictionary<string, int>();
		public Explorer()
		{
			InitializeComponent();
		}

		private void Explorer_Load(object sender, EventArgs e)
		{
			List<DirectoryInfo> paths = new List<DirectoryInfo>();
			foreach (string path in Environment.GetLogicalDrives())
			{
				paths.Add(new DirectoryInfo(path));
			}
			CreateNodes(paths.ToArray(), null);
		}

		private void CreateNodes(DirectoryInfo[] paths, TreeNode rootNode)
		{
			foreach (DirectoryInfo path in paths)
			{
				string key = (path.Parent == null ? path.FullName : "folder");
				if (!_icons.ContainsKey(key))
				{
					Icon ico = PaJaMa.Common.Imaging.GetIconForFile(path.FullName, true);
					_icons.Add(key, _icons.Count);
					imageList1.Images.Add(ico);
				}
				TreeNode node = (rootNode == null ? treeView1.Nodes : rootNode.Nodes).Add(path.FullName, path.Name);
				node.ImageIndex = _icons[key];
				node.SelectedImageIndex = _icons[key];
				node.Tag = path.FullName;
				node.Nodes.Add("__expand__");
			}
		}

		private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "__expand__")
			{
				e.Node.Nodes.Clear();
				try
				{
					CreateNodes(new DirectoryInfo(e.Node.Tag.ToString()).GetDirectories(), e.Node);
				}
				catch (IOException) {}
				catch (UnauthorizedAccessException) { }
			}
		}

		public event FolderSelectedEventHandler FolderSelected;
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (!_userSelecting) return;
			PopulateFiles(e.Node.Tag.ToString());
			if (FolderSelected != null) FolderSelected(this, new FolderSelectedEventArgs(new DirectoryInfo(e.Node.Tag.ToString())));
		}

		private void PopulateFiles(string path)
		{
			listView1.Items.Clear();
			DirectoryInfo dinf = new DirectoryInfo(path);
			try
			{
				foreach (DirectoryInfo dinfc in dinf.GetDirectories())
				{
					string key = (dinfc.Parent == null ? dinfc.FullName : "folder");
					if (!_icons.ContainsKey(key))
					{
						Icon ico = PaJaMa.Common.Imaging.GetIconForFile(dinfc.FullName, true);
						_icons.Add(key, _icons.Count);
						imageList1.Images.Add(ico);
					}
					ListViewItem lvi = listView1.Items.Add(dinfc.Name);
					lvi.ImageIndex = _icons[key];
					lvi.Tag = dinfc.FullName;
				}

				foreach (FileInfo finf in dinf.GetFiles())
				{
					if (!_icons.ContainsKey(finf.Extension))
					{
						Icon ico = PaJaMa.Common.Imaging.GetIconForFile(finf.FullName, true);
						_icons.Add(finf.Extension, _icons.Count);
						imageList1.Images.Add(ico);
					}
					ListViewItem lvi = listView1.Items.Add(finf.Name);
					lvi.ImageIndex = _icons[finf.Extension];
					lvi.Tag = finf.FullName;
				}
			}
			catch (IOException) { }
			catch (UnauthorizedAccessException) { }
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			_userSelecting = false;
			if (listView1.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in listView1.SelectedItems)
				{
					FileInfo finf = new FileInfo(lvi.Tag.ToString());
					if (finf.Exists)
						System.Diagnostics.Process.Start(finf.FullName);
					else
					{
						PopulateFiles(finf.FullName);
						if (treeView1.Nodes.Find(finf.FullName, true).Length < 1)
						{
							treeView1.Nodes.Find(new DirectoryInfo(finf.FullName).Parent.FullName, true)[0].Expand();
						}
						treeView1.Nodes.Find(finf.FullName, true)[0].EnsureVisible();
					}
				}
			}
			_userSelecting = true;
		}

		public event FilesFoldersSelectedEventHandler FilesFoldersSelected;
		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (!e.IsSelected) return;
			if (FilesFoldersSelected == null) return;
			List<string> items = new List<string>();
			foreach (ListViewItem lvi in listView1.SelectedItems)
				items.Add(lvi.Tag.ToString());
			FilesFoldersSelected(this, new FilesFoldersSelectedEventArgs(GetSelectedFolders(), GetSelectedFiles(), items.ToArray()));
		}

		public FileInfo[] GetSelectedFiles()
		{
			List<FileInfo> files = new List<FileInfo>();
			foreach (ListViewItem lvi in listView1.SelectedItems)
			{
				if (new FileInfo(lvi.Tag.ToString()).Exists)
					files.Add(new FileInfo(lvi.Tag.ToString()));
			}
			return files.ToArray();
		}

		public DirectoryInfo[] GetSelectedFolders()
		{
			List<DirectoryInfo> directories = new List<DirectoryInfo>();
			foreach (ListViewItem lvi in listView1.SelectedItems)
			{
				if (!new FileInfo(lvi.Tag.ToString()).Exists)
					directories.Add(new DirectoryInfo(lvi.Tag.ToString()));
			}
			return directories.ToArray();
		}

		
	}

	public delegate void FolderSelectedEventHandler(object sender, FolderSelectedEventArgs e);
	public class FolderSelectedEventArgs : EventArgs
	{
		public DirectoryInfo SelectedFolder { get; set; }
		public FolderSelectedEventArgs(DirectoryInfo selectedFolder)
		{
			this.SelectedFolder = selectedFolder;
		}
	}

	public delegate void FilesFoldersSelectedEventHandler(object sender, FilesFoldersSelectedEventArgs e);
	public class FilesFoldersSelectedEventArgs : EventArgs
	{
		public DirectoryInfo[] SelectedFolders { get; set; }
		public FileInfo[] SelectedFiles { get; set; }
		public string[] SelectedItems { get; set; }
		public FilesFoldersSelectedEventArgs(DirectoryInfo[] directories, FileInfo[] files, string[] selectedItems)
		{
			this.SelectedFiles = files;
			this.SelectedFolders = directories;
			this.SelectedItems = selectedItems;
		}
	}
}
