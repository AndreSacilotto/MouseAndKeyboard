using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

/// <summary>
///     Provides extended data for the MouseClickExt and MouseMoveExt events.
/// </summary>
public class MouseEventExtArgs : MouseEventArgs
{
    /// <param name="buttons">One of the MouseButtons values indicating which mouse button was pressed.</param>
    /// <param name="clicks">The number of times a mouse button was pressed.</param>
    /// <param name="point">The X and Y coordinate of a mouse click, in pixels.</param>
    /// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
    /// <param name="timestamp">The system tick count when the event occurred.</param>
    /// <param name="isMouseButtonDown">True if event signals mouse button down.</param>
    /// <param name="isMouseButtonUp">True if event signals mouse button up.</param>
    /// <param name="isHorizontalWheel">True if event signals horizontal wheel action.</param>
    public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta, int timestamp, bool isMouseButtonDown, bool isMouseButtonUp, bool isHorizontalWheel)
        : base(buttons, clicks, x, y, delta)
    {
        IsMouseButtonDown = isMouseButtonDown;
        IsMouseButtonUp = isMouseButtonUp;
        IsHorizontalWheel = isHorizontalWheel;
        Timestamp = timestamp;
    }

    #region Handled
    private bool handled = false;
    /// <summary>Value that represents if the event was already fully procesed</summary>
    public bool Handled => handled;
    /// <summary>Set this event as handled, used to prevent further processing of this event</summary>
    public void Handle() => handled = true;
    #endregion


    public bool IsHorizontalWheel { get; }

    public bool IsMouseButtonDown { get; }

    public bool IsMouseButtonUp { get; }

    /// <summary>The system tick count of when the event occurred</summary>
    public int Timestamp { get; }

    /// <summary>True if event contains information about wheel scroll</summary>
    public bool IsScroll => Delta != 0;

    /// <summary>True if event signals a click. False if it was only a move or wheel scroll</summary>
    public bool IsClick => Clicks > 0;

    /// <summary>
    ///     Creates <see cref="MouseEventExtArgs" /> from relevant mouse data.
    /// </summary>
    /// <param name="wParam">First Windows Message parameter.</param>
    /// <param name="mouseInfo">A MouseInput containing information from which to construct MouseEventExtArgs.</param>
    /// <returns>A new MouseEventExtArgs object.</returns>
    internal static MouseEventExtArgs FromRawData(WindowsMessages WM, ref MouseInput mouseHookStruct)
    {
        var button = MouseButtons.None;
        var mouseDelta = 0;
        var clickCount = 0;

        var isMouseButtonDown = false;
        var isMouseButtonUp = false;
        var isHorizontalWheel = false;

        switch (WM)
        {
            case WindowsMessages.LBUTTONDOWN:
                isMouseButtonDown = true;
                button = GetLeft();
                clickCount = 1;
                break;
            case WindowsMessages.LBUTTONUP:
                isMouseButtonUp = true;
                button = GetLeft();
                clickCount = 1;
                break;
            case WindowsMessages.LBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = GetLeft();
                clickCount = 2;
                break;
            case WindowsMessages.RBUTTONDOWN:
                isMouseButtonDown = true;
                button = GetRight();
                clickCount = 1;
                break;
            case WindowsMessages.RBUTTONUP:
                isMouseButtonUp = true;
                button = GetRight();
                clickCount = 1;
                break;
            case WindowsMessages.RBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = GetRight();
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
                mouseDelta = mouseHookStruct.GetWheelDelta();
                break;
            case WindowsMessages.MOUSEHWHEEL:
                isHorizontalWheel = true;
                mouseDelta = mouseHookStruct.GetWheelDelta();
                break;
            case WindowsMessages.XBUTTONDOWN:
                isMouseButtonDown = true;
                button = GetXButton(mouseHookStruct.AsXButton());
                clickCount = 1;
                break;
            case WindowsMessages.XBUTTONUP:
                isMouseButtonUp = true;
                button = GetXButton(mouseHookStruct.AsXButton());
                clickCount = 1;
                break;
            case WindowsMessages.XBUTTONDBLCLK:
                isMouseButtonDown = true;
                button = GetXButton(mouseHookStruct.AsXButton());
                clickCount = 2;
                break;
        }

        var e = new MouseEventExtArgs(
            button,
            clickCount,
            mouseHookStruct.X,
            mouseHookStruct.Y,
            mouseDelta,
            mouseHookStruct.time,
            isMouseButtonDown,
            isMouseButtonUp,
            isHorizontalWheel);

        return e;

        static MouseButtons GetXButton(MouseDataXButton mx) => mx == MouseDataXButton.XButton1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
        static MouseButtons GetLeft()
        {
            var mb = MouseButtons.Left;
            if (WinHook.SwapButtonThreshold > 0)
                mb = MouseButtons.Right;
            return mb;
        }
        static MouseButtons GetRight()
        {
            var mb = MouseButtons.Right;
            if (WinHook.SwapButtonThreshold > 0)
                mb = MouseButtons.Left;
            return mb;
        }

    }

    public Point GetPosition() => new(X, Y);

    public MouseEventExtArgs ToDoubleClickEventArgs() =>
        new(Button, 2, X, Y, Delta, Timestamp, IsMouseButtonDown, IsMouseButtonUp, IsHorizontalWheel);
}