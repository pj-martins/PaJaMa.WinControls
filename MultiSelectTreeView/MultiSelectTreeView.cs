using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls.MultiSelectTreeView
{
	public class MultiSelectTreeView : System.Windows.Forms.TreeView
	{
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<TreeNode> SelectedNodes { get; set; }

		public bool AllowDragNodes { get; set; }

		public event DragEventHandler NodesDrag;

		public MultiSelectTreeView()
		{
			this.SelectedNodes = new List<TreeNode>();
			this.DrawMode = TreeViewDrawMode.OwnerDrawText;
		}

		private List<TreeNode> getFlattenedNodes(TreeNodeCollection nodes)
		{
			List<TreeNode> flattened = new List<TreeNode>();
			foreach (TreeNode node in nodes)
			{
				flattened.Add(node);
				flattened.AddRange(getFlattenedNodes(node.Nodes));
			}
			return flattened;
		}

		protected override void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			base.OnDrawNode(e);
			if (this.SelectedNodes.Contains(e.Node))
			{
				e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.SystemColors.Highlight),
					new System.Drawing.Rectangle(e.Bounds.X, e.Bounds.Y, this.Width - e.Bounds.X, e.Bounds.Height));
			}

			var color = e.Node.ForeColor;
			if (color == System.Drawing.Color.Empty)
				color = ForeColor;
			e.Graphics.DrawString(e.Node.Text, e.Node.NodeFont ?? this.Font, new System.Drawing.SolidBrush(color),
				e.Bounds.X, e.Bounds.Y);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
			{
				SelectedNodes = getFlattenedNodes(Nodes);
				e.Handled = true;
				e.SuppressKeyPress = true;
				this.Invalidate();
			}
		}

		private bool _isDragging;
		private Point _clickPoint;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			var node = this.GetNodeAt(new System.Drawing.Point(e.X, e.Y));
			if (node != null)
			{
				BeginUpdate();
				if (ModifierKeys.HasFlag(Keys.Shift))
				{
					if (SelectedNode != null)
					{
						var flattened = this.getFlattenedNodes(this.Nodes);
						var index1 = flattened.FindIndex(n => n.Text == SelectedNode.Text);
						var index2 = flattened.FindIndex(n => n.Text == node.Text);
						var start = Math.Min(index1, index2);
						var end = Math.Max(index1, index2);
						for (int i = start; i <= end; i++)
						{
							var f = flattened[i];
							if (!SelectedNodes.Contains(f))
								SelectedNodes.Add(f);
						}
					}
				}
				else if (!this.SelectedNodes.Contains(node))
				{
					if (!ModifierKeys.HasFlag(Keys.Control))
						this.SelectedNodes.Clear();
					this.SelectedNodes.Add(node);
				}
				SelectedNode = node;
				this.Invalidate();
				EndUpdate();
				if (this.AllowDragNodes && e.Button == MouseButtons.Left && e.Clicks == 2)
					_isDragging = false;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.Left)
			{
				Point currentPosition = e.Location;
				double distanceX = Math.Abs(_clickPoint.X - currentPosition.X);
				double distanceY = Math.Abs(_clickPoint.Y - currentPosition.Y);
				if (distanceX > 10 || distanceY > 10)
				{
					if (!_isDragging)
					{
						_isDragging = true;
						var args = new DragEventArgs(new DataObject(typeof(List<TreeNode>).FullName,
							this.SelectedNodes), 0, e.X, e.Y, DragDropEffects.All, DragDropEffects.All);
						this.NodesDrag?.Invoke(this, args);
						this.DoDragDrop(args.Data, args.Effect);
					}
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			var node = this.GetNodeAt(new System.Drawing.Point(e.X, e.Y));
			if (node != null)
			{
				if (e.Button == MouseButtons.Left && !ModifierKeys.HasFlag(Keys.Control) && !ModifierKeys.HasFlag(Keys.Shift))
				{
					BeginUpdate();
					this.SelectedNodes.Clear();
					this.SelectedNodes.Add(this.SelectedNode);
					EndUpdate();
				}
				if (_isDragging)
					_isDragging = false;
			}
		}

		private static List<TreeNode> recursivelyGetChildren(TreeNode parent)
		{
			List<TreeNode> nodes = new List<TreeNode>();
			nodes.Add(parent);
			foreach (TreeNode child in parent.Nodes)
			{
				nodes.AddRange(recursivelyGetChildren(child));
			}
			return nodes;
		}

		public List<TreeNode> GetSelectedFlattenedNodes()
		{
			return GetFlattenedNodes(SelectedNodes);
		}

		public static List<TreeNode> GetFlattenedNodes(IEnumerable<TreeNode> parents)
		{
			List<TreeNode> nodes = new List<TreeNode>();
			foreach (var n in parents)
			{
				nodes.AddRange(recursivelyGetChildren(n));
			}
			nodes = nodes.Distinct().ToList();
			return nodes;
		}
	}
}
