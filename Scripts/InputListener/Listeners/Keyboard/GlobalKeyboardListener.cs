using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class GlobalKeyboardListener : KeyboardListener
{
    public GlobalKeyboardListener() : base(WinHook.HookGlobalKeyboard())
    {
    }

    protected override KeyHookEventArgs GetKeyEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var keyboardHookStruct = WinHook.MarshalHookParam<KeyboardInput>(lParam);

        var keyCode = (WindowsMessages)wParam;
        var isKeyDown = keyCode == WindowsMessages.KEYDOWN || keyCode == WindowsMessages.SYSKEYDOWN;
        var isKeyUp = keyCode == WindowsMessages.KEYUP || keyCode == WindowsMessages.SYSKEYUP;

        var keyData = KeyUtil.AppendModifierStates((Keys)keyboardHookStruct.wVk);

        var isExtendedKey = keyboardHookStruct.dwFlags.HasFlag(KeyEventF.ExtendedKey);

        return new KeyHookEventArgs(keyData, keyboardHookStruct.wScan, keyboardHookStruct.time, isKeyDown, isKeyUp, isExtendedKey);
    }

    protected override IEnumerable<KeyHookPressEventArgs> GetPressEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var WM = (WindowsMessages)wParam;

        if (WM != WindowsMessages.KEYDOWN && WM != WindowsMessages.SYSKEYDOWN)
            yield break;

        var keyboardHookStruct = WinHook.MarshalHookParam<KeyboardInput>(lParam);

        if (keyboardHookStruct.wVk == VirtualKey.PACKET)
        {
            var ch = (char)keyboardHookStruct.wScan;
            yield return new KeyHookPressEventArgs(ch, keyboardHookStruct.time);
        }
        else
        {
            var chars = KeyboardStateHelper.TryGetCharFromKeyboardState(keyboardHookStruct.wVk, keyboardHookStruct.wScan, keyboardHookStruct.dwFlags);
            if (chars != null)
                foreach (var current in chars)
                    yield return new KeyHookPressEventArgs(current, keyboardHookStruct.time);
        }
    }

}
