using MouseAndKeyboard.InputSimulation;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public sealed class YuumiSlave : IMKInput, IDisposable
{
    private readonly UDPSocketReceiver socket;
    public UDPSocket Socket => socket;

    private readonly YuumiPacketRead yuumiRead;

    private bool enabled;

    public YuumiSlave()
    {
#if DEBUG
        socket = new UDPSocketReceiver(true);
#else
		socket = new UDPSocketReceiver(false);
#endif

        socket.MySocket.SendBufferSize = YuumiPacketWrite.MAX_PACKET_BYTE_SIZE;

        yuumiRead = new YuumiPacketRead();

        yuumiRead.OnMouseMove += MouseMove;
        yuumiRead.OnMouseScroll += MouseScroll;
        yuumiRead.OnMouseClick += MouseClick;
        yuumiRead.OnKeyPress += Key;
        yuumiRead.OnKeyModifierPress += KeyModifier;
    }

    private void OnReceive(int bytes, byte[] data) => yuumiRead.ReadAll(data);

    #region Read

    public static void MouseMove(int x, int y)
    {
        Logger.WriteLine($"RECEIVE: MMove {x} {y}");
        MouseSender.MoveAbsolute(x, y);
    }

    public static void MouseScroll(int scrollDelta)
    {
        Logger.WriteLine($"RECEIVE: MScroll {scrollDelta}");
        MouseSender.ScrollWheel(scrollDelta);
    }

    public static void MouseClick(MouseButtons mouseButton, PressedState pressedState)
    {
        Logger.WriteLine($"RECEIVE: MClick {mouseButton} {pressedState}");
        MouseSender.Click(pressedState, mouseButton);
    }

    public static void Key(Keys keys, PressedState pressedState)
    {
        Logger.WriteLine($"RECEIVE: KKey {keys} {pressedState}");

        if (pressedState == PressedState.Down)
            KeyboardSender.SendKeyDown(keys);
        else if (pressedState == PressedState.Up)
            KeyboardSender.SendKeyUp(keys);
        else
            KeyboardSender.SendFull(keys);
    }

    public static void KeyModifier(Keys keys, Keys modifier, PressedState pressedState)
    {
        Logger.WriteLine($"RECEIVE: KKeyMod {keys} {pressedState}");

        if (pressedState == PressedState.Down)
            KeyboardSender.SendKeyDown(keys, modifier);
        else if (pressedState == PressedState.Up)
            KeyboardSender.SendKeyUp(keys, modifier);
        else
            KeyboardSender.SendFull(keys, modifier);
    }

    #endregion

    public bool Enabled
    {
        get => enabled;
        set
        {
            if (value == enabled)
                return;

            enabled = value;
            if (enabled)
                socket.OnReceive += OnReceive;
            else
                socket.OnReceive -= OnReceive;
        }
    }

    public void Dispose()
    {
        Enabled = false;

        yuumiRead.OnMouseMove -= MouseMove;
        yuumiRead.OnMouseScroll -= MouseScroll;
        yuumiRead.OnMouseClick -= MouseClick;
        yuumiRead.OnKeyPress -= Key;
        yuumiRead.OnKeyModifierPress -= KeyModifier;

        socket.Dispose();
    }
}