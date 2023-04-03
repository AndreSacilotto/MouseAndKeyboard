using MouseAndKeyboard.InputListener.RawInput;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;
using System.Windows.Forms;
using YuumiInstrumentation;

namespace MouseAndKeyboard;

public partial class MainForm : Form
{
    public YuumiMaster? ym;
    public YuumiSlave? ys;

    public MainForm()
    {
        InitializeComponent();
        //MaximumSize = Screen.FromControl(this).Bounds.Size;
    }

    #region Props

    public bool Console { get => chbConsole.Checked; set => chbConsole.Checked = value; }
    public bool MMove { get => chbMMove.Checked; set => chbMMove.Checked = value; }
    public bool MClick { get => chbMClick.Checked; set => chbMClick.Checked = value; }
    public bool MScroll { get => chbMScroll.Checked; set => chbMScroll.Checked = value; }
    public bool KKey { get => chbKKey.Checked; set => chbKKey.Checked = value; }

    #endregion

    private void MainForm_Load(object? sender, EventArgs e)
    {
        ChbConsole_CheckedChanged(null, null!);
        //Native.Kernel32.AllocConsole();
        RawInputListener.RegisterGlobal(Handle);
        var l = new RawInputListener();
        OnMSG += (_, msg, _, lparam) => { l.NewInputMessage((WindowsMessages)msg, lparam); return nint.Zero; };
    }

    internal event WindowProc? OnMSG;

    protected override void WndProc(ref Message msg)
    {
        OnMSG?.Invoke(msg.HWnd, (uint)msg.Msg, msg.WParam, msg.LParam);
        base.WndProc(ref msg);
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        Logger.OnLog -= LoggerEvents_OnLog;
        CloseNetwork();
    }

    private void CloseNetwork()
    {
        if (ym != null)
        {
            ym.Dispose();
            ym = null;
        }
        if (ys != null)
        {
            ys.Dispose();
            ys = null;
        }
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
    private void SetControlServerInput(bool enable)
    {
        txtPort.Enabled = enable;
        chbReceiver.Enabled = enable;
        ChbReceiver_CheckStateChanged(null, null!);
    }

    private void SetControlButtons(bool isRunning)
    {
        btnStart.Enabled = !isRunning;
        btnStop.Enabled = isRunning;
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        var ip = txtIP.Text;
        var port = int.Parse(txtPort.Text);
        var isReceiver = chbReceiver.Checked;

        if (isReceiver)
        {
            ys = new YuumiSlave();
            ys.Socket.StartServer(port);
            ys.Enabled = true;
        }
        else
        {
            ym = new YuumiMaster();
            ym.Socket.StartClient(NetworkUtil.ToAddress(ip, port));

            ym.EnabledMM = chbMMove.Checked;
            ym.EnabledMS = chbMScroll.Checked;
            ym.EnabledMC = chbMClick.Checked;
            ym.EnabledKK = chbKKey.Checked;
        }

        SetControlServerInput(false);
        SetControlButtons(true);
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        CloseNetwork();

        SetControlServerInput(true);

        SetControlButtons(false);
    }

    private void ChbConsole_CheckedChanged(object? sender, EventArgs e)
    {
        if (chbConsole.Checked)
            Logger.OnLog += LoggerEvents_OnLog;
        else
            Logger.OnLog -= LoggerEvents_OnLog;
    }

    private void ChbMMove_CheckedChanged(object? sender, EventArgs e)
    {
        if (ym is not null)
            ym.EnabledMM = chbMMove.Checked;
    }

    private void ChbMScroll_CheckedChanged(object? sender, EventArgs e)
    {
        if (ym is not null)
            ym.EnabledMS = chbMScroll.Checked;
    }

    private void ChbMClick_CheckedChanged(object? sender, EventArgs e)
    {
        if (ym is not null)
            ym.EnabledMC = chbMClick.Checked;
    }

    private void ChbKKey_CheckedChanged(object? sender, EventArgs e)
    {
        if (ym is not null)
            ym.EnabledKK = chbKKey.Checked;
    }

    private void ChbReceiver_CheckStateChanged(object? sender, EventArgs e)
    {
        txtIP.Enabled = !chbReceiver.Checked && txtPort.Enabled;
        chbMMove.Visible = chbMScroll.Visible = chbMClick.Visible = chbKKey.Visible = !chbReceiver.Checked;
    }

}