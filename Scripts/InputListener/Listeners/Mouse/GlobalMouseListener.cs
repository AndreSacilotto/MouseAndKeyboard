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
            previousClickedPosition = new(e.X, e.Y);
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
        var isXMoving = Math.Abs(e.X - previousClickedPosition.X) > doubleClickThresholdX;
        var isYMoving = Math.Abs(e.Y - previousClickedPosition.Y) > doubleClickThresholdY;

        return e.Button == previousClicked && !isXMoving && !isYMoving &&
            (e.Timestamp - previousClickedTime) <= systemDoubleClickTime;
    }

    protected override MouseEventExtArgs GetEventArgs(ref IntPtr wParam, ref IntPtr lParam)
    {
        var mouseHookStruct = WinHook.MarshalHookParam<MouseInput>(lParam);
        return MouseEventExtArgs.FromRawData((WindowsMessages)wParam, ref mouseHookStruct);
    }
}
