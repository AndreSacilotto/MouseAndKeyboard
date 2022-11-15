
namespace MouseAndKeyboard.InputSimulation;

public static class Keyboard
{
	#region Key Down

	internal static InputStruct KeyDownInput(Keys key)
	{
		var input = InputSender.NewKeyboardInput;
		input.union.ki.wScan = KeyboardUtil.KeyCodeToScanCode(key);
		input.union.ki.dwFlags = KeyEventF.ScanCode;
		return input;
	}

	public static void SendKeyDown(Keys key)
	{
		InputSender.SendInput(KeyDownInput(key));
	}
	public static void SendKeyDown(params Keys[] keys)
	{
		var inputs = new InputStruct[keys.Length];
		for (int i = 0; i < keys.Length; i++)
			inputs[i] = KeyDownInput(keys[i]);
		InputSender.SendInput(inputs);
	}

	#endregion

	#region Key Up

	internal static InputStruct KeyUpInput(Keys key)
	{
		var input = InputSender.NewKeyboardInput;
		input.union.ki.wScan = KeyboardUtil.KeyCodeToScanCode(key);
		input.union.ki.dwFlags = KeyEventF.KeyUp | KeyEventF.ScanCode;
		return input;
	}

	public static void SendKeyUp(Keys key)
	{
		InputSender.SendInput(KeyUpInput(key));
	}
	public static void SendKeyUp(params Keys[] keys)
	{
		var inputs = new InputStruct[keys.Length];
		for (int i = 0; i < keys.Length; i++)
			inputs[i] = KeyUpInput(keys[i]);
		InputSender.SendInput(inputs);
	}

	#endregion

	#region Key Full
	internal static void KeyFullInput(Keys key, out InputStruct down, out InputStruct up)
	{

		var input = InputSender.NewKeyboardInput;
		input.union.ki.wScan = KeyboardUtil.KeyCodeToScanCode(key);
		input.union.ki.dwFlags = KeyEventF.ScanCode;
		down = input;
		input.union.ki.dwFlags = KeyEventF.KeyUp | KeyEventF.ScanCode;
		up = input;
	}

	public static void SendFull(Keys key)
	{
		KeyFullInput(key, out var down, out var up);
		InputSender.SendInput(down, up);
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
		InputSender.SendInput(inputs);
	}

	#endregion

	#region Key Down - Modifiers

	public static void SendKeyDown(Keys key, Keys mod)
	{
		SendKeyDown(mod);
		SendKeyDown(key);
	}
	public static void SendKeyDown(Keys key, params Keys[] mods)
	{
		SendKeyDown(mods);
		SendKeyDown(key);
	}

	#endregion

	#region Key Up - Modifiers

	public static void SendKeyUp(Keys key, Keys mod)
	{
		SendKeyUp(mod);
		SendKeyUp(key);
	}
	public static void SendKeyUp(Keys key, params Keys[] mods)
	{
		SendKeyUp(mods);
		SendKeyUp(key);
	}


	#endregion

	#region Key Full - Modifiers

	public static void SendFull(Keys key, Keys mod)
	{
		SendFull(mod);
		SendFull(key);
	}
	public static void SendFull(Keys key, params Keys[] mods)
	{
		SendFull(mods);
		SendFull(key);
	}

	#endregion

}