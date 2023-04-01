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
    public static void SendKeyDown(Keys key, Keys modifier) => 
        InputSender.SendInput(KeyDownInputSC(modifier), KeyDownInputSC(key));
    public static void SendKeyDown(Keys key, params Keys[] modifiers)
    {
        var inputs = new InputStruct[modifiers.Length+1];
        for (int i = 0; i < modifiers.Length; i++)
            inputs[i] = KeyDownInputSC(modifiers[i]);
        inputs[modifiers.Length] = KeyDownInputSC(key);
        InputSender.SendInput(inputs);
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
    public static void SendKeyUp(Keys key, Keys modifier) => 
        InputSender.SendInput(KeyUpInputSC(modifier), KeyUpInputSC(key));
    public static void SendKeyUp(Keys key, params Keys[] modifiers)
    {
        var inputs = new InputStruct[modifiers.Length + 1];
        for (int i = 0; i < modifiers.Length; i++)
            inputs[i] = KeyUpInputSC(modifiers[i]);
        inputs[modifiers.Length] = KeyUpInputSC(key);
        InputSender.SendInput(inputs);
    }
    #endregion

    #region Click
    public static void SendKeyClick(Keys key) => InputSender.SendInput(KeyDownInputSC(key), KeyUpInputSC(key));
    public static void SendKeyClick(params Keys[] keys)
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
    public static void SendKeyClick(Keys key, Keys modifier)
    {
        InputSender.SendInput(
            KeyDownInputSC(modifier),
            KeyDownInputSC(key),
            KeyUpInputSC(key),
            KeyUpInputSC(modifier)
        );
    }
    public static void SendKeyClick(Keys key, params Keys[] modifiers)
    {
        var len = modifiers.Length;
        var inputs = new InputStruct[len * 2 + 2];

        inputs[len] = KeyDownInputSC(key);
        inputs[len + 1] = KeyUpInputSC(key);
        for (int i = 0; i < len; i++)
        {
            var mod = modifiers[i];
            inputs[i] = KeyDownInputSC(mod);
            inputs[len + i + 2] = KeyUpInputSC(mod);
        }
        InputSender.SendInput(inputs);
    }

    #endregion

    #region Press by State

    public static void KeyboardKeyPress(Keys key, PressState pressState)
    {
        if (pressState == PressState.Down)
            SendKeyDown(key);
        else if (pressState == PressState.Up)
            SendKeyUp(key);
        else
            SendKeyClick(key);
    }

    public static void KeyboardKeyPress(Keys key, Keys modifier, PressState pressState)
    {
        if (pressState == PressState.Down)
            SendKeyDown(key, modifier);
        else if (pressState == PressState.Up)
            SendKeyUp(key, modifier);
        else
            SendKeyClick(key, modifier);
    }

    #endregion

}