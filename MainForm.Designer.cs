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
            Label lblIP;
            Label lblPort;
            Label labelSeparator;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            txtIP = new TextBox();
            txtPort = new TextBox();
            chbReceiver = new CheckBox();
            btnStart = new Button();
            btnStop = new Button();
            chbConsole = new CheckBox();
            chbMMove = new CheckBox();
            chbMScroll = new CheckBox();
            chbMClick = new CheckBox();
            chbKKey = new CheckBox();
            txtConsole = new TextBox();
            lblIP = new Label();
            lblPort = new Label();
            labelSeparator = new Label();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Location = new Point(18, 15);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(17, 15);
            lblIP.TabIndex = 5;
            lblIP.Text = "IP";
            lblIP.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(18, 59);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 3;
            lblPort.Text = "Port";
            lblPort.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelSeparator
            // 
            labelSeparator.BorderStyle = BorderStyle.Fixed3D;
            labelSeparator.Location = new Point(18, 211);
            labelSeparator.Name = "labelSeparator";
            labelSeparator.Size = new Size(115, 2);
            labelSeparator.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblIP);
            flowLayoutPanel1.Controls.Add(txtIP);
            flowLayoutPanel1.Controls.Add(lblPort);
            flowLayoutPanel1.Controls.Add(txtPort);
            flowLayoutPanel1.Controls.Add(chbReceiver);
            flowLayoutPanel1.Controls.Add(btnStart);
            flowLayoutPanel1.Controls.Add(btnStop);
            flowLayoutPanel1.Controls.Add(chbConsole);
            flowLayoutPanel1.Controls.Add(labelSeparator);
            flowLayoutPanel1.Controls.Add(chbMMove);
            flowLayoutPanel1.Controls.Add(chbMScroll);
            flowLayoutPanel1.Controls.Add(chbMClick);
            flowLayoutPanel1.Controls.Add(chbKKey);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(15);
            flowLayoutPanel1.Size = new Size(169, 357);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(18, 33);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(128, 23);
            txtIP.TabIndex = 0;
            txtIP.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(18, 77);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(128, 23);
            txtPort.TabIndex = 1;
            txtPort.Text = "7777";
            // 
            // chbReceiver
            // 
            chbReceiver.AutoSize = true;
            chbReceiver.Location = new Point(18, 106);
            chbReceiver.Name = "chbReceiver";
            chbReceiver.Size = new Size(81, 19);
            chbReceiver.TabIndex = 6;
            chbReceiver.Text = "Is Receiver";
            chbReceiver.UseVisualStyleBackColor = true;
            chbReceiver.CheckStateChanged += ChbReceiver_CheckStateChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(18, 131);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += BtnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(18, 160);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 23);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += BtnStop_Click;
            // 
            // chbConsole
            // 
            chbConsole.AutoSize = true;
            chbConsole.Checked = true;
            chbConsole.CheckState = CheckState.Checked;
            chbConsole.Location = new Point(18, 189);
            chbConsole.Name = "chbConsole";
            chbConsole.Size = new Size(114, 19);
            chbConsole.TabIndex = 5;
            chbConsole.Text = "Console Enabled";
            chbConsole.UseVisualStyleBackColor = true;
            chbConsole.CheckedChanged += ChbConsole_CheckedChanged;
            // 
            // chbMMove
            // 
            chbMMove.AutoSize = true;
            chbMMove.Location = new Point(18, 216);
            chbMMove.Name = "chbMMove";
            chbMMove.Size = new Size(112, 19);
            chbMMove.TabIndex = 7;
            chbMMove.Text = "MMove Enabled";
            chbMMove.UseVisualStyleBackColor = true;
            chbMMove.CheckedChanged += ChbMMove_CheckedChanged;
            // 
            // chbMScroll
            // 
            chbMScroll.AutoSize = true;
            chbMScroll.Location = new Point(18, 241);
            chbMScroll.Name = "chbMScroll";
            chbMScroll.Size = new Size(111, 19);
            chbMScroll.TabIndex = 8;
            chbMScroll.Text = "MScroll Enabled";
            chbMScroll.UseVisualStyleBackColor = true;
            chbMScroll.CheckedChanged += ChbMScroll_CheckedChanged;
            // 
            // chbMClick
            // 
            chbMClick.AutoSize = true;
            chbMClick.Location = new Point(18, 266);
            chbMClick.Name = "chbMClick";
            chbMClick.Size = new Size(108, 19);
            chbMClick.TabIndex = 9;
            chbMClick.Text = "MClick Enabled";
            chbMClick.UseVisualStyleBackColor = true;
            chbMClick.CheckedChanged += ChbMClick_CheckedChanged;
            // 
            // chbKKey
            // 
            chbKKey.AutoSize = true;
            chbKKey.Location = new Point(18, 291);
            chbKKey.Name = "chbKKey";
            chbKKey.Size = new Size(97, 19);
            chbKKey.TabIndex = 10;
            chbKKey.Text = "KKey Enabled";
            chbKKey.UseVisualStyleBackColor = true;
            chbKKey.CheckedChanged += ChbKKey_CheckedChanged;
            // 
            // txtConsole
            // 
            txtConsole.BackColor = Color.Black;
            txtConsole.CausesValidation = false;
            txtConsole.Dock = DockStyle.Fill;
            txtConsole.ForeColor = SystemColors.Window;
            txtConsole.Location = new Point(169, 0);
            txtConsole.Margin = new Padding(10);
            txtConsole.Multiline = true;
            txtConsole.Name = "txtConsole";
            txtConsole.PlaceholderText = "Console";
            txtConsole.ReadOnly = true;
            txtConsole.ScrollBars = ScrollBars.Vertical;
            txtConsole.ShortcutsEnabled = false;
            txtConsole.Size = new Size(268, 357);
            txtConsole.TabIndex = 0;
            txtConsole.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 357);
            Controls.Add(txtConsole);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox txtPort;
        private Button btnStart;
        private Button btnStop;
        private Label lblIP;
        private TextBox txtIP;
        private Label lblPort;
        private CheckBox chbConsole;
        private TextBox txtConsole;
        private CheckBox chbMMove;
        private Label labelSeparator;
        private CheckBox chbMScroll;
        private CheckBox chbMClick;
        private CheckBox chbKKey;
        private CheckBox chbReceiver;
    }
}