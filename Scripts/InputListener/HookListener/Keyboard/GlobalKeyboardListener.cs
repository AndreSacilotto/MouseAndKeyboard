﻿using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener.Hook;

internal class GlobalKeyboardListener : KeyboardListener
{
    public GlobalKeyboardListener() : base(MKHookHandle.HookGlobalKeyboard())
    {
    }

    protected override KeyboardEventData GetKeyEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var keyboardHookStruct = MarshalExt.ToStruct<KeyboardHookStruct>(lParam);

        var WM = (WindowsMessages)wParam;

        var isDown = WM == WindowsMessages.KEYDOWN || WM == WindowsMessages.SYSKEYDOWN;
        var isUp = WM == WindowsMessages.KEYUP || WM == WindowsMessages.SYSKEYUP;

        //https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-kbdllhookstruct
        var flags = (byte)keyboardHookStruct.dwFlags;

        var isExtendedKey = (flags & 0b_0000_0001) > 0;
        var alt = ((flags & 0b_0010_0000) >> 5) > 0;

        var ctl = KeyUtil.GetControlState();
        var sht = KeyUtil.GetShiftState();

        return new((VirtualKey)keyboardHookStruct.wVk, (ScanCode)keyboardHookStruct.wScan, isDown, isUp, ctl, sht, alt, isExtendedKey, keyboardHookStruct.time);
    }

    protected override IEnumerable<KeyPressEventData> GetPressEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var WM = (WindowsMessages)wParam;

        if (WM != WindowsMessages.KEYDOWN && WM != WindowsMessages.SYSKEYDOWN)
            yield break;

        var keyboardHookStruct = MarshalExt.ToStruct<KeyboardHookStruct>(lParam);

        var vk = (VirtualKey)keyboardHookStruct.wVk;

        if (vk == VirtualKey.Packet)
        {
            var ch = (char)keyboardHookStruct.wScan;
            yield return new KeyPressEventData(ch, keyboardHookStruct.time);
        }
        else
        {
            var chars = KeyboardStateHelper.TryGetCharFromKeyboardState(vk, (ScanCode)keyboardHookStruct.wScan, keyboardHookStruct.dwFlags);
            foreach (var current in chars)
                yield return new KeyPressEventData(current, keyboardHookStruct.time);
        }
    }

}
