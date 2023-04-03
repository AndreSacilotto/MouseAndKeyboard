namespace MouseAndKeyboard.InputListener.Hook;

public abstract class BaseListener : IDisposable
{
    private MKHookHandle Handle { get; }

    protected BaseListener(MKHookHandle hook)
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

    protected abstract void HookCallback(ref IntPtr wParam, ref IntPtr lParam);
}