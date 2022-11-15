﻿using MouseAndKeyboard.InputSimulation;
using MouseAndKeyboard.Network;
using MouseAndKeyboard.Util;

namespace YuumiInstrumentation;

public class YuumiSlave : IMKInput
{
	private UDPSocketReceiver socket;
	public UDPSocket Socket => socket;

	private readonly YuumiPacketRead yuumiRead;

	private bool enabled;

	public YuumiSlave()
	{
#if DEBUG
		socket = new UDPSocketReceiver(true);
#else
		socket = new UDPSocketReceiver(false);
#endif

		socket.MySocket.SendBufferSize = YuumiPacketWrite.MAX_PACKET_BYTE_SIZE;

		yuumiRead = new YuumiPacketRead();

		yuumiRead.OnMouseMove += MouseMove;
		yuumiRead.OnMouseScroll += MouseScroll;
		yuumiRead.OnMouseClick += MouseClick;
		yuumiRead.OnKeyPress += Key;
		yuumiRead.OnKeyModifierPress += KeyModifier;
	}

	private void OnReceive(int bytes, byte[] data) => yuumiRead.ReadAll(data);

	#region Read

	public static void MouseMove(int x, int y)
	{
		LoggerEvents.WriteLine($"RECEIVE: MMove {x} {y}");
		Mouse.MoveAbsolute(x, y);
	}

	public static void MouseScroll(int scrollDelta)
	{
		LoggerEvents.WriteLine($"RECEIVE: MScroll {scrollDelta}");
		Mouse.ScrollWheel(scrollDelta);
	}

	public static void MouseClick(MouseButtons mouseButton, PressedState pressedState)
	{
		LoggerEvents.WriteLine($"RECEIVE: MClick {mouseButton} {pressedState}");
		Mouse.Click(pressedState, mouseButton);
	}

	public static void Key(Keys keys, PressedState pressedState)
	{
		LoggerEvents.WriteLine($"RECEIVE: KKey {keys} {pressedState}");

		if (pressedState == PressedState.Down)
			Keyboard.SendKeyDown(keys);
		else if (pressedState == PressedState.Up)
			Keyboard.SendKeyUp(keys);
		else
			Keyboard.SendFull(keys);
	}

	public static void KeyModifier(Keys keys, Keys modifier, PressedState pressedState)
	{
		LoggerEvents.WriteLine($"RECEIVE: KKeyMod {keys} {pressedState}");

		if (pressedState == PressedState.Down)
			Keyboard.SendKeyDown(keys, modifier);
		else if (pressedState == PressedState.Up)
			Keyboard.SendKeyUp(keys, modifier);
		else
			Keyboard.SendFull(keys, modifier);
	}

	#endregion


	#region Enable

	public bool Enabled
	{
		get => enabled;
		set {
			if (value == enabled)
				return;

			enabled = value;
			if (enabled)
				socket.OnReceive += OnReceive;
			else
				socket.OnReceive -= OnReceive;
		}
	}

	public void Stop()
	{
		Enabled = false;

		yuumiRead.OnMouseMove -= MouseMove;
		yuumiRead.OnMouseScroll -= MouseScroll;
		yuumiRead.OnMouseClick -= MouseClick;
		yuumiRead.OnKeyPress -= Key;
		yuumiRead.OnKeyModifierPress -= KeyModifier;

		socket?.Stop();
	}

	#endregion


}