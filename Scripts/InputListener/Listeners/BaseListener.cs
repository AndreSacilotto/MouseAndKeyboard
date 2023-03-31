namespace MouseAndKeyboard.InputListener;

public abstract class BaseListener : IDisposable
{
    private WinHook Handle { get; }

    protected BaseListener(WinHook hook)
    {
        Handle = hook;
        Handle.Callback += Callback;
    }

    public void Dispose()
    {
        Handle.Callback -= Callback;
        Handle.Dispose();
        GC.SuppressFinalize(this);
    }

    private void Callback(IntPtr wParam, IntPtr lParam) => CallbackInternal(wParam, lParam);
    protected abstract void CallbackInternal(IntPtr wParam, IntPtr lParam);
}