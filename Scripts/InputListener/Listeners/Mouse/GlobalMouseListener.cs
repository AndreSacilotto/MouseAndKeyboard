using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class GlobalMouseListener : MouseListener
{
    private readonly uint systemDoubleClickTime;
    private readonly int doubleClickThresholdX;
    private readonly int doubleClickThresholdY;
    private MouseButtons previousClicked;
    private Point previousClickedPosition;
    private int previousClickedTime;

    public GlobalMouseListener() : base(WinHook.HookGlobalMouse())
    {
        doubleClickThresholdX = SystemMetrics.GetXDoubleClickThreshold();
        doubleClickThresholdY = SystemMetrics.GetYDoubleClickThreshold();
    }

    protected override void InvokeMouseDown(MouseHookEventArgs e)
    {
        if (IsDoubleClick(e))
            e = e.ToDoubleClickEventArgs();
        else
        {
            //StartDoubleClickWaiting
            previousClicked = e.Button;
            previousClickedTime = e.Timestamp;
            previousClickedPosition = new(e.X, e.Y);
        }
        base.InvokeMouseDown(e);
    }

    protected override void InvokeMouseUp(MouseHookEventArgs e)
    {
        base.InvokeMouseUp(e);
        if (e.Clicks == 2)
        {
            //StopDoubleClickWaiting
            previousClicked = MouseButtons.None;
            previousClickedTime = 0;
            previousClickedPosition = offGridPoint;
        }
    }

    private bool IsDoubleClick(MouseHookEventArgs e)
    {
        var isXMoving = Math.Abs(e.X - previousClickedPosition.X) > doubleClickThresholdX;
        var isYMoving = Math.Abs(e.Y - previousClickedPosition.Y) > doubleClickThresholdY;

        return e.Button == previousClicked && !isXMoving && !isYMoving &&
            (e.Timestamp - previousClickedTime) <= systemDoubleClickTime;
    }

    protected override MouseHookEventArgs GetEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var mouseHookStruct = WinHook.MarshalHookParam<MouseInput>(lParam);
        return MouseHookEventArgs.NewEvent((WindowsMessages)wParam, ref mouseHookStruct, swapButtonThreshold);
    }
}
