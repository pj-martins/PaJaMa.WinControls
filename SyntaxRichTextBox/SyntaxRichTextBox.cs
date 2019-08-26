using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;
using System.Windows.Threading;
using PaJaMa.Common;

namespace PaJaMa.WinControls.SyntaxRichTextBox
{
	public class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
	{
		private bool _suspend = true;
		private string _keywords = "";
		private List<UndoRedoItem> _undoStack = new List<UndoRedoItem>();
		private List<UndoRedoItem> _redoStack = new List<UndoRedoItem>();
		private DebounceDispatcher _dispatcher;
		private frmFindReplace _findForm;

		/// <summary>
		/// The settings.
		/// </summary>
		public SyntaxSettings Settings { get; private set; }

		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, IntPtr lParam);

		const int WM_USER = 0x400;
		const int WM_SETREDRAW = 0x000B;
		const int EM_GETEVENTMASK = WM_USER + 59;
		const int EM_SETEVENTMASK = WM_USER + 69;
		const int EM_GETSCROLLPOS = WM_USER + 221;
		const int EM_SETSCROLLPOS = WM_USER + 222;

		Point _ScrollPoint;
		bool _Painting = true;
		IntPtr _EventMask;
		int _SuspendIndex = 0;
		int _SuspendLength = 0;

		public SyntaxRichTextBox() : base()
		{
			this.Settings = new SyntaxSettings();
			_dispatcher = new DebounceDispatcher();
		}

		public void SuspendPainting()
		{
			_suspend = true;
			if (_Painting)
			{
				_SuspendIndex = this.SelectionStart;
				_SuspendLength = this.SelectionLength;
				SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref _ScrollPoint);
				SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
				_EventMask = SendMessage(this.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
				_Painting = false;
			}
		}

		public void ResumePainting()
		{
			if (!_Painting)
			{
				this.Select(_SuspendIndex, _SuspendLength);
				SendMessage(this.Handle, EM_SETSCROLLPOS, 0, ref _ScrollPoint);
				SendMessage(this.Handle, EM_SETEVENTMASK, 0, _EventMask);
				SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
				_Painting = true;
				this.Invalidate();
			}
			_suspend = false;
		}

		private DateTime _lastChange = DateTime.MinValue;
		protected override void OnTextChanged(EventArgs e)
		{
			if (_suspend) return;
			_dispatcher.Debounce(300, x =>
			{
				this.Invoke(new Action(() =>
				{
					ProcessLine();
				}));
			});

		}

		public void InitLines()
		{
			this.SuspendPainting();
			SelectionStart = 0;
			SelectionLength = Text.Length;
			SelectionColor = Color.Black;
			this.processText(Text, 0);
			this.ResumePainting();
		}

		/// <summary>
		/// Process a line.
		/// </summary>
		private void ProcessLine()
		{
			this.SuspendPainting();

			int currentSelectionStart = SelectionStart;
			int currentSelectionLength = SelectionLength;

			// Find the start of the current line.
			int lineStart = currentSelectionStart;
			while ((lineStart > 0) && (Text[lineStart - 1] != '\n'))
				lineStart--;
			// Find the end of the current line.
			int lineEnd = currentSelectionLength + currentSelectionStart;
			while ((lineEnd < Text.Length) && (Text[lineEnd] != '\n'))
				lineEnd++;
			// Calculate the length of the line.
			int lineLength = lineEnd - lineStart;
			// Get the current line.
			string line = Text.Substring(lineStart, lineLength);

			this.SuspendPainting();
			// Save the position and make the whole line black
			int nPosition = SelectionStart;
			SelectionStart = lineStart;
			SelectionLength = lineLength;
			SelectionColor = Color.Black;

			this.processText(line, lineStart);

			SelectionStart = nPosition;
			SelectionLength = 0;
			SelectionColor = Color.Black;

			this.ResumePainting();
		}
		/// <summary>
		/// Process a regular expression.
		/// </summary>
		/// <param name="strRegex">The regular expression.</param>
		/// <param name="color">The color.</param>
		private void ProcessRegex(string input, int start, string strRegex, Color color)
		{
			Regex regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Match regMatch;

			for (regMatch = regKeywords.Match(input); regMatch.Success; regMatch = regMatch.NextMatch())
			{
				// Process the words
				int nStart = start + regMatch.Index;
				int nLenght = regMatch.Length;
				SelectionStart = nStart;
				SelectionLength = nLenght;
				SelectionColor = color;
			}
		}
		/// <summary>
		/// Compiles the keywords as a regular expression.
		/// </summary>
		public void CompileKeywords()
		{
			for (int i = 0; i < Settings.Keywords.Count; i++)
			{
				string strKeyword = Settings.Keywords[i];

				if (i == Settings.Keywords.Count - 1)
					_keywords += "\\b" + strKeyword + "\\b";
				else
					_keywords += "\\b" + strKeyword + "\\b|";
			}
		}

		private void processText(string text, int start)
		{
			if (text.Length > 50000) text = text.Substring(0, 50000);

			ProcessRegex(text, start, _keywords, Settings.KeywordColor);
			if (Settings.EnableIntegers)
				ProcessRegex(text, start, "\\b(?:[0-9]*\\.)?[0-9]+\\b", Settings.IntegerColor);
			if (Settings.EnableStrings)
				ProcessRegex(text, start, $"{Settings.QuoteIdentifier}[^{Settings.QuoteIdentifier}\\\\\\r\\n]*(?:\\\\.[^{Settings.QuoteIdentifier}\\\\\\r\\n]*)*{Settings.QuoteIdentifier}", Settings.StringColor);
			if (Settings.EnableComments && !string.IsNullOrEmpty(Settings.Comment))
				ProcessRegex(text, start, Settings.Comment + ".*", Settings.CommentColor);
		}

		public void CommentSelected()
		{
			this.SuspendPainting();
			int currentSelectionStart = SelectionStart;
			int currentSelectionEnd = SelectionStart + SelectionLength;

			if (_undoStack.Count > 20) _undoStack.RemoveAt(0);
			_undoStack.Add(new UndoRedoItem() { Text = Text, Position = SelectionStart });

			// Find the start of the current line.
			int lineStart = currentSelectionStart;
			while ((lineStart > 0) && (Text[lineStart - 1] != '\n'))
				lineStart--;
			SelectionLength = 0;
			for (int i = lineStart; i <= currentSelectionEnd; i++)
			{
				if (i == 0 || (i < Text.Length && Text[i - 1] == '\n'))
				{
					SelectionStart = i;
					SelectedText = this.Settings.Comment + " ";
					SelectionLength = 0;
				}
			}

			SelectionStart = currentSelectionStart;
			this.processText(Text, 0);
			this.ResumePainting();
		}

		public void UnCommentSelected()
		{
			this.SuspendPainting();
			int currentSelectionStart = SelectionStart;
			int currentSelectionEnd = SelectionStart + SelectionLength;

			if (_undoStack.Count > 20) _undoStack.RemoveAt(0);
			_undoStack.Add(new UndoRedoItem() { Text = Text, Position = SelectionStart });

			// Find the start of the current line.
			int lineStart = currentSelectionStart;
			while ((lineStart > 0) && (Text[lineStart - 1] != '\n'))
				lineStart--;
			SelectionLength = 0;
			for (int i = lineStart; i <= currentSelectionEnd; i++)
			{
				if (i == 0 || (i < Text.Length && Text[i - 1] == '\n'))
				{
					SelectionStart = i;
					SelectionLength = this.Settings.Comment.Length;
					if (SelectedText == this.Settings.Comment)
					{
						SelectedText = string.Empty;
					}
					SelectionStart = i;
					SelectionLength = 1;
					if (SelectedText == " ")
					{
						SelectedText = string.Empty;
					}
				}
			}

			this.processText(Text, 0);
			SelectionStart = currentSelectionStart;
			this.ResumePainting();
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
			{
				this.SuspendPainting();
				this.processText(Text, 0);
				this.ResumePainting();
			}
			else if (e.KeyCode == Keys.Space)
			{
				this.ProcessLine();
			}
			if (e.KeyCode == Keys.Enter)
			{
				this.ProcessLine();
				if (this.SelectionStart > 0 && this.SelectionLength == 0)
				{
					int lineStart = this.SelectionStart - 1;
					while ((lineStart > 0) && (Text[lineStart - 1] != '\n'))
					{
						lineStart--;
					}
					var indentMatch = Regex.Match(Text.Substring(lineStart, (this.SelectionStart - lineStart)),
						"^([ \t]+)");
					if (indentMatch.Success)
					{
						this.SuspendPainting();
						this.SelectedText = indentMatch.Groups[1].Value;
						this.ResumePainting();
						this.SelectionStart += indentMatch.Groups[1].Value.Length;
					}
				}

			}
		}

		private DateTime _lastStack = DateTime.MinValue;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			List<UndoRedoItem> addStack = null;
			List<UndoRedoItem> removeStack = null;
			if (e.Control && e.KeyCode == Keys.Z)
			{
				addStack = _redoStack;
				removeStack = _undoStack;
			}
			else if (e.Control && e.KeyCode == Keys.Y)
			{
				addStack = _undoStack;
				removeStack = _redoStack;
			}
			else if (e.Control && (e.KeyCode == Keys.F || e.KeyCode == Keys.H))
			{
				if (_findForm != null)
				{
					_findForm.Focus();
				}
				else
				{
					_findForm = new frmFindReplace();
					_findForm.TextBox = this;
					_findForm.chkReplace.Checked = e.KeyCode == Keys.H;
					_findForm.FormClosed += (object sender, FormClosedEventArgs args) => _findForm = null;
					_findForm.Show();
				}
			}
			else if (e.KeyCode == Keys.F3 && _findForm != null)
			{
				_findForm.Find(e.Shift);
			}
			else if (!e.Control && !e.Shift)
			{
				if ((DateTime.Now - _lastStack).TotalMilliseconds < 1000) return;
				_lastStack = DateTime.Now;
				if ((!_undoStack.Any() || Text != _undoStack.Last().Text))
				{
					if (_undoStack.Count > 20) _undoStack.RemoveAt(0);
					_undoStack.Add(new UndoRedoItem() { Text = Text, Position = SelectionStart });
				}
			}

			if (removeStack != null)
			{
				if (removeStack.Any())
				{
					addStack.Add(new UndoRedoItem() { Text = Text, Position = SelectionStart });
					var item = removeStack.Last();
					removeStack.RemoveAt(removeStack.Count - 1);
					SuspendPainting();
					Text = item.Text;
					SelectionStart = 0;
					SelectionLength = Text.Length;
					SelectionColor = Color.Black;
					processText(Text, 0);
					ResumePainting();
					SelectionStart = item.Position;
				}
				e.Handled = true;
				return;
			}
			base.OnKeyDown(e);
		}

		private bool _somethingHighlighted;
		private void resetSelectionHighlighting()
		{
			if (!_somethingHighlighted) return;
			var currSelection = SelectionStart;
			var currSelectionLength = SelectionLength;
			SelectionStart = 0;
			SelectionLength = Text.Length;
			SelectionBackColor = Color.White;
			SelectionLength = currSelectionLength;
			SelectionStart = currSelection;
			_somethingHighlighted = false;
		}


		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
			if (SelectedText.Length > 50) return;

			SuspendPainting();
			HighlightSelection();
			ResumePainting();
		}

		public void HighlightSelection()
		{
			resetSelectionHighlighting();
			if (SelectedText.Length > 1)
			{
				var matches = Regex.Matches(Text, $"{Regex.Escape(SelectedText.Trim())}", RegexOptions.IgnoreCase);
				if (matches.Count > 1)
				{
					var currSelection = SelectionStart;
					foreach (Match m in matches)
					{
						if (m.Index != currSelection)
						{
							SelectionStart = m.Index;
							SelectionBackColor = Settings.SelectionBackColor;
						}
					}
					_somethingHighlighted = true;
					SelectionStart = currSelection;
				}
			}
		}
	}

	public class SyntaxSettings
	{
		public List<string> Keywords { get; private set; }
		public Color KeywordColor { get; set; }
		public string QuoteIdentifier { get; set; }
		public string Comment { get; set; }
		public Tuple<string, string> CommentBlockStartEnd { get; set; }
		public Color CommentColor { get; set; }
		public bool EnableComments { get; set; }
		public bool EnableIntegers { get; set; }
		public bool EnableStrings { get; set; }
		public Color StringColor { get; set; }
		public Color IntegerColor { get; set; }
		public bool HighlightSelected { get; set; }
		public Color SelectionBackColor { get; set; }
		public bool AutoIndent { get; set; }

		public SyntaxSettings()
		{
			this.Keywords = new List<string>();
			this.KeywordColor = Color.Blue;
			this.CommentColor = Color.Gray;
			this.StringColor = Color.Gray;
			this.IntegerColor = Color.Red;
			this.SelectionBackColor = Color.LightGray;
			this.QuoteIdentifier = "\"";
			this.EnableComments = true;
			this.EnableIntegers = true;
			this.EnableStrings = true;
			this.HighlightSelected = true;
			this.AutoIndent = true;
		}
	}

	internal class UndoRedoItem
	{
		public string Text { get; set; }
		public int Position { get; set; }
	}
}
