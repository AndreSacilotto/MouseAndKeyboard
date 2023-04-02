using MouseAndKeyboard.InputShared;
using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

public abstract class MouseListener : BaseListener
{
    protected readonly static Point offGridPoint = new(-99999, -99999);

    protected readonly bool swapButtonThreshold;
    private readonly int dragThresholdX;
    private readonly int dragThresholdY;

    private MouseButton singleDown = MouseButton.None;
    private MouseButton doubleDown = MouseButton.None;

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

    protected override void HookCallback(IntPtr wParam, IntPtr lParam)
    {
        var mouseEvent = GetEventArgs(wParam, lParam);

        if (previousPosition != mouseEvent.GetPosition())
            InvokeMouseMove(mouseEvent);

        if (mouseEvent.IsButtonDown)
            InvokeMouseDown(mouseEvent);

        if (mouseEvent.IsButtonUp)
            InvokeMouseUp(mouseEvent);

        if (mouseEvent.IsScrollEvent)
        {
            if (mouseEvent.IsHorizontalWheel)
                InvokMouseWheelHorizontal(mouseEvent);
            else
                InvokMouseWheelVertical(mouseEvent);
        }

        InvokeMouseDrag(mouseEvent);
    }

    protected abstract MouseEventData GetEventArgs(IntPtr wParam, IntPtr lParam);

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
        if (singleDown.HasFlag(MouseButton.Left))
        {
            //Drag Start
            if (dragStartPosition == offGridPoint)
                dragStartPosition = e.GetPosition();
            if (!isDragging)
            {
                var isXDragging = Math.Abs(e.X - dragStartPosition.X) > dragThresholdX;
                var isYDragging = Math.Abs(e.Y - dragStartPosition.Y) > dragThresholdY;
                if (isXDragging || isYDragging)
                    MouseDragStarted?.Invoke(e);
            }
        }
        else
        {
            //Drag End
            dragStartPosition = offGridPoint;
            if (isDragging)
            {
                MouseDragFinished?.Invoke(e);
                isDragging = false;
            }
        }
    }

}
