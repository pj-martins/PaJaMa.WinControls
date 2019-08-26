using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls.SyntaxRichTextBox
{
	public partial class frmFindReplace : Form
	{
		public SyntaxRichTextBox TextBox { get; set; }
		public frmFindReplace()
		{
			InitializeComponent();
		}

		public bool Find(bool previous = false, bool somethingFound = true)
		{
			int currSelectionStart = TextBox.SelectionStart + (previous ? 0 : TextBox.SelectionLength);
			//string textToSearch = previous ? TextBox.Text.Substring(0, currSelectionStart) : TextBox.Text.Substring(currSelectionStart);
			string pattern = txtFind.Text;
			if (!chkRegex.Checked) pattern = Regex.Escape(pattern);
			RegexOptions options = RegexOptions.Multiline;
			if (!chkMatchCase.Checked) options |= RegexOptions.IgnoreCase;
			if (chkMatchWholeWord.Checked) pattern = $"\\b{pattern}\\b";
			if (previous) options |= RegexOptions.RightToLeft;
			MatchCollection matches;
			try
			{
				matches = Regex.Matches(TextBox.Text, pattern, options);
			}
			catch
			{
				lblResults.Text = "Invalid pattern.";
				return false;
			}
			var match = matches.OfType<Match>().FirstOrDefault(m => previous ? m.Index < currSelectionStart : m.Index >= currSelectionStart);
			if (match != null)
			{
				TextBox.SuspendPainting();
				TextBox.SelectionStart = match.Index; // + (previous ? 0 : currSelectionStart);
				TextBox.SelectionLength = match.Length;
				TextBox.HighlightSelection();
				TextBox.ResumePainting();
				TextBox.Focus();
				TextBox.SelectionStart = match.Index; // + (previous ? 0 : currSelectionStart);
				TextBox.SelectionLength = match.Length;
				lblResults.Text = $"{matches.Count} results found.";
				return true;
			}
			else if (previous && somethingFound)
			{
				TextBox.SelectionStart = TextBox.Text.Length;
				TextBox.SelectionLength = 0;
				return Find(true, false);
			}
			else if (currSelectionStart > 0 && somethingFound)
			{
				TextBox.SelectionStart = 0;
				TextBox.SelectionLength = 0;
				return Find(false, false);
			}
			else if (!somethingFound)
			{
				lblResults.Text = "No results found.";
			}
			return false;
		}

		private void ChkReplace_CheckedChanged(object sender, EventArgs e)
		{
			txtReplace.Enabled = btnReplace.Enabled = btnReplaceAll.Enabled = chkReplace.Checked;
		}

		private void BtnFind_Click(object sender, EventArgs e)
		{
			this.Find();
		}

		private void BtnPrevious_Click(object sender, EventArgs e)
		{
			this.Find(true);
		}

		private void TxtFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) this.Find();
		}

		private void FrmFindReplace_Load(object sender, EventArgs e)
		{
			FormSettings.LoadSettings(this);
		}

		private void FrmFindReplace_FormClosing(object sender, FormClosingEventArgs e)
		{
			FormSettings.SaveSettings(this);
		}

		private void BtnReplace_Click(object sender, EventArgs e)
		{
			TextBox.SuspendPainting();
			if (Find())
			{
				TextBox.SelectedText = txtReplace.Text;
			}
			TextBox.ResumePainting();
		}

		private void BtnReplaceAll_Click(object sender, EventArgs e)
		{
			int selectionStart = TextBox.SelectionStart;
			TextBox.SuspendPainting();
			while (Find())
			{
				TextBox.SelectedText = txtReplace.Text;
			}
			TextBox.SelectionStart = selectionStart;
			TextBox.SelectionLength = 0;
			TextBox.ResumePainting();
		}
	}
}
