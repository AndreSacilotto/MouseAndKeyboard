using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

public abstract class MouseListener : BaseListener
{
    protected record class Buttons(bool Left, bool Right, bool Middle, bool X1, bool X2);

    protected readonly static Point offGridPoint = new(-99999, -99999);

    protected readonly bool swapButtonThreshold;
    private readonly int dragThresholdX;
    private readonly int dragThresholdY;

    private MouseButtonsF singleDown = MouseButtonsF.None;
    private MouseButtonsF doubleDown = MouseButtonsF.None;

    private Point previousPosition = offGridPoint;

    private Point dragStartPosition = offGridPoint;
    private bool isDragging = false;

    protected MouseListener(WinHook hook) : base(hook)
    {
        swapButtonThreshold = SystemMetrics.GetSwapButtonThreshold() > 0;
        dragThresholdX = SystemMetrics.GetXDragThreshold();
        dragThresholdY = SystemMetrics.GetYDragThreshold();
    }

    #region Events
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
    protected abstract MouseEventData GetEventArgs(IntPtr wParam, IntPtr lParam);

    protected override void HookCallback(IntPtr wParam, IntPtr lParam)
    {
        var mouseEvent = GetEventArgs(wParam, lParam);

        if (previousPosition != mouseEvent.GetPosition())
            InvokeMouseMove(mouseEvent);

        if (mouseEvent.IsClickEvent)
        {
            if (mouseEvent.IsButtonDown)
                InvokeMouseDown(mouseEvent);

            if (mouseEvent.IsButtonUp)
                InvokeMouseUp(mouseEvent);
        }

        if (mouseEvent.IsScrollEvent)
        {
            if (mouseEvent.IsHorizontalWheel)
                InvokMouseWheelHorizontal(mouseEvent);
            else
                InvokMouseWheelVertical(mouseEvent);
        }

        InvokeMouseDrag(mouseEvent);
    }

    protected void InvokMouseWheelHorizontal(MouseEventData e)
    {
        MouseWheelHorizontal?.Invoke(e);
    }

    protected void InvokMouseWheelVertical(MouseEventData e)
    {
        MouseWheelVertical?.Invoke(e);
    }

    protected virtual void InvokeMouseDown(MouseEventData e)
    {
        MouseDown?.Invoke(e);
        if (e.Handled)
            return;

        if (e.Clicks == 1)
            singleDown |= e.Button;
        else if (e.Clicks == 2)
            doubleDown |= e.Button;
    }

    protected virtual void InvokeMouseUp(MouseEventData e)
    {
        MouseUp?.Invoke(e);
        if (e.Handled)
            return;

        if (singleDown.HasFlag(e.Button))
        {
            MouseClick?.Invoke(e);
            singleDown &= ~e.Button;
        }

        if (doubleDown.HasFlag(e.Button))
        {
            e = e.ToDoubleClickEventArgs();
            MouseDoubleClick?.Invoke(e);
            doubleDown &= ~e.Button;
        }
    }

    private void InvokeMouseMove(MouseEventData e)
    {
        previousPosition = e.GetPosition();
        MouseMove?.Invoke(e);
    }

    private void InvokeMouseDrag(MouseEventData e)
    {
        if (singleDown.HasFlag(MouseButtonsF.Left))
        {
            //Drag Start
            if (dragStartPosition == offGridPoint)
                dragStartPosition = e.GetPosition();
            if (!isDragging)
            {
                var isXDragging = Math.Abs(e.X - dragStartPosition.X) > dragThresholdX;
                var isYDragging = Math.Abs(e.Y - dragStartPosition.Y) > dragThresholdY;
                isDragging = isXDragging || isYDragging;
                if (isDragging)
                    MouseDragStarted?.Invoke(e);
            }
        }
        else
        {
            //Drag End
            dragStartPosition = offGridPoint;
            if (isDragging)
            {
                isDragging = false;
                MouseDragFinished?.Invoke(e);
            }
        }
    }

    protected static MouseEventData NewEvent(WindowsMessages WM, ref MouseInput mouseHookStruct, bool swapButton)
    {
        var button = MouseButtonsF.None;

        bool isMouseButtonVKDown = false;
        bool isMouseButtonVKUp = false;

        var clickCount = 0;
        var mouseScrollDelta = 0;
        var isHorizontalWheel = false;

        switch (WM)
        {
            //LEFT
            case WindowsMessages.LBUTTONDOWN:
            isMouseButtonVKDown = true;
            button = GetLeft();
            clickCount = 1;
            break;
            case WindowsMessages.LBUTTONUP:
            isMouseButtonVKUp = true;
            button = GetLeft();
            clickCount = 1;
            break;
            case WindowsMessages.LBUTTONDBLCLK:
            isMouseButtonVKDown = true;
            button = GetLeft();
            clickCount = 2;
            break;
            //RIGHT
            case WindowsMessages.RBUTTONDOWN:
            isMouseButtonVKDown = true;
            button = GetRight();
            clickCount = 1;
            break;
            case WindowsMessages.RBUTTONUP:
            isMouseButtonVKUp = true;
            button = GetRight();
            clickCount = 1;
            break;
            case WindowsMessages.RBUTTONDBLCLK:
            isMouseButtonVKDown = true;
            button = GetRight();
            clickCount = 2;
            break;
            //MIDDLE
            case WindowsMessages.MBUTTONDOWN:
            isMouseButtonVKDown = true;
            button = MouseButtonsF.Middle;
            clickCount = 1;
            break;
            case WindowsMessages.MBUTTONUP:
            isMouseButtonVKUp = true;
            button = MouseButtonsF.Middle;
            clickCount = 1;
            break;
            case WindowsMessages.MBUTTONDBLCLK:
            isMouseButtonVKDown = true;
            button = MouseButtonsF.Middle;
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
            isMouseButtonVKDown = true;
            button = XButtonToMB(mouseHookStruct.GetXButton());
            clickCount = 1;
            break;
            case WindowsMessages.XBUTTONUP:
            isMouseButtonVKUp = true;
            button = XButtonToMB(mouseHookStruct.GetXButton());
            clickCount = 1;
            break;
            case WindowsMessages.XBUTTONDBLCLK:
            isMouseButtonVKDown = true;
            button = XButtonToMB(mouseHookStruct.GetXButton());
            clickCount = 2;
            break;
        }

        return new MouseEventData(
            button,
            isMouseButtonVKDown,
            isMouseButtonVKUp,
            clickCount,
            mouseHookStruct.X,
            mouseHookStruct.Y,
            mouseScrollDelta,
            isHorizontalWheel,
            mouseHookStruct.time
        );

        static MouseButtonsF XButtonToMB(MouseDataXButton mx) => mx == MouseDataXButton.XButton1 ? MouseButtonsF.XButton1 : MouseButtonsF.XButton2;
        MouseButtonsF GetLeft() => swapButton ? MouseButtonsF.Right : MouseButtonsF.Left;
        MouseButtonsF GetRight() => swapButton ? MouseButtonsF.Left : MouseButtonsF.Right;
    }

}
