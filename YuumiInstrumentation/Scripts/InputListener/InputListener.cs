using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

public class InputListener : IDisposable
{
    private IKeyboardMouseEvents inputEvents;

    public bool mmove, mdown, mup, mdouble, mwheel, kdown, kup;

    public InputListener(bool enable = true)
    {
        inputEvents = Hook.GlobalEvents();
        ChangeState(enable);
        Subscribe();
    }

    public void ChangeState(bool enable) => mmove = mdown = mup = mdouble = mwheel = kdown = kup = enable;

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
        if (!mmove)
            return;

        InputListenerUtil.Print(e);
    }
    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        if (!mdown)
            return;
        InputListenerUtil.Print(e);
    }
    private void OnMouseDobleClick(object sender, MouseEventArgs e)
    {
        if (!mdouble)
            return;
        InputListenerUtil.Print(e);
    }
    private void OnMouseWheel(object sender, MouseEventArgs e)
    {
        if (!mwheel)
            return;
        InputListenerUtil.Print(e);
    }
    #endregion


    #region Keyboard
    private bool isHolding = false;

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (!kdown)
            return;
        if (isHolding)
            return;
        isHolding = true;

        InputListenerUtil.Print(e);
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (!kup)
            return;
        isHolding = false;

        //LoggerUtil.Print(e);
    }


    #endregion

    public void Dispose() => Unsubscribe();
}
