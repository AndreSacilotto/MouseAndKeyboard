namespace MouseAndKeyboard.InputListener;

public abstract class BaseListener : IDisposable
{
    private WinHook Handle { get; }

    protected BaseListener(WinHook hook)
    {
        Handle = hook;
        Handle.Callback += HookCallback;
    }

    public void Dispose()
    {
        Handle.Callback -= HookCallback;
        Handle.Dispose();
        GC.SuppressFinalize(this);
    }

    protected abstract void HookCallback(IntPtr wParam, IntPtr lParam);
}