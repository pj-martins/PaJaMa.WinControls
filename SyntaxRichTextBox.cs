using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PaJaMa.WinControls
{
	public class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
	{
		private readonly SyntaxSettings _settings = new SyntaxSettings();
		private bool _initial = false;
		private string _line = "";
		private int _contentLength = 0;
		private int _lineLength = 0;
		private int _lineStart = 0;
		private int _lineEnd = 0;
		private string m_strKeywords = "";
		private int m_nCurSelection = 0;

		/// <summary>
		/// The settings.
		/// </summary>
		public SyntaxSettings Settings
		{
			get { return _settings; }
		}

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

		public void SuspendPainting()
		{
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
		}

		/// <summary>
		/// OnTextChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnTextChanged(EventArgs e)
		{
			if (_initial) return;
			_contentLength = this.TextLength;

			int nCurrentSelectionStart = SelectionStart;
			int nCurrentSelectionLength = SelectionLength;

			this.SuspendPainting();

			// Find the start of the current line.
			_lineStart = nCurrentSelectionStart;
			while ((_lineStart > 0) && (Text[_lineStart - 1] != '\n'))
				_lineStart--;
			// Find the end of the current line.
			_lineEnd = nCurrentSelectionStart;
			while ((_lineEnd < Text.Length) && (Text[_lineEnd] != '\n'))
				_lineEnd++;
			// Calculate the length of the line.
			_lineLength = _lineEnd - _lineStart;
			// Get the current line.
			_line = Text.Substring(_lineStart, _lineLength);

			// Process this line.
			ProcessLine();

			this.ResumePainting();
		}
		/// <summary>
		/// Process a line.
		/// </summary>
		private void ProcessLine()
		{
			// Save the position and make the whole line black
			int nPosition = SelectionStart;
			SelectionStart = _lineStart;
			SelectionLength = _lineLength;
			SelectionColor = Color.Black;

			// Process the keywords
			ProcessRegex(m_strKeywords, Settings.KeywordColor);
			// Process numbers
			if(Settings.EnableIntegers)
				ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", Settings.IntegerColor);
			// Process strings
			if(Settings.EnableStrings)
				ProcessRegex("\"[^\"\\\\\\r\\n]*(?:\\\\.[^\"\\\\\\r\\n]*)*\"", Settings.StringColor);
			// Process comments
			if(Settings.EnableComments && !string.IsNullOrEmpty(Settings.Comment))
				ProcessRegex(Settings.Comment + ".*$", Settings.CommentColor);
			if (!string.IsNullOrEmpty(Settings.QuoteIdentifier))
				ProcessRegex($"({Settings.QuoteIdentifier}.*?{Settings.QuoteIdentifier})", Settings.QuoteColor);
			//if (Settings.EnableComments && Settings.CommentBlockStartEnd != null)
			//{
			//	// TODO spaces
			//	if (_line.StartsWith(Settings.CommentBlockStartEnd.Item1))
			//	{

			//	}
			//}

			SelectionStart = nPosition;
			SelectionLength = 0;
			SelectionColor = Color.Black;

			m_nCurSelection = nPosition;
		}
		/// <summary>
		/// Process a regular expression.
		/// </summary>
		/// <param name="strRegex">The regular expression.</param>
		/// <param name="color">The color.</param>
		private void ProcessRegex(string strRegex, Color color)
		{
			Regex regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Match regMatch;

			for (regMatch = regKeywords.Match(_line); regMatch.Success; regMatch = regMatch.NextMatch())
			{
				// Process the words
				int nStart = _lineStart + regMatch.Index;
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

				if (i == Settings.Keywords.Count-1)
					m_strKeywords += "\\b" + strKeyword + "\\b";
				else
					m_strKeywords += "\\b" + strKeyword + "\\b|";
			}
		}

		public void ProcessAllLines(bool initial)
		{
			_initial = initial;
			this.SuspendPainting();

			int nStartPos = 0;
			int i = 0;
			int nOriginalPos = SelectionStart;
			while (i < Lines.Length)
			{
				_line = Lines[i];
				_lineStart = nStartPos;
				_lineEnd = _lineStart + _line.Length;

				ProcessLine();
				i++;

				nStartPos += _line.Length+1;
			}

			this.ResumePainting();
			_initial = false;
		}

		public void CommentSelected()
		{
			this.SuspendPainting();
			int nCurrentSelectionStart = SelectionStart;
			int nCurrentSelectionEnd = SelectionStart + SelectionLength;
			
			// Find the start of the current line.
			_lineStart = nCurrentSelectionStart;
			while ((_lineStart > 0) && (Text[_lineStart - 1] != '\n'))
				_lineStart--;
			SelectionLength = 0;
			for (int i = _lineStart; i <= nCurrentSelectionEnd; i++)
			{
				if (i == 0 || (i < Text.Length && Text[i - 1] == '\n'))
				{
					SelectionStart = i;
					SelectedText = this.Settings.Comment + " ";
					SelectionLength = 0;
				}
			}

			SelectionStart = nCurrentSelectionStart;
			this.ResumePainting();
		}

		public void UnCommentSelected()
		{
			this.SuspendPainting();
			int nCurrentSelectionStart = SelectionStart;
			int nCurrentSelectionEnd = SelectionStart + SelectionLength;

			// Find the start of the current line.
			_lineStart = nCurrentSelectionStart;
			while ((_lineStart > 0) && (Text[_lineStart - 1] != '\n'))
				_lineStart--;
			SelectionLength = 0;
			for (int i = _lineStart; i <= nCurrentSelectionEnd; i++)
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

			SelectionStart = nCurrentSelectionStart;
			this.ResumePainting();
		}
	}

	/// <summary>
	/// Class to store syntax objects in.
	/// </summary>
	public class SyntaxList
	{
		public List<string> m_rgList = new List<string>();
		public Color m_color = new Color();
		public Color m_quoteColor = new Color();
	}

	/// <summary>
	/// Settings for the keywords and colors.
	/// </summary>
	public class SyntaxSettings
	{
		SyntaxList m_rgKeywords = new SyntaxList();
		Tuple<string, string> _commentBlockStartEnd;
		string m_strComment = "";
		Color m_colorComment = Color.Green;
		Color m_colorString = Color.Gray;
		Color m_colorInteger = Color.Red;
		bool m_bEnableComments = true;
		bool m_bEnableIntegers = true;
		bool m_bEnableStrings = true;

		#region Properties
		/// <summary>
		/// A list containing all keywords.
		/// </summary>
		public List<string> Keywords
		{
			get { return m_rgKeywords.m_rgList; }
		}
		/// <summary>
		/// The color of keywords.
		/// </summary>
		public Color KeywordColor
		{
			get { return m_rgKeywords.m_color; }
			set { m_rgKeywords.m_color = value; }
		}
		public string QuoteIdentifier { get; set; }
		/// <summary>
		/// The quote of keywords.
		/// </summary>
		public Color QuoteColor
		{
			get { return m_rgKeywords.m_quoteColor; }
			set { m_rgKeywords.m_quoteColor = value; }
		}
		/// <summary>
		/// A string containing the comment identifier.
		/// </summary>
		public string Comment
		{
			get { return m_strComment; }
			set { m_strComment = value; }
		}
		/// <summary>
		/// A string containing the comment identifier.
		/// </summary>
		public Tuple<string, string> CommentBlockStartEnd
		{
			get { return _commentBlockStartEnd; }
			set { _commentBlockStartEnd = value; }
		}
		/// <summary>
		/// The color of comments.
		/// </summary>
		public Color CommentColor
		{
			get { return m_colorComment; }
			set { m_colorComment = value; }
		}
		/// <summary>
		/// Enables processing of comments if set to true.
		/// </summary>
		public bool EnableComments
		{
			get { return m_bEnableComments; }
			set { m_bEnableComments = value; }
		}
		/// <summary>
		/// Enables processing of integers if set to true.
		/// </summary>
		public bool EnableIntegers
		{
			get { return m_bEnableIntegers; }
			set { m_bEnableIntegers = value; }
		}
		/// <summary>
		/// Enables processing of strings if set to true.
		/// </summary>
		public bool EnableStrings
		{
			get { return m_bEnableStrings; }
			set { m_bEnableStrings = value; }
		}
		/// <summary>
		/// The color of strings.
		/// </summary>
		public Color StringColor
		{
			get { return m_colorString; }
			set { m_colorString = value; }
		}
		/// <summary>
		/// The color of integers.
		/// </summary>
		public Color IntegerColor
		{
			get { return m_colorInteger; }
			set { m_colorInteger = value; }
		}
		#endregion
	}
}
