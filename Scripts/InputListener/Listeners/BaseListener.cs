namespace MouseAndKeyboard.InputListener;
public abstract class BaseListener : IDisposable
{
    protected WinHook Handle { get; }

    protected BaseListener(WinHook hook)
    {
        Handle = hook;
        Handle.Callback += Callback;
    }

    public void Dispose()
    {
        Handle.Dispose();
        GC.SuppressFinalize(this);
    }

    private void Callback(IntPtr wParam, IntPtr lParam) => CallbackInternal(ref wParam, ref lParam);
    protected abstract void CallbackInternal(ref IntPtr wParam, ref IntPtr lParam);
}