
using MouseAndKeyboard.Native;
using System.Text;

namespace MouseAndKeyboard.InputListener;

internal static class KeyboardStateHelper
{
    //Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods
    private static VirtualKey lastVirtualKeyCode;

    private static ScanCode lastScanCode;
    private static byte[] lastKeyState = new byte[byte.MaxValue];
    private static bool lastIsDead;

    /// <summary>
    ///     Gets the input locale identifier for the active application's thread.  Using this combined with the ToUnicodeEx and
    ///     MapVirtualKeyEx enables Windows to properly translate keys based on the keyboard layout designated for the
    ///     application.
    /// </summary>
    /// <returns>HKL</returns>
    internal static IntPtr GetActiveKeyboardLayout()
    {
        var hActiveWnd = HookNativeMethods.GetForegroundWindow(); //handle to focused window
        var hCurrentWnd = HookNativeMethods.GetWindowThreadProcessId(hActiveWnd, IntPtr.Zero); //thread of focused window
        return KeyboardNativeMethods.GetKeyboardLayout(hCurrentWnd); //get the layout identifier for the thread whose window is focused
    }

    /// <summary>Translates a virtual key to its character equivalent using a specified keyboard layout</summary>
    internal static void TryGetCharFromKeyboardState(VirtualKey virtualKeyCode, ScanCode scanCode, KeyEventF fuState, out char[]? chars)
    {
        var dwhkl = GetActiveKeyboardLayout();

        var pwszBuff = new StringBuilder(64);
        var keyboardState = KeyboardSnapshot.GetCurrent();
        var currentKeyboardState = keyboardState.KeyboardStateNative;
        var isDead = false;

        if (keyboardState.IsDown(Keys.ShiftKey))
            currentKeyboardState[(int)Keys.ShiftKey] = 0x80;

        if (keyboardState.IsToggled(Keys.CapsLock))
            currentKeyboardState[(int)Keys.CapsLock] = 0x01;

        var relevantChars = KeyNativeMethods.ToUnicodeEx(virtualKeyCode, scanCode, currentKeyboardState, pwszBuff, pwszBuff.Capacity, fuState, dwhkl);

        switch (relevantChars)
        {
            case -1:
                // ClearKeyboardBuffer
                var sb = new StringBuilder(10);
                int rc;
                do
                {
                    var lpKeyStateNull = new byte[byte.MaxValue];
                    rc = KeyNativeMethods.ToUnicodeEx(virtualKeyCode, scanCode, lpKeyStateNull, sb, sb.Capacity, 0, dwhkl);
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
                _ = KeyNativeMethods.ToUnicodeEx(lastVirtualKeyCode, lastScanCode, lastKeyState, sbTemp, sbTemp.Capacity, 0, dwhkl);
                lastIsDead = false;
                lastVirtualKeyCode = 0;
            }

            return;
        }

        lastScanCode = scanCode;
        lastVirtualKeyCode = virtualKeyCode;
        lastIsDead = isDead;
        lastKeyState = (byte[])currentKeyboardState.Clone();
    }

}
