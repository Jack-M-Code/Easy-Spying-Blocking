using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_Spying_Blocking {
	public partial class TprogressBar:Form {
		public TprogressBar() {
			InitializeComponent();
		}

		private void TprogressBar_Load(object sender, EventArgs e) {

		}

		public void show_form1_data(int endcount, string data) {

			label1.Text = data;

			progressBarX1.Value = endcount;

			progressBarX1.Text = Convert.ToString(endcount) + "%";

		}

	}
}
