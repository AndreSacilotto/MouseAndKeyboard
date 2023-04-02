﻿using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class AppMouseListener : MouseListener
{
    public AppMouseListener() : base(WinHook.HookAppMouse()) { }

    protected override MouseEventData GetEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var mouseHookStruct = WinHook.MarshalHookParam<MouseHookStructEx>(lParam);
        var mouseInput = mouseHookStruct.ToMouseInput();
        return NewEvent((WindowsMessages)wParam, ref mouseInput, swapButtonThreshold);
    }
}
