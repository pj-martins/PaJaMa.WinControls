using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect(e);

			if (ModifierKeys.HasFlag(Keys.Shift))
			{
				if (SelectedNode != null && e.Node != null)
				{
					var flattened = this.getFlattenedNodes(this.Nodes);
					var index1 = flattened.FindIndex(n => n.Text == SelectedNode.Text);
					var index2 = flattened.FindIndex(n => n.Text == e.Node.Text);
					var start = Math.Min(index1, index2);
					var end = Math.Max(index1, index2);
					for (int i = start; i <= end; i++)
					{
						var node = flattened[i];
						if (!SelectedNodes.Contains(node))
							SelectedNodes.Add(node);
					}
				}
			}
			else if (!ModifierKeys.HasFlag(Keys.Control))
			{
				this.SelectedNodes.Clear();
			}
		}

		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			BeginUpdate();
			if (!this.SelectedNodes.Contains(e.Node))
				this.SelectedNodes.Add(e.Node);
			base.OnAfterSelect(e);
			EndUpdate();
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
				this.Invalidate();
			}
		}
	}
}
