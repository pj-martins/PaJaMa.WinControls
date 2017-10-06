using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace PaJaMa.WinControls
{
	/// <summary>
	/// The InputBox class is used to show a prompt in a dialog box using the static method Show().
	/// </summary>
	/// <remarks>
	/// Copyright © 2003 Reflection IT
	/// 
	/// This software is provided 'as-is', without any express or implied warranty.
	/// In no event will the authors be held liable for any damages arising from the
	/// use of this software.
	/// 
	/// Permission is granted to anyone to use this software for any purpose,
	/// including commercial applications, subject to the following restrictions:
	/// 
	/// 1. The origin of this software must not be misrepresented; you must not claim
	/// that you wrote the original software. 
	/// 
	/// 2. No substantial portion of the source code of this library may be redistributed
	/// without the express written permission of the copyright holders, where
	/// "substantial" is defined as enough code to be recognizably from this library. 
	/// 
	/// </remarks>
	public class NumericInputBox : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.Button buttonOK;
		protected System.Windows.Forms.Button buttonCancel;
		protected System.Windows.Forms.Label labelPrompt;
		//protected System.Windows.Forms.ErrorProvider errorProviderText;
		private IContainer components;
		private NumericUpDown numValue;

		/// <summary>
		/// Delegate used to validate the object
		/// </summary>
		private InputBoxValidatingHandler _validator;

		private NumericInputBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelPrompt = new System.Windows.Forms.Label();
			this.numValue = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(288, 72);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.CausesValidation = false;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(376, 72);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// labelPrompt
			// 
			this.labelPrompt.AutoSize = true;
			this.labelPrompt.Location = new System.Drawing.Point(15, 15);
			this.labelPrompt.Name = "labelPrompt";
			this.labelPrompt.Size = new System.Drawing.Size(39, 13);
			this.labelPrompt.TabIndex = 0;
			this.labelPrompt.Text = "prompt";
			// 
			// numValue
			// 
			this.numValue.Location = new System.Drawing.Point(18, 40);
			this.numValue.Name = "numValue";
			this.numValue.Size = new System.Drawing.Size(433, 20);
			this.numValue.TabIndex = 4;
			// 
			// NumericInputBox
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(464, 104);
			this.Controls.Add(this.numValue);
			this.Controls.Add(this.labelPrompt);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NumericInputBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Input";
			((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			//this.Validator = null;
			this.Close();
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Displays a prompt in a dialog box, waits for the user to input text or click a button.
		/// </summary>
		/// <param name="prompt">String expression displayed as the message in the dialog box</param>
		/// <param name="title">String expression displayed in the title bar of the dialog box</param>
		/// <param name="defaultResponse">String expression displayed in the text box as the default response</param>
		/// <param name="validator">Delegate used to validate the text</param>
		/// <param name="xpos">Numeric expression that specifies the distance of the left edge of the dialog box from the left edge of the screen.</param>
		/// <param name="ypos">Numeric expression that specifies the distance of the upper edge of the dialog box from the top of the screen</param>
		/// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
		public static NumericInputBoxResult Show(string prompt, string title, decimal currValue = 0, decimal minValue = 0, decimal maxValue = -1, int xpos = -1, int ypos = -1)
		{
			using (NumericInputBox form = new NumericInputBox())
			{
				form.labelPrompt.Text = prompt;
				form.Text = title;
				form.numValue.Minimum = minValue;
				form.numValue.Maximum = maxValue < 0 ? decimal.MaxValue : maxValue;
				form.numValue.Value = currValue;
				if (xpos >= 0 && ypos >= 0)
				{
					form.StartPosition = FormStartPosition.Manual;
					form.Left = xpos;
					form.Top = ypos;
				}

				DialogResult result = form.ShowDialog();

				NumericInputBoxResult retval = new NumericInputBoxResult();
				retval.Result = result;
				if (result == DialogResult.OK)
				{
					retval.Value = form.numValue.Value;
				}
				return retval;
			}
		}

		/// <summary>
		/// Reset the ErrorProvider
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void textBoxText_TextChanged(object sender, System.EventArgs e)
		//{
		//	errorProviderText.SetError(textBoxText, "");
		//}

		/// <summary>
		/// Validate the Text using the Validator
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void textBoxText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		//{
		//	if (Validator != null)
		//	{
		//		InputBoxValidatingArgs args = new InputBoxValidatingArgs();
		//		args.Text = textBoxText.Text;
		//		Validator(this, args);
		//		if (args.Cancel)
		//		{
		//			e.Cancel = true;
		//			errorProviderText.SetError(textBoxText, args.Message);
		//		}
		//	}
		//}

		//protected InputBoxValidatingHandler Validator
		//{
		//	get
		//	{
		//		return (this._validator);
		//	}
		//	set
		//	{
		//		this._validator = value;
		//	}
		//}
	}

	/// <summary>
	/// Class used to store the result of an InputBox.Show message.
	/// </summary>
	public class NumericInputBoxResult
	{
		public DialogResult Result { get; set; }
		public decimal Value { get; set; }
	}

	///// <summary>
	///// EventArgs used to Validate an InputBox
	///// </summary>
	//public class InputBoxValidatingArgs : EventArgs
	//{
	//	public string Text;
	//	public string Message;
	//	public bool Cancel;
	//}

	///// <summary>
	///// Delegate used to Validate an InputBox
	///// </summary>
	//public delegate void InputBoxValidatingHandler(object sender, InputBoxValidatingArgs e);

}
