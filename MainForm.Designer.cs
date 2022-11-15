using System.Drawing;

namespace MouseAndKeyboard
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.lblIP = new System.Windows.Forms.Label();
			this.txtIP = new System.Windows.Forms.TextBox();
			this.lblPort = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.chbReceiver = new System.Windows.Forms.CheckBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.txtConsole = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.lblIP);
			this.flowLayoutPanel1.Controls.Add(this.txtIP);
			this.flowLayoutPanel1.Controls.Add(this.lblPort);
			this.flowLayoutPanel1.Controls.Add(this.txtPort);
			this.flowLayoutPanel1.Controls.Add(this.chbReceiver);
			this.flowLayoutPanel1.Controls.Add(this.btnStart);
			this.flowLayoutPanel1.Controls.Add(this.btnStop);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(15);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(169, 294);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// lblIP
			// 
			this.lblIP.AutoSize = true;
			this.lblIP.Location = new System.Drawing.Point(18, 15);
			this.lblIP.Name = "lblIP";
			this.lblIP.Size = new System.Drawing.Size(17, 15);
			this.lblIP.TabIndex = 5;
			this.lblIP.Text = "IP";
			this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtIP
			// 
			this.txtIP.Location = new System.Drawing.Point(18, 33);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new System.Drawing.Size(128, 23);
			this.txtIP.TabIndex = 0;
			this.txtIP.Text = "127.0.0.1";
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(18, 59);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(29, 15);
			this.lblPort.TabIndex = 3;
			this.lblPort.Text = "Port";
			this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(18, 77);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(128, 23);
			this.txtPort.TabIndex = 1;
			this.txtPort.Text = "7777";
			// 
			// chbReceiver
			// 
			this.chbReceiver.AutoSize = true;
			this.chbReceiver.Location = new System.Drawing.Point(18, 106);
			this.chbReceiver.Name = "chbReceiver";
			this.chbReceiver.Size = new System.Drawing.Size(81, 19);
			this.chbReceiver.TabIndex = 6;
			this.chbReceiver.Text = "Is Receiver";
			this.chbReceiver.UseVisualStyleBackColor = true;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(18, 131);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(18, 160);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 3;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.Color.Black;
			this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtConsole.ForeColor = System.Drawing.SystemColors.Window;
			this.txtConsole.Location = new System.Drawing.Point(169, 0);
			this.txtConsole.Margin = new System.Windows.Forms.Padding(10);
			this.txtConsole.MaxLength = 147483647;
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtConsole.Size = new System.Drawing.Size(268, 294);
			this.txtConsole.TabIndex = 0;
			this.txtConsole.TabStop = false;
			this.txtConsole.Text = "Console\r\n";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(437, 294);
			this.Controls.Add(this.txtConsole);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FlowLayoutPanel flowLayoutPanel1;
		private TextBox txtPort;
		private Button btnStart;
		private Button btnStop;
		private Label lblIP;
		private TextBox txtIP;
		private Label lblPort;
		private TextBox txtConsole;
		private CheckBox chbReceiver;
	}
}