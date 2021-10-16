using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public abstract class UDPSocket
{
    public Socket Socket { get; } = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public IPEndPoint ServerEndPoint { get; private set; }

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
        ServerEndPoint = new IPEndPoint(address, port);
        byteBuffer = new ByteBuffer(Socket.SendBufferSize);
        InternalStart(ServerEndPoint);
    }

    protected abstract void InternalStart(IPEndPoint hostEndPoint);

    public void Stop() => Socket.Close();

    public void Send(string text)
    {
        byte[] packetData = Encoding.ASCII.GetBytes(text);
        Socket.BeginSend(packetData, 0, packetData.Length, SocketFlags.None, (ar) =>
        {
            ByteBuffer so = (ByteBuffer)ar.AsyncState;
            int bytes = Socket.EndSend(ar);
            Console.WriteLine("SEND: {0}, {1}", bytes, text);
        }, byteBuffer);
    }

    protected void Receive()
    {
        Socket.BeginReceiveFrom(byteBuffer.buffer, 0, byteBuffer.Length, SocketFlags.None, ref senderEndPoint, recv = (ar) =>
        {
            ByteBuffer so = (ByteBuffer)ar.AsyncState;
            int bytes = Socket.EndReceiveFrom(ar, ref senderEndPoint);
            Console.WriteLine("RECV: {0}: {1}, {2}", senderEndPoint.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));

            Socket.BeginReceiveFrom(so.buffer, 0, byteBuffer.Length, SocketFlags.None, ref senderEndPoint, recv, so);
        }, byteBuffer);
    }
}
