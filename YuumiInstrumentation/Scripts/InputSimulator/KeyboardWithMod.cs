using System.Windows.Forms;

using static InputSimulation.InputSender;
using static InputSimulation.Keyboard;

namespace InputSimulation
{
    public static class KeyboardWithMod
    {

        #region Key Down

        public static void SendKeyDown(Keys key, Keys mod)
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