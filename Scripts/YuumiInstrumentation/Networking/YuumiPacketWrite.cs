﻿using MouseAndKeyboard.InputSimulator;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public class YuumiPacketWrite
{
    public const int MAX_PACKET_BYTE_SIZE = 16;

    private readonly Packet packet = new(MAX_PACKET_BYTE_SIZE);

    public Packet Packet => packet;

    #region WRITE 

    public void WriteScreen(int width, int height)
    {
        packet.Add((byte)Commands.Screen);
        packet.Add(width);
        packet.Add(height);
    }

    public void WriteMouseMove(int x, int y)
    {
        packet.Add((byte)Commands.MouseMove);
        packet.Add(x);
        packet.Add(y);
    }

    public void WriteMouseScroll(int scrollQuant = 120)
    {
        packet.Add((byte)Commands.MouseScroll);
        packet.Add(scrollQuant);
    }

    public void WriteMouseClick(MouseButtons mouseButton, PressedState pressedState)
    {
        packet.Add((byte)Commands.MouseClick);
        packet.Add((int)mouseButton);
        packet.Add((byte)pressedState);
    }

    public void WriteKey(Keys key, PressedState pressedState)
    {
        packet.Add((byte)Commands.Key);
        packet.Add((int)key);
        packet.Add((byte)pressedState);
    }

    public void WriteKeyModifier(Keys key, Keys modifiers, PressedState pressedState)
    {
        packet.Add((byte)Commands.KeyModifier);
        packet.Add((int)key);
        packet.Add((int)modifiers);
        packet.Add((byte)pressedState);
    }

    #endregion
}