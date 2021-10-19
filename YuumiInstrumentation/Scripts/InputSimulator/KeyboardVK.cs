using System.Windows.Forms;

using static InputSimulation.InputSender;

namespace InputSimulation
{
    public static class KeyboardVK
    {
        #region Key Down

        private static InputStruct KeyDownInput(Keys key)
        {
            var input = NewKeyboardInput;
            input.union.ki.wVk = (ushort)key;
            input.union.ki.dwFlags = 0;
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

        private static InputStruct KeyUpInput(Keys key)
        {
            var input = NewKeyboardInput;
            input.union.ki.wVk = (ushort)key;
            input.union.ki.dwFlags = KeyEventF.KeyUp;
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
        private static void KeyFullInput(Keys key, out InputStruct down, out InputStruct up)
        {
            var input = NewKeyboardInput;
            input.union.ki.wVk = (ushort)key;
            input.union.ki.dwFlags = 0;
            down = input;
            input.union.ki.dwFlags = KeyEventF.KeyUp;
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

        #region Send With Modifiers

        public static void SendWithModifier(Keys modifier, PressedState pressedState, Keys key)
        {
            SendKeyDown(modifier);
            Send(pressedState, key);
            SendKeyUp(modifier);
        }
        public static void SendWithModifier(Keys modifier, PressedState pressedState, params Keys[] keys)
        {
            SendKeyDown(modifier);
            Send(pressedState, keys);
            SendKeyUp(modifier);
        }

        public static void SendWithModifiers(Keys[] modifiers, PressedState pressedState, Keys key)
        {
            SendKeyDown(modifiers);
            Send(pressedState, key);
            SendKeyUp(modifiers);
        }
        public static void SendWithModifiers(Keys[] modifiers, PressedState pressedState, params Keys[] keys)
        {
            SendKeyDown(modifiers);
            Send(pressedState, keys);
            SendKeyUp(modifiers);
        }

        #endregion

        #region Find

        public static void Send(PressedState pressedState, Keys key)
        {
            if (pressedState == PressedState.Down)
                SendKeyDown(key);
            else if (pressedState == PressedState.Up)
                SendKeyUp(key);
            else
                SendFull(key);
        }

        public static void Send(PressedState pressedState, params Keys[] keys)
        {
            if (pressedState == PressedState.Down)
                SendKeyDown(keys);
            else if (pressedState == PressedState.Up)
                SendKeyUp(keys);
            else
                SendFull(keys);
        }

        #endregion

    }
}