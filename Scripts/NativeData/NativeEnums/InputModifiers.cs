namespace MouseAndKeyboard.Native;

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey
[Flags]
public enum InputModifiers : uint
{
    None = 0x0000,

    /// <summary>Either ALT key was held down</summary>
    Alt = 0x0001,

    /// <summary>Either CTRL key was held down</summary>
    Control = 0x0002,

    /// <summary>Either SHIFT key was held down</summary>
    Shift = 0x0004,

    /// <summary>
    /// Either WINDOWS key was held down. These keys are labeled with the Windows logo. 
    /// <br/>Hotkeys that involve the Windows key are reserved for use by the operating system
    /// </summary>
    Windows = 0x0008,

    /// <summary>
    /// Changes the hotkey behavior so that the keyboard auto-repeat does not yield multiple hotkey notifications.
    /// ~Windows Vista:  This flag is not supported.~
    /// </summary>
    NoRepeat = 0x4000,

    ControlShift = Control | Shift,
    ControlAlt = Control | Alt,
    ShiftAlt = Shift | Alt,
    ControlShiftAlt = Control | Shift | Alt,
}


