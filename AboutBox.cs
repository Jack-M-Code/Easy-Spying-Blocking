using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_Spying_Blocking {
	partial class AboutBox:Form {
		public AboutBox() {

			InitializeComponent();
		}


		private void AboutBox_Load(object sender, EventArgs e) {


			this.Text = String.Format("About {0}", AssemblyTitle);

			this.labelProductName.Text = AssemblyProduct;

			this.labelVersion.Text = String.Format("{0} 版", AssemblyVersion);

			//this.labelCopyright.Text = AssemblyCopyright;

			//this.labelCompanyName.Text = AssemblyCompany;


			//this.textBoxDescription.Text = AssemblyDescription;

		}



		#region 組件屬性存取子

		public string AssemblyTitle {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if(attributes.Length > 0) {
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if(titleAttribute.Title != "") {
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion {
			get {
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if(attributes.Length == 0) {
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyProduct {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if(attributes.Length == 0) {
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if(attributes.Length == 0) {
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany {
			get {
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if(attributes.Length == 0) {
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		private void okButton_Click(object sender, EventArgs e) {
			Form1 lForm1 = (Form1)this.Owner;
			lForm1.Show();
			this.Close();
		}



		private void AboutBox_FormClosed(object sender, FormClosedEventArgs e) {
			Form1 lForm1 = (Form1)this.Owner;
			lForm1.Show();
		}

		private void AboutBox_Activated(object sender, EventArgs e) {

			this.labelCompanyName.Text = GetHardwareInfo.GetProcessorCPU();

			this.labelCopyright.Text = "RAM " + GetHardwareInfo.GetMemory();


			this.labelOs.Text = GetHardwareInfo.GetOS();
		}
	}
}
