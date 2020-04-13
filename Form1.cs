using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Security.AccessControl;
using System.Management;
using System.Security.Principal;

namespace Easy_Spying_Blocking {
	public partial class Form1:Form {
		public Form1() {
			InitializeComponent();
		}

		int counts = 0;

		int tocount = 0;

		TprogressBar fr2;

		public string system32hosts = Environment.SystemDirectory + "\\drivers\\etc\\hosts";

		public string ShellCmdLocation = Environment.SystemDirectory + "\\cmd.exe";

		
		[DllImport("wininet.dll")]
		public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
		public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
		public const int INTERNET_OPTION_REFRESH = 37;
	


			
		public static bool IsAdministrator() {
			//回傳True表示為管理員身分；反之則非管理員
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		private void Form1_Load(object sender, EventArgs e) {
			
					OperatingSystem os_info = System.Environment.OSVersion;

					if(os_info.Version.Major  < 10) {


								MessageBox.Show("Can only run on windows 10", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);

								System.GC.Collect();
								System.Environment.Exit(0);
					
					} else if(IsAdministrator() == false) {

											MessageBox.Show("Your account now requires administrator permissions to execute", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
								                     System.GC.Collect();
											System.Environment.Exit(0);
					
					} else {


									
									if(System.IO.File.Exists(system32hosts)) {

													FileInfo fi = new FileInfo(system32hosts);
													if(fi.Attributes.ToString().Contains("ReadOnly")) {
														fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
													}

													if(fi.Attributes.ToString().Contains("Hidden")) {
														fi.Attributes = fi.Attributes & ~FileAttributes.Hidden;
													}
									
									}
								
									 this.Activate();




					}

		}

		private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent) {
					//MessageBox.Show("This is an awesome program", "Awesome Program");
					hlpevent.Handled = true;
		}

		private void Form1_HelpButtonClicked(object sender, CancelEventArgs e) {

					//MessageBox.Show("This is a more awesome program", "Awesome Program");

					e.Cancel = true;

					//this.Hide();
					AboutBox f = new AboutBox();
					f.Owner = this;
					f.Show();

		}

		private void ButCheckALL_Click(object sender, EventArgs e) {


			//Console.WriteLine(tabControl.SelectedTab.Name);
			if(tabControl.SelectedTab.Name == "tabPage1") {

				foreach(CheckBox control in panel1.Controls.OfType<CheckBox>()) {
					control.Checked = true;
				}

			} else if(tabControl.SelectedTab.Name == "tabPage2") {

				foreach(CheckBox control in panel2.Controls.OfType<CheckBox>()) {
					control.Checked = true;
				}
			}
		}

		private void ButUncheckALL_Click(object sender, EventArgs e) {
			if(tabControl.SelectedTab.Name == "tabPage1") {

				foreach(CheckBox control in panel1.Controls.OfType<CheckBox>()) {
						control.Checked = false;
				}

			} else if(tabControl.SelectedTab.Name == "tabPage2") {

				foreach(CheckBox control in panel2.Controls.OfType<CheckBox>()) {
						control.Checked = false;
				}
			}
		}

		private void ButApply_Click(object sender, EventArgs e) {

			var checkedBoxes1 = panel1.Controls.OfType<CheckBox>().Count(c => c.Checked);
			var checkedBoxes2 = panel2.Controls.OfType<CheckBox>().Count(c => c.Checked);
			var checkedBoxes3 = panel3.Controls.OfType<CheckBox>().Count(c => c.Checked);

			counts = checkedBoxes1 + checkedBoxes2 + checkedBoxes3 ;


			if(counts < 1) {

				MessageBox.Show("Please select the project to execute？", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);



			} else if(MessageBox.Show("You confirm that you want to execute the app？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {


						if(this.backgroundWorker1.IsBusy != true) {


										this.Hide();

										this.Cursor = Cursors.WaitCursor;

										fr2 = new TprogressBar();
										fr2.Show();
										fr2.show_form1_data(0, "");
										fr2.Cursor = Cursors.WaitCursor;

										this.backgroundWorker1.RunWorkerAsync();

						}

			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
					try {

						#region   Spying

								if(checkBoxDisableWindowsErrorreporting.Checked == true) {

												ReportProgress(1, "Update "+ checkBoxDisableWindowsErrorreporting.Text);


												UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", "1", RegistryValueKind.DWord);
												UpdateRegHklm(@"SOFTWARE\WOW6432Node\Microsoft\Windows\Windows Error Reporting", "Disabled", "1", RegistryValueKind.DWord);

												UpdateRegHklm(@"SYSTEM\ControlSet001\Services\EventLog\Application\Windows Error Reporting", "Disabled", "1", RegistryValueKind.DWord);

												UpdateRegHklm(@"SYSTEM\CurrentControlSet\Services\EventLog\Application\Windows Error Reporting", "Disabled", "1", RegistryValueKind.DWord);

												UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", "1", RegistryValueKind.DWord);




												System.Threading.Thread.Sleep(500);

								}

								if(checkBoxDisablePrivateSettings.Checked == true) {


											ReportProgress(1, "Update "+ checkBoxDisablePrivateSettings.Text);




											string[] regkeyvalandother ={
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{21157C1F-2651-4CC1-90CA-1F28B02263F6}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{2EEF81BE-33FA-4800-9670-1CD474972C3F}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{7D7E8402-7C54-4821-A34E-AEEFD62DED93}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{992AFA70-6F47-4148-B3E9-3003349C1548}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{9D9E0118-1807-4F2E-96E4-2CE57142E196}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{A8804298-2D5F-42E3-9531-9C8C39EB29CE}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{B19F89AF-E3EB-444B-8DEA-202575A71599}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{C1D23ACC-752B-43E5-8448-8D0E519CD6D6}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{D89823BA-7180-4B81-B50C-7E471E6121A3}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{E5323777-F976-4f5b-9B55-B94699C46E44}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{E6AD100E-5F4E-44CD-BE0F-2265D88D14F5}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{E83AF229-8640-4D18-A213-E22675EBB2C3}",
														@"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled"
											};
											foreach(var currentRegKey in regkeyvalandother) {
												UpdateRegHkcu(currentRegKey, "Value", "Deny", RegistryValueKind.String);
											}

											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "CortanaEnabled", "0",RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitInkCollection", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "DisableWebSearch", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", "ConnectedSearchUseWeb", "0",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors","DisableWindowsLocationProvider", "1", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocationScripting","1", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableSensors", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SYSTEM\CurrentControlSet\Services\lfsvc\Service\Configuration", "Status", "0",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}","SensorPermissionState", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Siuf\Rules", "NumberOfSIUFInPeriod", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Siuf\Rules", "PeriodInNanoSeconds", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", "0", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC", "PreventHandwritingDataSharing", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports","PreventHandwritingErrorReports", "1", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableInventory", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoLockScreenCamera", "1",RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0",RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0",RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Input\TIPC", "Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Biometrics", "Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\CredUI", "DisablePasswordReveal", "1",RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync", "SyncPolicy", "5",RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization","Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings","Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", "Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", "Enabled", "0",RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", "Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", "Enabled", "0",RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);

								}





								if(checkBoxKeyLoggerAndTelemetry.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxKeyLoggerAndTelemetry.Text);


											UpdateRegHkcu(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search","AllowCortana","0", RegistryValueKind.DWord);


											  foreach (var service in Global.servicesList) {
															RunWindowsCmd($"/c net stop {service}");
															Startargs("powershell", $"-command \"Set-Service -Name {service} -StartupType Disabled\"");
											  }
											System.Threading.Thread.Sleep(500);
								}



								if(checkBoxDisableOfficetelemetry.Checked == true) {
							
											ReportProgress(1, "Update "+ checkBoxDisableOfficetelemetry.Text);


											UpdateRegHkcu(@"SOFTWARE\Policies\Microsoft\Office\15.0\osm", "Enablelogging", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Policies\Microsoft\Office\15.0\osm", "EnableUpload", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Policies\Microsoft\Office\16.0\osm", "Enablelogging", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Policies\Microsoft\Office\16.0\osm" , "EnableUpload", "0", RegistryValueKind.DWord);


											System.Threading.Thread.Sleep(500);

								}


								if(checkBoxAddToHosts.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxAddToHosts.Text);
											var hostslocation = Environment.SystemDirectory + "\\drivers\\etc\\hosts";
											string hosts = File.ReadAllText(hostslocation);

											  foreach (var currentHostsDomain in Global.hostsdomains.Where(currentHostsDomain =>hosts != null && hosts.IndexOf(currentHostsDomain, StringComparison.Ordinal) == -1)){
								      
														RunWindowsCmd($"/c echo 0.0.0.0 {currentHostsDomain} >> \"{hostslocation}\"");
						
											  }

											System.Threading.Thread.Sleep(500);
								}


								if(checkBoxDisableSync.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxDisableSync.Text);


											UpdateRegHklm(@"Software\Policies\Microsoft\Windows\SettingSync","DisableSettingSync", "2", RegistryValueKind.DWord);
											UpdateRegHklm(@"Software\Policies\Microsoft\Windows\SettingSync", "DisableSettingSyncUserOverride", "1", RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);
								}


								if(checkBoxSPYTasks.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxSPYTasks.Text);

											foreach (var currentTask in Global.disabletaskslist){

												Startargs("SCHTASKS", $"/Change /TN \"{currentTask}\" /disable");
								 
											}


											System.Threading.Thread.Sleep(500);


								}





								if(checkBoxNoLicensechecking.Checked == true) {

													ReportProgress(1, "Update "+ checkBoxNoLicensechecking.Text);



													UpdateRegHklm(@"Software\Policies\Microsoft\Windows NT\CurrentVersion\Software Protection Platform", "NoGenTicket", "1", RegistryValueKind.DWord);

													System.Threading.Thread.Sleep(500);
								}



								if(checkBoxDisablepasswordrevealbutton.Checked == true) {

										ReportProgress(1, "Update "+ checkBoxDisablepasswordrevealbutton.Text);



										UpdateRegHklm(@"Software\Policies\Microsoft\Windows\CredUI", "DisablePasswordReveal", "1", RegistryValueKind.DWord);

										System.Threading.Thread.Sleep(500);

								}


								if(checkBoxWMDRM.Checked == true) {
				
										ReportProgress(1, "Update "+ checkBoxWMDRM.Text);



										UpdateRegHklm(@"Software\Policies\Microsoft\WMDRM", "DisableOnline", "1", RegistryValueKind.DWord);

										System.Threading.Thread.Sleep(500);
								}



								if(checkBoxDisableWiFiSense.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxDisableWiFiSense.Text);

											UpdateRegHklm(@"SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM", "0", RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);

								}


								if(checkBoxDisableAdvertisinginWindowsExplorer.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxDisableAdvertisinginWindowsExplorer.Text);

											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", "AllowAdvertising", "0", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "-", RegistryValueKind.DWord);

											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PenWorkspace", "PenWorkspaceAppSuggestionsEnabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);
								}


								if(checkBoxDisableAdvertisingID.Checked == true) {
											ReportProgress(1, "Update "+ checkBoxDisableAdvertisingID.Text);


											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSyncProviderNotifications", "0", RegistryValueKind.DWord);
											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "1", RegistryValueKind.DWord);

											UpdateRegHkcu(@"SOFTWARE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "1", RegistryValueKind.DWord);


											System.Threading.Thread.Sleep(500);
								}


								if(checkBoxDisableASW.Checked == true) { 

											ReportProgress(1, "Update "+ checkBoxDisableASW.Text);

											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);

											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);

											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PenWorkspace", "PenWorkspaceAppSuggestionsEnabled", "1", RegistryValueKind.DWord);


											System.Threading.Thread.Sleep(500);

								}


								if(checkBoxDBA.Checked == true) {
							
											ReportProgress(1, "Update "+ checkBoxDBA.Text);

											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PenWorkspace", "PenWorkspaceAppSuggestionsEnabled", "0", RegistryValueKind.DWord);

											UpdateRegHkcu(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", "AllowAdvertising", "1", RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);
								}



								if(checkBoxDSMAS.Checked == true) {


											ReportProgress(1, "Update "+ checkBoxDSMAS.Text);


											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "1", RegistryValueKind.DWord);


											System.Threading.Thread.Sleep(500);

								}



								if(checkBoxDTPS.Checked == true) {

											ReportProgress(1, "Update "+ checkBoxDTPS.Text);

											UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);
										
											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableThirdPartySuggestions", "-", RegistryValueKind.DWord);


											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SystemPaneSuggestionsEnabled", "0", RegistryValueKind.DWord);


											System.Threading.Thread.Sleep(500);

								}



								if(checkBoxDFTMLS.Checked == true) {


											ReportProgress(1, "Update "+ checkBoxDFTMLS.Text);


											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenOverlayEnabled", "1", RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);

								}


								if(checkBoxDL.Checked ==true) {

											ReportProgress(1, "Update "+ checkBoxDL.Text);


											UpdateRegHklm(@"SYSTEM\ControlSet001\Services\lfsvc\Service\Configuration", "Status", "1", RegistryValueKind.DWord);


											System.Threading.Thread.Sleep(500);
								}


								if(checkBoxDSAD.Checked == true) {

											ReportProgress(1, "Update " + checkBoxDSAD.Text);

											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CDP", "RomeSdkChannelUserAuthzPolicy", "1", RegistryValueKind.DWord);
										
											UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CDP", "CdpSessionUserAuthzPolicy", "-", RegistryValueKind.DWord);

											System.Threading.Thread.Sleep(500);

								}



								if(checkBoxDWCEIP.Checked == true) {

											ReportProgress(1, "Update " + checkBoxDSAD.Text);

											UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\SQMClient\Windows", "CEIPEnable", "-", RegistryValueKind.DWord);
										

											System.Threading.Thread.Sleep(500);
								}

						#endregion


						#region Apps  Deny = Off    Allow = On
						
						if(checkBox1.Checked == true) {

								ReportProgress(1, "Update " + checkBox1.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}


						if(checkBox2.Checked == true) {

								ReportProgress(1, "Update " + checkBox2.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appointments", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}										

						if(checkBox3.Checked == true) {

								ReportProgress(1, "Update " + checkBox3.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}

						if(checkBox4.Checked == true) {

								ReportProgress(1, "Update " + checkBox4.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\contacts", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}

						if(checkBox5.Checked == true) {

								ReportProgress(1, "Update " + checkBox5.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appDiagnostics", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}

						if(checkBox6.Checked == true) {

								ReportProgress(1, "Update " + checkBox6.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\documentsLibrary", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}

						if(checkBox7.Checked == true) {

								ReportProgress(1, "Update " + checkBox7.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\email", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}

						if(checkBox8.Checked == true) {

								ReportProgress(1, "Update " + checkBox8.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\broadFileSystemAccess", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);
						}



						if(checkBox9.Checked == true) {

								ReportProgress(1, "Update " + checkBox9.Text);

								UpdateRegHkcu(@"Control Panel\International\User Profile", "HttpAcceptLanguageOptOut", "0", RegistryValueKind.DWord);
								
								System.Threading.Thread.Sleep(500);

						}

						if(checkBox10.Checked == true) {

								ReportProgress(1, "Update " + checkBox10.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}

						if(checkBox11.Checked == true) {

								ReportProgress(1, "Update " + checkBox11.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\chat", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}

						if(checkBox12.Checked == true) {

								ReportProgress(1, "Update " + checkBox12.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}

						//Disable Access to Notifications

						if(checkBox13.Checked == true) {

								ReportProgress(1, "Update " + checkBox13.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userNotificationListener", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}

						if(checkBox14.Checked == true) {

								ReportProgress(1, "Update " + checkBox14.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\phoneCallHistory", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}

						if(checkBox15.Checked == true) {

								ReportProgress(1, "Update " + checkBox15.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\picturesLibrary", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}


						if(checkBox16.Checked == true) {

								ReportProgress(1, "Update " + checkBox16.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\radios", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}


						if(checkBox17.Checked == true) {

								ReportProgress(1, "Update " + checkBox17.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userDataTasks", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}

						if(checkBox18.Checked == true) {

								ReportProgress(1, "Update " + checkBox18.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\videosLibrary", "Value", "Deny", RegistryValueKind.String);
								
								System.Threading.Thread.Sleep(500);

						}


						if(checkBox19.Checked == true) {

								ReportProgress(1, "Update " + checkBox19.Text);

								UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications", "ToastEnabled", "0", RegistryValueKind.DWord);

								System.Threading.Thread.Sleep(500);

						}


						if(checkBox20.Checked == true) {

								ReportProgress(1, "Update " + checkBox20.Text);

								UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", "AITEnable", "-", RegistryValueKind.DWord);
								
								System.Threading.Thread.Sleep(500);

						}


						if(checkBox21.Checked == true) {

								ReportProgress(1, "Update " + checkBox21.Text);

								UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications", "GlobalUserDisabled", "0", RegistryValueKind.DWord);

								UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Search", "BackgroundAppGlobalToggle", "1", RegistryValueKind.DWord);

								System.Threading.Thread.Sleep(500);

						}



						if(checkBox22.Checked == true) {

								ReportProgress(1, "Update " + checkBox22.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\phoneCall", "Value", "Allow", RegistryValueKind.String);

								System.Threading.Thread.Sleep(500);

						}


						if(checkBox23.Checked == true) {

								ReportProgress(1, "Update " + checkBox23.Text);

								UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\PushToInstall", "DisablePushToInstall", "-", RegistryValueKind.DWord);

								System.Threading.Thread.Sleep(500);

						}

						if(checkBox24.Checked == true) {

								ReportProgress(1, "Update " + checkBox24.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "1", RegistryValueKind.DWord);
								
					
								UpdateRegHkcu(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", "1", RegistryValueKind.DWord);

								System.Threading.Thread.Sleep(500);

						}

						if(checkBox25.Checked == true) {

								ReportProgress(1, "Update " + checkBox25.Text);

								UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\bluetoothSync", "Value", "Allow", RegistryValueKind.String);
								

								System.Threading.Thread.Sleep(500);

						}

						#endregion


						#region   Optimization



						if(checkBoxCreateSystemRestorePoint.Checked == true) {

									ReportProgress(1, "Update "+ checkBoxCreateSystemRestorePoint.Text);

									var restorepointName = $"Create system restore {DateTime.Now}";

									CreateRestorePoint(restorepointName);

									System.Threading.Thread.Sleep(500);

						}



						if(checkBoxRemoveOneDrive.Checked ==true){

									ReportProgress(1, "Update " + checkBoxRemoveOneDrive.Text);


									RunWindowsCmd("/c taskkill /f /im OneDrive.exe > NUL 2>&1");

									RunWindowsCmd("/c ping 127.0.0.1 -n 5 > NUL 2>&1");

									if(File.Exists(Environment.SystemDirectory + @"\OneDriveSetup.exe")) {

										Startargs(Environment.SystemDirectory + @"\OneDriveSetup.exe", "/uninstall");

									} else if(File.Exists(Environment.GetEnvironmentVariable("WINDIR") + @"\SysWOW64\OneDriveSetup.exe")) {
					
										Startargs(Environment.GetEnvironmentVariable("WINDIR")  + @"\SysWOW64\OneDriveSetup.exe", "/uninstall");
									}


									RunWindowsCmd("/c ping 127.0.0.1 -n 5 > NUL 2>&1");
									RunWindowsCmd("/c rd \"%USERPROFILE%\\OneDrive\" /Q /S > NUL 2>&1");
									RunWindowsCmd("/c rd \"C:\\OneDriveTemp\" /Q /S > NUL 2>&1");
									RunWindowsCmd("/c rd \"%LOCALAPPDATA%\\Microsoft\\OneDrive\" /Q /S > NUL 2>&1");
									RunWindowsCmd("/c rd \"%PROGRAMDATA%\\Microsoft OneDrive\" /Q /S > NUL 2>&1");
									RunWindowsCmd("/c REG DELETE \"HKEY_CLASSES_ROOT\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /f > NUL 2>&1");
									RunWindowsCmd("/c REG DELETE \"HKEY_CLASSES_ROOT\\Wow6432Node\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /f > NUL 2>&1");

									UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC", "1",RegistryValueKind.DWord);





									System.Threading.Thread.Sleep(500);
						}



						if(checkBoxDisableWindowsDefender.Checked == true) {

									ReportProgress(1, "Update "+ checkBoxDisableWindowsDefender.Text);

									UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1",RegistryValueKind.DWord);
									UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "SpyNetReporting", "0",RegistryValueKind.DWord);
									UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "2",RegistryValueKind.DWord);
									UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\MRT", "DontReportInfectionInformation", "1",RegistryValueKind.DWord);
									UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", "SmartScreenEnabled", "Off",RegistryValueKind.String);

									System.Threading.Thread.Sleep(500);
						}


						if(checkBoxAsnrprogram.Checked == true) {

									ReportProgress(1, "Update "+ checkBoxAsnrprogram.Text);

									UpdateRegHkcu(@"Control Panel\Desktop", "AutoEndTasks","-",RegistryValueKind.String);

									System.Threading.Thread.Sleep(500);
						}

						if(checkBoxAcdwp.Checked == true) {

									ReportProgress(1, "Update "+ checkBoxAcdwp.Text);

									UpdateRegHklm(@"SOFTWARE\Policies\Microsoft\Windows\System", "AllowBlockingAppsAtShutdown", "1",RegistryValueKind.DWord);

									System.Threading.Thread.Sleep(500);
						}



						if(checkBoxUwdc.Checked == true) { 

									ReportProgress(1, "Update "+ checkBoxUwdc.Text);


									UpdateRegHklm(@"SYSTEM\ControlSet001\Control\Session Manager", "BootExecute", "hex(7)61,00,75,00", RegistryValueKind.MultiString);

									System.Threading.Thread.Sleep(500);

						}


						if(checkBoxCswdst.Checked == true) {

							ReportProgress(1, "Update " + checkBoxCswdst.Text);

							UpdateRegHklm(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explore", "GlobalAssocChangedCounter", "39", RegistryValueKind.DWord);
							UpdateRegHkcu(@"Software\Microsoft\Windows\CurrentVersion\Explorer", "link", "00,00,00,00", RegistryValueKind.Binary);


							System.Threading.Thread.Sleep(500);

						}





						#endregion


					} catch(Exception ex) {
							Console.WriteLine($"Error start prog  {ex.Message}");
					}


		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
					tocount = 0;
					counts = 0;
					fr2.show_form1_data(0, "");
					fr2.Hide();
					fr2.Cursor = Cursors.Default;
					this.Show();

					this.Cursor = Cursors.Default;

					this.backgroundWorker1.Dispose();
					
					InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
					InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
				
					System.GC.Collect();

					MessageBox.Show("You must reboot to take effect！", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}



		delegate void bar_up_msg(int values, string text);
		private void ReportProgress(int values, string text) {

					if(this.InvokeRequired) {

								bar_up_msg d = new bar_up_msg(ReportProgress);

								this.Invoke(d, values, text);

					} else {

								tocount += values;

								int endcount = (tocount * 100 / counts);


								fr2.show_form1_data(endcount, text);

					}



		}


		private static void CreateRestorePoint(string description) {
						var oScope = new ManagementScope("\\\\localhost\\root\\default");
						var oPath = new ManagementPath("SystemRestore");
						var oGetOp = new ObjectGetOptions();
						var oProcess = new ManagementClass(oScope, oPath, oGetOp);

						var oInParams = oProcess.GetMethodParameters("CreateRestorePoint");
						oInParams["Description"] = description;
						oInParams["RestorePointType"] = 12; // MODIFY_SETTINGS
						oInParams["EventType"] = 100;

						oProcess.InvokeMethod("CreateRestorePoint", oInParams, null);
		}



		private void DeleteWindows10MetroApp(string appname) {
			Startargs("powershell", $"-command \"Get-AppxPackage *{appname}* | Remove-AppxPackage\"");
		}

		public void RunWindowsCmd(string args) {
			Startargs(ShellCmdLocation, args);
		}

		private void Startargs(string name, string args) {
			try {
				var proc = new Process {
					StartInfo = new ProcessStartInfo {
						FileName = name,
						Arguments = args,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true,
						StandardOutputEncoding = Encoding.GetEncoding(866)
					}
				};
				proc.Start();
				string line = null;
				while(!proc.StandardOutput.EndOfStream) {
					line += Environment.NewLine + proc.StandardOutput.ReadLine();
				}
				proc.WaitForExit();

			} catch(Exception ex) {

				Console.WriteLine($"Error start prog {name} {args} {ex.Message}");

			}
		}

		#region  Update registry

		private void UpdateRegHkcu(string regkeyfolder, string paramname, string paramvalue, RegistryValueKind keytype) {

			var registryKey = Registry.CurrentUser.CreateSubKey(regkeyfolder);

			registryKey?.Close();

			var myKey = Registry.CurrentUser.OpenSubKey(regkeyfolder, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
			try {

				myKey?.SetValue(paramname, paramvalue, keytype);

			} catch(Exception ex) {

				Console.WriteLine($"Error UpdateRegHkcu: {ex.Message}");

			}

			myKey?.Close();
		}

		private void UpdateRegHklm(string regkeyfolder, string paramname, string paramvalue, RegistryValueKind keytype) {
			var registryKey = Registry.LocalMachine.CreateSubKey(regkeyfolder);
			registryKey?.Close();
			var myKey = Registry.LocalMachine.OpenSubKey(regkeyfolder, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);

			try {
				myKey?.SetValue(paramname, paramvalue, keytype);

			} catch(Exception ex) {

				Console.WriteLine($"Error UpdateRegHklm: {ex.Message}");

			}
			myKey?.Close();
		}









		#endregion

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			System.GC.Collect();
			System.Environment.Exit(0);
		}
	}
}
