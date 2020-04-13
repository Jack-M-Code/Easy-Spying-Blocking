namespace Easy_Spying_Blocking {
	partial class TprogressBar {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(360, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "...";
			// 
			// progressBarX1
			// 
			// 
			// 
			// 
			this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.progressBarX1.FocusCuesEnabled = false;
			this.progressBarX1.Location = new System.Drawing.Point(12, 51);
			this.progressBarX1.Name = "progressBarX1";
			this.progressBarX1.Size = new System.Drawing.Size(360, 23);
			this.progressBarX1.TabIndex = 2;
			this.progressBarX1.Text = "0%";
			this.progressBarX1.TextVisible = true;
			// 
			// TprogressBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(385, 94);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBarX1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TprogressBar";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update progress";
			this.Load += new System.EventHandler(this.TprogressBar_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
	}
}