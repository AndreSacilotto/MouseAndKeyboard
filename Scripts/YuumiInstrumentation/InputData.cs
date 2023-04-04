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


    public readonly static HashSet<VirtualKey> FunctionKeysVK = new()
    {
        VirtualKey.F1, VirtualKey.F2, VirtualKey.F3, VirtualKey.F4, VirtualKey.F5,
    };

    public readonly static HashSet<VirtualKey> MirrorWhenShiftVK = new() {

        VirtualKey.Q, VirtualKey.W, VirtualKey.E, VirtualKey.R,

        VirtualKey.D, VirtualKey.F,

        VirtualKey.D1, VirtualKey.D2, VirtualKey.D3, VirtualKey.D4, VirtualKey.D5,

        VirtualKey.Space,

        VirtualKey.B,
        VirtualKey.Y,

        VirtualKey.P,
        VirtualKey.Escape,
    };




}
