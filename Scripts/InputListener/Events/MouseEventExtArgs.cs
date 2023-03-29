using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;


/// <summary>
///     Provides extended data for the MouseClickExt and MouseMoveExt events.
/// </summary>
public class MouseEventExtArgs : MouseEventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MouseEventExtArgs" /> class.
    /// </summary>
    /// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
    /// <param name="clicks">The number of times a mouse button was pressed.</param>
    /// <param name="point">The X and Y coordinate of a mouse click, in pixels.</param>
    /// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
    /// <param name="timestamp">The system tick count when the event occurred.</param>
    /// <param name="isMouseButtonDown">True if event signals mouse button down.</param>
    /// <param name="isMouseButtonUp">True if event signals mouse button up.</param>
    /// <param name="isHorizontalWheel">True if event signals horizontal wheel action.</param>
    internal MouseEventExtArgs(MouseButtons buttons, int clicks, Point point, int delta, uint timestamp, bool isMouseButtonDown, bool isMouseButtonUp, bool isHorizontalWheel)
        : base(buttons, clicks, point.X, point.Y, delta)
    {
        IsMouseButtonDown = isMouseButtonDown;
        IsMouseButtonUp = isMouseButtonUp;
        IsHorizontalWheel = isHorizontalWheel;
        Timestamp = timestamp;
    }

    /// <summary>
    ///     Set this property to <b>true</b> inside your event handler to prevent further processing of the event in other
    ///     applications.
    /// </summary>
    public bool Handled { get; set; }

    /// <summary>
    ///     True if event contains information about wheel scroll.
    /// </summary>
    public bool IsScroll => Delta != 0;

    /// <summary>
    ///     True if event signals horizontal wheel action.
    /// </summary>
    public bool IsHorizontalWheel { get; }

    /// <summary>
    ///     True if event signals a click. False if it was only a move or wheel scroll.
    /// </summary>
    public bool IsClick => Clicks > 0;

    /// <summary>
    ///     True if event signals mouse button down.
    /// </summary>
    public bool IsMouseButtonDown { get; }

    /// <summary>
    ///     True if event signals mouse button up.
    /// </summary>
    public bool IsMouseButtonUp { get; }

    /// <summary>
    ///     The system tick count of when the event occurred.
    /// </summary>
    public uint Timestamp { get; }

    internal Point Point => new(X, Y);

    /// <summary>
    ///     Creates <see cref="MouseEventExtArgs" /> from relevant mouse data.
    /// </summary>
    /// <param name="wParam">First Windows Message parameter.</param>
    /// <param name="mouseInfo">A MouseInput containing information from which to construct MouseEventExtArgs.</param>
    /// <returns>A new MouseEventExtArgs object.</returns>
    internal static MouseEventExtArgs FromRawData(ref nint wParam, ref nint lParam)
    {
        var mouseHookStruct = WinHook.MarshalHookParam<MouseInput>(lParam);

        var button = MouseButtons.None;
        short mouseDelta = 0;
        var clickCount = 0;

        var isMouseButtonDown = false;
        var isMouseButtonUp = false;
        var isHorizontalWheel = false;

        switch ((WindowsMessages)wParam)
        {
            case WindowsMessages.LBUTTONDOWN:
                isMouseButtonDown = true;
                button = MouseButtons.Left;
                clickCount = 1;
                break;
            case WindowsMessages.LBUTTONUP:
                isMouseButtonUp = true;
                button = MouseButtons.Left;
                clickCount = 1;
                break;
            case WindowsMessages.LBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = MouseButtons.Left;
                clickCount = 2;
                break;
            case WindowsMessages.RBUTTONDOWN:
                isMouseButtonDown = true;
                button = MouseButtons.Right;
                clickCount = 1;
                break;
            case WindowsMessages.RBUTTONUP:
                isMouseButtonUp = true;
                button = MouseButtons.Right;
                clickCount = 1;
                break;
            case WindowsMessages.RBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = MouseButtons.Right;
                clickCount = 2;
                break;
            case WindowsMessages.MBUTTONDOWN:
                isMouseButtonDown = true;
                button = MouseButtons.Middle;
                clickCount = 1;
                break;
            case WindowsMessages.MBUTTONUP:
                isMouseButtonUp = true;
                button = MouseButtons.Middle;
                clickCount = 1;
                break;
            case WindowsMessages.MBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = MouseButtons.Middle;
                clickCount = 2;
                break;
            case WindowsMessages.MOUSEWHEEL:
                isHorizontalWheel = false;
                mouseDelta = mouseHookStruct.mouseData;
                break;
            case WindowsMessages.MOUSEHWHEEL:
                isHorizontalWheel = true;
                mouseDelta = mouseHookStruct.mouseData;
                break;
            case WindowsMessages.XBUTTONDOWN:
                button = mouseHookStruct.AsXButton() == MouseDataXButton.XButton1
                    ? MouseButtons.XButton1
                    : MouseButtons.XButton2;
                isMouseButtonDown = true;
                clickCount = 1;
                break;
            case WindowsMessages.XBUTTONUP:
                button = mouseHookStruct.AsXButton() == MouseDataXButton.XButton1
                    ? MouseButtons.XButton1
                    : MouseButtons.XButton2;
                isMouseButtonUp = true;
                clickCount = 1;
                break;
            case WindowsMessages.XBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = mouseHookStruct.AsXButton() == MouseDataXButton.XButton1
                    ? MouseButtons.XButton1
                    : MouseButtons.XButton2;
                clickCount = 2;
                break;
        }

        if (WinHook.SwapButtonThreshold > 0)
            button = button == MouseButtons.Left ? MouseButtons.Right : MouseButtons.Left;

        var e = new MouseEventExtArgs(
            button,
            clickCount,
            mouseHookStruct.GetPoint(),
            mouseDelta,
            mouseHookStruct.time,
            isMouseButtonDown,
            isMouseButtonUp,
            isHorizontalWheel);

        return e;
    }

    internal MouseEventExtArgs ToDoubleClickEventArgs()
    {
        return new MouseEventExtArgs(Button, 2, Point, Delta, Timestamp, IsMouseButtonDown, IsMouseButtonUp, IsHorizontalWheel);
    }
}