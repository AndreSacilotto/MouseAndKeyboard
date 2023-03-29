using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class GlobalMouseListener : MouseListener
{
    private readonly int systemDoubleClickTime;
    private readonly int doubleClickThresholdX;
    private readonly int doubleClickThresholdY;
    private MouseButtons previousClicked;
    private Point previousClickedPosition;
    private int previousClickedTime;

    public GlobalMouseListener() : base(WinHook.HookGlobalMouse())
    {
        systemDoubleClickTime = MouseNativeMethods.GetDoubleClickTime();
        doubleClickThresholdX = SystemMetrics.GetXDoubleClickThreshold();
        doubleClickThresholdY = SystemMetrics.GetYDoubleClickThreshold();
    }

    protected override void ProcessDown(MouseEventExtArgs e)
    {
        if (IsDoubleClick(e))
            e = e.ToDoubleClickEventArgs();
        else
        {
            //StartDoubleClickWaiting
            previousClicked = e.Button;
            previousClickedTime = e.Timestamp;
            previousClickedPosition = e.Point;
        }
        base.ProcessDown(e);
    }

    protected override void ProcessUp(MouseEventExtArgs e)
    {
        base.ProcessUp(e);
        if (e.Clicks == 2)
        {
            //StopDoubleClickWaiting
            previousClicked = MouseButtons.None;
            previousClickedTime = 0;
            previousClickedPosition = offGridPoint;
        }
    }

    private bool IsDoubleClick(MouseEventExtArgs e)
    {
        var isXMoving = Math.Abs(e.Point.X - previousClickedPosition.X) > doubleClickThresholdX;
        var isYMoving = Math.Abs(e.Point.Y - previousClickedPosition.Y) > doubleClickThresholdY;

        return e.Button == previousClicked && !isXMoving && !isYMoving &&
            (e.Timestamp - previousClickedTime) <= systemDoubleClickTime;
    }

    protected override MouseEventExtArgs GetEventArgs(ref nint wParam, ref nint lParam)
    {
        return MouseEventExtArgs.FromRawDataGlobal(ref wParam, ref lParam);
    }
}
