using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public static class Extensions
	{
		public static List<TreeNode> GetFlattenedNodes(this TreeView tv)
		{
			return recurisvelyGetFlattenedNodes(tv.Nodes);
		}

		private static List<TreeNode> recurisvelyGetFlattenedNodes(TreeNodeCollection parentCollection)
		{
			var flattened = new List<TreeNode>();
			foreach (TreeNode node in parentCollection)
			{
				flattened.Add(node);
				flattened.AddRange(recurisvelyGetFlattenedNodes(node.Nodes));
			}
			return flattened;
		}
	}
}
