using System.Buffers.Binary;
using System.Text;

namespace MouseAndKeyboard.Network;

//1b = bool | 16b = short | 32b = int | 64b = long

public class Packet
{
    /// <summary>8192</summary>
    const int SOCKET_DEFAULT_SIZE = 1 << 13;

    //https://stackoverflow.com/q/56285370

    private readonly byte[] buffer;
    private readonly ReadOnlyMemory<byte> memory;

    private int pointer;

    public Packet(byte[] arr)
    {
        buffer = arr;
        memory = buffer;
    }
    public Packet(int size = SOCKET_DEFAULT_SIZE) : this(new byte[size]) { }

    public int Pointer => pointer;
    public byte[] Buffer => buffer;
    public ReadOnlyMemory<byte> MemoryBuffer => memory;
    public ReadOnlySpan<byte> ReadOnlySpan => buffer;
    public byte this[int index]
    {
        get => buffer[index];
        set => buffer[index] = value;
    }
    public int Length => buffer.Length;

    #region Funcs

    public void Rewind() => pointer = 0;
    public void Foward(int amount) => pointer = Math.Clamp(pointer + amount, 0, buffer.Length);
    public void Backward(int amount) => pointer = Math.Clamp(pointer - amount, 0, buffer.Length);

    public void Reset()
    {
        Array.Clear(buffer, 0, Length);
        Rewind();
    }

    public byte[] CopyBuffer()
    {
        var arr = new byte[Length];
        Array.Copy(buffer, arr, Length);
        return arr;
    }
    public Packet Copy() => new(CopyBuffer());

    public override string ToString() => '(' + string.Join(' ', buffer) + ')';

    #endregion

    #region ADD

    public void Add(bool value)
    {
        buffer[pointer] = value ? (byte)1 : (byte)0;
        pointer += sizeof(byte);
    }

    public void Add(byte value)
    {
        buffer[pointer] = value;
        pointer += sizeof(byte);
    }

    public void Add(short value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(short)), value);
        pointer += sizeof(short);
    }

    public void Add(ushort value)
    {
        BinaryPrimitives.WriteUInt16LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(ushort)), value);
        pointer += sizeof(ushort);
    }

    public void Add(int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(int)), value);
        pointer += sizeof(int);
    }

    public void Add(uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(uint)), value);
        pointer += sizeof(uint);
    }

    public void Add(long value)
    {
        BinaryPrimitives.WriteInt64LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(long)), value);
        pointer += sizeof(long);
    }

    public void Add(string value, Encoding encoding) => Add(value.AsSpan(), encoding);
    public void Add(ReadOnlySpan<char> value, Encoding encoding)
    {
        int chars = encoding.GetBytes(value, buffer.AsSpan(pointer + sizeof(int)));
        Add(chars);
    }

    #endregion

    #region READ

    public void Get(out bool value)
    {
        value = buffer[pointer] != 0;
        pointer += sizeof(bool);
    }

    public void Get(out byte value)
    {
        value = buffer[pointer];
        pointer += sizeof(byte);
    }

    public void Get(out short value)
    {
        value = BinaryPrimitives.ReadInt16LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(short)));
        pointer += sizeof(short);
    }

    public void Get(out int value)
    {
        value = BinaryPrimitives.ReadInt32LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(int)));
        pointer += sizeof(int);
    }

    public void Get(out uint value)
    {
        value = BinaryPrimitives.ReadUInt32LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(uint)));
        pointer += sizeof(uint);
    }

    public void Get(out long value)
    {
        value = BinaryPrimitives.ReadInt64LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(long)));
        pointer += sizeof(long);
    }

    public void Get(out string value, Encoding encoding)
    {
        Get(out int chars);
        value = encoding.GetString(buffer.AsSpan(pointer, chars));
        pointer += chars;
    }

    #endregion

}