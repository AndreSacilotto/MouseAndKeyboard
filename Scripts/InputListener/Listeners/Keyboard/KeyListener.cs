using System.Linq;

namespace MouseAndKeyboard.InputListener;

public abstract class KeyboardListener : BaseListener
{
    protected KeyboardListener(WinHook hook) : base(hook)
    {
    }

    public event Action<KeyEventArgs>? KeyDown;
    public event Action<KeyPressEventArgsExt>? KeyPress;
    public event Action<KeyDownTxtEventArgs>? KeyDownTxt;
    public event Action<KeyEventArgs>? KeyUp;

    public void InvokeKeyDown(KeyEventArgsExt e)
    {
        if (KeyDown == null || e.Handled || !e.IsKeyDown)
            return;
        KeyDown(e);
    }

    public void InvokeKeyPress(KeyPressEventArgsExt e)
    {
        if (KeyPress == null || e.Handled || e.IsNonChar)
            return;
        KeyPress(e);
    }

    public void InvokeKeyDownTxt(KeyDownTxtEventArgs e)
    {
        if (KeyDownTxt == null || e.KeyEvent.Handled || !e.KeyEvent.IsKeyDown)
            return;
        KeyDownTxt(e);
    }

    public void InvokeKeyUp(KeyEventArgsExt e)
    {
        if (KeyUp == null || e.Handled || !e.IsKeyUp)
            return;
        KeyUp(e);
    }

    protected override void CallbackInternal(ref IntPtr wParam, ref IntPtr lParam)
    {
        var eDownUp = GetDownUpEventArgs(ref wParam, ref lParam);

        InvokeKeyDown(eDownUp);

        if (KeyPress != null || KeyDownTxt != null)
        {
            var pressEventArgs = GetPressEventArgs(ref wParam, ref lParam);

            foreach (var pressEventArg in pressEventArgs)
                InvokeKeyPress(pressEventArg);

            var downTxtEventArgs = GetDownTxtEventArgs(eDownUp, pressEventArgs);
            InvokeKeyDownTxt(downTxtEventArgs);
        }

        InvokeKeyUp(eDownUp);
    }

    private static KeyDownTxtEventArgs GetDownTxtEventArgs(KeyEventArgsExt eDownUp, IEnumerable<KeyPressEventArgsExt> pressEventArgs)
    {
        var charsCollection = pressEventArgs.Where(e => !e.IsNonChar).Select(e => e.KeyChar);
        var chars = string.Join(string.Empty, charsCollection);
        return new(eDownUp, chars);
    }

    protected abstract List<KeyPressEventArgsExt> GetPressEventArgs(ref IntPtr wParam, ref IntPtr lParam);
    protected abstract KeyEventArgsExt GetDownUpEventArgs(ref IntPtr wParam, ref IntPtr lParam);
}