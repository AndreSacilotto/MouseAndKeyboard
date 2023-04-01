using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.InputSimulator;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public sealed partial class YuumiMaster : IDisposable
{
    private readonly UDPSocket socket;

    private readonly MKListener inputEvents;
    private readonly YummiPacket ypacket = new();

    public UDPSocket Socket => socket;

    public YuumiMaster()
    {
        inputEvents = MKListener.FactoryGlobal();
#if DEBUG
        socket = new UDPSocket(true, YummiPacket.MAX_PACKET_BYTE_SIZE);
        inputEvents.KeyListener.KeyUp += (ev) => { if (ev.KeyCode == Keys.Pause) Application.Exit(); };//Emergency Exit
#else
		socket = new UDPSocket(false);
#endif

        socket.OnConnect += WhenConnect;
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

}