namespace MouseAndKeyboard.InputListener;

internal class AppMouseListener : MouseListener
{
    public AppMouseListener() : base(WinHook.HookAppMouse())
    {
    }

    protected override MouseEventExtArgs GetEventArgs(ref nint wParam, ref nint lParam)
    {
        return MouseEventExtArgs.FromRawData(ref wParam, ref lParam);
    }
}
