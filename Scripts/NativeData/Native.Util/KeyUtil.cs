using MouseAndKeyboard.Util;

namespace MouseAndKeyboard.Native;

public static class KeyUtil
{
    public static readonly Dictionary<VirtualKey, ScanCode> ScanCodesDict = new();

    public static Dictionary<VirtualKey, ScanCode> GetAllScanCodes()
    {
        var vkValues = EnumUtil.EnumToArray<VirtualKey>();
        var dict = new Dictionary<VirtualKey, ScanCode>(vkValues.Length);
        for (int i = 1; i < vkValues.Length; i++)
        {
            var key = vkValues[i];
            dict.TryAdd(key, (ScanCode)User32.MapVirtualKeyW(key, MapType.VK_TO_VSC));
        }
        return dict;
    }

    public static ScanCode VirtualKeyToScanCode(VirtualKey key)
    {
        if (!ScanCodesDict.TryGetValue(key, out var result))
        {
            result = (ScanCode)User32.MapVirtualKeyW(key, MapType.VK_TO_VSC);
            if (result != 0)
                ScanCodesDict.Add(key, result);
        }
        return result;
    }

    public static VirtualKey ScanCodeToVirtualKey(short key) => (VirtualKey)User32.MapVirtualKeyW((uint)key, MapType.VSC_TO_VK);

    public static VirtualKey CharToVirtualKey(char ch, out bool control, out bool shift, out bool alt)
    {
        var vk = User32.VkKeyScanW(ch);
        var order = new HighLowWORD(vk);

        var virtualKey = order.Low;
        var shiftState = order.High;

        //https://stackoverflow.com/a/2899364
        //var retval = (vk & 0xff);
        //var modifiers = vk >> 8;

        shift = (shiftState & 1) != 0;
        control = (shiftState & 2) != 0;
        alt = (shiftState & 4) != 0;

        return (VirtualKey)virtualKey;
    }

    #region KeyState
    // # It is not possible to distinguish Keys.LControlKey and Keys.RControlKey when they are modifiers. Same apply to Shift/Alt/Menu

    // # Windows KeyPress
    // combine LWin and RWin key with other keys will potentially corrupt the data
    // notable F5 | Keys.LWin == F12
    // and the KeyEventArgs.KeyData don't recognize combined data either

    // # Function KeyPress [Fn]
    // CANNOT determine state due to conversion inside keyboard
    // See http://en.wikipedia.org/wiki/Fn_key#Technical_details

    /// <returns>If true the key is down; otherwise, it is up.</returns>
    public static bool CheckKeyState(int vKey) => BitUtil.GetHighOrderBit(User32.GetKeyState(vKey)) != 0;
    public static bool CheckToggleState(int vKey) => BitUtil.GetLowOrderBit(User32.GetKeyState(vKey)) != 0;
    public static bool CheckKeyState(int vKey, out bool isToggle)
    {
        isToggle = BitUtil.GetLowOrderBit(User32.GetKeyState(vKey)) != 0;
        return BitUtil.GetHighOrderBit(User32.GetKeyState(vKey)) != 0;
    }

    public static bool GetNumLockToggleState() => CheckToggleState((int)VirtualKey.NumLock);
    public static bool GetCapsLockToggleState() => CheckToggleState((int)VirtualKey.CapitalLock);
    public static bool GetScrollLockToggleState() => CheckToggleState((int)VirtualKey.ScrollLock);

    public static bool GetControlState() => CheckKeyState((int)VirtualKey.Control);
    public static bool GetShiftState() => CheckKeyState((int)VirtualKey.Shift);
    public static bool GetAltState() => CheckKeyState((int)VirtualKey.Menu);
    public static InputModifiers CheckModifiersState() => 
        (GetControlState() ? InputModifiers.Control : InputModifiers.None) |
        (GetShiftState() ? InputModifiers.Shift : InputModifiers.None) |
        (GetAltState() ? InputModifiers.Alt : InputModifiers.None);

    #endregion

    public static VirtualKey[] ModifiersToVirtualKey(this InputModifiers mod)
    {
        var ctl = mod.HasFlag(InputModifiers.Control) ? 1 : 0;
        var sht = mod.HasFlag(InputModifiers.Shift) ? 1 : 0;
        var alt = mod.HasFlag(InputModifiers.Alt) ? 1 : 0;

        var arr = new VirtualKey[ctl + sht + alt];
        int i = 0;
        if (ctl == 1) arr[i++] = VirtualKey.Control;
        if (sht == 1) arr[i++] = VirtualKey.Shift;
        if (alt == 1) arr[i++] = VirtualKey.Alt;
        return arr;
    }

    public static VirtualKey ModifiersToVirtualKeySingle(this InputModifiers mod)
    {
        if (mod.HasFlag(InputModifiers.Control))
            return VirtualKey.Control;
        if (mod.HasFlag(InputModifiers.Shift))
            return VirtualKey.Shift;
        if (mod.HasFlag(InputModifiers.Alt))
            return VirtualKey.Alt;
        return VirtualKey.None;
    }

    public static InputModifiers KeyToModifier(this VirtualKey key)
    {
        var mod = InputModifiers.None;
        if (key.HasFlag(VirtualKey.LeftControl) || key.HasFlag(VirtualKey.RightControl))
            mod |= InputModifiers.Control;
        if (key.HasFlag(VirtualKey.LeftShift) || key.HasFlag(VirtualKey.RightShift))
            mod |= InputModifiers.Shift;
        if (key.HasFlag(VirtualKey.LeftMenu) || key.HasFlag(VirtualKey.RightMenu))
            mod |= InputModifiers.Alt;
        return mod;
    }

}