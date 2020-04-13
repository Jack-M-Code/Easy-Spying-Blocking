namespace Easy_Spying_Blocking {
	partial class AboutBox {
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent() {
                     System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
                     this.textBoxDescription = new System.Windows.Forms.TextBox();
                     this.okButton = new System.Windows.Forms.Button();
                     this.labelProductName = new System.Windows.Forms.Label();
                     this.labelVersion = new System.Windows.Forms.Label();
                     this.labelCopyright = new System.Windows.Forms.Label();
                     this.labelCompanyName = new System.Windows.Forms.Label();
                     this.labelOs = new System.Windows.Forms.Label();
                     this.SuspendLayout();
                     // 
                     // textBoxDescription
                     // 
                     this.textBoxDescription.Location = new System.Drawing.Point(7, 163);
                     this.textBoxDescription.Margin = new System.Windows.Forms.Padding(7, 4, 3, 4);
                     this.textBoxDescription.Multiline = true;
                     this.textBoxDescription.Name = "textBoxDescription";
                     this.textBoxDescription.ReadOnly = true;
                     this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                     this.textBoxDescription.Size = new System.Drawing.Size(431, 186);
                     this.textBoxDescription.TabIndex = 23;
                     this.textBoxDescription.TabStop = false;
                     this.textBoxDescription.Text = resources.GetString("textBoxDescription.Text");
                     // 
                     // okButton
                     // 
                     this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                     this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                     this.okButton.Location = new System.Drawing.Point(354, 364);
                     this.okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                     this.okButton.Name = "okButton";
                     this.okButton.Size = new System.Drawing.Size(87, 27);
                     this.okButton.TabIndex = 25;
                     this.okButton.Text = "Close(&C)";
                     this.okButton.Click += new System.EventHandler(this.okButton_Click);
                     // 
                     // labelProductName
                     // 
                     this.labelProductName.AutoSize = true;
                     this.labelProductName.Location = new System.Drawing.Point(7, 15);
                     this.labelProductName.Name = "labelProductName";
                     this.labelProductName.Size = new System.Drawing.Size(56, 16);
                     this.labelProductName.TabIndex = 26;
                     this.labelProductName.Text = "產品名稱";
                     // 
                     // labelVersion
                     // 
                     this.labelVersion.AutoSize = true;
                     this.labelVersion.Location = new System.Drawing.Point(7, 44);
                     this.labelVersion.Name = "labelVersion";
                     this.labelVersion.Size = new System.Drawing.Size(32, 16);
                     this.labelVersion.TabIndex = 27;
                     this.labelVersion.Text = "版本";
                     // 
                     // labelCopyright
                     // 
                     this.labelCopyright.AutoSize = true;
                     this.labelCopyright.Location = new System.Drawing.Point(7, 103);
                     this.labelCopyright.Name = "labelCopyright";
                     this.labelCopyright.Size = new System.Drawing.Size(17, 16);
                     this.labelCopyright.TabIndex = 28;
                     this.labelCopyright.Text = "...";
                     // 
                     // labelCompanyName
                     // 
                     this.labelCompanyName.AutoSize = true;
                     this.labelCompanyName.Location = new System.Drawing.Point(7, 73);
                     this.labelCompanyName.Name = "labelCompanyName";
                     this.labelCompanyName.Size = new System.Drawing.Size(17, 16);
                     this.labelCompanyName.TabIndex = 29;
                     this.labelCompanyName.Text = "...";
                     // 
                     // labelOs
                     // 
                     this.labelOs.AutoSize = true;
                     this.labelOs.Location = new System.Drawing.Point(8, 133);
                     this.labelOs.Name = "labelOs";
                     this.labelOs.Size = new System.Drawing.Size(17, 16);
                     this.labelOs.TabIndex = 30;
                     this.labelOs.Text = "...";
                     // 
                     // AboutBox
                     // 
                     this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
                     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                     this.ClientSize = new System.Drawing.Size(448, 400);
                     this.Controls.Add(this.labelOs);
                     this.Controls.Add(this.labelCompanyName);
                     this.Controls.Add(this.labelCopyright);
                     this.Controls.Add(this.labelVersion);
                     this.Controls.Add(this.labelProductName);
                     this.Controls.Add(this.okButton);
                     this.Controls.Add(this.textBoxDescription);
                     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                     this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                     this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                     this.MaximizeBox = false;
                     this.MinimizeBox = false;
                     this.Name = "AboutBox";
                     this.Padding = new System.Windows.Forms.Padding(10, 11, 10, 11);
                     this.ShowIcon = false;
                     this.ShowInTaskbar = false;
                     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                     this.Text = "AboutBox";
                     this.Activated += new System.EventHandler(this.AboutBox_Activated);
                     this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutBox_FormClosed);
                     this.Load += new System.EventHandler(this.AboutBox_Load);
                     this.ResumeLayout(false);
                     this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.Label labelCompanyName;
		private System.Windows.Forms.Label labelOs;
	}
}
