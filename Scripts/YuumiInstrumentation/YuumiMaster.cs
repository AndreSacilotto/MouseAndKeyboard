using MouseAndKeyboard.InputListener;
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

        var mainForm = (MouseAndKeyboard.MainForm)System.Windows.Forms.Application.OpenForms[System.Windows.Forms.Application.OpenForms.Count - 1]!;
        //inputEvents.KeyListener.KeyUp += (ev) =>
        //{
        //    switch (ev.KeyCode)
        //    {
        //        case TOGGLE_CONSOLE: mainForm.Console = !mainForm.Console; break;
        //        case TOGGLE_MOUSEMOVE: mainForm.MMove = !mainForm.MMove; break;
        //        case TOGGLE_MOUSECLICK: mainForm.MClick = !mainForm.MClick; break;
        //        case TOGGLE_KEYS: mainForm.KKey = !mainForm.KKey; break;
        //        case TOGGLE_MOUSESCROLL: mainForm.MScroll = !mainForm.MScroll; break;
        //        case EMERGENCY_QUIT: mainForm.Close(); break;
        //    }
        //};
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