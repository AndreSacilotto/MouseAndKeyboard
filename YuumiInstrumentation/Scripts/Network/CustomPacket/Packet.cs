using System;
using System.Collections.Generic;

public partial class Packet
{
    public byte[] buffer;
    public Packet(int size = 8192) => buffer = new byte[size];
    public int Length => buffer.Length;
    public byte this[int index]
    {
        get => buffer[index];
        set => buffer[index] = value;
    }


}

