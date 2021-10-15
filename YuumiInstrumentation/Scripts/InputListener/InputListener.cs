using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

public class InputListener : IDisposable
{
    private IKeyboardMouseEvents inputEvents;

    public InputListener(bool start)
    {
        inputEvents = Hook.GlobalEvents();

        if (start)
            Subscribe();
    }

    public void Subscribe()
    {
        inputEvents.MouseMove += OnMouseMove;
        inputEvents.MouseDown += OnMouseDown;
        inputEvents.MouseDoubleClick += OnMouseDobleClick;
        inputEvents.MouseWheel += OnMouseWheel;


        inputEvents.KeyDown += OnKeyDown;
        inputEvents.KeyUp += OnKeyUp;
    }

    private void Unsubscribe()
    {
        inputEvents.MouseMove -= OnMouseMove;
        inputEvents.MouseDown -= OnMouseDown;
        inputEvents.MouseDoubleClick -= OnMouseDobleClick;
        inputEvents.MouseWheel -= OnMouseWheel;

        inputEvents.KeyDown -= OnKeyDown;
        inputEvents.KeyUp -= OnKeyUp;

    }


    #region Mouse
    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        InputListenerUtil.Print(e);
    }
    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        InputListenerUtil.Print(e);
    }
    private void OnMouseDobleClick(object sender, MouseEventArgs e)
    {
        InputListenerUtil.Print(e);
    }
    private void OnMouseWheel(object sender, MouseEventArgs e)
    {
        InputListenerUtil.Print(e);
    }
    #endregion


    #region Keyboard
    private bool isHolding = false;

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (isHolding)
            return;
        isHolding = true;

        InputListenerUtil.Print(e);
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        isHolding = false;

        //LoggerUtil.Print(e);
    }


    #endregion

    public void Dispose() => Unsubscribe();
}
