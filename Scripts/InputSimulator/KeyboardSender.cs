using MouseAndKeyboard.Native;
using MouseAndKeyboard.Util;

namespace MouseAndKeyboard.InputSimulation;

public static partial class KeyboardSender
{
    internal static InputStruct KeyDownInput(Keys key)
    {
        KeyboardInput ki = new((VirtualKey)key, (ScanCode)KeyUtil.KeyCodeToScanCode(key), KeyEventF.ScanCode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyUpInput(Keys key)
    {
        KeyboardInput ki = new((VirtualKey)key, (ScanCode)KeyUtil.KeyCodeToScanCode(key), KeyEventF.KeyUp | KeyEventF.ScanCode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }

    #region Down
    public static void SendKeyDown(Keys key) => InputSender.SendInput(KeyDownInput(key));
    public static void SendKeyDown(params Keys[] keys)
    {
        var inputs = new InputStruct[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            inputs[i] = KeyDownInput(keys[i]);
        InputSender.SendInput(inputs);
    }
    public static void SendKeyDown(Keys key, Keys modifier)
    {
        SendKeyDown(modifier);
        SendKeyDown(key);
    }
    public static void SendKeyDown(Keys key, params Keys[] modifiers)
    {
        SendKeyDown(modifiers);
        SendKeyDown(key);
    }
    #endregion

    #region Up
    public static void SendKeyUp(Keys key) => InputSender.SendInput(KeyUpInput(key));
    public static void SendKeyUp(params Keys[] keys)
    {
        var inputs = new InputStruct[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            inputs[i] = KeyUpInput(keys[i]);
        InputSender.SendInput(inputs);
    }
    public static void SendKeyUp(Keys key, Keys modifier)
    {
        SendKeyUp(modifier);
        SendKeyUp(key);
    }
    public static void SendKeyUp(Keys key, params Keys[] modifiers)
    {
        SendKeyUp(modifiers);
        SendKeyUp(key);
    }
    #endregion


    #region Full
    public static void SendFull(Keys key) => InputSender.SendInput(KeyDownInput(key), KeyUpInput(key));
    public static void SendFull(params Keys[] keys)
    {
        var len = keys.Length;
        var inputs = new InputStruct[len * 2];
        for (int i = 0; i < len; i++)
        {
            var key = keys[i];
            inputs[i] = KeyDownInput(key);
            inputs[len + i] = KeyUpInput(key);
        }
        InputSender.SendInput(inputs);
    }
    public static void SendFull(Keys key, Keys modifier)
    {
        SendKeyDown(modifier);
        SendFull(key);
        SendKeyUp(modifier);
    }
    public static void SendFull(Keys key, params Keys[] modifiers)
    {
        SendKeyDown(modifiers);
        SendFull(key);
        SendKeyUp(modifiers);
    }

    #endregion

}