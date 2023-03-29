namespace MouseAndKeyboard.Native;

//https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
public enum InputType : uint
{
    Mouse = 0,
    Keyboard = 1,
    Hardware = 2
}

public enum MapType : uint
{
    /// <summary>VirtualKey to ScanCode</summary>
    VK_TO_VSC = 0x00,
    /// <summary>ScanCode to VirtualKey</summary>
    VSC_TO_VK = 0x01,
    /// <summary>VirtualKey to Undefined Char</summary>
    VK_TO_CHAR = 0x02,
    /// <summary>
    /// Windows NT/2000/XP: uCode is a scan code and is translated into a
    /// virtual-key code that distinguishes between left- and right-hand keys. If
    /// there is no translation, the function returns 0.
    /// </summary>
    MAPVK_VSC_TO_VK_EX = 0x03,
    /// <summary>Not currently documented</summary>
    MAPVK_VK_TO_VSC_EX = 0x04
}

#region Mouse Input

[Flags]
public enum MouseDataXButton : int
{
    None = 0x0,
    /// <summary>Set if the first X button is pressed or released</summary>
    XButton1 = 0x00000001,
    /// <summary>Set if the second X button is pressed or released</summary>
    XButton2 = 0x00000002
}

[Flags]
public enum MouseEventF : uint
{
    None = 0x000,
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
    /// The pt and dy members contain normalized absolute coordinates. 
    /// If the flag is not set, dxand dy contain relative data (the change in position since the last reported position).
    /// </summary>
    Absolute = 0x8000,

    LeftClick = LeftDown | LeftUp,
    RightClick = RightDown | RightUp,
    MiddleClick = MiddleDown | MiddleUp,
    XClick = XDown | XUp,
}

#endregion

#region Keyboard Input

[Flags]
public enum WHHotkey
{
    None = 0x0000,
    Alt = 0x0001,
    Control = 0x0002,
    Shift = 0x0004,
    Windows = 0x0008,
    NoRepeat = 0x4000,
}

[Flags]
public enum KeyEventF : uint
{
    /// <summary>If used, the scan code was preceded by a prefix byte that has the value 0xE0 (224)</summary>
    ExtendedKey = 0x0001,
    /// <summary>If used, the key is being released. If not specified, the key is being pressed</summary>
    KeyUp = 0x0002,
    /// <summary>If used, wScan identifies the key and wVk is ignored</summary>
    ScanCode = 0x0008,
    /// <summary>If used, the system synthesizes a VK_PACKET keystroke. The wVk parameter must be zero</summary>
    Unicode = 0x0004
}

#endregion
