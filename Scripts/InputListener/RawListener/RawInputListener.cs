using MouseAndKeyboard.Native;
using MouseAndKeyboard.Native.RawInput;

namespace MouseAndKeyboard.InputListener.RawInput;

public class RawInputListener
{
    #region Register

    public static void RegisterGlobal(IntPtr handle = 0) => Register(RawInputDeviceFlags.InputSink, handle);
    public static void RegisterBackground(IntPtr handle = 0) => Register(RawInputDeviceFlags.ExInputSink, handle);
    public static void RegisterApp(IntPtr handle = 0) => Register(RawInputDeviceFlags.None, handle);

    private static void Register(RawInputDeviceFlags flags, IntPtr handle)
    {
        var mouseDevice = new RawInputDevice(HIDUsagePage.Generic, HIDUsage.Mouse, flags, handle);
        var keyboardDevice = new RawInputDevice(HIDUsagePage.Generic, HIDUsage.Keyboard, flags, handle);

        RawInputAPI.RegisterRawInputDevices(mouseDevice, keyboardDevice);
    }

    #endregion

    #region Keyboard Events
    public event Action<KeyEventData>? KeyDown;
    public event Action<KeyPressEventData>? KeyPress;
    public event Action<KeyEventData>? KeyUp;
    #endregion

    #region Mouse Events
    public event Action<MouseEventData>? MouseMove;
    public event Action<MouseEventData>? MouseClick;
    public event Action<MouseEventData>? MouseDown;
    public event Action<MouseEventData>? MouseUp;
    public event Action<MouseEventData>? MouseWheelVertical;
    public event Action<MouseEventData>? MouseWheelHorizontal;
    public event Action<MouseEventData>? MouseDoubleClick;
    public event Action<MouseEventData>? MouseDragStarted;
    public event Action<MouseEventData>? MouseDragFinished;
    #endregion

    public void NewInputMessage(WindowsMessages uMsg, IntPtr lParam)
    {

        if (uMsg != WindowsMessages.INPUT)
            return;

        //var rim = (RawInputMessage)wParam;

        var outSize = RawInputAPI.GetRawInputData(lParam, RawInputCommand.Input, out var rawInput, out _);
        if (outSize == -1)
            return;

        switch (rawInput.header.dwType)
        {
            case RawInputType.TypeMouse:
            InvokeMouse(rawInput.data.mouse);
            break;
            case RawInputType.TypeKeyboard:
            InvokeKeyboard(rawInput.data.keyboard);
            break;
            case RawInputType.TypeHID:
            InvokeHID(rawInput.data.hid);
            break;
        }

        return;
    }

    private static void InvokeMouse(RawMouse mouse)
    {
        Logger.WriteLine(mouse);
    }

    private static void InvokeKeyboard(RawKeyboard keyboard)
    {
        Logger.WriteLine(keyboard);
    }

    private static void InvokeHID(RawHID hid)
    {
        Logger.WriteLine(hid);
    }


}
