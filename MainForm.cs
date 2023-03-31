using MouseAndKeyboard.Network;
using System.Net;
using YuumiInstrumentation;

namespace MouseAndKeyboard;

public partial class MainForm : Form
{
    private IMKInput? connection;

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        Logger.OnLog += LoggerEvents_OnLog;
    }

    private void LoggerEvents_OnLog(string text)
    {
        if (txtConsole.InvokeRequired)
            txtConsole.Invoke(LogAppend, new object[] { text });
        else
            LogAppend(text);
    }

    private void LogAppend(string text)
    {
        if (txtConsole.TextLength > txtConsole.MaxLength / 2)
        {
            txtConsole.ClearUndo();
            txtConsole.Clear();
        }
        txtConsole.AppendText(text);
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        var ip = txtIP.Text;
        var port = int.Parse(txtPort.Text);
        var isReceiver = chbReceiver.Checked;

        IPEndPoint address;
        if (isReceiver)
        {
            var ys = new YuumiSlave();
            address = new(IPAddress.Any, port);
            ys.Socket.Start(address);
            ys.Enabled = true;

            connection = ys;
        }
        else
        {
            var ym = new YuumiMaster();
            address = NetworkUtil.ToAddress(ip, port);
            ym.Socket.Start(address);

            ym.EnabledMM = chbMMove.Checked;
            ym.EnabledMS = chbMScroll.Checked;
            ym.EnabledMC = chbMClick.Checked;
            ym.EnabledKK = chbKKey.Checked;

            connection = ym;
        }

        SetControlServerInput(false);
        SetControlButtons(true);

        Logger.WriteLine("START");
    }

    private void SetControlServerInput(bool enable)
    {
        txtPort.Enabled = enable;
        chbReceiver.Enabled = enable;
        ChbReceiver_CheckStateChanged(null, null);
    }

    private void SetControlButtons(bool isRunning)
    {
        btnStart.Enabled = !isRunning;
        btnStop.Enabled = isRunning;
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs? e)
    {
        connection?.Dispose();
        connection = null;
        SetControlServerInput(true);
    }

    private void BtnStop_Click(object? sender, EventArgs? e)
    {
        MainForm_FormClosing(null, null);

        SetControlButtons(false);

        Logger.WriteLine("STOP");
    }

    private void ChbConsole_CheckedChanged(object? sender, EventArgs? e)
    {
        Logger.Enabled = chbConsole.Checked;
    }

    private void ChbMMove_CheckedChanged(object? sender, EventArgs? e)
    {
        if (connection is not null and YuumiMaster master)
            master.EnabledMM = chbMMove.Checked;
    }

    private void ChbMScroll_CheckedChanged(object? sender, EventArgs? e)
    {
        if (connection is not null and YuumiMaster master)
            master.EnabledMS = chbMScroll.Checked;
    }

    private void ChbMClick_CheckedChanged(object? sender, EventArgs? e)
    {
        if (connection is not null and YuumiMaster master)
            master.EnabledMC = chbMClick.Checked;
    }

    private void ChbKKey_CheckedChanged(object? sender, EventArgs? e)
    {
        if (connection is not null and YuumiMaster master)
            master.EnabledKK = chbKKey.Checked;
    }

    private void ChbReceiver_CheckStateChanged(object? sender, EventArgs? e)
    {
        txtIP.Enabled = !chbReceiver.Checked && txtPort.Enabled;
        chbMMove.Visible = chbMScroll.Visible = chbMClick.Visible = chbKKey.Visible = !chbReceiver.Checked;
    }

}