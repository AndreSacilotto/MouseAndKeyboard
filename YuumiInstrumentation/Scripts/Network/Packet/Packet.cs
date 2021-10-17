using System;
using System.Buffers.Binary;
using System.Text;

//16 = short | 32 = int | 64 = long

public class Packet
{
    private byte[] buffer;
    private Memory<byte> memory;

    private int pointer;

    public Packet(byte[] arr)
    {
        buffer = arr;
        memory = new Memory<byte>(buffer);
    }

    public Packet(int size = 8192)
    {
        buffer = new byte[size];
        memory = new Memory<byte>(buffer);
    }

    public byte[] GetBuffer => buffer;
    public byte this[int index]
    {
        get => buffer[index];
        set => buffer[index] = value;
    }

    #region Main Funcs

    public int Length => buffer.Length;
    public void Rewind() => pointer = 0;

    public void Clear()
    {
        Array.Clear(buffer, 0, Length);
        Rewind();
    }

    #endregion

    #region Util Funcs

    public byte[] Copy()
    {
        var arr = new byte[Length];
        Array.Copy(buffer, arr, Length);
        return arr;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var item in memory.Span)
            sb.AppendLine(item.ToString());
        return sb.ToString();
    }

    #endregion

    #region ADD

    public void Add(byte value)
    {
        buffer[pointer] = value;
        pointer += sizeof(byte);
    }
    public void Add(int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(memory.Span.Slice(pointer, sizeof(int)), value);
        pointer += sizeof(int);
    }
    public void Add(params int[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            BinaryPrimitives.WriteInt32LittleEndian(memory.Span.Slice(pointer, sizeof(int)), values[i]);
            pointer += sizeof(int);
        }
    }

    public void Add(uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(memory.Span.Slice(pointer, sizeof(uint)), value);
        pointer += sizeof(uint);
    }
    public void Add(long value)
    {
        BinaryPrimitives.WriteInt64LittleEndian(memory.Span.Slice(pointer, sizeof(uint)), value);
        pointer += sizeof(long);
    }
    #endregion

    #region READ
    public int ReadInt()
    {
        var value = BinaryPrimitives.ReadInt32LittleEndian(memory.Span.Slice(pointer, sizeof(int)));
        pointer += sizeof(int);
        return value;
    }

    public byte ReadByte()
    {
        var value = buffer[pointer];
        pointer += sizeof(byte);
        return value;
    }

    #endregion

}

