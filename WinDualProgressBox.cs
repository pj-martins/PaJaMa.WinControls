using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public partial class WinDualProgressBox : Form
	{
		[DefaultValue(1)]
		public int Total { get; set; }

		private decimal _totalProgress = 0;
		public int Current
		{
			set
			{
				if (value < 0) value = 0;
				if (value > 100) value = 100;
				progIndividual.Value = value;
				decimal totalProgress = (decimal)value / (decimal)Total;
				if (_totalProgress + totalProgress < 0)
					progTotal.Value = 0;
				else if (_totalProgress + totalProgress > 100)
					progTotal.Value = 100;
				else
					progTotal.Value = (int)(_totalProgress + totalProgress);
				if (value == 100)
					_totalProgress += 100 / (decimal)this.Total;
			}
		}

		public void ResetValues()
		{
			this.Total = 1;
			progIndividual.Value = 0;
			progTotal.Value = 0;
			_totalProgress = 0;
			this.StatusText = string.Empty;
		}

		public string StatusText
		{
			set { lblProgress.Text = value; }
		}

		public WinDualProgressBox()
		{
			InitializeComponent();
		}

		public event EventHandler CancelTriggered;

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (CancelTriggered != null)
				CancelTriggered(this, new EventArgs());
		}


	}
}
