using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native.RawInput;

//https://learn.microsoft.com/en-us/windows/win32/inputdev/raw-input
internal partial class RawInputAPI
{
    /* Some help from:
    * https://github.com/dahall/vanara
    * https://github.com/mfakane/rawinput-sharp
    */

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerrawinputdevices
    [LibraryImport(User32.USER_32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray)] RawInputDevice[] pRawInputDevices, int uiNumDevices, int cbSize);
    internal static bool RegisterRawInputDevices(params RawInputDevice[] devices) => RegisterRawInputDevices(devices, devices.Length, RawInputDevice.Size);

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getregisteredrawinputdevices
    [LibraryImport(User32.USER_32, SetLastError = true)]
    private static partial uint GetRegisteredRawInputDevices([Optional, MarshalAs(UnmanagedType.LPArray)] ref RawInputDevice[]? pRawInputDevices, ref uint puiNumDevices, int cbSize);
    internal static RawInputDevice[]? GetRegisteredRawInputDevices()
    {
        var size = RawInputDevice.Size;
        uint count = 0;
        RawInputDevice[]? rid = null;
        _ = GetRegisteredRawInputDevices(ref rid, ref count, size);
        rid = new RawInputDevice[count];
        if (GetRegisteredRawInputDevices(ref rid, ref count, size) < 0)
            return null;
        return rid;
    }

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-defrawinputproc
    [LibraryImport(User32.USER_32)]
    private static partial IntPtr DefRawInputProc(RawInputStruct[] paRawInput, int nInput, int cbSizeHeader);
    internal static IntPtr DefRawInputProc(RawInputStruct[] paRawInput, int nInput) => DefRawInputProc(paRawInput, nInput, RawInputHeader.Size);

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputbuffer
    [LibraryImport(User32.USER_32, SetLastError = true)]
    private static partial uint GetRawInputBuffer([Optional] IntPtr pData, ref uint pcbSize, int cbSizeHeader);
    internal static uint GetRawInputBuffer([Optional] IntPtr pData, ref uint pcbSize) => GetRawInputBuffer(pData, ref pcbSize, RawInputHeader.Size);

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdata
    [LibraryImport(User32.USER_32)]
    private static partial int GetRawInputData(IntPtr hRawInput, RawInputCommand uiCommand, out RawInputStruct pData, ref int pcbSize, int cbSizeHeader);
    internal static int GetRawInputData(IntPtr hRawInput, RawInputCommand uiCommand, out RawInputStruct pData, out int pcbSize)
    {
        var size = RawInputStruct.Size;
        var result = GetRawInputData(hRawInput, uiCommand, out pData, ref size, RawInputHeader.Size);
        pcbSize = size;
        return result;
    }

    //https://www.pinvoke.net/default.aspx/user32/GetRawInputDeviceInfo.html
    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdeviceinfoa
    [LibraryImport(User32.USER_32, SetLastError = true)]
    internal static partial uint GetRawInputDeviceInfoA([Optional] IntPtr hDevice, DeviceInfoTypes uiCommand, [Optional] ref DeviceInfo pData, ref int pcbSize);

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdevicelist
    [LibraryImport(User32.USER_32, SetLastError = true)]
    private static partial uint GetRawInputDeviceList([Optional, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] out RawInputDevice[]? pRawInputDeviceList, ref uint puiNumDevices, int cbSize);
    internal static uint GetRawInputDeviceList(out RawInputDevice[]? pRawInputDeviceList, ref uint puiNumDevices) => GetRawInputDeviceList(out pRawInputDeviceList, ref puiNumDevices, RawInputDeviceList.Size);

}

#region Raw Input

// https://learn.microsoft.com/en-us/windows/win32/inputdev/wm-input
internal enum RawInputMessage
{
    Input = 0,
    InputSink = 1,
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdata
internal enum RawInputCommand : uint
{
    Input = 0x10000003,
    Header = 0x10000005,
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputheader
[StructLayout(LayoutKind.Sequential)]
internal readonly struct RawInputHeader
{
    /// <summary>Type of device the input is coming from.</summary>
    public readonly RawInputType dwType;
    /// <summary>Size of the packet of data.</summary>
    public readonly DWORD dwSize;
    /// <summary>Handle to the device sending the data.</summary>
    public readonly IntPtr hDevice;
    /// <summary>wParam from the window message.</summary>
    public readonly IntPtr wParam;
    public static int Size => Marshal.SizeOf<RawInputHeader>();
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinput
[StructLayout(LayoutKind.Sequential)]
internal readonly struct RawInputStruct
{
    /// <summary>Header for the data</summary>
    public readonly RawInputHeader header;
    public readonly Union data;
    [StructLayout(LayoutKind.Explicit)]
    public struct Union
    {
        /// <summary>Mouse raw input data</summary>
        [FieldOffset(0)] public RawMouse mouse;
        /// <summary>Keyboard raw input data</summary>
        [FieldOffset(0)] public RawKeyboard keyboard;
        /// <summary>HID raw input data</summary>
        [FieldOffset(0)] public RawHID hid;
    }

    public static int Size => Marshal.SizeOf<RawInputStruct>();
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawhid
[StructLayout(LayoutKind.Sequential)]
public readonly record struct RawHID
{
    /// <summary>
    /// <para>Type: <c>DWORD</c></para>
    /// <para>The size, in bytes, of each HID input in <c>bRawData</c>.</para>
    /// </summary>
    public readonly uint dwSizeHid;
    /// <summary>
    /// <para>Type: <c>DWORD</c></para>
    /// <para>The number of HID inputs in <c>bRawData</c>.</para>
    /// </summary>
    public readonly uint dwCount;
    /// <summary>
    /// <para>Type: <c>BYTE[1]</c></para>
    /// <para>The raw input data, as an array of bytes.</para>
    /// </summary>
    public readonly IntPtr bRawData;
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawkeyboard
[StructLayout(LayoutKind.Sequential)]
public readonly record struct RawKeyboard
{
    /// <summary>Scan code for key depression</summary>
    public readonly ushort MakeCode;
    /// <summary>Scan code information</summary>
    public readonly RawKeyboardFlags Flags;
    /// <summary>Reserved</summary>
    public readonly ushort Reserved;
    /// <summary>Virtual key code</summary>
    public readonly VirtualKey VKey;
    /// <summary>Corresponding window message</summary>
    public readonly WindowsMessages Message;
    /// <summary>Extra information</summary>
    public readonly ulong ExtraInformation;

    [Flags]
    public enum RawKeyboardFlags : ushort
    {
        /// <summary>The key is down</summary>
        RI_KEY_MAKE = 0,
        /// <summary>The key is up</summary>
        RI_KEY_BREAK = 1,
        /// <summary>The scan code has the E0 prefix</summary>
        RI_KEY_E0 = 2,
        /// <summary>The scan code has the E1 prefix</summary>
        RI_KEY_E1 = 4,
    }
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawmouse
[StructLayout(LayoutKind.Sequential)]
public readonly record struct RawMouse
{
    [Flags]
    public enum RawMouseFlags : ushort
    {
        /// <summary>Relative to the last position</summary>
        MoveRelative = 0,
        /// <summary>Absolute positioning</summary>
        MoveAbsolute = 1,
        /// <summary>Coordinate data is mapped to a virtual desktop</summary>
        VirtualDesktop = 2,
        /// <summary>Attributes for the mouse have changed</summary>
        AttributesChanged = 4
    }

    [Flags]
    public enum RawMouseButtons : ushort
    {
        /// <summary>No button</summary>
        None = 0,
        /// <summary>Left (button 1) down</summary>
        LeftDown = 0x0001,
        /// <summary>Left (button 1) up</summary>
        LeftUp = 0x0002,
        /// <summary>Right (button 2) down</summary>
        RightDown = 0x0004,
        /// <summary>Right (button 2) up</summary>
        RightUp = 0x0008,
        /// <summary>Middle (button 3) down</summary>
        MiddleDown = 0x0010,
        /// <summary>Middle (button 3) up</summary>
        MiddleUp = 0x0020,
        /// <summary>X1 (button 4) down</summary>
        Button4Down = 0x0040,
        /// <summary>X1 (button 4) up</summary>
        Button4Up = 0x0080,
        /// <summary>X2 (button 5) down</summary>
        Button5Down = 0x0100,
        /// <summary>X2 (button 5) up</summary>
        Button5Up = 0x0200,
        /// <summary>Mouse wheel moved</summary>
        MouseWheel = 0x0400
    }

    /// <summary>Almost the same as HighLowDWORD</summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct ButtonUnion
    {
        [FieldOffset(0)] public readonly uint ulButtons;
        /// <summary>Flags for the event</summary>
        [FieldOffset(0)] public readonly RawMouseButtons usButtonFlags;
        /// <summary>If the mouse wheel is moved, this will contain the delta amount</summary>
        [FieldOffset(2)] public readonly ushort usButtonData;
    }

    /// <summary>The mouse state</summary>
    public readonly RawMouseFlags usFlags;

    /// <summary>High-Low DWORD</summary>
    public readonly ButtonUnion buttons;

    /// <summary>Raw button data</summary>
    public readonly uint ulRawButtons;
    /// <summary>
    /// The motion in the X direction. This is signed relative motion or
    /// absolute motion, depending on the value of usFlags
    /// </summary>
    public readonly int lLastX;
    /// <summary>
    /// The motion in the Y direction. This is signed relative motion or absolute motion,
    /// depending on the value of usFlags
    /// </summary>
    public readonly int lLastY;
    /// <summary>The device-specific additional information for the event</summary>
    public readonly uint ulExtraInformation;
}

#endregion

#region Raw Input Device

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputheader
//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevicelist
internal enum RawInputType : DWORD
{
    /// <summary>Raw input comes from the mouse.</summary>
    TypeMouse = 0,
    /// <summary>Raw input comes from the keyboard.</summary>
    TypeKeyboard = 1,
    /// <summary>Raw input comes from some device that is not a keyboard or a mouse.</summary>
    TypeHID = 2,
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevicelist
[StructLayout(LayoutKind.Sequential)]
internal struct RawInputDeviceList
{
    public IntPtr hDevice;
    public RawInputType Type;
    public static int Size => Marshal.SizeOf<RawInputDeviceList>();
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
[StructLayout(LayoutKind.Sequential)]
internal struct RawInputDevice
{
    /// <summary>Top level collection Usage page for the raw input device</summary>
    public HIDUsagePage usUsagePage;
    /// <summary>Top level collection Usage for the raw input device. </summary>
    public HIDUsage usUsage;
    /// <summary>Mode flag that specifies how to interpret the information provided by UsagePage and Usage</summary>
    public RawInputDeviceFlags dwFlags;
    /// <summary>Handle to the target device. If NULL, it follows the keyboard focus</summary>
    public IntPtr hwndTarget;

    public RawInputDevice(HIDUsagePage usUsagePage, HIDUsage usUsage, RawInputDeviceFlags dwFlags, nint hwndTarget)
    {
        this.usUsagePage = usUsagePage;
        this.usUsage = usUsage;
        this.dwFlags = dwFlags;
        this.hwndTarget = hwndTarget;
    }

    public static int Size => Marshal.SizeOf<RawInputDevice>();
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
[Flags]
internal enum RawInputDeviceFlags : DWORD
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>If set, this removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection.</summary>
    Remove = 0x00000001,
    /// <summary>If set, this specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with PageOnly.</summary>
    Exclude = 0x00000010,
    /// <summary>If set, this specifies all devices whose top level collection is from the specified usUsagePage. Note that Usage must be zero. To exclude a particular top level collection, use Exclude.</summary>
    PageOnly = 0x00000020,
    /// <summary>If set, this prevents any devices specified by UsagePage or Usage from generating legacy messages. This is only for the mouse and keyboard.</summary>
    NoLegacy = 0x00000030,
    /// <summary>If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that WindowHandle must be specified.</summary>
    InputSink = 0x00000100,
    /// <summary>If set, the mouse button click does not activate the other window.</summary>
    CaptureMouse = 0x00000200,
    /// <summary>If set, the application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. NoHotKeys can be specified even if NoLegacy is not specified and WindowHandle is NULL.</summary>
    NoHotKeys = CaptureMouse,
    /// <summary>If set, application keys are handled.  NoLegacy must be specified.  Keyboard only.</summary>
    AppKeys = 0x00000400,
    /// <summary>
    /// If set, this enables the caller to receive input in the background only if the foreground application does not process it. In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.
    /// <br/>~Windows XP: This flag is not supported until Windows Vista~
    /// </summary>
    ExInputSink = 0x00001000,
    /// <summary>
    /// If set, this enables the caller to receive WM_INPUT_DEVICE_CHANGE notifications for device arrival and device removal.
    /// <br/>~Windows XP: This flag is not supported until Windows Vista~
    /// </summary>
    DevNotify = 0x00002000,
}

#endregion

#region HID https://github.com/tpn/winsdk-10/blob/master/Include/10.0.10240.0/shared/hidusage.h


//https://www.pinvoke.net/default.aspx/Enums/HIDUsagePage.html
//https://learn.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-page
internal enum HIDUsagePage : ushort
{
    /// <summary>Unknown usage page</summary>
    Undefined = 0x00,
    /// <summary>Generic desktop controls</summary>
    Generic = 0x01,
    /// <summary>Simulation controls</summary>
    Simulation = 0x02,
    /// <summary>Virtual reality controls</summary>
    VR = 0x03,
    /// <summary>Sports controls</summary>
    Sport = 0x04,
    /// <summary>Games controls</summary>
    Game = 0x05,
    /// <summary>Keyboard controls</summary>
    Keyboard = 0x07,
    /// <summary>LED controls</summary>
    LED = 0x08,
    /// <summary>Button</summary>
    Button = 0x09,
    /// <summary>Ordinal</summary>
    Ordinal = 0x0A,
    /// <summary>Telephony</summary>
    Telephony = 0x0B,
    /// <summary>Consumer</summary>
    Consumer = 0x0C,
    /// <summary>Digitizer</summary>
    Digitizer = 0x0D,
    /// <summary>Physical interface device</summary>
    PID = 0x0F,
    /// <summary>Unicode</summary>
    Unicode = 0x10,
    /// <summary>Alphanumeric display</summary>
    AlphaNumeric = 0x14,
    /// <summary>Medical instruments</summary>
    Medical = 0x40,
    /// <summary>Monitor page 0</summary>
    MonitorPage0 = 0x80,
    /// <summary>Monitor page 1</summary>
    MonitorPage1 = 0x81,
    /// <summary>Monitor page 2</summary>
    MonitorPage2 = 0x82,
    /// <summary>Monitor page 3</summary>
    MonitorPage3 = 0x83,
    /// <summary>Power page 0</summary>
    PowerPage0 = 0x84,
    /// <summary>Power page 1</summary>
    PowerPage1 = 0x85,
    /// <summary>Power page 2</summary>
    PowerPage2 = 0x86,
    /// <summary>Power page 3</summary>
    PowerPage3 = 0x87,
    /// <summary>Bar code scanner</summary>
    BarCode = 0x8C,
    /// <summary>Scale page</summary>
    Scale = 0x8D,
    /// <summary>Magnetic strip reading devices</summary>
    MSR = 0x8E
}

#pragma warning disable IDE0079 // Enums values should not be duplicated
#pragma warning disable CA1069 // Enums values should not be duplicated
//https://www.pinvoke.net/default.aspx/Enums/HIDUsage.html
//https://learn.microsoft.com/en-us/windows-hardware/drivers/hid/hid-usages#usage-id
internal enum HIDUsage : ushort
{
    /// <summary></summary>
    Pointer = 0x01,
    /// <summary></summary>
    Mouse = 0x02,
    /// <summary></summary>
    Joystick = 0x04,
    /// <summary></summary>
    Gamepad = 0x05,
    /// <summary></summary>
    Keyboard = 0x06,
    /// <summary></summary>
    Keypad = 0x07,
    /// <summary></summary>
    SystemControl = 0x80,
    /// <summary></summary>
    X = 0x30,
    /// <summary></summary>
    Y = 0x31,
    /// <summary></summary>
    Z = 0x32,
    /// <summary></summary>
    RelativeX = 0x33,
    /// <summary></summary>    
    RelativeY = 0x34,
    /// <summary></summary>
    RelativeZ = 0x35,
    /// <summary></summary>
    Slider = 0x36,
    /// <summary></summary>
    Dial = 0x37,
    /// <summary></summary>
    Wheel = 0x38,
    /// <summary></summary>
    HatSwitch = 0x39,
    /// <summary></summary>
    CountedBuffer = 0x3A,
    /// <summary></summary>
    ByteCount = 0x3B,
    /// <summary></summary>
    MotionWakeup = 0x3C,
    /// <summary></summary>
    VX = 0x40,
    /// <summary></summary>
    VY = 0x41,
    /// <summary></summary>
    VZ = 0x42,
    /// <summary></summary>
    VBRX = 0x43,
    /// <summary></summary>
    VBRY = 0x44,
    /// <summary></summary>
    VBRZ = 0x45,
    /// <summary></summary>
    VNO = 0x46,
    /// <summary></summary>
    SystemControlPower = 0x81,
    /// <summary></summary>
    SystemControlSleep = 0x82,
    /// <summary></summary>
    SystemControlWake = 0x83,
    /// <summary></summary>
    SystemControlContextMenu = 0x84,
    /// <summary></summary>
    SystemControlMainMenu = 0x85,
    /// <summary></summary>
    SystemControlApplicationMenu = 0x86,
    /// <summary></summary>
    SystemControlHelpMenu = 0x87,
    /// <summary></summary>
    SystemControlMenuExit = 0x88,
    /// <summary></summary>
    SystemControlMenuSelect = 0x89,
    /// <summary></summary>
    SystemControlMenuRight = 0x8A,
    /// <summary></summary>
    SystemControlMenuLeft = 0x8B,
    /// <summary></summary>
    SystemControlMenuUp = 0x8C,
    /// <summary></summary>
    SystemControlMenuDown = 0x8D,
    /// <summary></summary>
    KeyboardNoEvent = 0x00,
    /// <summary></summary>
    KeyboardRollover = 0x01,
    /// <summary></summary>
    KeyboardPostFail = 0x02,
    /// <summary></summary>
    KeyboardUndefined = 0x03,
    /// <summary></summary>
    KeyboardaA = 0x04,
    /// <summary></summary>
    KeyboardzZ = 0x1D,
    /// <summary></summary>
    Keyboard1 = 0x1E,
    /// <summary></summary>
    Keyboard0 = 0x27,
    /// <summary></summary>
    KeyboardLeftControl = 0xE0,
    /// <summary></summary>
    KeyboardLeftShift = 0xE1,
    /// <summary></summary>
    KeyboardLeftALT = 0xE2,
    /// <summary></summary>
    KeyboardLeftGUI = 0xE3,
    /// <summary></summary>
    KeyboardRightControl = 0xE4,
    /// <summary></summary>
    KeyboardRightShift = 0xE5,
    /// <summary></summary>
    KeyboardRightALT = 0xE6,
    /// <summary></summary>
    KeyboardRightGUI = 0xE7,
    /// <summary></summary>
    KeyboardScrollLock = 0x47,
    /// <summary></summary>
    KeyboardNumLock = 0x53,
    /// <summary></summary>
    KeyboardCapsLock = 0x39,
    /// <summary></summary>
    KeyboardF1 = 0x3A,
    /// <summary></summary>
    KeyboardF12 = 0x45,
    /// <summary></summary>
    KeyboardReturn = 0x28,
    /// <summary></summary>
    KeyboardEscape = 0x29,
    /// <summary></summary>
    KeyboardDelete = 0x2A,
    /// <summary></summary>
    KeyboardPrintScreen = 0x46,
    /// <summary></summary>
    LEDNumLock = 0x01,
    /// <summary></summary>
    LEDCapsLock = 0x02,
    /// <summary></summary>
    LEDScrollLock = 0x03,
    /// <summary></summary>
    LEDCompose = 0x04,
    /// <summary></summary>
    LEDKana = 0x05,
    /// <summary></summary>
    LEDPower = 0x06,
    /// <summary></summary>
    LEDShift = 0x07,
    /// <summary></summary>
    LEDDoNotDisturb = 0x08,
    /// <summary></summary>
    LEDMute = 0x09,
    /// <summary></summary>
    LEDToneEnable = 0x0A,
    /// <summary></summary>
    LEDHighCutFilter = 0x0B,
    /// <summary></summary>
    LEDLowCutFilter = 0x0C,
    /// <summary></summary>
    LEDEqualizerEnable = 0x0D,
    /// <summary></summary>
    LEDSoundFieldOn = 0x0E,
    /// <summary></summary>
    LEDSurroundFieldOn = 0x0F,
    /// <summary></summary>
    LEDRepeat = 0x10,
    /// <summary></summary>
    LEDStereo = 0x11,
    /// <summary></summary>
    LEDSamplingRateDirect = 0x12,
    /// <summary></summary>
    LEDSpinning = 0x13,
    /// <summary></summary>
    LEDCAV = 0x14,
    /// <summary></summary>
    LEDCLV = 0x15,
    /// <summary></summary>
    LEDRecordingFormatDet = 0x16,
    /// <summary></summary>
    LEDOffHook = 0x17,
    /// <summary></summary>
    LEDRing = 0x18,
    /// <summary></summary>
    LEDMessageWaiting = 0x19,
    /// <summary></summary>
    LEDDataMode = 0x1A,
    /// <summary></summary>
    LEDBatteryOperation = 0x1B,
    /// <summary></summary>
    LEDBatteryOK = 0x1C,
    /// <summary></summary>
    LEDBatteryLow = 0x1D,
    /// <summary></summary>
    LEDSpeaker = 0x1E,
    /// <summary></summary>
    LEDHeadset = 0x1F,
    /// <summary></summary>
    LEDHold = 0x20,
    /// <summary></summary>
    LEDMicrophone = 0x21,
    /// <summary></summary>
    LEDCoverage = 0x22,
    /// <summary></summary>
    LEDNightMode = 0x23,
    /// <summary></summary>
    LEDSendCalls = 0x24,
    /// <summary></summary>
    LEDCallPickup = 0x25,
    /// <summary></summary>
    LEDConference = 0x26,
    /// <summary></summary>
    LEDStandBy = 0x27,
    /// <summary></summary>
    LEDCameraOn = 0x28,
    /// <summary></summary>
    LEDCameraOff = 0x29,
    /// <summary></summary>    
    LEDOnLine = 0x2A,
    /// <summary></summary>
    LEDOffLine = 0x2B,
    /// <summary></summary>
    LEDBusy = 0x2C,
    /// <summary></summary>
    LEDReady = 0x2D,
    /// <summary></summary>
    LEDPaperOut = 0x2E,
    /// <summary></summary>
    LEDPaperJam = 0x2F,
    /// <summary></summary>
    LEDRemote = 0x30,
    /// <summary></summary>
    LEDForward = 0x31,
    /// <summary></summary>
    LEDReverse = 0x32,
    /// <summary></summary>
    LEDStop = 0x33,
    /// <summary></summary>
    LEDRewind = 0x34,
    /// <summary></summary>
    LEDFastForward = 0x35,
    /// <summary></summary>
    LEDPlay = 0x36,
    /// <summary></summary>
    LEDPause = 0x37,
    /// <summary></summary>
    LEDRecord = 0x38,
    /// <summary></summary>
    LEDError = 0x39,
    /// <summary></summary>
    LEDSelectedIndicator = 0x3A,
    /// <summary></summary>
    LEDInUseIndicator = 0x3B,
    /// <summary></summary>
    LEDMultiModeIndicator = 0x3C,
    /// <summary></summary>
    LEDIndicatorOn = 0x3D,
    /// <summary></summary>
    LEDIndicatorFlash = 0x3E,
    /// <summary></summary>
    LEDIndicatorSlowBlink = 0x3F,
    /// <summary></summary>
    LEDIndicatorFastBlink = 0x40,
    /// <summary></summary>
    LEDIndicatorOff = 0x41,
    /// <summary></summary>
    LEDFlashOnTime = 0x42,
    /// <summary></summary>
    LEDSlowBlinkOnTime = 0x43,
    /// <summary></summary>
    LEDSlowBlinkOffTime = 0x44,
    /// <summary></summary>
    LEDFastBlinkOnTime = 0x45,
    /// <summary></summary>
    LEDFastBlinkOffTime = 0x46,
    /// <summary></summary>
    LEDIndicatorColor = 0x47,
    /// <summary></summary>
    LEDRed = 0x48,
    /// <summary></summary>
    LEDGreen = 0x49,
    /// <summary></summary>
    LEDAmber = 0x4A,
    /// <summary></summary>
    LEDGenericIndicator = 0x3B,
    /// <summary></summary>
    TelephonyPhone = 0x01,
    /// <summary></summary>
    TelephonyAnsweringMachine = 0x02,
    /// <summary></summary>
    TelephonyMessageControls = 0x03,
    /// <summary></summary>
    TelephonyHandset = 0x04,
    /// <summary></summary>
    TelephonyHeadset = 0x05,
    /// <summary></summary>
    TelephonyKeypad = 0x06,
    /// <summary></summary>
    TelephonyProgrammableButton = 0x07,
    /// <summary></summary>
    SimulationRudder = 0xBA,
    /// <summary></summary>
    SimulationThrottle = 0xBB
}
#pragma warning restore CA1069 // Enums values should not be duplicated
#pragma warning restore IDE0079 // Enums values should not be duplicated

#endregion

#region Device Info

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdeviceinfoa
internal enum DeviceInfoTypes : DWORD
{
    PreParsedData = 0x20000005,
    DeviceName = 0x20000007,
    DeviceInfo = 0x2000000B
}

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rid_device_info
[StructLayout(LayoutKind.Sequential)]
internal readonly struct DeviceInfo
{
    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rid_device_info_mouse
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct DeviceInfoMouse
    {
        public readonly DWORD dwId;
        public readonly DWORD dwNumberOfButtons;
        public readonly DWORD dwSampleRate;
        [MarshalAs(UnmanagedType.Bool)]
        public readonly byte fHasHorizontalWheel;
        public bool HasHorizontalWheel => fHasHorizontalWheel > 0;
    }
    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rid_device_info_keyboard
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct DeviceInfoKeyboard
    {
        public readonly DWORD dwType;
        public readonly DWORD dwSubType;
        public readonly DWORD dwKeyboardMode;
        public readonly DWORD dwNumberOfFunctionKeys;
        public readonly DWORD dwNumberOfIndicators;
        public readonly DWORD dwNumberOfKeysTotal;
    }
    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rid_device_info_hid
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct DeviceInfoHID
    {
        public readonly DWORD dwVendorId;
        public readonly DWORD dwProductId;
        public readonly DWORD dwVersionNumber;
        public readonly ushort usUsagePage;
        public readonly ushort usUsage;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal readonly struct DeviceInfoUnion
    {
        [FieldOffset(0)] public readonly DeviceInfoMouse mouse;
        [FieldOffset(0)] public readonly DeviceInfoKeyboard keyboard;
        [FieldOffset(0)] public readonly DeviceInfoHID hid;
    }

    public readonly DWORD cbSize = Size;
    public readonly RawInputType dwType;
    public readonly DeviceInfoUnion union;

    public DeviceInfo() { }

    public static int Size => Marshal.SizeOf<DeviceInfo>();
}

#endregion