using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputSimulator;

public static partial class KeyboardSender
{
    #region Input
    internal static InputStruct KeyDownInputVK(VirtualKey key)
    {
        KeyboardInput ki = new(key, 0, 0, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyUpInputVK(VirtualKey key)
    {
        KeyboardInput ki = new(key, 0, KeyEventF.KeyUp, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }

    internal static InputStruct KeyDownInputSC(VirtualKey key)
    {
        KeyboardInput ki = new(0, (ScanCode)KeyUtil.VirtualKeyToScanCode(key), KeyEventF.ScanCode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyUpInputSC(VirtualKey key)
    {
        KeyboardInput ki = new(0, (ScanCode)KeyUtil.VirtualKeyToScanCode(key), KeyEventF.KeyUp | KeyEventF.ScanCode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }

    internal static InputStruct KeyDownInputUnicode(char key)
    {
        KeyboardInput ki = new(0, (ScanCode)key, KeyEventF.Unicode, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }
    internal static InputStruct KeyUpInputUnicode(char key)
    {
        KeyboardInput ki = new(0, (ScanCode)key, KeyEventF.Unicode | KeyEventF.KeyUp, Environment.TickCount, 0);
        return InputStruct.NewInput(ki);
    }

    #endregion

    #region Down
    public static void SendKeyDown(VirtualKey key) => InputSender.SendInput(KeyDownInputSC(key));
    public static void SendKeyDown(VirtualKey[] VirtualKey)
    {
        var inputs = new InputStruct[VirtualKey.Length];
        for (int i = 0; i < VirtualKey.Length; i++)
            inputs[i] = KeyDownInputSC(VirtualKey[i]);
        InputSender.SendInput(inputs);
    }
    public static void SendKeyDown(VirtualKey key, VirtualKey modifier) =>
        InputSender.SendInput(KeyDownInputSC(modifier), KeyDownInputSC(key));
    public static void SendKeyDown(VirtualKey key, VirtualKey[] modifiers)
    {
        var inputs = new InputStruct[modifiers.Length + 1];
        for (int i = 0; i < modifiers.Length; i++)
            inputs[i] = KeyDownInputSC(modifiers[i]);
        inputs[modifiers.Length] = KeyDownInputSC(key);
        InputSender.SendInput(inputs);
    }
    #endregion

    #region Up
    public static void SendKeyUp(VirtualKey key) => InputSender.SendInput(KeyUpInputSC(key));
    public static void SendKeyUp(VirtualKey[] VirtualKey)
    {
        var inputs = new InputStruct[VirtualKey.Length];
        for (int i = 0; i < VirtualKey.Length; i++)
            inputs[i] = KeyUpInputSC(VirtualKey[i]);
        InputSender.SendInput(inputs);
    }
    public static void SendKeyUp(VirtualKey key, VirtualKey modifier) =>
        InputSender.SendInput(KeyUpInputSC(modifier), KeyUpInputSC(key));
    public static void SendKeyUp(VirtualKey key, VirtualKey[] modifiers)
    {
        var inputs = new InputStruct[modifiers.Length + 1];
        for (int i = 0; i < modifiers.Length; i++)
            inputs[i] = KeyUpInputSC(modifiers[i]);
        inputs[modifiers.Length] = KeyUpInputSC(key);
        InputSender.SendInput(inputs);
    }
    #endregion

    #region Click
    public static void SendKeyClick(VirtualKey key) => InputSender.SendInput(KeyDownInputSC(key), KeyUpInputSC(key));
    public static void SendKeyClick(VirtualKey[] VirtualKey)
    {
        var len = VirtualKey.Length;
        var inputs = new InputStruct[len * 2];
        for (int i = 0; i < len; i++)
        {
            var key = VirtualKey[i];
            inputs[i] = KeyDownInputSC(key);
            inputs[len + i] = KeyUpInputSC(key);
        }
        InputSender.SendInput(inputs);
    }
    public static void SendKeyClick(VirtualKey key, VirtualKey modifier)
    {
        InputSender.SendInput(
            KeyDownInputSC(modifier),
            KeyDownInputSC(key),
            KeyUpInputSC(key),
            KeyUpInputSC(modifier)
        );
    }
    public static void SendKeyClick(VirtualKey key, VirtualKey[] modifiers)
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

}