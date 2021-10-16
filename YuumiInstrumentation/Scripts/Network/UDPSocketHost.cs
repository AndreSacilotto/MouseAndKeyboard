using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSocketHost : UDPSocket
{
    public UDPSocketHost(bool clientCanHost = false) : base()
    {
        if (clientCanHost)
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    }

    protected override void InternalStart(IPEndPoint hostEndPoint)
    {
        Socket.Bind(hostEndPoint);
        Receive();
    }
}
