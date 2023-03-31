using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

public abstract class MouseListener : BaseListener
{
    protected readonly Point offGridPoint = new(-99999, -99999);

    private MouseButtons doubleDown;
    private MouseButtons singleDown;
    protected readonly int swapButtonThreshold;
    private readonly int dragThresholdX;
    private readonly int dragThresholdY;
    private Point dragStartPosition;

    private bool isDragging;

    private Point previousPosition;

    protected MouseListener(WinHook hook) : base(hook)
    {
        swapButtonThreshold = SystemMetrics.GetSwapButtonThreshold();
        dragThresholdX = SystemMetrics.GetXDragThreshold();
        dragThresholdY = SystemMetrics.GetYDragThreshold();
        isDragging = false;

        previousPosition = offGridPoint;
        dragStartPosition = offGridPoint;

        doubleDown = MouseButtons.None;
        singleDown = MouseButtons.None;
    }

    #region Events
    public event Action<MouseHookEventArgs>? MouseMove;
    public event Action<MouseHookEventArgs>? MouseClick;
    public event Action<MouseHookEventArgs>? MouseDown;
    public event Action<MouseHookEventArgs>? MouseUp;
    public event Action<MouseHookEventArgs>? MouseWheelVertical;
    public event Action<MouseHookEventArgs>? MouseWheelHorizontal;
    public event Action<MouseHookEventArgs>? MouseDoubleClick;
    public event Action<MouseHookEventArgs>? MouseDragStarted;
    public event Action<MouseHookEventArgs>? MouseDragFinished;
    #endregion

    protected override void CallbackInternal(IntPtr wParam, IntPtr lParam)
    {
        var mouseEvent = GetEventArgs(wParam, lParam);

        if (mouseEvent.IsMouseButtonDown)
            InvokeMouseDown(mouseEvent);

        if (mouseEvent.IsMouseButtonUp)
            InvokeMouseUp(mouseEvent);

        if (mouseEvent.IsScroll)
        {
            if (mouseEvent.IsHorizontalScroll)
                InvokMouseWheelHorizontal(mouseEvent);
            else
                InvokMouseWheelVertical(mouseEvent);
        }

        if (previousPosition != mouseEvent.GetPosition())
            InvokeMouseMove(mouseEvent);

        InvokeMouseDrag(mouseEvent);
    }

    protected abstract MouseHookEventArgs GetEventArgs(IntPtr wParam, IntPtr lParam);

    protected void InvokMouseWheelHorizontal(MouseHookEventArgs e)
    {
        MouseWheelHorizontal?.Invoke(e);
    }

    protected void InvokMouseWheelVertical(MouseHookEventArgs e)
    {
        MouseWheelVertical?.Invoke(e);
    }

    protected virtual void InvokeMouseDown(MouseHookEventArgs e)
    {
        MouseDown?.Invoke(e);
        if (e.Handled)
            return;

        if (e.Clicks == 2)
            doubleDown |= e.Button;

        if (e.Clicks == 1)
            singleDown |= e.Button;
    }

    protected virtual void InvokeMouseUp(MouseHookEventArgs e)
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

    private void InvokeMouseMove(MouseHookEventArgs e)
    {
        previousPosition = e.GetPosition();
        MouseMove?.Invoke(e);
    }

    private void InvokeMouseDrag(MouseHookEventArgs e)
    {
        if (singleDown.HasFlag(MouseButtons.Left))
        {
            if (dragStartPosition.Equals(offGridPoint))
                dragStartPosition = e.GetPosition();
            InvokeMouseDragStarted(e);
        }
        else
        {
            dragStartPosition = offGridPoint;
            InvokeMouseDragFinished(e);
        }
    }

    private void InvokeMouseDragStarted(MouseHookEventArgs e)
    {
        if (!isDragging)
        {
            var isXDragging = Math.Abs(e.X - dragStartPosition.X) > dragThresholdX;
            var isYDragging = Math.Abs(e.Y - dragStartPosition.Y) > dragThresholdY;
            isDragging = isXDragging || isYDragging;

            if (isDragging)
            {
                MouseDragStarted?.Invoke(e);
            }
        }
    }

    private void InvokeMouseDragFinished(MouseHookEventArgs e)
    {
        if (isDragging)
        {
            MouseDragFinished?.Invoke(e);
            isDragging = false;
        }
    }

}
