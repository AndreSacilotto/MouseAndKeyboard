using System.Net;
using System.Net.Sockets;

//Base code: https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9

namespace MouseAndKeyboard.Network;

public abstract class UDPSocket : IDisposable
{
    protected bool closed = false;

    public Socket MySocket { get; } = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public IPEndPoint? HostEndPoint { get; private set; }

    protected EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
    protected AsyncCallback? recv = null;

    public void Dispose()
    {
        if (closed)
            return;
        closed = true;
        MySocket.Close(); // Calls Dispose internally
        GC.SuppressFinalize(this);
    }

    public void Start(IPEndPoint endPoint)
    {
        if (MySocket.Connected)
            return;
        HostEndPoint = endPoint;
        InternalStart(endPoint);
    }
    public void Start(IPAddress address, int port)
    {
        if (MySocket.Connected)
            return;
        HostEndPoint = new IPEndPoint(address, port);
        InternalStart(HostEndPoint);
    }

    protected abstract void InternalStart(IPEndPoint hostEndPoint);
}
