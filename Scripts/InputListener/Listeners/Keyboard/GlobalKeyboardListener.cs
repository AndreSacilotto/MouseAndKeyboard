using MouseAndKeyboard.Native;
using MouseAndKeyboard.Util;

namespace MouseAndKeyboard.InputListener;

internal class GlobalKeyboardListener : KeyboardListener
{
    public GlobalKeyboardListener() : base(WinHook.HookGlobalKeyboard())
    {
    }

    protected override List<KeyPressEventArgsExt> GetPressEventArgs(ref nint wParam, ref nint lParam)
    {
        var WM = (WindowsMessages)wParam;

        var list = new List<KeyPressEventArgsExt>();

        if (WM != WindowsMessages.KEYDOWN && WM != WindowsMessages.SYSKEYDOWN)
            return list;

        var keyboardHookStruct = WinHook.MarshalHookParam<KeyboardInput>(lParam);

        if (keyboardHookStruct.wVk == VirtualKeyShort.PACKET)
        {
            var ch = (char)keyboardHookStruct.wScan;
            list.Add(new KeyPressEventArgsExt(ch, keyboardHookStruct.time));
        }
        else
        {
            KeyboardStateHelper.TryGetCharFromKeyboardState(keyboardHookStruct.wVk, keyboardHookStruct.wScan, keyboardHookStruct.dwFlags, out var chars);
            if (chars != null)
                foreach (var current in chars)
                    list.Add(new KeyPressEventArgsExt(current, keyboardHookStruct.time));
        }
        return list;
    }

    protected override KeyEventArgsExt GetDownUpEventArgs(ref nint wParam, ref nint lParam)
    {
        var keyboardHookStruct = WinHook.MarshalHookParam<KeyboardInput>(lParam);

        var keyData = KeyUtil.AppendModifierStates((Keys)keyboardHookStruct.wVk);

        var keyCode = (WindowsMessages)wParam;
        var isKeyDown = keyCode == WindowsMessages.KEYDOWN || keyCode == WindowsMessages.SYSKEYDOWN;
        var isKeyUp = keyCode == WindowsMessages.KEYUP || keyCode == WindowsMessages.SYSKEYUP;

        const uint maskExtendedKey = 0x1;
        var isExtendedKey = ((uint)keyboardHookStruct.dwFlags & maskExtendedKey) > 0;

        return new KeyEventArgsExt(keyData, keyboardHookStruct.wScan, keyboardHookStruct.time, isKeyDown, isKeyUp, isExtendedKey);
    }

}
