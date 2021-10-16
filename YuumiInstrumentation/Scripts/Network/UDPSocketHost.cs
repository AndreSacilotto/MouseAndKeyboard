using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSocketHost : UDPSocket
{
    public UDPSocketHost(bool clientCanHost = false) : base()
    {
        if (clientCanHost)
            MySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    }

    protected override void InternalStart(IPEndPoint hostEndPoint)
    {
        MySocket.Bind(hostEndPoint);
        Receive();
    }

    protected override void Receive()
    {
        MySocket.BeginReceiveFrom(byteBuffer.buffer, 0, byteBuffer.Length, SocketFlags.None, ref senderEndPoint, recv = (ar) =>
        {
            ByteBuffer so = (ByteBuffer)ar.AsyncState;
            int bytes = MySocket.EndReceiveFrom(ar, ref senderEndPoint);
            Console.WriteLine("RECV: {0}: {1}, {2}", senderEndPoint.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));

            MySocket.BeginReceiveFrom(so.buffer, 0, byteBuffer.Length, SocketFlags.None, ref senderEndPoint, recv, so);
        }, byteBuffer);
    }
}
