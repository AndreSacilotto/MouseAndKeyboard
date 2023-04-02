using MouseAndKeyboard.InputShared;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public class YummiPacket : Packet
{
    public const int MAX_PACKET_BYTE_SIZE = 16;

    public YummiPacket(byte[] arr) : base(arr) { }
    public YummiPacket(int size = MAX_PACKET_BYTE_SIZE) : base(size) { }

    #region WRITE 

    public void WriteScreen(int width, int height)
    {
        Add((byte)Command.Screen);
        Add(width);
        Add(height);
    }

    public void WriteMouseMove(int x, int y)
    {
        Add((byte)Command.MouseMove);
        Add(x);
        Add(y);
    }

    public void WriteMouseScroll(int scrollQuant = 120, bool isHorizontal = false)
    {
        Add((byte)Command.MouseScroll);
        Add(scrollQuant);
        Add(isHorizontal);
    }

    public void WriteMouseClick(MouseButton mouseButton, PressState pressedState)
    {
        Add((byte)Command.MouseClick);
        Add((short)mouseButton);
        Add((byte)pressedState);
    }

    public void WriteKey(VirtualKey key, PressState pressedState)
    {
        Add((byte)Command.Key);
        Add((short)key);
        Add((byte)pressedState);
    }

    public void WriteKeyModifier(VirtualKey key, InputModifiers modifiers, PressState pressedState)
    {
        Add((byte)Command.KeyWithModifier);
        Add((short)key);
        Add((uint)modifiers);
        Add((byte)pressedState);
    }

    #endregion

    #region READ

    public void ReadCommand(out Command commands) => commands = (Command)ReadByte();

    public void ReadScreen(out int width, out int height)
    {
        width = ReadInt();
        height = ReadInt();
    }

    public void ReadMouseMove(out int x, out int y)
    {
        x = ReadInt();
        y = ReadInt();
    }

    public void ReadMouseScroll(out int scrollDelta, out bool isHorizontal)
    {
        scrollDelta = ReadInt();
        isHorizontal = ReadBool();
    }

    public void ReadMouseClick(out MouseButton mouseButton, out PressState pressedState)
    {
        mouseButton = (MouseButton)ReadShort();
        pressedState = (PressState)ReadByte();
    }

    public void ReadKeyPress(out VirtualKey key, out PressState pressedState)
    {
        key = (VirtualKey)ReadShort();
        pressedState = (PressState)ReadByte();
    }

    public void ReadKeyWithModifier(out VirtualKey key, out InputModifiers modifiers, out PressState pressedState)
    {
        key = (VirtualKey)ReadShort();
        modifiers = (InputModifiers)ReadUInt();
        pressedState = (PressState)ReadByte();
    }

    #endregion

}
