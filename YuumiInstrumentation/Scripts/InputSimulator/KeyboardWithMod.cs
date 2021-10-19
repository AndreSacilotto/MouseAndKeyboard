using System.Windows.Forms;

namespace InputSimulation
{
    public static class KeyboardWithMod
    {

        #region Key Down

        public static void SendKeyDown(Keys key, Keys mod)
        {
            Keyboard.SendKeyDown(mod);
            Keyboard.SendKeyDown(key);
        }
        public static void SendKeyDown(Keys key, params Keys[] mods)
        {
            Keyboard.SendKeyDown(mods);
            Keyboard.SendKeyDown(key);
        }

        #endregion

        #region Key Up

        public static void SendKeyUp(Keys key, Keys mod)
        {
            Keyboard.SendKeyUp(mod);
            Keyboard.SendKeyUp(key);
        }
        public static void SendKeyUp(Keys key, params Keys[] mods)
        {
            Keyboard.SendKeyUp(mods);
            Keyboard.SendKeyUp(key);
        }


        #endregion

        #region Key Full

        public static void SendFull(Keys key, Keys mod)
        {
            Keyboard.SendFull(mod);
            Keyboard.SendFull(key);
        }
        public static void SendFull(Keys key, params Keys[] mods)
        {
            Keyboard.SendFull(mods);
            Keyboard.SendFull(key);
        }

        #endregion


    }
}