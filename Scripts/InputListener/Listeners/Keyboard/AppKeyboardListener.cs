using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

internal class AppKeyboardListener : KeyboardListener
{
    public AppKeyboardListener() : base(WinHook.HookAppKeyboard())
    {
    }

    protected override KeyEventData GetKeyEventArgs(IntPtr wParam, IntPtr lParam)
    {
        //http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx

        const uint maskExtendedKey = 0x1000000; // for bit 24
        const uint maskKeydown = 0x40000000; // for bit 30
        const uint maskKeyup = 0x80000000; // for bit 31

        var flags = (uint)lParam;

        //bit 30 Specifies the previous key state. The Value is 1 if the key is down before the message is sent; it is 0 if the key is up.
        var wasKeyDown = (flags & maskKeydown) > 0;
        //bit 31 Specifies the transition state. The Value is 0 if the key is being pressed and 1 if it is being released.
        var isKeyReleased = (flags & maskKeyup) > 0;
        //bit 24 Specifies the extended key state. The Value is 1 if the key is an extended key, otherwise the Value is 0.
        var isExtendedKey = (flags & maskExtendedKey) > 0;

        var keyData = (VirtualKey)wParam;

        KeyUtil.CheckeModifiersState(out var ctl, out var sht, out var alt);

        int scanCode = (int)((flags & 0x10000) | (flags & 0x20000) | (flags & 0x40000) | (flags & 0x80000) |
            (flags & 0x100000) | (flags & 0x200000) | (flags & 0x400000) | (flags & 0x800000)) >> 16;

        var isKeyDown = !isKeyReleased;
        var isKeyUp = wasKeyDown && isKeyReleased;

        var timestamp = Environment.TickCount;

        return new KeyEventData(keyData, (ScanCode)scanCode, isKeyDown, isKeyUp, ctl, sht, alt, isExtendedKey, timestamp);
    }

    protected override IEnumerable<KeyPressEventData> GetPressEventArgs(IntPtr wParam, IntPtr lParam)
    {
        //https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644984(v=vs.85)#syntax

        const uint maskScanCode = 0xFF0000; // for bit 16-23
        const uint maskKeydown = 0x40000000; // for bit 30
        const uint maskKeyup = 0x80000000; // for bit 31

        var flags = (uint)lParam;

        //bit 30 Specifies the previous key state. The Value is 1 if the key is down before the message is sent; it is 0 if the key is up.
        var wasKeyDown = (flags & maskKeydown) > 0;
        //bit 31 Specifies the transition state. The Value is 0 if the key is being pressed and 1 if it is being released.
        var isKeyReleased = (flags & maskKeyup) > 0;

        var list = new List<KeyPressEventData>();

        if (!wasKeyDown && !isKeyReleased)
            return list;

        var scanCode = checked(flags & maskScanCode);

        var chars = KeyboardStateHelper.TryGetCharFromKeyboardState((VirtualKey)wParam, (ScanCode)scanCode, 0);
        if (chars != null)
            foreach (var ch in chars)
                list.Add(new KeyPressEventData(ch, Environment.TickCount));
        return list;
    }
}