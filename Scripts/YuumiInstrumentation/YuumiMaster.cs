using Gma.System.MouseKeyHook;
using MouseAndKeyboard.InputSimulation;
using MouseAndKeyboard.Network;
using MouseAndKeyboard.Util;

using static YuumiInstrumentation.KeysData;

namespace YuumiInstrumentation;

public sealed class YuumiMaster : IMKInput, IDisposable
{
    private readonly UDPSocketShipper socket = new();

    private readonly YuumiPacketWrite mkPacket = new();
    private readonly IKeyboardMouseEvents inputEvents;

    public UDPSocket Socket => socket;

    public YuumiMaster()
    {
        inputEvents = Hook.GlobalEvents();
    }

    public void Dispose()
    {
        EnabledMM = false;
        EnabledMS = false;
        EnabledMC = false;
        EnabledKK = false;
        socket.Dispose();
    }

    #region Events/Enable

    private bool enabledMM, enabledMS, enabledMC, enabledKK;

    public bool Enabled => enabledMM || enabledMS || enabledMC || enabledKK;

    public bool EnabledMM
    {
        get => enabledMM;
        set
        {
            if (value == enabledMM)
                return;
            enabledMM = value;
            if (enabledMM)
                inputEvents.MouseMove += OnMouseMove;
            else
                inputEvents.MouseMove -= OnMouseMove;
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
                inputEvents.MouseWheel += OnMouseScroll;
            else
                inputEvents.MouseWheel -= OnMouseScroll;
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
                inputEvents.MouseDown += OnMouseDown;
                inputEvents.MouseUp += OnMouseUp;
            }
            else
            {
                inputEvents.MouseDown -= OnMouseDown;
                inputEvents.MouseUp -= OnMouseUp;
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
                inputEvents.KeyDown += OnKeyDown;
                inputEvents.KeyUp += OnKeyUp;
            }
            else
            {
                inputEvents.KeyDown -= OnKeyDown;
                inputEvents.KeyUp -= OnKeyUp;
            }
        }
    }

    #endregion

    #region Network Util

    private void SendPacket()
    {
        socket.Send(mkPacket.GetPacket);
        mkPacket.Reset();
    }

    private void WriteKey(Keys key, PressedState pressedState)
    {
        mkPacket.WriteKey(key, pressedState);
        SendPacket();
    }

    #endregion

    #region MOUSE EVENTS
    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        LoggerEvents.WriteLine($"SEND: MMove {e.X} {e.Y}");
        mkPacket.WriteMouseMove(e.X, e.Y);
        SendPacket();
    }

    private void OnMouseScroll(object? sender, MouseEventArgs e)
    {
        LoggerEvents.WriteLine("SEND: MScroll " + e.Delta);
        mkPacket.WriteMouseScroll(e.Delta);
        SendPacket();
    }

    private void OnMouseDown(object? sender, MouseEventArgs e) => UnifyMouse(e, PressedState.Down);

    private void OnMouseUp(object? sender, MouseEventArgs e) => UnifyMouse(e, PressedState.Up);

    #endregion

    #region KEYS EVENTS
    private void OnKeyDown(object? sender, KeyEventArgs e) => UnifyKey(e, PressedState.Down);

    private void OnKeyUp(object? sender, KeyEventArgs e) => UnifyKey(e, PressedState.Up);
    #endregion

    #region Unify

    private void UnifyMouse(MouseEventArgs e, PressedState pressed)
    {
        if (MouseToKey.TryGetValue(e.Button, out var key))
        {
            LoggerEvents.WriteLine($"SEND: MClick {pressed,-5}: {e.Button} => {key}");
            WriteKey(key, pressed);
        }
        else
        {
            LoggerEvents.WriteLine($"SEND: MClick {pressed,-5}: {e.Button}");
            mkPacket.WriteMouseClick(e.Button, pressed);
            SendPacket();
        }
    }

    private void UnifyKey(KeyEventArgs e, PressedState pressed)
    {
        if (e.Shift && MirrorWhenShiftKeys.Contains(e.KeyCode))
        {
            LoggerEvents.WriteLine($"SEND: KKey {pressed,-5}: {e.KeyCode} | Shift");
            WriteKey(e.KeyCode, pressed);
        }
        else if (e.Control && SkillUpKeys.TryGetValue(e.KeyCode, out var key))
        {
            LoggerEvents.WriteLine($"SEND: KKey {pressed,-5}: {e.KeyCode} => {key} | Control");
            mkPacket.WriteKeyModifier(key, Keys.LControlKey, pressed);
            SendPacket();
        }
    }
    #endregion

}