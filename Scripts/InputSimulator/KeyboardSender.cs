using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputSimulator;

public static partial class KeyboardSender
{
    internal static InputStruct KeyDownInputSC(Keys key)
    {
        KeyboardInput ki = new((VirtualKey)key, (ScanCode)KeyUtil.KeyCodeToScanCode(key), KeyEventF.ScanCode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyUpInputSC(Keys key)
    {
        KeyboardInput ki = new((VirtualKey)key, (ScanCode)KeyUtil.KeyCodeToScanCode(key), KeyEventF.KeyUp | KeyEventF.ScanCode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyDownInputVK(Keys key)
    {
        KeyboardInput ki = new((VirtualKey)key, 0, 0, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyUpInputVK(Keys key)
    {
        KeyboardInput ki = new((VirtualKey)key, 0, KeyEventF.KeyUp, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }

    #region Down
    public static void SendKeyDown(Keys key) => InputSender.SendInput(KeyDownInputSC(key));
    public static void SendKeyDown(params Keys[] keys)
    {
        var inputs = new InputStruct[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            inputs[i] = KeyDownInputSC(keys[i]);
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
    public static void SendKeyUp(Keys key) => InputSender.SendInput(KeyUpInputSC(key));
    public static void SendKeyUp(params Keys[] keys)
    {
        var inputs = new InputStruct[keys.Length];
        for (int i = 0; i < keys.Length; i++)
            inputs[i] = KeyUpInputSC(keys[i]);
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
    public static void SendFull(Keys key) => InputSender.SendInput(KeyDownInputSC(key), KeyUpInputSC(key));
    public static void SendFull(params Keys[] keys)
    {
        var len = keys.Length;
        var inputs = new InputStruct[len * 2];
        for (int i = 0; i < len; i++)
        {
            var key = keys[i];
            inputs[i] = KeyDownInputSC(key);
            inputs[len + i] = KeyUpInputSC(key);
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