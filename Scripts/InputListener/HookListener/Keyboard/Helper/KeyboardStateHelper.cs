
using MouseAndKeyboard.Native;
using System.Text;

namespace MouseAndKeyboard.InputListener.Hook;

internal static class KeyboardStateHelper
{
    //Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the Low word of a 32-bit Virtual Key Value used for non-keyboard input methods
    private static VirtualKey lastVirtualKeyCode;
    private static ScanCode lastScanCode;

    private static byte[] lastKeyState = new byte[byte.MaxValue];
    private static bool lastIsDead;

    /// <summary>Translates a virtual key to its character equivalent using a specified keyboard layout</summary>
    internal static char[] TryGetCharFromKeyboardState(VirtualKey virtualKeyCode, ScanCode scanCode, KeyEventF fuState)
    {
        char[]? chars;

        var dwhkl = DeviceUtil.GetActiveKeyboardLayout();

        var pwszBuff = new StringBuilder(64);
        var keyboardState = KeyboardSnapshot.CreateSnapshot();
        var currentKeyboardState = keyboardState.KeyboardStateNative;
        var isDead = false;

        if (keyboardState.IsDown(VirtualKey.Shift))
            currentKeyboardState[(int)VirtualKey.Shift] = 0x80;

        if (keyboardState.IsToggled(VirtualKey.CapitalLock))
            currentKeyboardState[(int)VirtualKey.CapitalLock] = 0x01;

        var relevantChars = User32.ToUnicodeEx((uint)virtualKeyCode, (uint)scanCode, currentKeyboardState, pwszBuff, pwszBuff.Capacity, (uint)fuState, dwhkl);

        switch (relevantChars)
        {
            case -1:
            // ClearKeyboardBuffer
            var sb = new StringBuilder(10);
            int rc;
            do
            {
                var lpKeyStateNull = new byte[byte.MaxValue];
                rc = User32.ToUnicodeEx((uint)virtualKeyCode, (uint)scanCode, lpKeyStateNull, sb, sb.Capacity, 0, dwhkl);
            } while (rc < 0);
            isDead = true;
            chars = null;
            break;

            case 0:
            chars = null;
            break;

            case 1:
            if (pwszBuff.Length > 0)
                chars = new[] { pwszBuff[0] };
            else
                chars = null;
            break;

            // Two or more (only two of them is relevant)
            default:
            if (pwszBuff.Length > 1)
                chars = new[] { pwszBuff[0], pwszBuff[1] };
            else
                chars = new[] { pwszBuff[0] };
            break;
        }

        if (lastVirtualKeyCode != 0 && lastIsDead)
        {
            if (chars != null)
            {
                var sbTemp = new StringBuilder(5);
                _ = User32.ToUnicodeEx((uint)lastVirtualKeyCode, (uint)lastScanCode, lastKeyState, sbTemp, sbTemp.Capacity, 0, dwhkl);
                lastIsDead = false;
                lastVirtualKeyCode = 0;
            }
        }
        else
        {
            lastScanCode = scanCode;
            lastVirtualKeyCode = virtualKeyCode;
            lastIsDead = isDead;
            lastKeyState = (byte[])currentKeyboardState.Clone();
        }

        return chars ?? Array.Empty<char>();
    }

}
