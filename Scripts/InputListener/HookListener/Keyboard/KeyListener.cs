namespace MouseAndKeyboard.InputListener.Hook;

public abstract class KeyboardListener : BaseListener
{
    protected KeyboardListener(MKWinHook hook) : base(hook) { }

    public event Action<KeyEventData>? KeyDown;
    public event Action<KeyPressEventData>? KeyPress;
    public event Action<KeyEventData>? KeyUp;

    public void InvokeKeyDown(KeyEventData e)
    {
        if (KeyDown == null || e.Handled || !e.IsKeyDown)
            return;
        KeyDown(e);
    }

    public void InvokeKeyPress(KeyPressEventData e)
    {
        if (KeyPress == null || e.Handled || e.IsNonChar)
            return;
        KeyPress(e);
    }

    public void InvokeKeyUp(KeyEventData e)
    {
        if (KeyUp == null || e.Handled || !e.IsKeyUp)
            return;
        KeyUp(e);
    }

    protected override void HookCallback(IntPtr wParam, IntPtr lParam)
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

    protected abstract KeyEventData GetKeyEventArgs(IntPtr wParam, IntPtr lParam);
    protected abstract IEnumerable<KeyPressEventData> GetPressEventArgs(IntPtr wParam, IntPtr lParam);
}