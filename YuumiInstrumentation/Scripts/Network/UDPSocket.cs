using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

    public void Start(IPAddress address, int port)
    {
        HostEndPoint = new IPEndPoint(address, port);
        byteBuffer = new ByteBuffer(MySocket.SendBufferSize);
        InternalStart(HostEndPoint);
    }

    protected abstract void InternalStart(IPEndPoint hostEndPoint);

    public void Stop() => MySocket.Close();

    public virtual void Send(int value) { }
    public virtual void Send(float value) { }
    public void Send(string text)
    {
        byte[] packetData = Encoding.ASCII.GetBytes(text);
        MySocket.BeginSend(packetData, 0, packetData.Length, SocketFlags.None, (ar) =>
        {
            ByteBuffer so = (ByteBuffer)ar.AsyncState;
            int bytes = MySocket.EndSend(ar);
            Console.WriteLine("SEND: {0}, {1}", bytes, text);
        }, byteBuffer);
    }

    protected abstract void Receive();

}
