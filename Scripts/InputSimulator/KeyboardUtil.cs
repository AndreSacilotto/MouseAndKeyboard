using System.Runtime.InteropServices;

namespace MouseAndKeyboard.InputSimulation;

public static partial class KeyboardUtil
{
	/// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeya</summary>
	[DllImport("user32.dll")]
	private static extern uint MapVirtualKey(uint uCode, uint uMapType);

	/// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-vkkeyscana</summary>
	[DllImport("user32.dll")]
	private static extern short VkKeyScan(char ch);

	private enum Map : uint
	{
		/// <summary>VirtualKey to ScanCode</summary>
		VK_TO_VSC = 0,
		/// <summary>ScanCode to VirtualKey</summary>
		VSC_TO_VK = 1,
		/// <summary>VirtualKey to Undefined Char</summary>
		VK_TO_CHAR = 2,
		VSC_TO_VK_EX = 3,
		VK_TO_VSC_EX = 4,
	}

	private static Dictionary<Keys, ushort> scanCodesDict = new();

	public static ushort KeyCodeToScanCode(Keys key)
	{
		if (!scanCodesDict.TryGetValue(key, out ushort result))
		{
			result = (ushort)MapVirtualKey((uint)key, (uint)Map.VK_TO_VSC);
			scanCodesDict.Add(key, result);
		}
		return result;
	}

	public static Keys CharToVirtualKey(char ch)
	{
		short vkey = VkKeyScan(ch);
		Keys retval = (Keys)(vkey & 0xff);
		int modifiers = vkey >> 8;

		if ((modifiers & 1) != 0)
			retval |= Keys.Shift;
		if ((modifiers & 2) != 0)
			retval |= Keys.Control;
		if ((modifiers & 4) != 0)
			retval |= Keys.Alt;
		return retval;
	}

	public static Keys[] ModifiersToKeys(Keys modifiers)
	{
		var list = new List<Keys>();
		if (modifiers.HasFlag(Keys.Control))
			list.Add(Keys.LControlKey);
		if (modifiers.HasFlag(Keys.Shift))
			list.Add(Keys.LShiftKey);
		if (modifiers.HasFlag(Keys.Alt))
			list.Add(Keys.LMenu);
		return list.ToArray();
	}
}