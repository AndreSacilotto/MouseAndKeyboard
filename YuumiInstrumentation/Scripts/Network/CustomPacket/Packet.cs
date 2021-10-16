using System;
using System.Collections.Generic;
using MiscUtil.Conversion;

public partial class Packet
{
    const byte INT_SIZE = sizeof(int);
    const byte BYTE_SIZE = sizeof(byte);
    //const int CHAR_SIZE = sizeof(char);
    //const int FLOAT_SIZE = sizeof(float);

    private byte[] byteBuffer;

    private int pointer;

    public Packet(int size = 8192)
    {
        byteBuffer = new byte[size];
    }

    public byte this[int index]
    {
        get => byteBuffer[index];
        set => byteBuffer[index] = value;
    }
    public int Length => byteBuffer.Length;
    public void Rewind() => pointer = 0;

    public void Reset()
    {
        for (int i = 0; i < Length; i++)
            byteBuffer[i] = 0;
        Rewind();
    }

    public void AddInt(int value)
    {
        EndianBitConverter.Little.CopyBytes(value, byteBuffer, pointer);
        pointer += INT_SIZE;
    }

    public void AddByte(byte value)
    {
        byteBuffer[pointer] = value;
        pointer += BYTE_SIZE;
    }

}

