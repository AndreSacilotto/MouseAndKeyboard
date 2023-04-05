using MouseAndKeyboard.InputListener;
using static YuumiInstrumentation.InputData;

namespace MouseAndKeyboard.Forms;

partial class MainForm
{
    private void WndProcInit()
    {
        mkListener.KeyListener.KeyUp += HotkeysKeyboardEvent;
        //mkListener.MouseListener.MouseUp += HotkeysMouseEvent;
    }
    private void WndProcClose()
    {
        mkListener.KeyListener.KeyUp -= HotkeysKeyboardEvent;
        //mkListener.MouseListener.MouseUp -= HotkeysMouseEvent;
        mkListener.Dispose();
    }

    //private void HotkeysMouseEvent(MouseEventData data)
    //{
    //}

    private void HotkeysKeyboardEvent(KeyboardEventData data)
    {
        switch ((HotkeysVK)data.KeyCode)
        {
            case HotkeysVK.EmergencyQuit: Close(); break;
            case HotkeysVK.ToggleConsole: chbConsole.Checked = !chbConsole.Checked; break;
            case HotkeysVK.ToggleMouseMove: chbMMove.Checked = !chbMMove.Checked; break;
            case HotkeysVK.ToggleMouseClick: chbMClick.Checked = !chbMClick.Checked; break;
            case HotkeysVK.ToggleKeyboard: chbKKey.Checked = !chbKKey.Checked; break;
            case HotkeysVK.ToggleMouseScroll: chbMScroll.Checked = !chbMScroll.Checked; break;
            case HotkeysVK.Start: BtnStart_Click(this, EventArgs.Empty); break;
            case HotkeysVK.Stop: BtnStop_Click(this, EventArgs.Empty); break;
        }
    }



    /* HotkeysKeyboardEvent dont do what I need
    private int hotkeyId = 1;
    private void RegisterHotKey(Keys key, InputModifiers mods = InputModifiers.None)
    {
        if (!User32.RegisterHotKey(Handle, hotkeyId++, mods, (uint)key))
            throw new InvalidOperationException("Couldn’t register the hot key.");
    }

    private void UnregisterHotKeys()
    {
        for (int i = hotkeyId; i > 0; i--)
            User32.UnregisterHotKey(Handle, i);
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if ((WindowsMessages)m.Msg != WindowsMessages.HOTKEY)
            return;

        var hlKey = new HighLowDWORD((int)m.LParam);

        VirtualKey keys = (VirtualKey)hlKey.High;
        InputModifiers mods = (InputModifiers)hlKey.Low;

        Logger.WriteLine(keys, mods);
    }
    */

}
