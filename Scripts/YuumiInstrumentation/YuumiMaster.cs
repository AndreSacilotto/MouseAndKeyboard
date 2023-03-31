using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.InputSimulator;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public sealed partial class YuumiMaster : IDisposable
{
    private readonly UDPSocketShipper socket;

    private readonly YuumiPacketWrite mkPacket = new();
    private readonly MKListener inputEvents;
    //private readonly IKeyboardMouseEvents inputEvents;

    public UDPSocketShipper Socket => socket;

    public YuumiMaster()
    {
#if DEBUG
        socket = new UDPSocketShipper(true);
#else
		socket = new UDPSocketShipper(false);
#endif

        socket.OnConnect += WhenConnect;

        inputEvents = MKListener.FactoryGlobal();
        //inputEvents = Hook.GlobalEvents();

        //Safe Exit
        inputEvents.KeyListener.KeyUp += (ev) => { if (ev.KeyCode == Keys.Pause) Application.Exit(); };
    }

    private void WhenConnect(UDPSocketShipper socket)
    {
        var width = PrimaryMonitor.Instance.Width;
        var height = PrimaryMonitor.Instance.Height;
        Logger.WriteLine($"SEND: Screen {width} {height}");
        mkPacket.WriteScreen(width, height);
        SendPacket();
    }

    public void Dispose()
    {
        EnabledMM = false;
        EnabledMS = false;
        EnabledMC = false;
        EnabledKK = false;
        socket.Dispose();
        inputEvents.Dispose();
    }

    private void SendPacket()
    {
        socket.Send(mkPacket.Packet);
        mkPacket.Packet.Reset();
    }

    #region Enabling

    private bool enabledMM, enabledMS, enabledMC, enabledKK;

    public bool EnabledMM
    {
        get => enabledMM;
        set
        {
            if (value == enabledMM)
                return;
            enabledMM = value;
            if (enabledMM)
                inputEvents.MouseListener.MouseMove += WhenMouseMove;
            else
                inputEvents.MouseListener.MouseMove -= WhenMouseMove;
        }
    }

    public bool EnabledMS
    {
        get => enabledMS;
        set
        {
            if (value == enabledMS)
                return;
            enabledMS = value;
            if (enabledMS)
                inputEvents.MouseListener.MouseWheelVertical += WhenMouseScroll;
            else
                inputEvents.MouseListener.MouseWheelVertical -= WhenMouseScroll;
        }
    }

    public bool EnabledMC
    {
        get => enabledMC;
        set
        {
            if (value == enabledMC)
                return;
            enabledMC = value;
            if (enabledMC)
            {
                inputEvents.MouseListener.MouseDown += WhenMouseDown;
                inputEvents.MouseListener.MouseUp += WhenMouseUp;
            }
            else
            {
                inputEvents.MouseListener.MouseDown -= WhenMouseDown;
                inputEvents.MouseListener.MouseUp -= WhenMouseUp;
            }
        }
    }

    public bool EnabledKK
    {
        get => enabledKK;
        set
        {
            if (value == enabledKK)
                return;
            enabledKK = value;
            if (enabledKK)
            {
                inputEvents.KeyListener.KeyDown += WhenKeyDown;
                inputEvents.KeyListener.KeyUp += WhenKeyUp;
            }
            else
            {
                inputEvents.KeyListener.KeyDown -= WhenKeyDown;
                inputEvents.KeyListener.KeyUp -= WhenKeyUp;
            }
        }
    }
    #endregion

    #region Event Funcs

    private void WhenKeyDown(KeyHookEventArgs ev) => UnifyKey(PressedState.Down, ev);
    private void WhenKeyUp(KeyHookEventArgs ev) => UnifyKey(PressedState.Up, ev);

    private void WhenMouseDown(MouseHookEventArgs ev) => UnifyMouse(PressedState.Down, ev);
    private void WhenMouseUp(MouseHookEventArgs ev) => UnifyMouse(PressedState.Up, ev);

    private void WhenMouseMove(MouseHookEventArgs ev)
    {
        Logger.WriteLine($"SEND: MMove {ev.X} {ev.Y}");
        mkPacket.WriteMouseMove(ev.X, ev.Y);
        SendPacket();
    }

    private void WhenMouseScroll(MouseHookEventArgs ev)
    {
        Logger.WriteLine("SEND: MScroll " + ev.Delta);
        mkPacket.WriteMouseScroll(ev.Delta);
        SendPacket();
    }

    #endregion

    #region Unify
    private void UnifyKey(PressedState pressed, KeyHookEventArgs ev)
    {
        if (ev.Shift && MirrorWhenShiftKeys.Contains(ev.KeyCode))
        {
            Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} | Shift");
            mkPacket.WriteKey(ev.KeyCode, pressed);
            SendPacket();
        }
        else if (ev.Control && SkillUpKeys.TryGetValue(ev.KeyCode, out var key))
        {
            Logger.WriteLine($"SEND: KKey {pressed,-5}: {ev.KeyCode} => {key} | Control");
            mkPacket.WriteKeyModifier(key, Keys.LControlKey, pressed);
            SendPacket();
        }
    }
    private void UnifyMouse(PressedState pressed, MouseHookEventArgs ev)
    {
        if (MouseToKey.TryGetValue(ev.Button, out var key))
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev.Button} => {key}");
            mkPacket.WriteKey(key, pressed);
            SendPacket();
        }
        else
        {
            Logger.WriteLine($"SEND: MClick {pressed,-5}: {ev.Button}");
            mkPacket.WriteMouseClick(ev.Button, pressed);
            SendPacket();
        }
    }
    #endregion

}