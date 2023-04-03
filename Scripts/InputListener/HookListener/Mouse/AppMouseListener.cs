using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener.Hook;

internal class AppMouseListener : MouseListener
{
    public AppMouseListener() : base(MKHookHandle.HookAppMouse()) { }

    protected override MouseEventData GetEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var mouseHookStruct = MKHookHandle.MarshalHookParam<MouseHookStructEx>(lParam);
        var mouseInput = mouseHookStruct.ToMouseInput();
        return NewEvent((WindowsMessages)wParam, ref mouseInput, swapButtonThreshold);
    }
}
