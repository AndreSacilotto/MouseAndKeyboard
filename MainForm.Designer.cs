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
			System.Windows.Forms.Label lblIP;
			System.Windows.Forms.Label lblPort;
			System.Windows.Forms.Label labelSeparator;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.txtIP = new System.Windows.Forms.TextBox();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.chbReceiver = new System.Windows.Forms.CheckBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.chbConsole = new System.Windows.Forms.CheckBox();
			this.chbMMove = new System.Windows.Forms.CheckBox();
			this.chbMScroll = new System.Windows.Forms.CheckBox();
			this.chbMClick = new System.Windows.Forms.CheckBox();
			this.chbKKey = new System.Windows.Forms.CheckBox();
			this.txtConsole = new System.Windows.Forms.TextBox();
			lblIP = new System.Windows.Forms.Label();
			lblPort = new System.Windows.Forms.Label();
			labelSeparator = new System.Windows.Forms.Label();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblIP
			// 
			lblIP.AutoSize = true;
			lblIP.Location = new System.Drawing.Point(18, 15);
			lblIP.Name = "lblIP";
			lblIP.Size = new System.Drawing.Size(17, 15);
			lblIP.TabIndex = 5;
			lblIP.Text = "IP";
			lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPort
			// 
			lblPort.AutoSize = true;
			lblPort.Location = new System.Drawing.Point(18, 59);
			lblPort.Name = "lblPort";
			lblPort.Size = new System.Drawing.Size(29, 15);
			lblPort.TabIndex = 3;
			lblPort.Text = "Port";
			lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelSeparator
			// 
			labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			labelSeparator.Location = new System.Drawing.Point(18, 211);
			labelSeparator.Name = "labelSeparator";
			labelSeparator.Size = new System.Drawing.Size(115, 2);
			labelSeparator.TabIndex = 7;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(lblIP);
			this.flowLayoutPanel1.Controls.Add(this.txtIP);
			this.flowLayoutPanel1.Controls.Add(lblPort);
			this.flowLayoutPanel1.Controls.Add(this.txtPort);
			this.flowLayoutPanel1.Controls.Add(this.chbReceiver);
			this.flowLayoutPanel1.Controls.Add(this.btnStart);
			this.flowLayoutPanel1.Controls.Add(this.btnStop);
			this.flowLayoutPanel1.Controls.Add(this.chbConsole);
			this.flowLayoutPanel1.Controls.Add(labelSeparator);
			this.flowLayoutPanel1.Controls.Add(this.chbMMove);
			this.flowLayoutPanel1.Controls.Add(this.chbMScroll);
			this.flowLayoutPanel1.Controls.Add(this.chbMClick);
			this.flowLayoutPanel1.Controls.Add(this.chbKKey);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(15);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(169, 357);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// txtIP
			// 
			this.txtIP.Location = new System.Drawing.Point(18, 33);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new System.Drawing.Size(128, 23);
			this.txtIP.TabIndex = 0;
			this.txtIP.Text = "127.0.0.1";
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
			this.chbReceiver.CheckStateChanged += new System.EventHandler(this.ChbReceiver_CheckStateChanged);
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
			// chbConsole
			// 
			this.chbConsole.AutoSize = true;
			this.chbConsole.Checked = true;
			this.chbConsole.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbConsole.Location = new System.Drawing.Point(18, 189);
			this.chbConsole.Name = "chbConsole";
			this.chbConsole.Size = new System.Drawing.Size(114, 19);
			this.chbConsole.TabIndex = 5;
			this.chbConsole.Text = "Console Enabled";
			this.chbConsole.UseVisualStyleBackColor = true;
			this.chbConsole.CheckedChanged += new System.EventHandler(this.ChbConsole_CheckedChanged);
			// 
			// chbMMove
			// 
			this.chbMMove.AutoSize = true;
			this.chbMMove.Location = new System.Drawing.Point(18, 216);
			this.chbMMove.Name = "chbMMove";
			this.chbMMove.Size = new System.Drawing.Size(112, 19);
			this.chbMMove.TabIndex = 7;
			this.chbMMove.Text = "MMove Enabled";
			this.chbMMove.UseVisualStyleBackColor = true;
			this.chbMMove.CheckedChanged += new System.EventHandler(this.ChbMMove_CheckedChanged);
			// 
			// chbMScroll
			// 
			this.chbMScroll.AutoSize = true;
			this.chbMScroll.Location = new System.Drawing.Point(18, 241);
			this.chbMScroll.Name = "chbMScroll";
			this.chbMScroll.Size = new System.Drawing.Size(111, 19);
			this.chbMScroll.TabIndex = 8;
			this.chbMScroll.Text = "MScroll Enabled";
			this.chbMScroll.UseVisualStyleBackColor = true;
			this.chbMScroll.CheckedChanged += new System.EventHandler(this.ChbMScroll_CheckedChanged);
			// 
			// chbMClick
			// 
			this.chbMClick.AutoSize = true;
			this.chbMClick.Location = new System.Drawing.Point(18, 266);
			this.chbMClick.Name = "chbMClick";
			this.chbMClick.Size = new System.Drawing.Size(108, 19);
			this.chbMClick.TabIndex = 9;
			this.chbMClick.Text = "MClick Enabled";
			this.chbMClick.UseVisualStyleBackColor = true;
			this.chbMClick.CheckedChanged += new System.EventHandler(this.ChbMClick_CheckedChanged);
			// 
			// chbKKey
			// 
			this.chbKKey.AutoSize = true;
			this.chbKKey.Location = new System.Drawing.Point(18, 291);
			this.chbKKey.Name = "chbKKey";
			this.chbKKey.Size = new System.Drawing.Size(97, 19);
			this.chbKKey.TabIndex = 10;
			this.chbKKey.Text = "KKey Enabled";
			this.chbKKey.UseVisualStyleBackColor = true;
			this.chbKKey.CheckedChanged += new System.EventHandler(this.ChbKKey_CheckedChanged);
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.Color.Black;
			this.txtConsole.CausesValidation = false;
			this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtConsole.ForeColor = System.Drawing.SystemColors.Window;
			this.txtConsole.Location = new System.Drawing.Point(169, 0);
			this.txtConsole.Margin = new System.Windows.Forms.Padding(10);
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.PlaceholderText = "Console";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtConsole.ShortcutsEnabled = false;
			this.txtConsole.Size = new System.Drawing.Size(268, 357);
			this.txtConsole.TabIndex = 0;
			this.txtConsole.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(437, 357);
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
		private CheckBox chbReceiver;
		private CheckBox chbConsole;
		internal TextBox txtConsole;
		private CheckBox chbMMove;
		private Label labelSeparator;
		private CheckBox chbMScroll;
		private CheckBox chbMClick;
		private CheckBox chbKKey;
	}
}