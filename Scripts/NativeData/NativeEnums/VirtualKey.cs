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
    ///<summary>Backspace KeyPress</summary>
    Back = 0x08,
    ///<summary>Tab KeyPress</summary>
    Tab = 0x09,
    ///<summary>Clear KeyPress</summary>
    Clear = 0x0C,
    ///<summary>Enter KeyPress</summary>
    Return = 0x0D,
    ///<summary>Shift KeyPress</summary>
    Shift = 0x10,
    ///<summary>Ctrl KeyPress</summary>
    Control = 0x11,
    ///<summary>Menu/Alt KeyPress</summary>
    Menu = 0x12,
    Alt = Menu,
    ///<summary>Pause KeyPress</summary>
    Pause = 0x13,
    ///<summary>Caps Lock KeyPress</summary>
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
    ///<summary>Esc KeyPress</summary>
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
    ///<summary>Page Up KeyPress</summary>
    Prior = 0x21,
    ///<summary>Page Down KeyPress</summary>
    Next = 0x22,
    ///<summary>End KeyPress</summary>
    End = 0x23,
    ///<summary>Home KeyPress</summary>
    Home = 0x24,
    ///<summary>Left Arrow KeyPress</summary>
    LeftArrow = 0x25,
    ///<summary>Up Arrow KeyPress</summary>
    UpArrow = 0x26,
    ///<summary>Right Arrow KeyPress</summary>
    RightArrow = 0x27,
    ///<summary>Down Arrow KeyPress</summary>
    DownArrow = 0x28,
    ///<summary>Select KeyPress</summary>
    Select = 0x29,
    ///<summary>Print KeyPress</summary>
    Print = 0x2A,
    ///<summary>Execute KeyPress</summary>
    Execute = 0x2B,
    ///<summary>Print Screen KeyPress</summary>
    Snapshot = 0x2C,
    ///<summary>Ins KeyPress</summary>
    Insert = 0x2D,
    ///<summary>Del KeyPress</summary>
    Delete = 0x2E,
    ///<summary>Help KeyPress</summary>
    Help = 0x2F,
    ///<summary>Numeric 0 KeyPress</summary>
    D0 = 0x30,
    ///<summary>Numeric 1 KeyPress</summary>
    D1 = 0x31,
    ///<summary>Numeric 2 KeyPress</summary>
    D2 = 0x32,
    ///<summary>Numeric 3 KeyPress</summary>
    D3 = 0x33,
    ///<summary>Numeric 4 KeyPress</summary>
    D4 = 0x34,
    ///<summary>Numeric 5 KeyPress</summary>
    D5 = 0x35,
    ///<summary>Numeric 6 KeyPress</summary>
    D6 = 0x36,
    ///<summary>Numeric 7 KeyPress</summary>
    D7 = 0x37,
    ///<summary>Numeric 8 KeyPress</summary>
    D8 = 0x38,
    ///<summary>Numeric 9 KeyPress</summary>
    D9 = 0x39,
    ///<summary>A KeyPress</summary>
    A = 0x41,
    ///<summary>B KeyPress</summary>
    B = 0x42,
    ///<summary>C KeyPress</summary>
    C = 0x43,
    ///<summary>D KeyPress</summary>
    D = 0x44,
    ///<summary>E KeyPress</summary>
    E = 0x45,
    ///<summary>F KeyPress</summary>
    F = 0x46,
    ///<summary>G KeyPress</summary>
    G = 0x47,
    ///<summary>H KeyPress</summary>
    H = 0x48,
    ///<summary>I KeyPress</summary>
    I = 0x49,
    ///<summary>J KeyPress</summary>
    J = 0x4A,
    ///<summary>K KeyPress</summary>
    K = 0x4B,
    ///<summary>L KeyPress</summary>
    L = 0x4C,
    ///<summary>M KeyPress</summary>
    M = 0x4D,
    ///<summary>N KeyPress</summary>
    N = 0x4E,
    ///<summary>O KeyPress</summary>
    O = 0x4F,
    ///<summary>P KeyPress</summary>
    P = 0x50,
    ///<summary>Q KeyPress</summary>
    Q = 0x51,
    ///<summary>R KeyPress</summary>
    R = 0x52,
    ///<summary>S KeyPress</summary>
    S = 0x53,
    ///<summary>T KeyPress</summary>
    T = 0x54,
    ///<summary>U KeyPress</summary>
    U = 0x55,
    ///<summary>V KeyPress</summary>
    V = 0x56,
    ///<summary>W KeyPress</summary>
    W = 0x57,
    ///<summary>X KeyPress</summary>
    X = 0x58,
    ///<summary>Y KeyPress</summary>
    Y = 0x59,
    ///<summary>Z KeyPress</summary>
    Z = 0x5A,
    ///<summary>Left Windows KeyPress (Microsoft Natural Keyboard)</summary>
    LeftWindows = 0x5B,
    ///<summary>Right Windows KeyPress (Natural Keyboard)</summary>
    RightWindows = 0x5C,
    ///<summary>Applications KeyPress (Natural Keyboard)</summary>
    Apps = 0x5D,
    ///<summary>Computer Sleep KeyPress</summary>
    Sleep = 0x5F,
    ///<summary>Numeric Keypad 0 KeyPress</summary>
    Numpad0 = 0x60,
    ///<summary>Numeric Keypad 1 KeyPress</summary>
    Numpad1 = 0x61,
    ///<summary>Numeric Keypad 2 KeyPress</summary>
    Numpad2 = 0x62,
    ///<summary>Numeric Keypad 3 KeyPress</summary>
    Numpad3 = 0x63,
    ///<summary>Numeric Keypad 4 KeyPress</summary>
    Numpad4 = 0x64,
    ///<summary>Numeric Keypad 5 KeyPress</summary>
    Numpad5 = 0x65,
    ///<summary>Numeric Keypad 6 KeyPress</summary>
    Numpad6 = 0x66,
    ///<summary>Numeric Keypad 7 KeyPress</summary>
    Numpad7 = 0x67,
    ///<summary>Numeric Keypad 8 KeyPress</summary>
    Numpad8 = 0x68,
    ///<summary>Numeric Keypad 9 KeyPress</summary>
    Numpad9 = 0x69,
    ///<summary>Numpad Multiply KeyPress</summary>
    Multiply = 0x6A,
    ///<summary>Numpad Add KeyPress</summary>
    Add = 0x6B,
    ///<summary>Numpad Separator KeyPress</summary>
    Separator = 0x6C,
    ///<summary>Numpad Subtract KeyPress</summary>
    Subtract = 0x6D,
    ///<summary>Numpad Decimal KeyPress</summary>
    Decimal = 0x6E,
    ///<summary>Numpad Divide KeyPress</summary>
    Divide = 0x6F,
    ///<summary>F1 KeyPress</summary>
    F1 = 0x70,
    ///<summary>F2 KeyPress</summary>
    F2 = 0x71,
    ///<summary>F3 KeyPress</summary>
    F3 = 0x72,
    ///<summary>F4 KeyPress</summary>
    F4 = 0x73,
    ///<summary>F5 KeyPress</summary>
    F5 = 0x74,
    ///<summary>F6 KeyPress</summary>
    F6 = 0x75,
    ///<summary>F7 KeyPress</summary>
    F7 = 0x76,
    ///<summary>F8 KeyPress</summary>
    F8 = 0x77,
    ///<summary>F9 KeyPress</summary>
    F9 = 0x78,
    ///<summary>F10 KeyPress</summary>
    F10 = 0x79,
    ///<summary>F11 KeyPress</summary>
    F11 = 0x7A,
    ///<summary>F12 KeyPress</summary>
    F12 = 0x7B,
    ///<summary>F13 KeyPress</summary>
    F13 = 0x7C,
    ///<summary>F14 KeyPress</summary>
    F14 = 0x7D,
    ///<summary>F15 KeyPress</summary>
    F15 = 0x7E,
    ///<summary>F16 KeyPress</summary>
    F16 = 0x7F,
    ///<summary>F17 KeyPress</summary>
    F17 = 0x80,
    ///<summary>F18 KeyPress</summary>
    F18 = 0x81,
    ///<summary>F19 KeyPress</summary>
    F19 = 0x82,
    ///<summary>F20 KeyPress</summary>
    F20 = 0x83,
    ///<summary>F21 KeyPress</summary>
    F21 = 0x84,
    ///<summary>F22 KeyPress, (Ppc Only) KeyPress Used To Lock Device.</summary>
    F22 = 0x85,
    ///<summary>F23 KeyPress</summary>
    F23 = 0x86,
    ///<summary>F24 KeyPress</summary>
    F24 = 0x87,
    ///<summary>Num Lock KeyPress</summary>
    NumLock = 0x90,
    ///<summary>ScrollLock Lock KeyPress</summary>
    ScrollLock = 0x91,
    ///<summary>Left Shift KeyPress</summary>
    LeftShift = 0xA0,
    ///<summary>Right Shift KeyPress</summary>
    RightShift = 0xA1,
    ///<summary>Left Control KeyPress</summary>
    LeftControl = 0xA2,
    ///<summary>Right Control KeyPress</summary>
    RightControl = 0xA3,
    ///<summary>Left Menu/Alt KeyPress</summary>
    LeftMenu = 0xA4,
    ///<summary>Right Menu/Alt KeyPress</summary>
    RightMenu = 0xA5,
    ///<summary>Browser Back KeyPress</summary>
    BrowserBack = 0xA6,
    ///<summary>Browser Forward KeyPress</summary>
    BrowserForward = 0xA7,
    ///<summary>Browser Refresh KeyPress</summary>
    BrowserRefresh = 0xA8,
    ///<summary>Browser Stop KeyPress</summary>
    BrowserStop = 0xA9,
    ///<summary>Browser Search KeyPress</summary>
    BrowserSearch = 0xAA,
    ///<summary>Browser Favorites KeyPress</summary>
    BrowserFavorites = 0xAB,
    ///<summary>Browser Startclient And Home KeyPress</summary>
    BrowserHome = 0xAC,
    ///<summary>Volume Mute KeyPress</summary>
    VolumeMute = 0xAD,
    ///<summary>Volume Down KeyPress</summary>
    VolumeDown = 0xAE,
    ///<summary>Volume Up KeyPress</summary>
    VolumeUp = 0xAF,
    ///<summary>Next Track KeyPress</summary>
    MediaNextTrack = 0xB0,
    ///<summary>Previous Track KeyPress</summary>
    MediaPrevTrack = 0xB1,
    ///<summary>Stop Media KeyPress</summary>
    MediaStop = 0xB2,
    ///<summary>Play/Pause Media KeyPress</summary>
    MediaPlayPause = 0xB3,
    ///<summary>Startclient Mail KeyPress</summary>
    LaunchMail = 0xB4,
    ///<summary>Select Media KeyPress</summary>
    LaunchMediaSelect = 0xB5,
    ///<summary>Startclient Application 1 KeyPress</summary>
    LaunchApp1 = 0xB6,
    ///<summary>Startclient Application 2 KeyPress</summary>
    LaunchApp2 = 0xB7,
    ///<summary>Used for miscellaneous characters; it can vary by Keyboard.</summary>
    Oem1 = 0xBA,
    ///<summary>For any country/region, the '+' KeyPress</summary>
    OemPlus = 0xBB,
    ///<summary>For any country/region, the ',' KeyPress</summary>
    OemComma = 0xBC,
    ///<summary>For any country/region, the '-' KeyPress</summary>
    OemMinus = 0xBD,
    ///<summary>For any country/region, the '.' KeyPress</summary>
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
    ///<summary>Windows 95/98/Me, Windows Nt 4.0, IME Process KeyPress</summary>
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
    ///<summary>ATTN KeyPress</summary>
    Attn = 0xF6,
    ///<summary>Crsel KeyPress</summary>
    CrSel = 0xF7,
    ///<summary>Exsel KeyPress</summary>
    ExSel = 0xF8,
    ///<summary>Erase EoF KeyPress</summary>
    EraseEoF = 0xF9,
    ///<summary>Play KeyPress</summary>
    Play = 0xFA,
    ///<summary>Zoom KeyPress</summary>
    Zoom = 0xFB,
    ///<summary>Reserved</summary>
    NoName = 0xFC,
    ///<summary>PA1 KeyPress</summary>
    PA1 = 0xFD,
    ///<summary>Clear KeyPress</summary>
    OemClear = 0xFE,
}
