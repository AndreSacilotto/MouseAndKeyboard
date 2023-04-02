namespace MouseAndKeyboard.Native;

//https://www.pinvoke.net/default.aspx/Structures/KEYBDINPUT.html
//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
public enum VirtualKey : WORD
{
    None = 0x00,
    ///<summary>Left Mouse button</summary>
    LeftButton = 0x01,
    ///<summary>Right Mouse button</summary>
    RightButton = 0x02,
    ///<summary>Control-Break Processing</summary>
    Cancel = 0x03,
    ///<summary>Middle Mouse button (Three-button Mouse). Not Contiguous</summary>
    MiddleButton = 0x04,
    ///<summary>X1 Mouse button. Not Contiguous</summary>
    XButton1 = 0x05,
    ///<summary>X2 Mouse button. Not Contiguous</summary>
    XButton2 = 0x06,
    ///<summary>Backspace Key</summary>
    Back = 0x08,
    ///<summary>Tab Key</summary>
    Tab = 0x09,
    ///<summary>Clear Key</summary>
    Clear = 0x0C,
    ///<summary>Enter Key</summary>
    Return = 0x0D,
    ///<summary>Shift Key</summary>
    Shift = 0x10,
    ///<summary>Ctrl Key</summary>
    Control = 0x11,
    ///<summary>Menu/Alt Key</summary>
    Menu = 0x12,
    Alt = Menu,
    ///<summary>Pause Key</summary>
    Pause = 0x13,
    ///<summary>Caps Lock Key</summary>
    CapitalLock = 0x14,
    ///<summary>Input Method Editor (IME) Kana Mode</summary>
    Kana = 0x15,
    ///<summary>IME Hangul Mode</summary>
    Hangul = Kana,
    ///<summary>IME Junja Mode</summary>
    Junja = 0x17,
    ///<summary>IME Final Mode</summary>
    Final = 0x18,
    ///<summary>IME Hanja Mode</summary>
    Hanja = 0x19,
    ///<summary>IME Kanji Mode</summary>
    Kanji = Hanja,
    ///<summary>Esc Key</summary>
    Escape = 0x1B,
    ///<summary>IME Convert</summary>
    Convert = 0x1C,
    ///<summary>IME Nonconvert</summary>
    NonConvert = 0x1D,
    ///<summary>IME Accept</summary>
    Accept = 0x1E,
    ///<summary>IME Mode Change Request</summary>
    ModeChange = 0x1F,
    ///<summary>Spacebar</summary>
    Space = 0x20,
    ///<summary>Page Up Key</summary>
    Prior = 0x21,
    ///<summary>Page Down Key</summary>
    Next = 0x22,
    ///<summary>End Key</summary>
    End = 0x23,
    ///<summary>Home Key</summary>
    Home = 0x24,
    ///<summary>Left Arrow Key</summary>
    LeftArrow = 0x25,
    ///<summary>Up Arrow Key</summary>
    UpArrow = 0x26,
    ///<summary>Right Arrow Key</summary>
    RightArrow = 0x27,
    ///<summary>Down Arrow Key</summary>
    DownArrow = 0x28,
    ///<summary>Select Key</summary>
    Select = 0x29,
    ///<summary>Print Key</summary>
    Print = 0x2A,
    ///<summary>Execute Key</summary>
    Execute = 0x2B,
    ///<summary>Print Screen Key</summary>
    Snapshot = 0x2C,
    ///<summary>Ins Key</summary>
    Insert = 0x2D,
    ///<summary>Del Key</summary>
    Delete = 0x2E,
    ///<summary>Help Key</summary>
    Help = 0x2F,
    ///<summary>Numeric 0 Key</summary>
    D0 = 0x30,
    ///<summary>Numeric 1 Key</summary>
    D1 = 0x31,
    ///<summary>Numeric 2 Key</summary>
    D2 = 0x32,
    ///<summary>Numeric 3 Key</summary>
    D3 = 0x33,
    ///<summary>Numeric 4 Key</summary>
    D4 = 0x34,
    ///<summary>Numeric 5 Key</summary>
    D5 = 0x35,
    ///<summary>Numeric 6 Key</summary>
    D6 = 0x36,
    ///<summary>Numeric 7 Key</summary>
    D7 = 0x37,
    ///<summary>Numeric 8 Key</summary>
    D8 = 0x38,
    ///<summary>Numeric 9 Key</summary>
    D9 = 0x39,
    ///<summary>A Key</summary>
    A = 0x41,
    ///<summary>B Key</summary>
    B = 0x42,
    ///<summary>C Key</summary>
    C = 0x43,
    ///<summary>D Key</summary>
    D = 0x44,
    ///<summary>E Key</summary>
    E = 0x45,
    ///<summary>F Key</summary>
    F = 0x46,
    ///<summary>G Key</summary>
    G = 0x47,
    ///<summary>H Key</summary>
    H = 0x48,
    ///<summary>I Key</summary>
    I = 0x49,
    ///<summary>J Key</summary>
    J = 0x4A,
    ///<summary>K Key</summary>
    K = 0x4B,
    ///<summary>L Key</summary>
    L = 0x4C,
    ///<summary>M Key</summary>
    M = 0x4D,
    ///<summary>N Key</summary>
    N = 0x4E,
    ///<summary>O Key</summary>
    O = 0x4F,
    ///<summary>P Key</summary>
    P = 0x50,
    ///<summary>Q Key</summary>
    Q = 0x51,
    ///<summary>R Key</summary>
    R = 0x52,
    ///<summary>S Key</summary>
    S = 0x53,
    ///<summary>T Key</summary>
    T = 0x54,
    ///<summary>U Key</summary>
    U = 0x55,
    ///<summary>V Key</summary>
    V = 0x56,
    ///<summary>W Key</summary>
    W = 0x57,
    ///<summary>X Key</summary>
    X = 0x58,
    ///<summary>Y Key</summary>
    Y = 0x59,
    ///<summary>Z Key</summary>
    Z = 0x5A,
    ///<summary>Left Windows Key (Microsoft Natural Keyboard)</summary>
    LeftWindows = 0x5B,
    ///<summary>Right Windows Key (Natural Keyboard)</summary>
    RightWindows = 0x5C,
    ///<summary>Applications Key (Natural Keyboard)</summary>
    Apps = 0x5D,
    ///<summary>Computer Sleep Key</summary>
    Sleep = 0x5F,
    ///<summary>Numeric Keypad 0 Key</summary>
    Numpad0 = 0x60,
    ///<summary>Numeric Keypad 1 Key</summary>
    Numpad1 = 0x61,
    ///<summary>Numeric Keypad 2 Key</summary>
    Numpad2 = 0x62,
    ///<summary>Numeric Keypad 3 Key</summary>
    Numpad3 = 0x63,
    ///<summary>Numeric Keypad 4 Key</summary>
    Numpad4 = 0x64,
    ///<summary>Numeric Keypad 5 Key</summary>
    Numpad5 = 0x65,
    ///<summary>Numeric Keypad 6 Key</summary>
    Numpad6 = 0x66,
    ///<summary>Numeric Keypad 7 Key</summary>
    Numpad7 = 0x67,
    ///<summary>Numeric Keypad 8 Key</summary>
    Numpad8 = 0x68,
    ///<summary>Numeric Keypad 9 Key</summary>
    Numpad9 = 0x69,
    ///<summary>Numpad Multiply Key</summary>
    Multiply = 0x6A,
    ///<summary>Numpad Add Key</summary>
    Add = 0x6B,
    ///<summary>Numpad Separator Key</summary>
    Separator = 0x6C,
    ///<summary>Numpad Subtract Key</summary>
    Subtract = 0x6D,
    ///<summary>Numpad Decimal Key</summary>
    Decimal = 0x6E,
    ///<summary>Numpad Divide Key</summary>
    Divide = 0x6F,
    ///<summary>F1 Key</summary>
    F1 = 0x70,
    ///<summary>F2 Key</summary>
    F2 = 0x71,
    ///<summary>F3 Key</summary>
    F3 = 0x72,
    ///<summary>F4 Key</summary>
    F4 = 0x73,
    ///<summary>F5 Key</summary>
    F5 = 0x74,
    ///<summary>F6 Key</summary>
    F6 = 0x75,
    ///<summary>F7 Key</summary>
    F7 = 0x76,
    ///<summary>F8 Key</summary>
    F8 = 0x77,
    ///<summary>F9 Key</summary>
    F9 = 0x78,
    ///<summary>F10 Key</summary>
    F10 = 0x79,
    ///<summary>F11 Key</summary>
    F11 = 0x7A,
    ///<summary>F12 Key</summary>
    F12 = 0x7B,
    ///<summary>F13 Key</summary>
    F13 = 0x7C,
    ///<summary>F14 Key</summary>
    F14 = 0x7D,
    ///<summary>F15 Key</summary>
    F15 = 0x7E,
    ///<summary>F16 Key</summary>
    F16 = 0x7F,
    ///<summary>F17 Key</summary>
    F17 = 0x80,
    ///<summary>F18 Key</summary>
    F18 = 0x81,
    ///<summary>F19 Key</summary>
    F19 = 0x82,
    ///<summary>F20 Key</summary>
    F20 = 0x83,
    ///<summary>F21 Key</summary>
    F21 = 0x84,
    ///<summary>F22 Key, (Ppc Only) Key Used To Lock Device.</summary>
    F22 = 0x85,
    ///<summary>F23 Key</summary>
    F23 = 0x86,
    ///<summary>F24 Key</summary>
    F24 = 0x87,
    ///<summary>Num Lock Key</summary>
    NumLock = 0x90,
    ///<summary>ScrollLock Lock Key</summary>
    ScrollLock = 0x91,
    ///<summary>Left Shift Key</summary>
    LeftShift = 0xA0,
    ///<summary>Right Shift Key</summary>
    RightShift = 0xA1,
    ///<summary>Left Control Key</summary>
    LeftControl = 0xA2,
    ///<summary>Right Control Key</summary>
    RightControl = 0xA3,
    ///<summary>Left Menu/Alt Key</summary>
    LeftMenu = 0xA4,
    ///<summary>Right Menu/Alt Key</summary>
    RightMenu = 0xA5,
    ///<summary>Browser Back Key</summary>
    BrowserBack = 0xA6,
    ///<summary>Browser Forward Key</summary>
    BrowserForward = 0xA7,
    ///<summary>Browser Refresh Key</summary>
    BrowserRefresh = 0xA8,
    ///<summary>Browser Stop Key</summary>
    BrowserStop = 0xA9,
    ///<summary>Browser Search Key</summary>
    BrowserSearch = 0xAA,
    ///<summary>Browser Favorites Key</summary>
    BrowserFavorites = 0xAB,
    ///<summary>Browser Startclient And Home Key</summary>
    BrowserHome = 0xAC,
    ///<summary>Volume Mute Key</summary>
    VolumeMute = 0xAD,
    ///<summary>Volume Down Key</summary>
    VolumeDown = 0xAE,
    ///<summary>Volume Up Key</summary>
    VolumeUp = 0xAF,
    ///<summary>Next Track Key</summary>
    MediaNextTrack = 0xB0,
    ///<summary>Previous Track Key</summary>
    MediaPrevTrack = 0xB1,
    ///<summary>Stop Media Key</summary>
    MediaStop = 0xB2,
    ///<summary>Play/Pause Media Key</summary>
    MediaPlayPause = 0xB3,
    ///<summary>Startclient Mail Key</summary>
    LaunchMail = 0xB4,
    ///<summary>Select Media Key</summary>
    LaunchMediaSelect = 0xB5,
    ///<summary>Startclient Application 1 Key</summary>
    LaunchApp1 = 0xB6,
    ///<summary>Startclient Application 2 Key</summary>
    LaunchApp2 = 0xB7,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem1 = 0xBA,
    ///<summary>For any country/region, the '+' Key</summary>
    OemPlus = 0xBB,
    ///<summary>For any country/region, the ',' Key</summary>
    OemComma = 0xBC,
    ///<summary>For any country/region, the '-' Key</summary>
    OemMinus = 0xBD,
    ///<summary>For any country/region, the '.' Key</summary>
    OemPeriod = 0xBE,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem2 = 0xBF,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem3 = 0xC0,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem4 = 0xDB,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem5 = 0xDC,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem6 = 0xDD,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem7 = 0xDE,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem8 = 0xDF,
    ///<summary>Either the angle bracket key or the backslash key on the rt 102-key keyboard</summary>
    Oem102 = 0xE2,
    ///<summary>Windows 95/98/Me, Windows Nt 4.0, IME Process Key</summary>
    ProcessKey = 0xE5,
    IcoClear = 0xE6,
    ///<summary>
    /// Used to pass unicode characters as if they were keystrokes.
    /// <br/>The VK_PACKET key is the Low word of a 32-bit virtual key Value used for non-keyboard input methods.
    /// <br/>For more information, see remark in KEYBDINPUT, SendInput, WM_KEYDOWN, And WM_KEYUP
    ///</summary>
    Packet = 0xE7,
    OemReset = 0xE9,
    OemJump = 0xEA,
    OemPA1 = 0xEB,
    OemPA2 = 0xEC,
    OemPA3 = 0xED,
    OemWsCtrl = 0xEE,
    OemCuSel = 0xEF,
    OemAttn = 0xF0,
    OemFinish = 0xF1,
    OemCopy = 0xF2,
    OemAuto = 0xF3,
    OemEnlw = 0xF4,
    OemBacktab = 0xF5,
    ///<summary>ATTN Key</summary>
    Attn = 0xF6,
    ///<summary>Crsel Key</summary>
    CrSel = 0xF7,
    ///<summary>Exsel Key</summary>
    ExSel = 0xF8,
    ///<summary>Erase EoF Key</summary>
    EraseEoF = 0xF9,
    ///<summary>Play Key</summary>
    Play = 0xFA,
    ///<summary>Zoom Key</summary>
    Zoom = 0xFB,
    ///<summary>Reserved</summary>
    NoName = 0xFC,
    ///<summary>PA1 Key</summary>
    PA1 = 0xFD,
    ///<summary>Clear Key</summary>
    OemClear = 0xFE,
}
