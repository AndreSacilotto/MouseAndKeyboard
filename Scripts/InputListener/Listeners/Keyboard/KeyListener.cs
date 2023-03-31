using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

public abstract class KeyboardListener : BaseListener
{
    protected KeyboardListener(WinHook hook) : base(hook) { }

    public event Action<KeyHookEventArgs>? KeyDown;
    public event Action<KeyHookPressEventArgs>? KeyPress;
    public event Action<KeyHookEventArgs>? KeyUp;

    public void InvokeKeyDown(KeyHookEventArgs e)
    {
        if (KeyDown == null || e.Handled || !e.IsKeyDown)
            return;
        KeyDown(e);
    }

    public void InvokeKeyPress(KeyHookPressEventArgs e)
    {
        if (KeyPress == null || e.Handled || e.IsNonChar)
            return;
        KeyPress(e);
    }

    public void InvokeKeyUp(KeyHookEventArgs e)
    {
        if (KeyUp == null || e.Handled || !e.IsKeyUp)
            return;
        KeyUp(e);
    }

    protected override void CallbackInternal(IntPtr wParam, IntPtr lParam)
    {
        var keyEvent = GetKeyEventArgs(wParam, lParam);

        InvokeKeyDown(keyEvent);

        if (KeyPress != null)
        {
            foreach (var ev in GetPressEventArgs(wParam, lParam))
                InvokeKeyPress(ev);
        }

        InvokeKeyUp(keyEvent);
    }

    protected abstract KeyHookEventArgs GetKeyEventArgs(IntPtr wParam, IntPtr lParam);
    protected abstract IEnumerable<KeyHookPressEventArgs> GetPressEventArgs(IntPtr wParam, IntPtr lParam);
}