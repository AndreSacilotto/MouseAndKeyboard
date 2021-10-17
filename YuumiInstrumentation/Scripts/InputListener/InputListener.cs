using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

public class InputListener : IDisposable
{
    private IKeyboardMouseEvents inputEvents;

    private KeyEventHandler OnKeyDown;

    public InputListener(MouseEventHandler OnMouseMove, MouseEventHandler OnMouseDown, MouseEventHandler OnMouseDoubleClick, MouseEventHandler OnMouseScroll, KeyEventHandler OnKeyDown, KeyEventHandler OnKeyUp)
    {
        inputEvents = Hook.GlobalEvents();

        inputEvents.MouseMove += OnMouseMove;
        inputEvents.MouseDown += OnMouseDown;
        inputEvents.MouseDoubleClick += OnMouseDoubleClick;
        inputEvents.MouseWheel += OnMouseScroll;

        this.OnKeyDown = OnKeyDown;
        inputEvents.KeyDown += OnKeyDownBase;
        inputEvents.KeyUp += OnKeyUp;
        inputEvents.KeyUp += OnKeyUpBase;
    }

    private bool isHolding = false;
    private void OnKeyDownBase(object sender, KeyEventArgs e)
    {
        if (isHolding)
            return;
        isHolding = true;
        OnKeyDown?.Invoke(sender, e);
    }

    private void OnKeyUpBase(object sender, KeyEventArgs e) => isHolding = false;

    public void Dispose()
    {
        inputEvents.Dispose();
    }
}
