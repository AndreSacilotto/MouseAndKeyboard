using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

//Base code: https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9

public abstract class UDPSocket
{
    public Socket MySocket { get; } = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public IPEndPoint HostEndPoint { get; private set; }

    protected EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
    protected AsyncCallback recv = null;

    protected ByteBuffer byteBuffer;

    public class ByteBuffer
    {
        public byte[] buffer;
        public ByteBuffer(int size = 8192) => buffer = new byte[size];
        public int Length => buffer.Length;
        public byte this[int index]
        {
            get => buffer[index];
            set => buffer[index] = value;
        }
    }


    public void Start(IPAddress address, int port, int bufferSize = 0)
    {
        if (bufferSize == 0)
            bufferSize = MySocket.SendBufferSize;
        byteBuffer = new ByteBuffer(bufferSize);
        HostEndPoint = new IPEndPoint(address, port);
        InternalStart(HostEndPoint);
    }

    protected abstract void InternalStart(IPEndPoint hostEndPoint);

    public void Stop() => MySocket.Close();

    public virtual void Send(int value) { }
    public virtual void Send(float value) { }
    public void Send(string text)
    {
        byte[] packetData = Encoding.ASCII.GetBytes(text);
        MySocket.BeginSend(packetData, 0, packetData.Length, SocketFlags.None, (aResult) =>
        {
            int bytes = MySocket.EndSend(aResult);
            Console.WriteLine("SEND: {0} | {1}", bytes, text);
        }, null);
    }

    protected virtual void Receive()
    {
        MySocket.BeginReceiveFrom(byteBuffer.buffer, 0, byteBuffer.Length, SocketFlags.None, ref senderEndPoint, recv = (ar) =>
        {
            ByteBuffer so = (ByteBuffer)ar.AsyncState;
            int bytes = MySocket.EndReceiveFrom(ar, ref senderEndPoint);
            Console.WriteLine("RECV - {0} | {1} | {2}", senderEndPoint.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));

            MySocket.BeginReceiveFrom(so.buffer, 0, byteBuffer.Length, SocketFlags.None, ref senderEndPoint, recv, so);
        }, byteBuffer);
    }

}
