using System.Buffers.Binary;
using System.Text;

namespace MouseAndKeyboard.Network;

//1b = bool | 16b = short | 32b = int | 64b = long

public class Packet
{
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

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var item in buffer.AsSpan())
            sb.AppendLine(item.ToString());
        return sb.ToString();
    }

    #endregion

    #region ADD

    public void Add(bool value)
    {
        buffer[pointer] = value ? (byte)1 : (byte)0;
        pointer += sizeof(bool);
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

    #endregion

    #region READ

    public bool ReadBool()
    {
        var value = buffer[pointer] != 0;
        pointer += sizeof(bool);
        return value;
    }

    public byte ReadByte()
    {
        var value = buffer[pointer];
        pointer += sizeof(byte);
        return value;
    }

    public short ReadShort()
    {
        var value = BinaryPrimitives.ReadInt16LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(short)));
        pointer += sizeof(short);
        return value;
    }

    public int ReadInt()
    {
        var value = BinaryPrimitives.ReadInt32LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(int)));
        pointer += sizeof(int);
        return value;
    }

    public uint ReadUInt()
    {
        var value = BinaryPrimitives.ReadUInt32LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(uint)));
        pointer += sizeof(uint);
        return value;
    }

    public long ReadLong()
    {
        var value = BinaryPrimitives.ReadInt64LittleEndian(buffer.AsSpan().Slice(pointer, sizeof(long)));
        pointer += sizeof(long);
        return value;
    }

    #endregion

}