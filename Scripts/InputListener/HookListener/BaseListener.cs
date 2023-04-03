namespace MouseAndKeyboard.InputListener.Hook;

public abstract class BaseListener : IDisposable
{
    private MKWinHook Handle { get; }

    protected BaseListener(MKWinHook hook)
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