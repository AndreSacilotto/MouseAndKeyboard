using System;
using System.Runtime.InteropServices;

//Base code: https://stackoverflow.com/questions/20482338/simulate-keyboard-input-in-c-sharp

public static class InputSender
{
    /// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput</summary>
    [DllImport("user32.dll")]
    static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] InputStruct[] pInputs, int cbSize);

    #region Inputs Merge

    public static int SendInput(params InputStruct[] inputs) => (int)SendInput((uint)inputs.Length, inputs, InputStruct.Size);

    //https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
    public enum InputType : uint
    {
        Mouse = 0,
        Keyboard = 1,
        Hardware = 2
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct InputStruct
    {
        public readonly InputType type;
        public InputUnion union;
        public InputStruct(InputType type)
        {
            this.type = type;
            union = new InputUnion();
        }
        public static int Size => Marshal.SizeOf(typeof(InputStruct));
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)] internal MouseInput mi;
        [FieldOffset(0)] internal KeyboardInput ki;
        [FieldOffset(0)] internal HardwareInput hi;
    }

    #endregion

    #region Mouse Input
    //https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        internal int dx;
        internal int dy;
        internal int mouseData; //MouseDataXButton
        internal MouseEventF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }

    [Flags]
    public enum MouseDataXButton : int
    {
        /// <summary>Set if the first X button is pressed or released</summary>
        XButton1 = 0x00000001,
        /// <summary>Set if the second X button is pressed or released</summary>
        XButton2 = 0x00000002
    }

    [Flags]
    public enum MouseEventF : uint
    {
        /// <summary>Movement occurred.</summary>
        Move = 0x0001,
        /// <summary>The left button was pressed.</summary>
        LeftDown = 0x0002,
        /// <summary>The left button was released.</summary>
        LeftUp = 0x0004,
        /// <summary>The right button was pressed.</summary>
        RightDown = 0x0008,
        /// <summary>The right button was released.</summary>
        RightUp = 0x0010,
        /// <summary>The middle button was pressed.</summary>
        MiddleDown = 0x0020,
        /// <summary>The middle button was released.</summary>
        MiddleUp = 0x0040,
        /// <summary>An X button was pressed.</summary>
        XDown = 0x0080,
        /// <summary>An X button was released.</summary>
        XUp = 0x0100,
        /// <summary>The wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseData.</summary>
        Wheel = 0x0800,
        /// <summary>The wheel was moved horizontally, if the mouse has a wheel. The amount of movement is specified in mouseData.</summary>
        HWheel = 0x01000,
        /// <summary>The WM_MOUSEMOVE messages will not be coalesced. The default behavior is to coalesce WM_MOUSEMOVE messages.</summary>
        Move_NoCoalesce = 0x2000,
        /// <summary>Maps coordinates to the entire desktop. Must be used with MOUSEEVENTF_ABSOLUTE.</summary>
        VirtualDesk = 0x4000,
        /// <summary>
        /// The dx and dy members contain normalized absolute coordinates. 
        /// If the flag is not set, dxand dy contain relative data (the change in position since the last reported position).
        /// </summary>
        Absolute = 0x8000,

        LeftClick = MouseEventF.LeftDown | MouseEventF.LeftUp,
        RightClick = MouseEventF.RightDown | MouseEventF.RightUp,
        MiddleClick = MouseEventF.MiddleDown | MouseEventF.MiddleUp,
        XClick = MouseEventF.XDown | MouseEventF.XUp,
    }

    #endregion

    #region Keyboard Input
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        /// <summary>https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes</summary>
        internal ushort wVk;
        /// <summary>https://www.millisecond.com/support/docs/v6/html/language/scancodes.htm</summary>
        internal ushort wScan;
        internal KeyEventF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }

    [Flags]
    public enum KeyEventF : uint
    {
        ExtendedKey = 0x0001,
        KeyUp = 0x0002,
        ScanCode = 0x0008,
        Unicode = 0x0004
    }

    #endregion

    #region Hardware Input

    /// <summary>Define HardwareInput struct</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        internal uint uMsg;
        internal ushort wParamL;
        internal ushort wParamH;
    }

    #endregion

}
