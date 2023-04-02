using MouseAndKeyboard.InputShared;
using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

/// <param name="Button">Indicates which MouseButton was pressed</param>
/// <param name="IsButtonDown">True if event signals mouse button down</param>
/// <param name="IsButtonUp">True if event signals mouse button up</param>
/// <param name="X">The X coordinate/position of the mouse</param>
/// <param name="Y">The Y coordinate/position of the mouse</param>
/// <param name="Clicks">The number of times a mouse button was pressed</param>
/// <param name="SrollDelta">A signed count of the number of detents the wheel has rotated</param>
/// <param name="IsHorizontalWheel">True if event signals horizontal wheel action</param>
/// <param name="Timestamp">The system tick count when the event occurred</param>
public record class MouseEventData(MouseButton Button, bool IsButtonDown, bool IsButtonUp, int Clicks, int X, int Y, int SrollDelta, bool IsHorizontalWheel, int Timestamp) : EventData(Timestamp)
{
    /// <summary>True if event contains information about wheel scroll</summary>
    public bool IsScrollEvent => SrollDelta != 0;

    /// <summary>Gets the location of the mouse during MouseEvent</summary>
    public Point GetPosition() => new(X, Y);

    public MouseEventData ToDoubleClickEventArgs() => new(Button, IsButtonDown, IsButtonUp, 2, X, Y, SrollDelta, IsHorizontalWheel, Timestamp);

    internal static MouseEventData NewEvent(WindowsMessages WM, ref MouseInput mouseHookStruct, bool swapButton)
    {
        var button = MouseButton.None;

        bool isMouseButtonDown = false;
        bool isMouseButtonUp = false;

        var clickCount = 0;
        var mouseScrollDelta = 0;
        var isHorizontalWheel = false;

        switch (WM)
        {
            //LEFT
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
            //RIGHT
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
            //MIDDLE
            case WindowsMessages.MBUTTONDOWN:
            isMouseButtonDown = true;
            button = MouseButton.Middle;
            clickCount = 1;
            break;
            case WindowsMessages.MBUTTONUP:
            isMouseButtonUp = true;
            button = MouseButton.Middle;
            clickCount = 1;
            break;
            case WindowsMessages.MBUTTONDBLCLK:
            isMouseButtonDown = true;
            button = MouseButton.Middle;
            clickCount = 2;
            break;
            //WHEEL
            case WindowsMessages.MOUSEWHEEL:
            isHorizontalWheel = false;
            mouseScrollDelta = mouseHookStruct.GetWheelDelta();
            break;
            case WindowsMessages.MOUSEHWHEEL:
            isHorizontalWheel = true;
            mouseScrollDelta = mouseHookStruct.GetWheelDelta();
            break;
            //XButton
            case WindowsMessages.XBUTTONDOWN:
            isMouseButtonDown = true;
            button = XButtonToMB(mouseHookStruct.AsXButton());
            clickCount = 1;
            break;
            case WindowsMessages.XBUTTONUP:
            isMouseButtonUp = true;
            button = XButtonToMB(mouseHookStruct.AsXButton());
            clickCount = 1;
            break;
            case WindowsMessages.XBUTTONDBLCLK:
            isMouseButtonDown = true;
            button = XButtonToMB(mouseHookStruct.AsXButton());
            clickCount = 2;
            break;
        }

        MouseEventData e = new(
            button,
            isMouseButtonDown,
            isMouseButtonUp,
            clickCount,
            mouseHookStruct.X,
            mouseHookStruct.Y,
            mouseScrollDelta,
            isHorizontalWheel,
            mouseHookStruct.time
        );

        return e;

        static MouseButton XButtonToMB(MouseDataXButton mx) => mx == MouseDataXButton.XButton1 ? MouseButton.XButton1 : MouseButton.XButton2;
        MouseButton GetLeft() => swapButton ? MouseButton.Right : MouseButton.Left;
        MouseButton GetRight() => swapButton ? MouseButton.Left : MouseButton.Right;
    }
}