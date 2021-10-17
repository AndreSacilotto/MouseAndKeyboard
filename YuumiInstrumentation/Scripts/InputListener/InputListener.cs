using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

public class InputListener : IDisposable
{
    private IKeyboardMouseEvents inputEvents;

    public InputListener(MouseEventHandler OnMouseMove, MouseEventHandler OnMouseDown, MouseEventHandler OnMouseDoubleClick, MouseEventHandler OnMouseScroll, KeyEventHandler OnKeyDown, KeyEventHandler OnKeyUp)
    {
        inputEvents = Hook.GlobalEvents();

        inputEvents.KeyDown += OnKeyDownBase;
        inputEvents.KeyUp += OnKeyUpBase;
    }

    public event MouseEventHandler OnMoveMove
    {
        add => inputEvents.MouseMove += value;
        remove => inputEvents.MouseMove -= value;
    }
    public event MouseEventHandler OnMouseDown
    {
        add => inputEvents.MouseDown += value;
        remove => inputEvents.MouseDown -= value;
    }
    public event MouseEventHandler OnMouseDoubleClick
    {
        add => inputEvents.MouseDoubleClick += value;
        remove => inputEvents.MouseDoubleClick -= value;
    }
    public event MouseEventHandler OnMouseScroll
    {
        add => inputEvents.MouseWheel += value;
        remove => inputEvents.MouseWheel -= value;
    }

    public event KeyEventHandler OnKeyDown;
    public event KeyEventHandler OnKeyUp;

    private bool isHolding = false;
    private void OnKeyDownBase(object sender, KeyEventArgs e)
    {
        if (isHolding)
            return;
        isHolding = true;
        OnKeyDown?.Invoke(sender, e);
    }

    private void OnKeyUpBase(object sender, KeyEventArgs e)
    {
        isHolding = false;
        OnKeyDown?.Invoke(sender, e);
    }

    public void Dispose()
    {
        inputEvents.KeyDown -= OnKeyDown;
        inputEvents.KeyUp -= OnKeyUp;
        inputEvents.Dispose();
    }
}
