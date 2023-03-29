namespace YuumiInstrumentation;

partial class YuumiMaster
{
    public readonly static Dictionary<MouseButtons, Keys> MouseToKey = new()
    {
        [MouseButtons.XButton1] = Keys.F,
        [MouseButtons.XButton2] = Keys.D,
    };

    public readonly static Dictionary<Keys, Keys> SkillUpKeys = new()
    {
        [Keys.D8] = Keys.Q,
        [Keys.D9] = Keys.W,
        [Keys.D0] = Keys.E,
        [Keys.OemMinus] = Keys.R,
    };

    public readonly static HashSet<Keys> MirrorWhenShiftKeys = new() {
        Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5,

        Keys.Q, Keys.W, Keys.E, Keys.R,

        Keys.D, Keys.F,

        Keys.D1, Keys.D2, Keys.D3, Keys.D4,

        Keys.Space,

        Keys.B,
        Keys.Y,
        Keys.P,

        Keys.Escape,
    };

}
