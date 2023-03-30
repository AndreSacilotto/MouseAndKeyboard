using MouseAndKeyboard.Native;
using System.Text;

namespace MouseAndKeyboard.Util;

public static class KeyUtil
{
    private static readonly Dictionary<Keys, uint> scanCodesDict = new();

    public static uint KeyCodeToScanCode(Keys key)
    {
        if (!scanCodesDict.TryGetValue(key, out var result))
        {
            result = KeyNativeMethods.MapVirtualKeyW((uint)key, MapType.VK_TO_VSC);
            scanCodesDict.Add(key, result);
        }
        return result;
    }

    public static Keys CharToVirtualKey(char ch)
    {
        var vkey = KeyNativeMethods.VkKeyScanW(ch);
        var retval = (Keys)(vkey & 0xff);
        int modifiers = vkey >> 8;

        if ((modifiers & 1) != 0)
            retval |= Keys.Shift;
        if ((modifiers & 2) != 0)
            retval |= Keys.Control;
        if ((modifiers & 4) != 0)
            retval |= Keys.Alt;
        return retval;
    }

    public static Keys NormalizeModifier(this Keys key)
    {
        if (key.HasFlag(Keys.LControlKey) || key.HasFlag(Keys.RControlKey))
            return Keys.Control;
        if (key.HasFlag(Keys.LShiftKey) || key.HasFlag(Keys.RShiftKey))
            return Keys.Shift;
        if (key.HasFlag(Keys.LMenu) || key.HasFlag(Keys.RMenu))
            return Keys.Alt;
        return key;
    }

    // # It is not possible to distinguish Keys.LControlKey and Keys.RControlKey when they are modifiers
    // Check for Keys.Control instead
    // Same for Shift and Alt(Menu)
    public static bool CheckModifier(int vKey) => (KeyboardNativeMethods.GetKeyState(vKey) & 0x8000) > 0;

    public static Keys AppendModifierStates(Keys keyData)
    {
        // Is Shift being held down?
        var shift = CheckModifier((int)VirtualKey.SHIFT);
        // Is Control being held down?
        var control = CheckModifier((int)VirtualKey.CONTROL);
        // Is Alt being held down?
        var alt = CheckModifier((int)VirtualKey.MENU);

        // Windows keys
        // # combine LWin and RWin key with other keys will potentially corrupt the data
        // notable F5 | Keys.LWin == F12
        // and the KeyEventArgs.KeyData don't recognize combined data either

        // Function (Fn) key
        // # CANNOT determine state due to conversion inside keyboard
        // See http://en.wikipedia.org/wiki/Fn_key#Technical_details

        return keyData |
               (control ? Keys.Control : Keys.None) |
               (shift ? Keys.Shift : Keys.None) |
               (alt ? Keys.Alt : Keys.None);
    }

}