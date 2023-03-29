using MouseAndKeyboard.Native;
using MouseAndKeyboard.Util;

namespace MouseAndKeyboard.InputListener;

internal class AppKeyboardListener : KeyboardListener
{
    public AppKeyboardListener() : base(WinHook.HookAppKeyboard())
    {
    }

    protected override List<KeyPressEventArgsExt> GetPressEventArgs(ref nint wParam, ref nint lParam)
    {
        //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

        const uint maskScanCode = 0xFF0000; // for bit 16-23
        const uint maskKeydown = 0x40000000; // for bit 30
        const uint maskKeyup = 0x80000000; // for bit 31

        var flags = (uint)lParam;

        //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
        var wasKeyDown = (flags & maskKeydown) > 0;
        //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
        var isKeyReleased = (flags & maskKeyup) > 0;

        var list = new List<KeyPressEventArgsExt>();

        if (!wasKeyDown && !isKeyReleased)
            return list;

        var scanCode = checked(flags & maskScanCode);

        KeyboardStateHelper.TryGetCharFromKeyboardState((VirtualKeyShort)wParam, (ScanCodeShort)scanCode, 0, out var chars);
        if (chars != null)
            foreach (var ch in chars)
                list.Add(new KeyPressEventArgsExt(ch));
        return list;
    }

    protected override KeyEventArgsExt GetDownUpEventArgs(ref nint wParam, ref nint lParam)
    {
        //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

        const uint maskExtendedKey = 0x1000000; // for bit 24
        const uint maskKeydown = 0x40000000; // for bit 30
        const uint maskKeyup = 0x80000000; // for bit 31

        var timestamp = Environment.TickCount;

        var flags = (uint)lParam;

        //bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
        var wasKeyDown = (flags & maskKeydown) > 0;
        //bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
        var isKeyReleased = (flags & maskKeyup) > 0;
        //bit 24 Specifies the extended key state. The value is 1 if the key is an extended key, otherwise the value is 0.
        var isExtendedKey = (flags & maskExtendedKey) > 0;


        var keyData = KeyUtil.AppendModifierStates((Keys)wParam);
        int scanCode = (int)((flags & 0x10000) | (flags & 0x20000) | (flags & 0x40000) | (flags & 0x80000) |
            (flags & 0x100000) | (flags & 0x200000) | (flags & 0x400000) | (flags & 0x800000)) >> 16;

        var isKeyDown = !isKeyReleased;
        var isKeyUp = wasKeyDown && isKeyReleased;

        return new KeyEventArgsExt(keyData, (ScanCodeShort)scanCode, (uint)timestamp, isKeyDown, isKeyUp, isExtendedKey);
    }

}