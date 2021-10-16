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
}
