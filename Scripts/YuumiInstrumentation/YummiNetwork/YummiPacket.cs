using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public class YummiPacket : Packet
{
    public const int MAX_PACKET_BYTE_SIZE = 16;

    private delegate void ReadFunc(out object T);
    public YummiPacket(byte[] arr) : base(arr) { }
    public YummiPacket(int size = MAX_PACKET_BYTE_SIZE) : this(new byte[size]) { }

    #region Enum Add and Get

    public void Add(Command value) => Add((byte)value);
    public void Get(out Command value)
    {
        Get(out byte v);
        value = (Command)v;
    }

    public void Add(VirtualKey value) => Add((short)value);
    public void Get(out VirtualKey value)
    {
        Get(out short v);
        value = (VirtualKey)v;
    }

    public void Add(PressState value) => Add((byte)value);
    public void Get(out PressState value)
    {
        Get(out byte v);
        value = (PressState)v;
    }

    public void Add(MouseButtonsF value) => Add((byte)value);
    public void Get(out MouseButtonsF value)
    {
        Get(out byte v);
        value = (MouseButtonsF)v;
    }

    public void Add(InputModifiers value) => Add((uint)value);
    public void Get(out InputModifiers value)
    {
        Get(out uint v);
        value = (InputModifiers)v;
    }

    #endregion

    #region Write and Read
    /* TODO: make a source generator that genarate the methods in this region*/

    public void WriteScreen(int width, int height)
    {
        Add(Command.Screen);
        Add(width);
        Add(height);
    }
    public void ReadScreen(out int width, out int height)
    {
        Get(out width);
        Get(out height);
    }

    public void WriteMouseMove(int x, int y)
    {
        Add(Command.MouseMove);
        Add(x);
        Add(y);
    }
    public void ReadMouseMove(out int x, out int y)
    {
        Get(out x);
        Get(out y);
    }

    public void WriteMouseScroll(int scrollQuant = 120, bool isHorizontal = false)
    {
        Add(Command.MouseScroll);
        Add(scrollQuant);
        Add(isHorizontal);
    }
    public void ReadMouseScroll(out int scrollDelta, out bool isHorizontal)
    {
        Get(out scrollDelta);
        Get(out isHorizontal);
    }

    public void WriteMousePress(MouseButtonsF mouseButton, PressState pressedState)
    {
        Add(Command.MouseClick);
        Add(mouseButton);
        Add(pressedState);
    }
    public void ReadMousePress(out MouseButtonsF mouseButton, out PressState pressedState)
    {
        Get(out mouseButton);
        Get(out pressedState);
    }

    public void WriteKeyPress(VirtualKey key, PressState pressedState)
    {
        Add(Command.KeyPress);
        Add(key);
        Add(pressedState);
    }
    public void ReadKeyPress(out VirtualKey key, out PressState pressedState)
    {
        Get(out key);
        Get(out pressedState);
    }

    public void WriteKeyPressWithModifier(VirtualKey key, InputModifiers modifiers, PressState pressedState)
    {
        Add(Command.KeyPressWithModifier);
        Add(key);
        Add(modifiers);
        Add(pressedState);
    }
    public void ReadKeyPressWithModifier(out VirtualKey key, out InputModifiers modifiers, out PressState pressedState)
    {
        Get(out key);
        Get(out modifiers);
        Get(out pressedState);
    }

    #endregion

}
