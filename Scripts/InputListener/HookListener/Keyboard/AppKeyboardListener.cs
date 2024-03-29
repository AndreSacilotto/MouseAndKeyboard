using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener.Hook;

internal class AppKeyboardListener : KeyboardListener
{
    public AppKeyboardListener() : base(MKHookHandle.HookAppKeyboard())
    {
    }

    protected override IEnumerable<KeyPressEventData> GetPressEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var keyEvent = GetKeyEventArgs(wParam, lParam);

        if (!keyEvent.IsKeyDown)
            yield break;

        var chars = KeyboardStateHelper.TryGetCharFromKeyboardState(keyEvent.KeyCode, keyEvent.ScanCode, 0);
        foreach (var ch in chars)
            yield return new KeyPressEventData(ch, Environment.TickCount);
    }

    protected override KeyboardEventData GetKeyEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var vk = (VirtualKey)wParam;

        var bits = checked((uint)lParam);

        // https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644984(v=vs.85)#parameters
        //var repeatCount = (short)   ((bits &  0b_0000_0000__0000_0000__1111_1111__1111_1111) >> 0);
        var scanCode = (ScanCode)((bits & 0b_0000_0000__1111_1111__0000_0000__0000_0000) >> 16);
        var isExtendedKey = ((bits & 0b_0000_0001__0000_0000__0000_0000__0000_0000) >> 24) > 0;

        var contextCode = ((bits & 0b_0010_0000__0000_0000__0000_0000__0000_0000) >> 29) > 0;
        var previousKeyState = ((bits & 0b_0100_0000__0000_0000__0000_0000__0000_0000) >> 30) > 0;
        var transitionData = ((bits & 0b_1000_0000__0000_0000__0000_0000__0000_0000) >> 31) > 0;

        var ctl = KeyUtil.GetControlState();
        var sht = KeyUtil.GetShiftState();

        var isKeyDown = !transitionData;
        var isKeyUp = previousKeyState && transitionData;

        return new KeyboardEventData(vk, scanCode, isKeyDown, isKeyUp, ctl, sht, contextCode, isExtendedKey, Environment.TickCount);
    }


}