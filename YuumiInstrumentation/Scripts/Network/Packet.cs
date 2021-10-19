using System;
using System.Buffers.Binary;
using System.Text;

namespace MouseKeyboard.Network
{

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

        public void Reset()
        {
            Array.Clear(buffer, 0, Length);
            Rewind();
        }

        #endregion

        #region Util Funcs

        public byte[] CopyBuffer()
        {
            var arr = new byte[Length];
            Array.Copy(buffer, arr, Length);
            return arr;
        }
        public Packet Copy() => new Packet(CopyBuffer());

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
        public void Add(params byte[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }

        public void Add(short value)
        {
            BinaryPrimitives.WriteInt16LittleEndian(memory.Span.Slice(pointer, sizeof(short)), value);
            pointer += sizeof(short);
        }
        public void Add(params short[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }


        public void Add(int value)
        {
            BinaryPrimitives.WriteInt32LittleEndian(memory.Span.Slice(pointer, sizeof(int)), value);
            pointer += sizeof(int);
        }
        public void Add(params int[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }

        public void Add(uint value)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(memory.Span.Slice(pointer, sizeof(uint)), value);
            pointer += sizeof(uint);
        }
        public void Add(params uint[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }

        public void Add(long value)
        {
            BinaryPrimitives.WriteInt64LittleEndian(memory.Span.Slice(pointer, sizeof(uint)), value);
            pointer += sizeof(long);
        }
        public void Add(params long[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }

        #endregion

        #region READ

        public byte ReadByte()
        {
            var value = buffer[pointer];
            pointer += sizeof(byte);
            return value;
        }
        public short ReadShort()
        {
            var value = BinaryPrimitives.ReadInt16LittleEndian(memory.Span.Slice(pointer, sizeof(short)));
            pointer += sizeof(short);
            return value;
        }

        public int ReadInt()
        {
            var value = BinaryPrimitives.ReadInt32LittleEndian(memory.Span.Slice(pointer, sizeof(int)));
            pointer += sizeof(int);
            return value;
        }

        #endregion

    }
}