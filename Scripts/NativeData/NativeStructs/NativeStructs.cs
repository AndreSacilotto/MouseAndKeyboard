﻿using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
[StructLayout(LayoutKind.Sequential)]
public readonly struct InputStruct
{
    public enum InputType : Int32
    {
        Mouse = 0,
        Keyboard = 1,
        Hardware = 2
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct InputUnion
    {
        [FieldOffset(0)] public readonly MouseInput mi;
        [FieldOffset(0)] public readonly KeyboardInput ki;
        [FieldOffset(0)] public readonly HardwareInput hi;
        internal InputUnion(MouseInput mi) => this.mi = mi;
        internal InputUnion(KeyboardInput ki) => this.ki = ki;
        internal InputUnion(HardwareInput hi) => this.hi = hi;
    }

    public readonly InputType type;
    public readonly InputUnion union;
    private InputStruct(InputType type, InputUnion union = default)
    {
        this.type = type;
        this.union = union;
    }
    public static int Size => Marshal.SizeOf(typeof(InputStruct));

    public static InputStruct NewInput(MouseInput input) => new(InputType.Mouse, new(input));
    public static InputStruct NewInput(KeyboardInput input) => new(InputType.Keyboard, new(input));
    public static InputStruct NewInput(HardwareInput input) => new(InputType.Hardware, new(input));
}

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-hardwareinput
[StructLayout(LayoutKind.Sequential)]
public readonly struct HardwareInput
{
    public readonly Int32 uMsg;
    public readonly Int16 wParamL;
    public readonly Int16 wParamH;
    public HardwareInput(int uMsg = default, short wParamL = default, short wParamH = default)
    {
        this.uMsg = uMsg;
        this.wParamL = wParamL;
        this.wParamH = wParamH;
    }
}

// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardInput
{
    public readonly VirtualKey wVk;
    public readonly ScanCode wScan;
    public readonly KeyEventF dwFlags;
    public readonly Int32 time;
    public readonly UIntPtr dwExtraInfo;
    public KeyboardInput(VirtualKey wVk, ScanCode wScan, KeyEventF dwFlags, int time = default, UIntPtr dwExtraInfo = default)
    {
        this.wVk = wVk;
        this.wScan = wScan;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
}

// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msllhookstruct
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseInput
{
    public readonly int X;
    public readonly int Y;
    //or public readonly Point pt;
    public readonly Int32 mouseData; //MouseDataXButton or ScrollAmount
    public readonly MouseEventF dwFlags;
    public readonly Int32 time;
    public readonly UIntPtr dwExtraInfo;
    public MouseInput(Point pt, MouseEventF dwFlags, int mouseData = default, int time = default, UIntPtr dwExtraInfo = default)
    {
        X = pt.X;
        Y = pt.Y;
        this.mouseData = mouseData;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
    public MouseInput(int x, int y, MouseEventF dwFlags, int mouseData = default, int time = default, nuint dwExtraInfo = default)
    {
        X = x;
        Y = y;
        this.mouseData = mouseData;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }

    public Point GetPoint() => new(X, Y);
    public MouseDataXButton AsXButton() => (MouseDataXButton)mouseData;

    private int GetHighWORD() => mouseData >> (sizeof(int) * 4);
    internal int GetWheelDelta() => GetHighWORD();
}
