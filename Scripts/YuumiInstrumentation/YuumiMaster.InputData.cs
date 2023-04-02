using MouseAndKeyboard.Native;

namespace YuumiInstrumentation;

partial class YuumiMaster
{
    public const VirtualKey EMERGENCY_QUIT = VirtualKey.Pause;

    public const VirtualKey TOGGLE_CONSOLE = VirtualKey.ScrollLock;
    public const VirtualKey TOGGLE_MOUSEMOVE = VirtualKey.Numpad7;
    public const VirtualKey TOGGLE_MOUSESCROLL = VirtualKey.Numpad4;
    public const VirtualKey TOGGLE_MOUSECLICK = VirtualKey.Numpad1;
    public const VirtualKey TOGGLE_KEYS = VirtualKey.Numpad0;

    public readonly static Dictionary<MouseButtonsF, VirtualKey> MouseToKey = new()
    {
        [MouseButtonsF.XButton1] = VirtualKey.F,
        [MouseButtonsF.XButton2] = VirtualKey.D,
    };

    public readonly static Dictionary<VirtualKey, VirtualKey> KeyWithShiftWhenControlShift = new()
    {
        [VirtualKey.D8] = VirtualKey.Q,
        [VirtualKey.D9] = VirtualKey.W,
        [VirtualKey.D0] = VirtualKey.E,
        [VirtualKey.OemMinus] = VirtualKey.R,
    };

    public readonly static HashSet<VirtualKey> MirrorWhenShiftKeys = new() {
        VirtualKey.F1, VirtualKey.F2, VirtualKey.F3, VirtualKey.F4, VirtualKey.F5,

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
