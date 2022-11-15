using System.Net.Sockets;
using MouseAndKeyboard.InputSimulation;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public class YuumiPacketRead
{
	public event Action<int, int> OnMouseMove;
	public event Action<int> OnMouseScroll;
	public event Action<MouseButtons, PressedState> OnMouseClick;
	public event Action<Keys, PressedState> OnKeyPress;
	public event Action<Keys, Keys, PressedState> OnKeyModifierPress;

	public void ReadAll(byte[] data) => ReadAll(new Packet(data));

	public void ReadAll(Packet packet)
	{
		var cmd = (Commands)packet.ReadByte();

		switch (cmd)
		{
			case Commands.MouseMove:
			ReadMouseMove(packet);
			break;
			case Commands.MouseScroll:
			ReadMouseScroll(packet);
			break;
			case Commands.MouseClick:
			ReadMouseClick(packet);
			break;
			case Commands.Key:
			ReadKeyPress(packet);
			break;
			case Commands.KeyModifier:
			ReadKeyModifier(packet);
			break;
		}
	}

	private void ReadMouseMove(Packet packet)
	{
		if (OnMouseMove == null)
			return;

		var x = packet.ReadInt();
		var y = packet.ReadInt();
		OnMouseMove(x, y);
	}

	private void ReadMouseScroll(Packet packet)
	{
		if (OnMouseScroll == null)
			return;

		int scrollDelta = packet.ReadInt();
		OnMouseScroll(scrollDelta);
	}

	private void ReadMouseClick(Packet packet)
	{
		if (OnMouseClick == null)
			return;

		var mouseButton = (MouseButtons)packet.ReadInt();
		var pressedState = (PressedState)packet.ReadByte();
		OnMouseClick(mouseButton, pressedState);
	}

	private void ReadKeyPress(Packet packet)
	{
		if (OnKeyPress == null)
			return;

		var keys = (Keys)packet.ReadInt();
		var pressedState = (PressedState)packet.ReadByte();
		OnKeyPress(keys, pressedState);
	}

	private void ReadKeyModifier(Packet packet)
	{
		if (OnKeyModifierPress == null)
			return;

		var keys = (Keys)packet.ReadInt();
		var mods = (Keys)packet.ReadInt();
		var pressedState = (PressedState)packet.ReadByte();
		OnKeyModifierPress(keys, mods, pressedState);
	}

}