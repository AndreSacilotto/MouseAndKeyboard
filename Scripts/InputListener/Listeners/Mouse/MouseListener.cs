using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

public abstract class MouseListener : BaseListener
{
    protected readonly Point offGridPoint = new(-99999, -99999);

    private MouseButtons doubleDown;
    private MouseButtons singleDown;
    private readonly int dragThresholdX;
    private readonly int dragThresholdY;
    private Point dragStartPosition;

    private bool isDragging;

    private Point previousPosition;

    protected MouseListener(WinHook hook) : base(hook)
    {
        dragThresholdX = SystemMetrics.GetXDragThreshold();
        dragThresholdY = SystemMetrics.GetYDragThreshold();
        isDragging = false;

        previousPosition = offGridPoint;
        dragStartPosition = offGridPoint;

        doubleDown = MouseButtons.None;
        singleDown = MouseButtons.None;
    }

    #region Events
    public event Action<MouseEventArgs>? MouseMove;
    public event Action<MouseEventExtArgs>? MouseMoveExt;
    public event Action<MouseEventArgs>? MouseClick;
    public event Action<MouseEventArgs>? MouseDown;
    public event Action<MouseEventExtArgs>? MouseDownExt;
    public event Action<MouseEventArgs>? MouseUp;
    public event Action<MouseEventExtArgs>? MouseUpExt;
    public event Action<MouseEventArgs>? MouseWheel;
    public event Action<MouseEventExtArgs>? MouseWheelExt;
    public event Action<MouseEventArgs>? MouseHWheel;
    public event Action<MouseEventExtArgs>? MouseHWheelExt;
    public event Action<MouseEventArgs>? MouseDoubleClick;
    public event Action<MouseEventArgs>? MouseDragStarted;
    public event Action<MouseEventExtArgs>? MouseDragStartedExt;
    public event Action<MouseEventArgs>? MouseDragFinished;
    public event Action<MouseEventExtArgs>? MouseDragFinishedExt;
    #endregion

    protected override void CallbackInternal(ref IntPtr wParam, ref IntPtr lParam)
    {
        var e = GetEventArgs(ref wParam, ref lParam);

        if (e.IsMouseButtonDown)
            ProcessDown(e);

        if (e.IsMouseButtonUp)
            ProcessUp(e);

        if (e.IsScroll)
        {
            if (e.IsHorizontalWheel)
                ProcessHWheel(e);
            else
                ProcessWheel(e);
        }

        if (previousPosition != e.GetPosition())
            ProcessMove(e);

        ProcessDrag(e);
    }

    protected abstract MouseEventExtArgs GetEventArgs(ref IntPtr wParam, ref IntPtr lParam);

    protected virtual void ProcessWheel(MouseEventExtArgs e)
    {
        MouseWheel?.Invoke(e);
        MouseWheelExt?.Invoke(e);
    }

    protected virtual void ProcessHWheel(MouseEventExtArgs e)
    {
        MouseHWheel?.Invoke(e);
        MouseHWheelExt?.Invoke(e);
    }

    protected virtual void ProcessDown(MouseEventExtArgs e)
    {
        MouseDown?.Invoke(e);
        MouseDownExt?.Invoke(e);
        if (e.Handled)
            return;

        if (e.Clicks == 2)
            doubleDown |= e.Button;

        if (e.Clicks == 1)
            singleDown |= e.Button;
    }

    protected virtual void ProcessUp(MouseEventExtArgs e)
    {
        MouseUp?.Invoke(e);
        MouseUpExt?.Invoke(e);
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

    private void ProcessMove(MouseEventExtArgs e)
    {
        previousPosition = e.GetPosition();

        MouseMove?.Invoke(e);
        MouseMoveExt?.Invoke(e);
    }

    private void ProcessDrag(MouseEventExtArgs e)
    {
        if (singleDown.HasFlag(MouseButtons.Left))
        {
            if (dragStartPosition.Equals(offGridPoint))
                dragStartPosition = e.GetPosition();
            ProcessDragStarted(e);
        }
        else
        {
            dragStartPosition = offGridPoint;
            ProcessDragFinished(e);
        }
    }

    private void ProcessDragStarted(MouseEventExtArgs e)
    {
        if (!isDragging)
        {
            var isXDragging = Math.Abs(e.X - dragStartPosition.X) > dragThresholdX;
            var isYDragging = Math.Abs(e.Y - dragStartPosition.Y) > dragThresholdY;
            isDragging = isXDragging || isYDragging;

            if (isDragging)
            {
                MouseDragStarted?.Invoke(e);
                MouseDragStartedExt?.Invoke(e);
            }
        }
    }

    private void ProcessDragFinished(MouseEventExtArgs e)
    {
        if (isDragging)
        {
            MouseDragFinished?.Invoke(e);
            MouseDragFinishedExt?.Invoke(e);
            isDragging = false;
        }
    }

}
