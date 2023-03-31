namespace MouseAndKeyboard.Native;

/*
* DWORD => 32b = int or uint | (I will use Int32 in this cases or UInt32 if the code needed it)
* WORD => 16b = short or ushort | (I will use Int16 in this cases or UInt32 if the code needed it)
*/

#region Mouse Input

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput
[Flags]
public enum MouseDataXButton : Int32
{
    None = 0,
    /// <summary>Set if the first X button is pressed or released</summary>
    XButton1 = 1,
    /// <summary>Set if the second X button is pressed or released</summary>
    XButton2 = 2
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput
[Flags]
public enum MouseEventF : Int32
{
    None = 0x0000,
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

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey
[Flags]
public enum FsModifiers : uint
{
    None = 0,
    Alt = 1,
    Control = 2,
    Shift = 4,
    Windows = 8,
    NoRepeat = 1 << 14,
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
[Flags]
public enum KeyEventF : Int32
{
    /// <summary>If used, the scan code was preceded by a prefix byte that has the value 0xE0 (224)</summary>
    ExtendedKey = 1,
    /// <summary>If used, the key is being released. If not specified, the key is being pressed</summary>
    KeyUp = 2,
    /// <summary>
    /// If specified, the system synthesizes a VK_PACKET keystroke. 
    /// <br/>The wVk parameter must be zero. 
    /// <br/>This flag can only be combined with the KEYEVENTF_KEYUP flag. 
    /// </summary>
    Unicode = 4,
    /// <summary>If used, wScan identifies the key and wVk is ignored</summary>
    ScanCode = 8,
}


//https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input#keystroke-message-flags
// https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
[Flags]
public enum KeyFlags : int
{
    /// <summary>Manipulates the extended key flag</summary>
    KF_EXTENDED = 0x0100,
    /// <summary>Manipulates the dialog mode flag, which indicates whether a dialog box is active</summary>
    KF_DLGMODE = 0x0800,
    /// <summary>Manipulates the menu mode flag, which indicates whether a menu is active</summary>
    KF_MENUMODE = 0x1000,
    /// <summary>Manipulates the ALT key flag, which indicated if the ALT key is pressed</summary>
    KF_ALTDOWN = 0x2000,
    /// <summary>Manipulates the repeat count</summary>
    KF_REPEAT = 0x4000,
    /// <summary>Manipulates the transition state flag</summary>
    KF_UP = 0x8000
}


#endregion
