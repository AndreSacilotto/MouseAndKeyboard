using MouseAndKeyboard.Native;

namespace YuumiInstrumentation;

public static class InputData
{
    public enum HotkeysVK
    {
        EmergencyQuit = VirtualKey.Pause,
        Start = VirtualKey.Multiply,
        Stop = VirtualKey.Subtract,
        ToggleConsole = VirtualKey.Add,
        ToggleMouseMove = VirtualKey.Numpad7,
        ToggleMouseScroll = VirtualKey.Numpad4,
        ToggleMouseClick = VirtualKey.Numpad1,
        ToggleKeyboard = VirtualKey.Numpad0,
    }


    public readonly static Dictionary<VirtualKey, VirtualKey> KeyWithShiftWhenControlShift = new()
    {
        [VirtualKey.D8] = VirtualKey.Q,
        [VirtualKey.D9] = VirtualKey.W,
        [VirtualKey.D0] = VirtualKey.E,
        [VirtualKey.OemMinus] = VirtualKey.R,
    };

    public readonly static Dictionary<VirtualKey, int> FunctionKeys = new()
    {
        [VirtualKey.F1] = 1 << 0, 
        [VirtualKey.F2] = 1 << 1,
        [VirtualKey.F3] = 1 << 2,
        [VirtualKey.F4] = 1 << 3,
        [VirtualKey.F5] = 1 << 4,
    };

    public readonly static HashSet<VirtualKey> MirrorWhenShiftKeys = new() {

        VirtualKey.Q, VirtualKey.W, VirtualKey.E, VirtualKey.R,

        VirtualKey.D, VirtualKey.F,

        VirtualKey.D1, VirtualKey.D2, VirtualKey.D3, VirtualKey.D4,

        VirtualKey.Space,

        VirtualKey.B,
        VirtualKey.Y,

        VirtualKey.P,
        VirtualKey.Escape,
    };




}
