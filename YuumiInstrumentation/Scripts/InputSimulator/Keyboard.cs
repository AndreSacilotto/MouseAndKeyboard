using System.Windows.Forms;
using static InputSimulation.InputSender;

namespace InputSimulation
{
    public static class Keyboard
    {
        #region Key Down

        internal static InputStruct KeyDownInput(Keys key)
        {
            var input = NewKeyboardInput;
            input.union.ki.wScan = KeyboardUtil.KeyCodeToScanCode(key);
            input.union.ki.dwFlags = KeyEventF.ScanCode;
            return input;
        }

        public static void SendKeyDown(Keys key)
        {
            SendInput(KeyDownInput(key));
        }
        public static void SendKeyDown(params Keys[] keys)
        {
            var inputs = new InputStruct[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                inputs[i] = KeyDownInput(keys[i]);
            SendInput(inputs);
        }

        #endregion

        #region Key Up

        internal static InputStruct KeyUpInput(Keys key)
        {
            var input = NewKeyboardInput;
            input.union.ki.wScan = KeyboardUtil.KeyCodeToScanCode(key);
            input.union.ki.dwFlags = KeyEventF.KeyUp | KeyEventF.ScanCode;
            return input;
        }

        public static void SendKeyUp(Keys key)
        {
            SendInput(KeyUpInput(key));
        }
        public static void SendKeyUp(params Keys[] keys)
        {
            var inputs = new InputStruct[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                inputs[i] = KeyUpInput(keys[i]);
            SendInput(inputs);
        }


        #endregion

        #region Key Full
        internal static void KeyFullInput(Keys key, out InputStruct down, out InputStruct up)
        {
            var input = NewKeyboardInput;
            input.union.ki.wScan = KeyboardUtil.KeyCodeToScanCode(key);
            input.union.ki.dwFlags = KeyEventF.ScanCode;
            down = input;
            input.union.ki.dwFlags = KeyEventF.KeyUp | KeyEventF.ScanCode;
            up = input;
        }

        public static void SendFull(Keys key)
        {
            KeyFullInput(key, out var down, out var up);
            SendInput(down, up);
        }

        public static void SendFull(params Keys[] keys)
        {
            var inputs = new InputStruct[keys.Length * 2];
            for (int i = 0, e = 0; i < keys.Length; i++)
            {
                KeyFullInput(keys[i], out var down, out var up);
                inputs[e++] = down;
                inputs[e++] = up;
            }
            SendInput(inputs);
        }

        #endregion

    }
}