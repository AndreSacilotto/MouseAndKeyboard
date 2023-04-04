namespace MouseAndKeyboard.InputListener.Hook;

public abstract class KeyboardListener : BaseListener
{
    protected KeyboardListener(MKHookHandle hook) : base(hook) { }

    public event Action<KeyboardEventData>? KeyDown;
    public event Action<KeyPressEventData>? KeyPress;
    public event Action<KeyboardEventData>? KeyUp;

    public void InvokeKeyDown(KeyboardEventData e)
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

    public void InvokeKeyUp(KeyboardEventData e)
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

    protected abstract KeyboardEventData GetKeyEventArgs(IntPtr wParam, IntPtr lParam);
    protected abstract IEnumerable<KeyPressEventData> GetPressEventArgs(IntPtr wParam, IntPtr lParam);
}