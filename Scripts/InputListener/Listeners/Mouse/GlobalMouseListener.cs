using MouseAndKeyboard.InputShared;
using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class GlobalMouseListener : MouseListener
{
    private readonly uint systemDoubleClickTime;
    private readonly int doubleClickThresholdX;
    private readonly int doubleClickThresholdY;
    private MouseButton previousClicked;
    private Point previousClickedPosition;
    private int previousClickedTime;

    public GlobalMouseListener() : base(WinHook.HookGlobalMouse())
    {
        systemDoubleClickTime = MouseNativeMethods.GetDoubleClickTime();
        doubleClickThresholdX = SystemMetrics.GetXDoubleClickThreshold();
        doubleClickThresholdY = SystemMetrics.GetYDoubleClickThreshold();
    }

    protected override void InvokeMouseDown(MouseEventData e)
    {
        bool IsDoubleClick = e.Button == previousClicked &&
            Math.Abs(e.X - previousClickedPosition.X) <= doubleClickThresholdX &&
            Math.Abs(e.Y - previousClickedPosition.Y) <= doubleClickThresholdY &&
            (e.Timestamp - previousClickedTime) <= systemDoubleClickTime;

        if(IsDoubleClick)
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

    protected override void InvokeMouseUp(MouseEventData e)
    {
        base.InvokeMouseUp(e);
        if (e.Clicks == 2)
        {
            //StopDoubleClickWaiting
            previousClicked = MouseButton.None;
            previousClickedTime = 0;
            previousClickedPosition = offGridPoint;
        }
    }

    protected override MouseEventData GetEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var mouseHookStruct = WinHook.MarshalHookParam<MouseInput>(lParam);
        return MouseEventData.NewEvent((WindowsMessages)wParam, ref mouseHookStruct, swapButtonThreshold);
    }
}
