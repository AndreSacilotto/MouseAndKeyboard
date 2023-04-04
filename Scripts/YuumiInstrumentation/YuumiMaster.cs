using MouseAndKeyboard.InputListener;
using MouseAndKeyboard.InputListener.Hook;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public sealed partial class YuumiMaster : IDisposable
{
    private readonly UDPSocket socket;
    private readonly YummiPacket ypacket = new();

    private readonly bool handleInputEvents;
    private readonly MKHookListener inputEvents;

    public UDPSocket Socket => socket;

    #region Ctor
    public YuumiMaster() : this(MKHookListener.FactoryGlobal(), true) { }

    public YuumiMaster(MKHookListener inputEvents, bool handleInputEvents = false)
    {
        this.handleInputEvents = handleInputEvents;
        this.inputEvents = inputEvents;

        socket = new UDPSocket(MouseAndKeyboard.Util.DebuggingService.IsDebugMode, YummiPacket.MAX_PACKET_BYTE_SIZE);

        socket.OnConnect += WhenConnect;
    }

    #endregion

    public void Dispose()
    {
        EnabledMM = false;
        EnabledMS = false;
        EnabledMC = false;
        EnabledKK = false;
        socket.Dispose();
        if (handleInputEvents)
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