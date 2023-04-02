using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class GlobalKeyboardListener : KeyboardListener
{
    public GlobalKeyboardListener() : base(WinHook.HookGlobalKeyboard())
    {
    }

    protected override KeyEventData GetKeyEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var keyboardHookStruct = WinHook.MarshalHookParam<KeyboardInput>(lParam);

        var WM = (WindowsMessages)wParam;

        var down = WM == WindowsMessages.KEYDOWN || WM == WindowsMessages.SYSKEYDOWN;
        var up = WM == WindowsMessages.KEYUP || WM == WindowsMessages.SYSKEYUP;

        KeyUtil.CheckeModifiersState(out var ctl, out var sht, out var alt);

        var isExtendedKey = keyboardHookStruct.dwFlags.HasFlag(KeyEventF.ExtendedKey);

        return new KeyEventData(keyboardHookStruct.wVk, (ScanCode)keyboardHookStruct.wScan, down, up, ctl, sht, alt, isExtendedKey, keyboardHookStruct.time);
    }

    protected override IEnumerable<KeyPressEventData> GetPressEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var WM = (WindowsMessages)wParam;

        if (WM != WindowsMessages.KEYDOWN && WM != WindowsMessages.SYSKEYDOWN)
            yield break;

        var keyboardHookStruct = WinHook.MarshalHookParam<KeyboardInput>(lParam);

        if ((VirtualKey)keyboardHookStruct.wVk == VirtualKey.Packet)
        {
            var ch = (char)keyboardHookStruct.wScan;
            yield return new KeyPressEventData(ch, keyboardHookStruct.time);
        }
        else
        {
            var chars = KeyboardStateHelper.TryGetCharFromKeyboardState((VirtualKey)keyboardHookStruct.wVk, (ScanCode)keyboardHookStruct.wScan, keyboardHookStruct.dwFlags);
            if (chars != null)
                foreach (var current in chars)
                    yield return new KeyPressEventData(current, keyboardHookStruct.time);
        }
    }

}
