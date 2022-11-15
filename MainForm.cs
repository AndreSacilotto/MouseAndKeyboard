using System.Net;
using MouseAndKeyboard.Network;
using MouseAndKeyboard.Util;
using YuumiInstrumentation;

namespace MouseAndKeyboard;

public partial class MainForm : Form
{
	private IMKInput connection;

	public MainForm()
	{
		InitializeComponent();
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		LoggerEvents.OnLog += LoggerEvents_OnLog;
	}

	private void LoggerEvents_OnLog(string text)
	{
		if (txtConsole.TextLength > ushort.MaxValue)
			txtConsole.Clear();
		txtConsole.AppendText(text);
	}

	private void BtnStart_Click(object sender, EventArgs e)
	{
		var ip = txtIP.Text;
		var port = int.Parse(txtPort.Text);
		var isReceiver = chbReceiver.Checked;

		System.Net.IPEndPoint address;
		if (isReceiver)
		{
			connection = new YuumiSlave();
			address = new(IPAddress.Any, port);
		}
		else
		{ 
			connection = new YuumiMaster();
			address = NetworkUtil.ToAddress(ip, port);
		}

		connection.Socket.Start(address);
		connection.Enabled = true;

		btnStart.Enabled = false;
		btnStop.Enabled = true;

		LoggerEvents.WriteLine("START");
	}

	private void BtnStop_Click(object sender, EventArgs e)
	{
		MainForm_FormClosing(null, null);

		btnStart.Enabled = true;
		btnStop.Enabled = false;

		LoggerEvents.WriteLine("STOP");
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		connection?.Stop();
	}

	private void ChbConsole_CheckedChanged(object sender, EventArgs e)
	{
		LoggerEvents.Enabled = chbConsole.Checked;
	}
}